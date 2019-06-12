using System;
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
        
        // Retorna os registros de ponto do funcionario
        [HttpGet("ListaRegistroPonto/{funcionarioId}")]
        public ActionResult<string> ListaRegistroPonto(int funcionarioId)
        {
            return ObterDadosPonto(funcionarioId);
        }

        // Retorna os dados da localização pela data
        [HttpGet("ListaLocalizacaoPorData/{data}")]
        public ActionResult<string> ListaLocalizacaoPorData(string data)
        {
            return ObterDadosLocalizacaoPorData(data);
        }
        
        // Retorna os dados da localização pela data
        [HttpGet("ListaLocalizacao/{data}")]
        public ActionResult<string> ListaLocalizacao()
        {
            return ObterDadosLocalizacao();
        }

        // POST api/MeuPonto
        [HttpGet("CadastrarPontoFuncionario/{latitude}/{longitude}")]
        public ActionResult<bool> CadastrarPontoFuncionario(string latitude, string longitude)
        {
            return CadastrarPonto(latitude, longitude);
        }

        // Retorna os registros de ponto do funcionario
        [HttpGet("ObterRegistroSolicitacao/{funcionarioId}")]
        public ActionResult<string> ObterRegistroSolicitacao(int funcionarioId)
        {
            return ObterSolicitacao(funcionarioId);
        }

        // POST api/MeuPonto
        [HttpGet("RegistrarSolicitacaoFuncionario/{data}/{horarioBatida}/{descricao}/{situacao}/{funcionarioId}")]
        public ActionResult<bool> RegistrarSolicitacaoFuncionario(string data, string horarioBatida, string descricao, string situacao, int funcionarioId)
        {
            return RegistrarSolicitacao(data, horarioBatida, descricao, situacao, funcionarioId);
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

        //Retorna as localizações de acordo com a data
        public string ObterDadosLocalizacaoPorData(string dataRegistroPonto)
        {
            string data = dataRegistroPonto.Replace("-", "/");
            Localizacao localizacao = new Localizacao();
            List<Localizacao> localizacaoList;

            localizacao.GetRegistroLocalizacao(data, out localizacaoList);

            return JsonConvert.SerializeObject(localizacaoList);
        }
        
        //Retorna as localizações de acordo com a data
        public string ObterDadosLocalizacao()
        {
            //string data = dataRegistroPonto.Replace("-", "/");
            Localizacao localizacao = new Localizacao();
            List<Localizacao> localizacaoList;

            localizacao.GetRegistroLocalizacao(string.Empty, out localizacaoList);

            return JsonConvert.SerializeObject(localizacaoList);
        }

        /// <summary>
        /// Cadastra no banco o ponto do funcionario
        /// </summary>
        /// <param name="funcionarioId">Id do funcionario autenticado</param>
        /// <returns></returns>
        public bool CadastrarPonto(string latitude, string longitude)
        {
            Ponto ponto = new Ponto();

            if (ponto.RegistrarPonto(latitude, longitude))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Registra no banco a solicitação do cliente
        public bool RegistrarSolicitacao(string data, string horarioBatida, string descricao, string situacao, int funcionarioId)
        {
            DateTime.TryParse(data, out DateTime dateTime);

            data = Convert.ToString(dateTime.ToShortDateString()).ToString();

            Solicitacao solicitacao = new Solicitacao();

            if (solicitacao.RegistrarSolicitacao(data, horarioBatida, descricao, situacao, funcionarioId))
            {
                return true;
            }

            return false;
        }

        //Retornar as solicitações do usuario
        public string ObterSolicitacao(int funcionarioId)
        {
            Solicitacao solicitacao = new Solicitacao();
            List<Solicitacao> solicitacaoList;

            solicitacao.GetSolicitacao(funcionarioId, out solicitacaoList);

            return JsonConvert.SerializeObject(solicitacaoList);
        }
    }
}
