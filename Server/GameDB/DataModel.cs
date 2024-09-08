using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDB
{
    [Table("Player")]
    public class PlayerDb 
    {
        public int PlayerDbId { get; set; }
        public string UserId { get; set; }
        public int Scrap { get; set; }
    }
}
