using MeuPonto.Dados;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeuPonto.Model
{
    //Link para duvida de data e hora
    //https://forum.imasters.com.br/topic/338129-resolvido%C2%A0pegar-apenas-a-hora-de-um-campo-datetime/
    public class Ponto
    {
        public int Id { get; set; }
        public string Entrada { get; set; }
        public string Saida { get; set; }
        public string IdaAlmoco { get; set; }
        public string VoltaAlmoco { get; set; }
        public int FuncionarioId { get; set; }
        public string DiaSemana { get; set; }
        public Localizacao localizacao { get; set; }

        //Retorna os dados de ponto do funcionario
        public void GetRegistroPonto(int funcionarioId, out List<Ponto> pontoList)
        {
            Ponto ponto = null;
            pontoList = new List<Ponto>();
            DAL dados = new DAL();

            if (!Directory.Exists(@"C:\temp\Fotos"))
                Directory.CreateDirectory(@"C:\temp\Fotos");

            //Monta a string de pesquisa
            string sql = string.Format("SELECT * FROM PONTO WHERE FuncionarioId = {0}", funcionarioId);

            DataTable dt = dados.RetDataTable(sql);
            if (dt != null)
            {
                //Verifica se o resultado não é vazio
                if (dt.Rows.Count > 0)
                {
                    //percorre a lista de funcionarios retornados e adiciona na lista
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ponto = new Ponto();
                        ponto.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                        ponto.FuncionarioId = Convert.ToInt32(dt.Rows[i]["FuncionarioId"].ToString());
                        ponto.Entrada = dt.Rows[i]["Entrada"].ToString();
                        ponto.Saida = dt.Rows[i]["Saida"].ToString();
                        ponto.IdaAlmoco = dt.Rows[i]["IdaAlmoco"].ToString();
                        ponto.VoltaAlmoco = dt.Rows[i]["VoltaAlmoco"].ToString();
                        ponto.DiaSemana = dt.Rows[i]["DiaSemana"].ToString();

                        //Adiciona o objeto na lista
                        pontoList.Add(ponto);
                    }
                }
            }
        }

        /// <summary>
        /// Cadastra o registro de ponto no banco
        /// </summary>
        /// <param name="dia">Dia que o ponto esta sendo registrado</param>
        /// <param name="horario">Horario  que o ponto esta sendo registrado</param>
        /// <param name="funcionarioId">Id do funcionario</param>
        /// <returns></returns>
        public bool RegistrarPonto(string latitude, string longitude)
        {
            DAL dados = new DAL();

            //Monta a string de pesquisa
            string sql = string.Format("SELECT * FROM PONTO WHERE FuncionarioId = '{0}' AND DiaSemana = '{1}'", 6, DateTime.Now.ToShortDateString());

            DataTable dt = dados.RetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                Ponto ponto = new Ponto();
                localizacao = new Localizacao();
                localizacao.Latitude = latitude;
                localizacao.Longitude = longitude;
                recuperaDados(dt, ref ponto);
                preencherHorario(ref ponto);
                dados.ExecutarComandoSql(atualizarRegistro(ponto));
                return true;
            }
            else
            {
                Ponto ponto = new Ponto();
                localizacao = new Localizacao();
                localizacao.Latitude = latitude;
                localizacao.Longitude = longitude;
                ponto.FuncionarioId = 6;
                preencherHorario(ref ponto);
                dados.ExecutarComandoSql(salvarRegistro(ponto));
                return true;
            }
        }

        //Montar string de INSERT
        private string salvarRegistro(Ponto ponto)
        {
            return string.Format("INSERT INTO PONTO VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}')", ponto.FuncionarioId, ponto.Entrada, 
                                 ponto.IdaAlmoco, ponto.VoltaAlmoco, ponto.Saida, DateTime.Now.ToShortDateString());
        }

        //Montar string de UPDATE
        private string atualizarRegistro(Ponto ponto)
        {
            return string.Format("UPDATE PONTO SET FuncionarioId = {0}, Entrada = '{1}', IdaAlmoco = '{2}', VoltaAlmoco= '{3}', " +
                                 "Saida = '{4}' WHERE Id = {5}",
                                 ponto.FuncionarioId, ponto.Entrada, ponto.IdaAlmoco, ponto.VoltaAlmoco, ponto.Saida, ponto.Id);
        }

        //Adiciona no objeto os dados recuperados do banco
        private void recuperaDados(DataTable dt, ref Ponto ponto)
        {

            //percorre a lista de ponto retornados e adiciona na lista
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ponto = new Ponto();
                ponto.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                ponto.FuncionarioId = Convert.ToInt32(dt.Rows[i]["FuncionarioId"].ToString());
                ponto.Entrada = dt.Rows[i]["Entrada"].ToString();
                ponto.Saida = dt.Rows[i]["Saida"].ToString();
                ponto.IdaAlmoco = dt.Rows[i]["IdaAlmoco"].ToString();
                ponto.VoltaAlmoco = dt.Rows[i]["VoltaAlmoco"].ToString();
                ponto.DiaSemana = dt.Rows[i]["DiaSemana"].ToString();
            }
        }

        //Adiciona o horario de batida para cada funcionario
        private void preencherHorario(ref Ponto ponto)
        {
            if (string.IsNullOrEmpty(ponto.Entrada))
            {
                ponto.Entrada = DateTime.Now.ToShortTimeString();
                localizacao.salvarRegistro("Entrada");
                return;
            }

            if (string.IsNullOrEmpty(ponto.IdaAlmoco))
            {
                ponto.IdaAlmoco = DateTime.Now.ToShortTimeString();
                localizacao.salvarRegistro("IdaAlmoco");
                return;
            }

            if (string.IsNullOrEmpty(ponto.VoltaAlmoco))
            {
                ponto.VoltaAlmoco = DateTime.Now.ToShortTimeString();
                localizacao.salvarRegistro("VoltaAlmoco");
                return;
            }

            if (string.IsNullOrEmpty(ponto.Saida))
            {
                ponto.Saida = DateTime.Now.ToShortTimeString();
                localizacao.salvarRegistro("Saida");
                return;
            }

        }
    }
}

