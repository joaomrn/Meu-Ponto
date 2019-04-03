using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MeuPonto.Dados
{
    public class DAL
    {
        private static String server = "joao";
        private static String database = "MeuPonto";
        private static String user = "sa";
        private static String password = "sqladmin";

        //Cria String de conexao
        private String connectionString = $"Server={server};Database={database};user={user};password={password};";
        private SqlConnection connection;

        public DAL()
        {
            //Recebe a string de conexao no construtor
            connection = new SqlConnection(connectionString);
            //Abri a conexao
            connection.Open();
        }

        //Executa SELECTs
        public DataTable RetDataTable(String sql)
        {
            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dataTable);
            return dataTable;
        }

        //Executa INSERTs, UPDATs e DELETEs
        public void ExecutarComandoSql(String sql)
        {
            SqlCommand command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}
