using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvMII.VO;

namespace EnvMII.SSAppServer.TH10WWiFi
{
    /// <summary>
    /// TH10W-WiFi无线温湿度记录仪数据解析
    /// </summary>
    public class TH10WDataResolver
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

            //温度
            itemDatas.Add(
                new InspectItemData {
                    DeviceSN=oData.DeviceSN,
                    ItemCode = ItemCodes.temperatureCode,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(14, 4))/10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );

            //湿度
            itemDatas.Add(
                new InspectItemData
                {
                    DeviceSN = oData.DeviceSN,
                    ItemCode = ItemCodes.humidityCode,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(18, 4)) / 10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );

            return itemDatas;
        }
    }
}
