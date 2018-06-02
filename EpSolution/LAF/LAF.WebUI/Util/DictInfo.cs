using System;
using System.Collections.Generic;
using System.Text;

namespace LAF.WebUI.Util
{
    /// <summary>
    /// 字典信息
    /// </summary>
    public class DictInfo 
    {
        private string _ID = null;//主键
        private string _Code = null;//编号
        private string _Des = null;//名称
        private string _Type = null;//类型
        private int _Seq;//序号
        private object _Tag = null;//数据

        /// <summary>
        /// 主键
        /// </summary>
        public string ID
        {
            set
            {
                if (this._ID != value)
                    this._ID = value;
            }
            get
            {
                return this._ID;
            }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code
        {
            set
            {
                if (this._Code != value)
                    this._Code = value;
            }
            get
            {
                return this._Code;
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Des
        {
            set
            {
                if (this._Des != value)
                    this._Des = value;
            }
            get
            {
                return this._Des;
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            set
            {
                if (this._Type != value)
                    this._Type = value;
            }
            get
            {
                return this._Type;
            }
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int Seq
        {
            set
            {
                if (this._Seq != value)
                    this._Seq = value;
            }
            get
            {
                return this._Seq;
            }
        }
        

        /// <summary>
        /// 数据
        /// </summary>
        public object Tag
        {
            set
            {
                if (this._Tag != value)
                    this._Tag = value;
            }
            get
            {
                return this._Tag;
            }
        }
    }
}
