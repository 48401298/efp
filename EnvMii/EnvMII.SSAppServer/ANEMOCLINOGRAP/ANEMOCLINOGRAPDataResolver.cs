using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvMII.VO;

namespace EnvMII.SSAppServer.ANEMOCLINOGRAP
{
    /// <summary>
    /// 风速风向仪数据解析
    /// </summary>
    public class ANEMOCLINOGRAPDataResolver
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

            //风速
            itemDatas.Add(
                new InspectItemData {
                    DeviceSN=oData.DeviceSN,
                    ItemCode = ItemCodes.windSpeed,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(6, 2) + oData.InspectData.Substring(4, 2))/10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );

            //风向
            itemDatas.Add(
                new InspectItemData
                {
                    DeviceSN = oData.DeviceSN,
                    ItemCode = ItemCodes.windDirection,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(10, 2) + oData.InspectData.Substring(8, 2)) / 10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );

            return itemDatas;
        }
    }
}
