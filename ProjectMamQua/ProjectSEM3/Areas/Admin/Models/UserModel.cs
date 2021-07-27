using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Areas.Admin.Models
{
    public class UserModel
    {
        [StringLength(50)]
        [Display(Name = "Tên tài khoản")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu cũ")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới không trùng khớp!")]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}