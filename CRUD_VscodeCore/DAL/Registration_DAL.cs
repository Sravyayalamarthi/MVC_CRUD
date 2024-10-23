using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Registration_DAL
    {
        private readonly DBConnection _context;
        public Registration_DAL(DBConnection context)
        {
            _context = context;
        }

        public Registration Insert_EmployeeDetails(string EmployeeID, string EmployeeName, string EmailID, string Password, string Address)
        {
            try
            {
                var result = _context.Registrations.FromSqlRaw("EXEC Insert_EmployeeDetails @EmployeeID,@EmployeeName,@EmailID,@Password,@Address",
                    new SqlParameter("@EmployeeID", EmployeeID),
                    new SqlParameter("@EmployeeName", EmployeeName),
                    new SqlParameter("@EmailID ", EmailID),
                    new SqlParameter("@Password", Password),
                    new SqlParameter("@Address", Address)).AsEnumerable().FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Login> GetUserData(string EmailID, string Password)
        {
            try
            {
                var User = _context.Logins.FromSqlRaw("EXEC GetEmployeeDetails @EmailID, @Password",
                    new SqlParameter("@EmailID", EmailID),
                    new SqlParameter("@Password", Password)).ToList();
                return User;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Registration Delete_EmployeeDetails(string EmployeeID)
        {
            try
            {
                var result = _context.Registrations.FromSqlRaw("EXEC Delete_EmployeeDetails @EmployeeID",
                    new SqlParameter("@EmployeeID", EmployeeID)).AsEnumerable().FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Registration Update_EmployeeDetails(string EmployeeID, string EmployeeName, string EmailID, string Address)
        {
            try
            {
                var result = _context.Registrations.FromSqlRaw("EXEC update_EmployeeData @EmployeeID,@EmployeeName,@EmailID,@Address",
                    new SqlParameter("@EmployeeID", EmployeeID),
                    new SqlParameter("@EmployeeName", EmployeeName),
                    new SqlParameter("@EmailID ", EmailID),                    
                    new SqlParameter("@Address", Address)).AsEnumerable().FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}