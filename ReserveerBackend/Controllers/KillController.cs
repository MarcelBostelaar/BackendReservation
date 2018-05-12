using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReserveerBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Kill")]
    public class KillController : Controller
    {
        [HttpGet]
        public void Kill()
        {
            Environment.Exit(0);
        }
    }
}