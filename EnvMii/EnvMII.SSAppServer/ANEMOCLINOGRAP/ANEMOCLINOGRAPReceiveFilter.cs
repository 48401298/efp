using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;

namespace EnvMII.SSAppServer.ANEMOCLINOGRAP
{
    /// <summary>
    /// 风速风向仪消息过滤
    /// </summary>
    public class ANEMOCLINOGRAPReceiveFilter : FixedHeaderReceiveFilter<BinaryRequestInfo>
    {
        public ANEMOCLINOGRAPReceiveFilter()
            : base(4)
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
            string l = header[offset + 2].ToString().PadLeft(2, '0');
            string h = header[offset + 3].ToString().PadLeft(2, '0');
            return Tools.HexStrToInt(h + l) + 2;//加crc和尾
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
            return new BinaryRequestInfo(Tools.BytesToHexStr(header.Array, 2), bodyBuffer.CloneRange(offset, length));
        }
    }
}
