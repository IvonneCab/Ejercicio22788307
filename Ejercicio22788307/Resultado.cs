using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Ejercicio22788307
{
    [Table("Resultado")]
   public class Resultado
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("A")]

        public string? A { get; set; }
        [Column("B")]

        public string? B { get; set; }

        [Column("calcular")]
        public string? Calcular { get; set; }
    }
}