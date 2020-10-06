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
        public double monto_apostado { get; set; }
        public int numero_apostado { get; set; }
        public bool? resultado { get; set; }
        public double? monto_ganado { get; set; }
        public bool tipo_apuesta { get; set; }
    }
}
