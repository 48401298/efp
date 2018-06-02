using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace EnvMII.SSAppServer.ANEMOCLINOGRAP
{
    /// <summary>
    /// 风速风向仪监控服务
    /// </summary>
    public class ANEMOCLINOGRAPProtocolServer : AppServer<ANEMOCLINOGRAPProtocolSession, BinaryRequestInfo>
    {
        public ANEMOCLINOGRAPProtocolServer()
            : base(new DefaultReceiveFilterFactory<ANEMOCLINOGRAPReceiveFilter, BinaryRequestInfo>())
        {

        }
    }
}
