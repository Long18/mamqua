using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ProjectMamQua.EF
{

    [Table("Content")]
    public partial class Content
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Đường dẫn (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]

        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]

        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]

        public string Image { get; set; }

        [Display(Name = "Danh mục (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]

        public long? ContentCategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [AllowHtml]
        [Display(Name = "Chi tiết (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]

        public string Detail { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModyfiedDate { get; set; }

        [StringLength(50)]
        public string ModyfiedBy { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

       [Display(Name = "Hiển thị (*)")]

        public bool Status { get; set; }

        [Display(Name = "Phổ biến (*)")]

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        public bool Active { get; set; }

        public virtual ContentCategory ContentCategory { get; set; }
    }
}
