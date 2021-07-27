using System.Web.Mvc;

namespace ProjectMamQua.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Prices = new HashSet<Price>();
            Sales = new HashSet<Sale>();
        }

        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string Name { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã sản phẩm")]
        public string Code { get; set; }

        [StringLength(250)]
        [Display(Name = "Đường dẫn (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string MetaTitle { get; set; }

        [Column(TypeName = "ntext")]
        [AllowHtml]
        [Display(Name = "Mô tả (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        [Display(Name = "Số lượng (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public int? Quantity { get; set; }

        [Display(Name = "Danh mục")]
        public long? ProductCategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [AllowHtml]
        [Display(Name = "Chi tiết (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string Detail { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Phổ biến")]
        public DateTime? TopHot { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModyfiedDate { get; set; }

        [StringLength(50)]
        public string ModyfiedBy { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        public bool Status { get; set; }

        public int? ViewCount { get; set; }

        [Display(Name = "NSX")]
        public long? ProducerID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Price> Prices { get; set; }

        public virtual Producer Producer { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
