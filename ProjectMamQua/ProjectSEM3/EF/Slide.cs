namespace ProjectMamQua.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public long ID { get; set; }

        [StringLength(200)]
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(200)]
        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string Image { get; set; }

        [Display(Name = "Hiển thị")]
        public bool Status { get; set; }

        public long? TypeID { get; set; }

        public virtual SlideType SlideType { get; set; }
    }
}
