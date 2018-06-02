using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;

namespace EnvMII.SSAppServer.TH11SBRS485
{
    /// <summary>
    /// TH11S-B RS485通讯型温湿度变送器消息过滤
    /// </summary>
    public class TH11SReceiveFilter : FixedHeaderReceiveFilter<BinaryRequestInfo>
    {

        public TH11SReceiveFilter()
            : base(3)
        {

        }

        /// <summary>
        /// 获取消息体长度
        /// </summary>
        /// <param name="header">头信息</param>
        /// <param name="offset">起始位</param>
        /// <param name="length">长度</param>
        /// <returns>消息体长度</returns>
        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return 6;
        }

        /// <summary>
        /// 解析请求信息
        /// </summary>
        /// <param name="header">消息头</param>
        /// <param name="bodyBuffer">消息体</param>
        /// <param name="offset">起始位</param>
        /// <param name="length">长度</param>
        /// <returns>请求信息</returns>
        protected override BinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            return new BinaryRequestInfo(Tools.BytesToHexStr(header.Array, 3), bodyBuffer.CloneRange(offset, length));
        }
    }
}
