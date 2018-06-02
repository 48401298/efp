using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvMII.VO;

namespace EnvMII.SSAppServer.ACLWCAR
{
    /// <summary>
    /// ACTW-CAR小型清洁刷式温盐传感器数据解析
    /// </summary>
    public class ACLWCARDataResolver
    {
        public InspectOriginalData CreateOriginalData(string DeviceSN, SuperSocket.SocketBase.Protocol.BinaryRequestInfo requestInfo)
        {
            InspectOriginalData data = new InspectOriginalData();

            data.InspectTime = CommonDateConvert.ConvertGMTToUTC(DateTime.Now);
            data.InspectData = requestInfo.Key + Tools.BytesToHexStr(requestInfo.Body, requestInfo.Body.Length);
            data.DeviceSN = DeviceSN;

            return data;
        }

        public List<InspectItemData> ResolveItemData(InspectOriginalData oData)
        {
            List<InspectItemData> itemDatas = new List<InspectItemData>();

            //温度值
            itemDatas.Add(
                new InspectItemData
                {
                    DeviceSN = oData.DeviceSN,
                    ItemCode = ItemCodes.temperatureCode,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(10, 2)) / 10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );

            //叶绿素值
            itemDatas.Add(
                new InspectItemData
                {
                    DeviceSN = oData.DeviceSN,
                    ItemCode = ItemCodes.chlorophyll,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(14, 2)) / 10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );

            //浊度值
            itemDatas.Add(
                new InspectItemData
                {
                    DeviceSN = oData.DeviceSN,
                    ItemCode = ItemCodes.turbidity,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(18, 2)) / 10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );

            itemDatas.Add(
               new InspectItemData
               {
                   DeviceSN = oData.DeviceSN,
                   ItemCode = ItemCodes.S,
                   InspectData = (Tools.HexStringToString(oData.InspectData.Substring(22, 2), Encoding.UTF8)).ToString(),
                   InspectTime = oData.InspectTime
               }
           );

            return itemDatas;
        }
    }
}
