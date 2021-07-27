namespace ProjectMamQua.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long ID { get; set; }

        [Display(Name = "Tài khoản")]
        public long? UserID { get; set; }

        [StringLength(60)]
        [Display(Name = "Họ tên (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string ShipName { get; set; }

        [StringLength(20)]
        [Display(Name = "SĐT (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string ShipPhone { get; set; }

        [StringLength(500)]
        [Display(Name = "Địa chỉ (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string ShipAddress { get; set; }

        [Display(Name = "Ngày đặt")]
        public DateTime ShipCreateDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Email (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string ShipEmail { get; set; }


        public long? PaymentID { get; set; }

        public int? Status { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
