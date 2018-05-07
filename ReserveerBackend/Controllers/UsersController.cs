using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("CreateUsernamePassword")]
        [Authorize(Roles = Authorization.AdminOrHigher)]
        public async Task<IActionResult> Create(string Username, string Password, string email, string role)
        {
            if(String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(role))
            {
                //Response.StatusCode = 400;
                return BadRequest("Fields not filled in");
            }
            Role? castrole = Authorization.FromString(role);
            Role _role = Role.Student;
            if (!castrole.HasValue)
            {
                Response.StatusCode = 400;
                return Content("Role is invalid");
            }
            else
                _role = castrole.Value;
            var newuser = new User();
            newuser.Role = _role;
            newuser.Email = email;
            newuser.EmailNotification = false;
            newuser.PasswordLogin = PasswordLoginUtilities.GenerateNewLogin(Username, Password);
            newuser.PasswordLogin.User = newuser;

            if (DoesUserExist(newuser.PasswordLogin))
            {
                Response.StatusCode = 409;
                return Content("User already exists");
            }

            _context.Users.Add(newuser);
            _context.UserPasswordLogins.Add(newuser.PasswordLogin);
            _context.SaveChanges();
            return Content(string.Format("Succesfully registered user with username: {0}", Username));
        }

        [HttpPost]
        [Route("GetUsers")]
        [Authorize(Roles = Authorization.ServiceOrHigher)]
        public IEnumerable<User> GetUsers(string Username, string email, string role, int? Id, bool? emailnotifications)
        {
            var users = _context.Users.AsQueryable();
            if (!String.IsNullOrEmpty(Username))
                users = users.Where(x => x.PasswordLogin.Username == Username);
            if (!String.IsNullOrEmpty(email))
                users = users.Where(x => x.Email == email);
            var castrole = Authorization.FromString(role);
            if (castrole.HasValue)
                users = users.Where(x => x.Role == castrole.Value);
            if (Id.HasValue)
                users = users.Where(x => x.Id == Id.Value);
            if (emailnotifications.HasValue)
                users = users.Where(x => x.EmailNotification == emailnotifications.Value);
            return users;
        }

        private bool DoesUserExist(UserPasswordLogin userlogin)
        {
            return _context.UserPasswordLogins.First(u => u.Username == userlogin.Username) != null;
        }
    }
}
