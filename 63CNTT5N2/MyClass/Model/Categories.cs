using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Categories")]
    public class Categories
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên loại SP không để trống")]
        [Display(Name = "Tên loại sản phẩm")]
        public string Name { get; set; }

        [Display(Name = "Tên viêt gọn")]
        public string Slug { get; set; }

        [Display(Name = "Tên cấp cha")]
        public int? ParentID { get; set; }

        public int? Order { get; set; }

        [Required(ErrorMessage = "Mô tả không để trống")]
        [Display(Name = "Mô tả")]
        public String MetaDesc { get; set; }

        [Required(ErrorMessage = "Từ khóa không để trống")]
        [Display(Name = "Từ khóa")]
        public String MetaKey { get; set; }

        [Display(Name = "Người tạo")]
        public int CreatedBy { get; set; }

        [Display(Name = "Ngày tạo")]
        [Required (ErrorMessage ="Người tạo không được để trống")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Người cập nhật")]
        [Required (ErrorMessage ="Người cập nhật không để trống")]
        public int UpdateBy { get; set; }

        [Display(Name = "Ngày cập nhật")]
        [Required(ErrorMessage = "Ngày cập nhật không để trống")]
        public DateTime UpdateAt { get; set; }

        [Display(Name = "Trạng thái")]
        public int Status { get; set; }
    }
}
