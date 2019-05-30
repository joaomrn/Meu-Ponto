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
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        //Retorna os dados de ponto do funcionario
        public List<Ponto> GetRegistroPonto(int funcionarioId, out List<Ponto> pontoList)
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
                        ponto.Latitude = dt.Rows[i]["Latitude"].ToString();
                        ponto.Longitude = dt.Rows[i]["Longitude"].ToString();

                        //Adiciona o objeto na lista
                        pontoList.Add(ponto);
                    }
                }
            }

            return pontoList;
        }

        /// <summary>
        /// Cadastra o registro de ponto no banco
        /// </summary>
        /// <param name="dia">Dia que o ponto esta sendo registrado</param>
        /// <param name="horario">Horario  que o ponto esta sendo registrado</param>
        /// <param name="funcionarioId">Id do funcionario</param>
        /// <returns></returns>
        public bool RegistrarPonto(int funcionarioId)
        {
            //int hora = DateTime.Now.Hour;
            //string horaFormatada = DateTime.Now.ToString("HH:mm");
            //string d = DateTime.Now.ToShortTimeString();

            bool sucesso = false;
            DAL dados = new DAL();

            //Monta a string de pesquisa
            string sql = string.Format("SELECT * FROM PONTO WHERE FuncionarioId = '{0}' AND DiaSemana = '{1}'", funcionarioId, DateTime.Now.ToShortDateString());

            DataTable dt = dados.RetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                Ponto ponto = new Ponto();
                recuperaDados(dt, ref ponto);
                preencherHorario(ref ponto);
                dados.ExecutarComandoSql(atualizarRegistro(ponto));
                sucesso = true;
            }
            else
            {
                Ponto ponto = new Ponto();
                ponto.FuncionarioId = funcionarioId;
                preencherHorario(ref ponto);
                dados.ExecutarComandoSql(salvarRegistro(ponto));
                sucesso = true;
            }

            return sucesso;
        }

        //Montar string de INSERT
        private string salvarRegistro(Ponto ponto)
        {
            return string.Format("INSERT INTO PONTO VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", ponto.FuncionarioId, ponto.Entrada, 
                                 ponto.IdaAlmoco, ponto.VoltaAlmoco, ponto.Saida, DateTime.Now.ToShortDateString(), ponto.Latitude, ponto.Longitude);
        }

        //Montar string de UPDATE
        private string atualizarRegistro(Ponto ponto)
        {
            return string.Format("UPDATE PONTO SET FuncionarioId = {0}, Entrada = '{1}', IdaAlmoco = '{2}', VoltaAlmoco= '{3}', " +
                                 "Saida = '{4}', WHERE Id = {5}",
                                 ponto.FuncionarioId, ponto.Entrada, ponto.IdaAlmoco, ponto.VoltaAlmoco, ponto.Saida, ponto.Id);
        }

        //Adiciona no objeto os dados recuperados do banco
        private void recuperaDados(DataTable dt, ref Ponto ponto)
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
                ponto.Latitude = dt.Rows[i]["Latitude"].ToString();
                ponto.Longitude = dt.Rows[i]["Longitude"].ToString();
            }
        }

        //Adiciona o horario de batida para cada funcionario
        private void preencherHorario(ref Ponto ponto)
        {
            if (string.IsNullOrEmpty(ponto.Entrada))
            {
                ponto.Entrada = DateTime.Now.ToShortTimeString();
                return;
            }

            if (string.IsNullOrEmpty(ponto.IdaAlmoco))
            {
                ponto.IdaAlmoco = DateTime.Now.ToShortTimeString();
                return;
            }

            if (string.IsNullOrEmpty(ponto.VoltaAlmoco))
            {
                ponto.VoltaAlmoco = DateTime.Now.ToShortTimeString();
                return;
            }

            if (string.IsNullOrEmpty(ponto.Saida))
            {
                ponto.Saida = DateTime.Now.ToShortTimeString();
                return;
            }

        }
    }
}

