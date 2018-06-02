using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace EnvMII.SSAppServer.ACLWCAR
{
    /// <summary>
    /// ACTW-CAR小型清洁刷式温盐传感器监控服务
    /// </summary>
    public class ACLWCARProtocolServer : AppServer<ACLWCARProtocolSession, BinaryRequestInfo>
    {
        public ACLWCARProtocolServer()
            : base(new DefaultReceiveFilterFactory<ACLWCARReceiveFilter, BinaryRequestInfo>())
        {

        }
    }
}
