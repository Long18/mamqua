using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEM3.Areas.Admin.Models.BusinessModel
{
    public class PermissionAction
    {
        public long PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string Desciption { get; set; }
        public bool IsGranted { get; set; }
    }
}