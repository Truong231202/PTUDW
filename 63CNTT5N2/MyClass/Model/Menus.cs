using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Menus")]
    public class Menus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TableID { get; set; }
        public string TypeMenu { get; set; }
        public string Position { get; set;}
        public string Link { get; set;}
        public int? ParentID { get; set; }
        public int? Order { get; set;}
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set;}
        public int UpdatedBy { get; set;}
        [Required]
        public DateTime UpdatedAt { get; set;}
        public int Status { get; set;}
    }
}
