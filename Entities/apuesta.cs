using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIRuleta.Entities
{
    public class apuesta
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("ruleta")]
        public int id_ruleta { get; set; }
        public int monto_apostado { get; set; }
        public int numero_apostado { get; set; }
        public int? resultado { get; set; }
        public int? monto_ganado { get; set; }
    }
}
