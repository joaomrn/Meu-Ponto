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
        public Guid Id { get; set; }
        public string RazaoSocial { get; set; }
        public Guid FuncionarioId { get; set; }
        public string Cnpj { get; set; }


        
    }
}
