namespace ProjectMamQua.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Price")]
    public partial class Price
    {
        public long Id { get; set; }

        [Column("Price")]
        [Display(Name = "Giá (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public decimal? Price1 { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public long? ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}
