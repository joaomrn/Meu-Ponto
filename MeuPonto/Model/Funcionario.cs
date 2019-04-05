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

        /// <summary>
        /// Lista todos os funcionarios da empresa cadastrados no banco
        /// </summary>
        /// <param name="funcionarioList"></param>
        /// <returns></returns>
        public List<Funcionario> ListarFuncionariosPorEmpresa(int empresaId, out List<Funcionario> funcionarioList)
        {
            funcionarioList = new List<Funcionario>();
            Funcionario funcionario;
            DAL dados = new DAL();

            //Monta a string de pesquisa
            string sql = string.Format("SELECT * FROM FUNCIONARIO WHERE EmpresaId = {0}", empresaId);

            DataTable dt = dados.RetDataTable(sql);

            //Verifica se o resultado não é vazio
            if (dt != null && dt.Rows.Count > 0)
            {
                //percorre a lista de funcionarios retornados e adiciona na lista
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    funcionario = new Funcionario();
                    funcionario.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                    funcionario.Nome = dt.Rows[i]["Nome"].ToString();
                    funcionario.EmpresaId = Convert.ToInt32(dt.Rows[i]["EmpresaId"].ToString());
                    funcionario.Matricula = dt.Rows[i]["Matricula"].ToString();
                    funcionario.PontoId = Convert.ToInt32(dt.Rows[i]["PontoId"].ToString());

                    funcionarioList.Add(funcionario);
                }
            }
            return funcionarioList;
        }

    }
}
