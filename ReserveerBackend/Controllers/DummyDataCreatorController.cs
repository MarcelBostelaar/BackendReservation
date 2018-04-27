using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveerBackend.CreateDummyData;

namespace ReserveerBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/DummyDataCreator")]
    public class DummyDataCreatorController : Controller
    {
        private readonly ReserveerDBContext db;

        public DummyDataCreatorController(ReserveerDBContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public string Generate()
        {
            DummyDataCreator.CreateDummyData(db);
            return "Succes";
        }
    }
}