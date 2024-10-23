using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeDetails.Models;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace EmployeeDetails.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly DBConnection _context;
        public RegistrationController(DBConnection context, ILogger<RegistrationController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(string EmployeeID, string EmployeeName, string EmailID, string Password, string Address)
        {
            try
            {
                Registration_DAL objdal = new Registration_DAL(_context);
                var result = objdal.Insert_EmployeeDetails(EmployeeID, EmployeeName, EmailID, Password, Address);
                if (result.ID == 1)
                {
                    TempData["SuccessMessage"] = result.msg;
                }
                else
                {
                    TempData["ErrorMessage"] = result.msg;
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string EmailID, string Password)
        {
            Registration_DAL objdal = new Registration_DAL(_context);
            var result = objdal.GetUserData(EmailID, Password);
            if (result.Count > 0)
            {
                TempData["EmailID"] = EmailID;
                TempData["Password"] = Password;
                return RedirectToAction("Report", "Registration");
            }
            else
            {
                TempData["ErrorMessage"] = "Login Failed!";
                return RedirectToAction("Login");
            }

        }

        public IActionResult Report()
        {

            string emailID = TempData["EmailID"] as string;
            string password = TempData["Password"] as string;
            TempData.Keep("EmailID");
            TempData.Keep("Password");
            Registration_DAL objdal = new Registration_DAL(_context);
            var result = objdal.GetUserData(emailID, password);
            return View(result);
        }


        public IActionResult Delete_EmployeeDetails(string id)
        {
            Registration_DAL objdal = new Registration_DAL(_context);
            var result = objdal.Delete_EmployeeDetails(id);
            if (result.ID == 1)
            {
                TempData["SuccessMessage"] = result.msg;
            }
            else
            {
                TempData["ErrorMessage"] = result.msg;
            }
            return RedirectToAction("Report", "Registration");
        }

        [HttpPost]
        public IActionResult Update_EmployeeDetails(string EmployeeID, string EmployeeName, string EmailID, string Address)
        {
            try
            {
                Registration_DAL objdal = new Registration_DAL(_context);
                var result = objdal.Update_EmployeeDetails(EmployeeID, EmployeeName, EmailID, Address);
                if (result.ID == 1)
                {
                    return Json(new { success = true, message = result.msg });
                }
                else if (result.ID == 2)
                {
                    return Json(new { success = false, message = result.msg });
                }
                else
                {
                    return Json(new { success = false, message = "Error occured while updating data" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string connectionString = "Data Source=APOLBO03-218;Initial Catalog=EvotingDB;Integrated Security=True";
        [HttpGet]
        public ActionResult Login1()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Login1(string name, string phone)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT EmployeeID, GuidToken, GuidTokenExpiry FROM Employee WHERE Name = @Name AND Phone = @Phone", conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Phone", phone);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Guid? guidToken = reader["GuidToken"] as Guid?;
                    DateTime? guidTokenExpiry = reader["GuidTokenExpiry"] as DateTime?;

                    if (guidTokenExpiry.HasValue && guidTokenExpiry.Value > DateTime.Now)
                    {
                        // Token is valid, set it in the session
                        HttpContext.Session.SetString("GuidToken", guidToken.Value.ToString());
                        return RedirectToAction("Login"); // Or whatever page
                    }
                    else
                    {
                        // Token expired or not set, generate a new one
                        reader.Close(); // Close reader before executing the update query
                                        // Token expired - insert it into the ExpiredGuids table
                        cmd = new SqlCommand("INSERT INTO ExpiredGuids (Name, ExpiredGuid, ExpiredAt) VALUES (@Name, @ExpiredGuid, GETDATE())", conn);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@ExpiredGuid", guidToken.Value);
                        cmd.ExecuteNonQuery();

                        //ViewBag.AlertMessage = "Your session has expired. Please log in again.";
                        cmd = new SqlCommand("UPDATE Employee SET GuidToken = NEWID(), GuidTokenExpiry = DATEADD(MINUTE, 1, GETDATE()) WHERE Name = @Name AND Phone = @Phone", conn);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.ExecuteNonQuery();

                        // Retrieve new token
                        cmd = new SqlCommand("SELECT GuidToken FROM Employee WHERE Name = @Name AND Phone = @Phone", conn);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        guidToken = (Guid)cmd.ExecuteScalar();
                        TempData["SuccessMessage"] = $"Your GUID has expired.New GUIID is: {guidToken} plz Login Again";
                        // Set the new token in the session
                        HttpContext.Session.SetString("GuidToken", guidToken.Value.ToString());
                        return RedirectToAction("Login1"); // Or whatever page
                    }
                }
                else
                {
                    // Handle invalid login
                    ViewBag.ErrorMessage = "Invalid Name or Phone Number";
                    return View();
                }
            }
        }





        public IActionResult Index()
        {
            var aqiCalculator = new AQICalculator();

            // Example dynamically fetched data (you can replace this with actual data from an API)
            var aqiData = new List<AQIModel>
        {
            new AQIModel { Location = "Visakhapatnam", Latitude = 17.6868, Longitude = 83.2185, PollutantConcentration = 40.0 },
            new AQIModel { Location = "Vijayawada", Latitude = 16.5062, Longitude = 80.6480, PollutantConcentration = 20.0 },
            new AQIModel { Location = "Tirupati", Latitude = 13.6288, Longitude = 79.4192, PollutantConcentration = 50.0 },
            new AQIModel { Location = "Guntur", Latitude = 16.3067, Longitude = 80.4365, PollutantConcentration = 30.0 },
            new AQIModel { Location = "Rajahmundry", Latitude = 17.0005, Longitude = 81.8040, PollutantConcentration = 70.0 }
        };

            // Calculate the AQI for each location
            foreach (var aqi in aqiData)
            {
                aqi.AQIValue = aqiCalculator.CalculateAQI(aqi.PollutantConcentration);
            }

            return View(aqiData); // Passing the AQI data to the view
        }




        public IActionResult ShapeFile()
        {
            return View();
        }
    }

}
