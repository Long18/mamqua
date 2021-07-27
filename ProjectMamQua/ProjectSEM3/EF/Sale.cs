namespace ProjectMamQua.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale
    {
        public long ID { get; set; }

        [Display(Name = "Giá (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public decimal? Price { get; set; }

        [Display(Name = "Ngày bắt đầu (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Ngày kết thúc (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Sản phẩm (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public long? ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}
