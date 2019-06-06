using MeuPonto.Dados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MeuPonto.Model
{
    public class Solicitacao
    {
        public int Id { get; set; }
        public int FuncionarioId { get; set; }
        public string Data { get; set; }
        public string HorarioBatida { get; set; }
        public string Descricao { get; set; }
        public string SituacaoPonto { get; set; }

        //Retorna os dados de solicitações abertas pelo funcionario
        public void GetSolicitacao(int funcionarioId, out List<Solicitacao> solicitacaoList)
        {
            Solicitacao solicitacao = null;
            solicitacaoList = new List<Solicitacao>();
            DAL dados = new DAL();

            //Monta a string de pesquisa
            string sql = string.Format("SELECT * FROM SOLICITACAO WHERE FuncionarioId = {0}", funcionarioId);

            DataTable dt = dados.RetDataTable(sql);
            if (dt != null)
            {
                //Verifica se o resultado não é vazio
                if (dt.Rows.Count > 0)
                {
                    //percorre a lista de funcionarios retornados e adiciona na lista
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        solicitacao = new Solicitacao();
                        solicitacao.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                        solicitacao.FuncionarioId = Convert.ToInt32(dt.Rows[i]["FuncionarioId"].ToString());
                        solicitacao.Data = dt.Rows[i]["Data"].ToString();
                        solicitacao.HorarioBatida = dt.Rows[i]["HorarioBatida"].ToString();
                        solicitacao.Descricao = dt.Rows[i]["Descricao"].ToString();
                        solicitacao.SituacaoPonto = dt.Rows[i]["SituacaoPonto"].ToString();

                        //Adiciona o objeto na lista
                        solicitacaoList.Add(solicitacao);
                    }
                }
            }
        }

        /// <summary>
        /// Salva no banco a solicitação feita pelo funcionario
        /// </summary>
        /// <param name="dia">Dia que o ponto esta sendo registrado</param>
        /// <param name="horario">Horario  que o ponto esta sendo registrado</param>
        /// <param name="funcionarioId">Id do funcionario</param>
        /// <returns></returns>
        public bool RegistrarSolicitacao(string data, string horarioBatida, string descricao, string situacao, int funcionarioId)
        {
            DAL dados = new DAL();
            
            try
            {
                //Monta a string de pesquisa
                string sql = string.Format("INSERT INTO SOLICITACAO VALUES ({0}, '{1}', '{2}', '{3}', '{4}')",
                    funcionarioId, data, horarioBatida, descricao, situacao);

                DataTable dt = dados.RetDataTable(sql);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
