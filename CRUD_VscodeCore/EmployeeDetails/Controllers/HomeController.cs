using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeDetails.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using iTextSharp.text.pdf;

namespace EmployeeDetails.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult upload()
        {
            return View();
        }

        // Method to handle the PDF upload and extract text
        [HttpPost]
        public async Task<IActionResult> upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Save the uploaded file to a directory
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Extract text from the uploaded PDF
                var content = ExtractTextFromPdf(filePath);

                // Store extracted content temporarily for answering questions
                TempData["ExtractedContent"] = content;
                TempData.Keep("ExtractedContent");

                return RedirectToAction("AnswerQuestion");
            }

            return View();
        }

        // Method to extract text from a PDF file
        public string ExtractTextFromPdf(string path)
        {
            StringBuilder text = new StringBuilder();

            using (PdfReader reader = new PdfReader(path))
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i));
                }
            }

            return text.ToString();
        }

        // Method to render the question form
        [HttpGet]
        public IActionResult AnswerQuestion()
        {
            return View();
        }

        // Method to handle user questions based on extracted PDF content
        [HttpPost]
        public IActionResult AnswerQuestion(string question)
        {
            var content = TempData["ExtractedContent"] as string;

            if (string.IsNullOrEmpty(content))
            {
                ViewBag.Answer = "No PDF content found. Please upload a PDF first.";
                return View();
            }

            var answer = AnswerQuestion(question, content);
            ViewBag.Answer = answer;

            // Keep content in TempData to allow asking multiple questions
            TempData.Keep("ExtractedContent");

            return View();
        }

        public string AnswerQuestion(string question, string pdfContent)
        {
            // Tokenize the question into individual words (excluding common stop words)
            var questionWords = question.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                        .Select(q => q.ToLowerInvariant())
                                        .Where(q => q.Length > 2); // Skip very short words

            // Search for each keyword in the content and find a relevant match
            foreach (var word in questionWords)
            {
                int index = pdfContent.IndexOf(word, StringComparison.OrdinalIgnoreCase);
                if (index >= 0)
                {
                    // Return the surrounding paragraph or sentence where the word is found
                    return GetSurroundingText(pdfContent, index);
                }
            }

            return "No relevant information found for your question.";
        }

        // This method returns the surrounding sentence or paragraph of a matched word
        public string GetSurroundingText(string content, int matchIndex)
        {
            // Get the surrounding text around the found word (matchIndex)
            // We look for sentence or paragraph boundaries (like ".", "\n", etc.)
            int start = content.LastIndexOfAny(new[] { '.', '\n' }, matchIndex);
            start = (start == -1) ? 0 : start + 1; // If no sentence end is found, start from the beginning

            int end = content.IndexOfAny(new[] { '.', '\n' }, matchIndex);
            end = (end == -1) ? content.Length : end + 1; // If no sentence end is found, go to the end

            return content.Substring(start, end - start).Trim();
        }



    }
}
