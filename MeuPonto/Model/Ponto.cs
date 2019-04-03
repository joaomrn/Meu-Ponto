using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuPonto.Model
{
    public class Ponto
    {
        public Guid Id { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }
        public DateTime IdaAlmoco { get; set; }
        public DateTime VoltaAlmoco { get; set; }
        public Guid FuncionarioId { get; set; }
    }
}
