using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NCSCore.Entity.DataTable;
using NCSCore.Service.Interfaces;

namespace NCSCore.WebAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private IDemoService DemoService;
        public DemoController(IDemoService demoService)
        {
            DemoService = demoService;
        }
        [HttpPost]
        public string Add(DemoData demoData)
        {
            DemoService.InsertDemo(demoData);
            return "";
        }
    }
}