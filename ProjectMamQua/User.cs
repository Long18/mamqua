using Microsoft.AspNet.Identity;

namespace ProjectSEM3.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên tài khoản")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ Tên")]
        public string Name { get; set; }

        [Display(Name = "Ảnh")]
        [StringLength(100)]
        public string Avatar { get; set; }

        [StringLength(200)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(5)]
        [Display(Name = "Giới tính")]
        public string Genre { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày sinh")]
        public DateTime? BirthDay { get; set; }

        [StringLength(50)]
        [Display(Name = "SĐT")]
        public string Phone { get; set; }

        public DateTime CreateDate { get; set; }

        [Display(Name = "Quyền")]
        public long? GroupUserID { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModyfiedDate { get; set; }

        [StringLength(50)]
        public string ModyfiedBy { get; set; }

        public bool Active { get; set; }

        public bool? Status { get; set; }

    
        public virtual GroupUser GroupUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
