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
        [HttpGet("GetFuncionario/{empresaId}")]
        public ActionResult<List<Funcionario>> GetFuncionario(int empresaId)
        {
            return GetFuncionarioPorEmpresa(empresaId);
        }

        // GET api/values/5
        [HttpGet("GetEmpresa/{id}")]
        public ActionResult<Empresa> GetEmpresa(int id)
        {
            return ObterDadosEmpresa(id);
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

        //Retorna a lista de funcionarios de determinada empresa
        public List<Funcionario> GetFuncionarioPorEmpresa(int empresaId)
        {
            Funcionario funcionario = new Funcionario();
            List<Funcionario> funcionarioList = new List<Funcionario>();

            funcionario.ListarFuncionariosPorEmpresa(empresaId, out funcionarioList);
            return funcionarioList;
        }

        //Retorna as informações da empresa
        public Empresa ObterDadosEmpresa(int Id)
        {
            Empresa empresa = new Empresa();

            return empresa.DadosEmpresa(Id);
        }
    }
}
