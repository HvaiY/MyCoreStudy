using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBackendApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackendApi.Controllers
{
    [Route("api/[controller]")]
    public class TestCreadteDBController : Controller
    {
        private MyDbconetxt _dbconetxt;
        public TestCreadteDBController(MyDbconetxt dbconetxt)
        {
            _dbconetxt = dbconetxt;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}