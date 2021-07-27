using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSEM3.Common
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { get; set; }
        public string UserName { get; set; }

        public long? GroupUserID { get; set; }


        public UserLogin(long userId, string userName, long? groupUserId)
        {
            UserID = userId;
            UserName = userName;
            GroupUserID = groupUserId;
        }
        public UserLogin()
        {
            
        }
    }
}
