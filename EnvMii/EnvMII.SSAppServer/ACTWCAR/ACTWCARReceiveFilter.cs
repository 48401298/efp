using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;

namespace EnvMII.SSAppServer.ACTWCAR
{
    /// <summary>
    /// ACLW-CAR传感器通讯协议与电气参数消息过滤
    /// </summary>
    public class ACTWCARReceiveFilter : BeginEndMarkReceiveFilter<BinaryRequestInfo>
    {

        //开始和结束标记也可以是两个或两个以上的字节
       
        private readonly static byte[] BeginMark = Tools.hexStrToByte("1111");
        private readonly static byte[] EndMark = Tools.hexStrToByte("2C0D");

        public ACTWCARReceiveFilter()
            : base(BeginMark, EndMark) //传入开始标记和结束标记
        {
        
        }

        protected override BinaryRequestInfo ProcessMatchedRequest(byte[] readBuffer, int offset, int length)
        {
          
            //TODO: 通过解析到的数据来构造请求实例，并返回
            string data = Tools.BytesToHexStr(readBuffer, readBuffer.Length);
            return new BinaryRequestInfo("04", Tools.hexStrToByte(data.Substring(4, data.Length - 2 - 4)));
        }

        //public ACTWCARReceiveFilter()
        //    : base(7)
        //{

        //}

        ///// <summary>
        ///// 获取消息体长度
        ///// </summary>
        ///// <param name="header">头信息</param>
        ///// <param name="offset">起始位</param>
        ///// <param name="length">长度</param>
        ///// <returns>消息体长度</returns>
        //protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        //{
        //    return 24;
        //}

        ///// <summary>
        ///// 解析请求信息
        ///// </summary>
        ///// <param name="header">消息头</param>
        ///// <param name="bodyBuffer">消息体</param>
        ///// <param name="offset">起始位</param>
        ///// <param name="length">长度</param>
        ///// <returns>请求信息</returns>
        //protected override BinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        //{
        //    return new BinaryRequestInfo(Tools.BytesToHexStr(header.Array, 7), bodyBuffer.CloneRange(offset, length));
        //}
    }
}
