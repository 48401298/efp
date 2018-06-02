using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;

namespace EnvMII.SSAppServer
{
    /// <summary>
    /// 设备SESSION类
    /// </summary>
    public class CommonSession
    {
        public string DeviceSN { set; get; }

        public object session { set; get; }

        public bool isWorking { set; get; }
    }
}
