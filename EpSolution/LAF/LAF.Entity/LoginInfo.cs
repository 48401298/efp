using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Entity
{
    /// <summary>
    /// 登录信息
    /// </summary>
    [Serializable]
    public class LoginInfo
    {
        public string UserID { get; set; }
        public string LoginUserID { get; set; }
        public string OrgaID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string UserDes { get; set; }
        /// <summary>
        /// 所在班组
        /// </summary>
        public string WorkGroupID { get; set; }

        /// <summary>
        /// 会话ID
        /// </summary>
        public string ServiceSessionID { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<string> Powers { get; set; }

    }
}
