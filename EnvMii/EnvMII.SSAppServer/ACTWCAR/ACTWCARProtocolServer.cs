using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace EnvMII.SSAppServer.ACTWCAR
{
    /// <summary>
    /// ACLW-CAR传感器通讯协议与电气参数监控服务
    /// </summary>
    public class ACTWCARProtocolServer : AppServer<ACTWCARProtocolSession, BinaryRequestInfo>
    {
        public ACTWCARProtocolServer()
            : base(new DefaultReceiveFilterFactory<ACTWCARReceiveFilter, BinaryRequestInfo>())
        {

        }
    }
}
