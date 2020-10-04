using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIRuleta.Context;
using APIRuleta.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIRuleta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ruletaController : ControllerBase
    {
        private readonly AppDbContext context;
        public ruletaController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<ruleta> Get()
        {
            return context.ruleta.ToList();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ruleta Get(long id)
        {
            var ruleta = context.ruleta.FirstOrDefault(p=>p.id==id);
            return ruleta;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
