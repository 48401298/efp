using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Entity
{
    /// <summary>
    /// 祖先实体
    /// </summary>
    [Serializable]
    public class BaseEntity
    {
        //浅复制
        public object Clone()
        {
            return (Object)this.MemberwiseClone();
        }
    }
}
