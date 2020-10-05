using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIRuleta.Entities
{
    public class ruleta
    {
        private bool _estado;
        [Key]
        public int id { get; set; }
        public bool estado { get; set ; }
        public int? numero { get; set; }
        public DateTime? fecha_juego { get; set; }
    }
}
