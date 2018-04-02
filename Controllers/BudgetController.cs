using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using budget.Models;

namespace budget.Controllers
{
    [Route("api/[controller]")]
    public class BudgetController : Controller
    {
        private Microsoft.Extensions.Configuration.IConfiguration _config;

        public BudgetController(Microsoft.Extensions.Configuration.IConfiguration config) {
            _config = config;
        }

        [HttpGet]
        public IEnumerable<Budget> Get([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            return Budget.Get(start, end, _config.GetSection("ConnectionStrings")["Budget"]);
        }

        [HttpGet("{id}")]
        public Budget Get(string id)
        {
            return Budget.Get(id, _config.GetSection("ConnectionStrings")["Budget"]);
        }

        [HttpPost]
        public void Post([FromBody] Budget budget)
        {
            Budget.Save(budget, _config.GetSection("ConnectionStrings")["Budget"]);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}