using MeuPonto.Dados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MeuPonto.Model
{
    public class Empresa
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }

        /// <summary>
        /// Retorna os dados da empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Empresa DadosEmpresa(int id)
        {
            Empresa empresa = new Empresa();
            DAL dados = new DAL();

            //Monta a string de pesquisa
            string sql = string.Format("SELECT * FROM EMPRESA WHERE Id = {0}", id);

            DataTable dt = dados.RetDataTable(sql);
            if (dt != null)
            {
                //Verifica se o resultado não é vazio
                if (dt.Rows.Count > 0)
                {
                    empresa.Id = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    empresa.RazaoSocial = dt.Rows[0]["RazaoSocial"].ToString();
                    empresa.Cnpj = dt.Rows[0]["Cnpj"].ToString();
                }
            }

            return empresa;
        }
    }
}
