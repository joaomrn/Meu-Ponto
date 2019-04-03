using MeuPonto.Dados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MeuPonto.Model
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EmpresaId { get; set; }
        public string Matricula { get; set; }
        public int PontoId { get; set; }


        public void ListarFuncionarios()
        {
            DAL dados = new DAL();
            DataTable dt = dados.RetDataTable("SELECT * FROM FUNCIONARIO");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    Nome = dt.Rows[0]["Nome"].ToString();
                    EmpresaId = Convert.ToInt32(dt.Rows[0]["EmpresaId"].ToString());
                    Matricula = dt.Rows[0]["Matricula"].ToString();
                    PontoId = Convert.ToInt32(dt.Rows[0]["PontoId"].ToString());
                }
            }
        }

    }
}
