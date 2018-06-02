using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace EnvMII.SSAppServer.TH11SBRS485
{
    /// <summary>
    /// TH11S-B RS485通讯型温湿度变送器监控服务
    /// </summary>
    public class TH11SProtocolServer : AppServer<TH11SProtocolSession, BinaryRequestInfo>
    {
        public TH11SProtocolServer()
            : base(new DefaultReceiveFilterFactory<TH11SReceiveFilter, BinaryRequestInfo>())
        {

        }
    }
}
