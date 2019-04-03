using System.Collections.Generic;
using MeuPonto.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MeuPonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeuPontoController : ControllerBase
    {
        // GET api/MeuPonto
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "API REST Iniciada!";
        }
        
        // Retorna todos os funcionarios de determinada empresa
        [HttpGet("GetFuncionario/{empresaId}")]
        public ActionResult<string> GetFuncionario(int empresaId)
        {
            return GetFuncionarioPorEmpresa(empresaId);
        }

        // Retorna os dados da empresa
        [HttpGet("GetEmpresa/{id}")]
        public ActionResult<string> GetEmpresa(int id)
        {
            return ObterDadosEmpresa(id);
        }
        
        // Retorna os registros de ponto do funcionario
        [HttpGet("GetPonto/{funcionarioId}")]
        public ActionResult<string> GetPonto(int funcionarioId)
        {
            return ObterDadosPonto(funcionarioId);
        }

        // POST api/MeuPonto
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/MeuPonto/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/MeuPonto/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //Retorna a lista de funcionarios de determinada empresa
        public string GetFuncionarioPorEmpresa(int empresaId)
        {
            Funcionario funcionario = new Funcionario();
            List<Funcionario> funcionarioList = new List<Funcionario>();

            funcionario.ListarFuncionariosPorEmpresa(empresaId, out funcionarioList);

            return JsonConvert.SerializeObject(funcionarioList);
        }

        //Retorna as informações da empresa
        public string ObterDadosEmpresa(int Id)
        {
            Empresa empresa = new Empresa();

            return JsonConvert.SerializeObject(empresa.DadosEmpresa(Id));
        }
        
        //Retorna as informações da empresa
        public string ObterDadosPonto(int funcionarioId)
        {
            Ponto ponto = new Ponto();
            List<Ponto> pontoList;

            ponto.RegistroFuncionario(funcionarioId, out pontoList);

            return JsonConvert.SerializeObject(pontoList);
        }
    }
}
