namespace ProjectMamQua.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Permission")]
    public partial class Permission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Permission()
        {
            GrantPermissions = new HashSet<GrantPermission>();
        }

        public long ID { get; set; }

        [StringLength(260)]
        public string PermissionName { get; set; }

        [StringLength(250)]
        [Display(Name = "Mô tả")]
        public string Desciption { get; set; }

        [StringLength(60)]
        public string BusinessID { get; set; }

        public virtual Business Business { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GrantPermission> GrantPermissions { get; set; }
    }
}
