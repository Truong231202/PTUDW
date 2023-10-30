using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Suppliers")]
    public class Suppliers
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Ảnh")]
        public string Image { get; set; }
        [Display(Name = "Tên viết gọn")]
        public string Slug { get; set; }
        public int? Order { get; set; }
        [Display(Name = "Tên đầy đủ")]
        public string Fullname { get; set; }
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string UrlSite { get; set; }
        [Required]
        
        public string MetaDesc { get; set; }
        
        [Required(ErrorMessage = "Từ khóa không để trống")]
        [Display(Name = "Từ khóa")]
        public string MetaKey { get; set; }

        [Display(Name = "Người tạo")]
        public int CreateBy { get; set; }
      
        [Display(Name = "Ngày tạo")]
        [Required(ErrorMessage = "Người tạo không được để trống")]
        public DateTime CreateAt { get; set; }
        public int UpdateBy { get; set; }
        [Required]
        public DateTime UpdateAt { get; set; }
        public int Status { get; set; }
    }
}
