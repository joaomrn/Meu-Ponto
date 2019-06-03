using MeuPonto.Dados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MeuPonto.Model
{
    public class Localizacao
    {
        public int Id { get; set; }
        public string DataRegistroPonto { get; set; }
        public string RegistroPonto { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }


        //Montar string de INSERT e salva no banco
        public void salvarRegistro(string registroPonto)
        {
            DAL dados = new DAL();

            string sql =  string.Format("INSERT INTO Localizacao VALUES ('{0}', '{1}', '{2}', '{3}')", DateTime.Now.ToShortDateString(), registroPonto, Latitude, Longitude);

            dados.ExecutarComandoSql(sql);
        }

        public void GetRegistroLocalizacao(string dataRegistroPonto, out List<Localizacao> localizacaoList)
        {
            DAL dados = new DAL();
            Localizacao localizacao = new Localizacao();
            localizacaoList = new List<Localizacao>();

            //Monta a string de pesquisa
            string sql = string.Format("SELECT * FROM LOCALIZACAO WHERE DataRegistroPonto = '{0}'", dataRegistroPonto);

            DataTable dt = dados.RetDataTable(sql);
            if (dt != null)
            {
                //Verifica se o resultado não é vazio
                if (dt.Rows.Count > 0)
                {
                    //percorre a lista de funcionarios retornados e adiciona na lista
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        localizacao = new Localizacao();
                        localizacao.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                        localizacao.DataRegistroPonto = dt.Rows[i]["DataRegistroPonto"].ToString();
                        localizacao.RegistroPonto = dt.Rows[i]["RegistroPonto"].ToString();
                        localizacao.Latitude = dt.Rows[i]["Latitude"].ToString();
                        localizacao.Longitude = dt.Rows[i]["Longitude"].ToString();

                        //Adiciona o objeto na lista
                        localizacaoList.Add(localizacao);
                    }
                }
            }
        }
    }
}
