using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReserveerBackend;
using ReserveerBackend.Models;

namespace ReserveerBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly ReserveerDBContext _context;

        public UsersController(ReserveerDBContext context)
        {
            _context = context;
        }
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(string Username, string Password, string email)
        {
            if(String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(email))
            {
                Response.StatusCode = 400;
                return Content("Fields not filled in");
            }
            var newuser = new User();
            newuser.Role = Models.Role.Student;
            newuser.Email = email;
            newuser.EmailNotification = false;
            newuser.PasswordLogin = PasswordLoginUtilities.GenerateNewLogin(Username, Password);
            newuser.PasswordLogin.User = newuser;
            _context.Users.Add(newuser);
            _context.UserPasswordLogins.Add(newuser.PasswordLogin);
            _context.SaveChanges();
            return Content(string.Format("Succesfully registered user with username: {0}", Username));
        }
    }
}
