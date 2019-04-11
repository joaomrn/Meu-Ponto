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
        [HttpGet("ListaFuncionario/{empresaId}")]
        public ActionResult<string> ListaFuncionario(int empresaId)
        {
            return GetFuncionarioPorEmpresa(empresaId);
        }

        // Retorna os dados da empresa
        [HttpGet("DadosEmpresa/{id}")]
        public ActionResult<string> DadosEmpresa(int id)
        {
            return ObterDadosEmpresa(id);
        }
        
        // Retorna os registros de ponto do funcionario
        [HttpGet("ListaRegistroPonto/{funcionarioId}")]
        public ActionResult<string> ListaRegistroPonto(int funcionarioId)
        {
            return ObterDadosPonto(funcionarioId);
        }

        // POST api/MeuPonto
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/MeuPonto/5
        [HttpPut("CadastrarPontoFuncionario/{funcionarioId}")]
        public bool CadastrarPontoFuncionario(int funcionarioId)
        {
            return CadastrarPonto(funcionarioId);
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
            List<Funcionario> funcionarioList;

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

            ponto.GetRegistroPonto(funcionarioId, out pontoList);

            return JsonConvert.SerializeObject(pontoList);
        }

        /// <summary>
        /// Cadastra no banco o ponto do funcionario
        /// </summary>
        /// <param name="funcionarioId">Id do funcionario autenticado</param>
        /// <returns></returns>
        public bool CadastrarPonto(int funcionarioId)
        {
            Ponto ponto = new Ponto();
            if (ponto.RegistrarPonto(funcionarioId))
                return true;
            else
                return false;
        }
    }
}
