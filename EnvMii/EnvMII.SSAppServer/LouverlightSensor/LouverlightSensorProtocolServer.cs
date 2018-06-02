using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace EnvMII.SSAppServer.LouverlightSensor
{
    /// <summary>
    /// 百叶箱光照传感器
    /// </summary>
    public class LouverlightSensorProtocolServer : AppServer<LouverlightSensorProtocolSession, BinaryRequestInfo>
    {
        public LouverlightSensorProtocolServer()
            : base(new DefaultReceiveFilterFactory<LouverlightSensorReceiveFilter, BinaryRequestInfo>())
        {

        }
    }
}
