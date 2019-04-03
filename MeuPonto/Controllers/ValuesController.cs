using System.Collections.Generic;
using MeuPonto.Model;
using Microsoft.AspNetCore.Mvc;

namespace MeuPonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet("GetFuncionario")]
        public ActionResult<Funcionario> GetFuncionario()
        {
            Funcionario funcionario = new Funcionario();
            funcionario.ListarFuncionarios();

            return funcionario;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "Teste";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
