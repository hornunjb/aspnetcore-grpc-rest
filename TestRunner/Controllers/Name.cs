using aspnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace WebAPI.Controllers
{
    public class Name
    {
        public string name { get; set; }
    }
    public class NameController : Controller
    {
        [HttpPost]
        public void Post([FromBody] Name n)
        {
        }
    }
}
