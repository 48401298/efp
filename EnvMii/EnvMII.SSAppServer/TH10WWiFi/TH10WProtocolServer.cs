using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace EnvMII.SSAppServer.TH10WWiFi
{
    /// <summary>
    /// TH10W-WiFi无线温湿度记录仪监控服务
    /// </summary>
    public class TH10WProtocolServer : AppServer<TH10WProtocolSession, BinaryRequestInfo>
    {
        public TH10WProtocolServer()
            : base(new DefaultReceiveFilterFactory<TH10WReceiveFilter, BinaryRequestInfo>())
        {

        }
    }
}
