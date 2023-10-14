using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Contacts")]
    public class Contacts
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        public String Title { get; set; }
        [Required]
        public String Details { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public int UpdateBy { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set;}
        public int Status { get; set; }
    }
}
