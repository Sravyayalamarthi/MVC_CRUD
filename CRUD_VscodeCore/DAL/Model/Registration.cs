using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace DAL
{
    public class Registration
    {
        public int ID { get; set; }
        public string msg { get; set; }
    }
    public class Login
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }


    }

   
}