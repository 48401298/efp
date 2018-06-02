using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvMII.VO;

namespace EnvMII.SSAppServer.LouverlightSensor
{
    /// <summary>
    /// 百叶箱光照传感器
    /// </summary>
    public class LouverlightSensorDataResolver
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
                new InspectItemData {
                    DeviceSN=oData.DeviceSN,
                    ItemCode = ItemCodes.temperatureCode,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(10, 4))/10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );

            //温度值
            itemDatas.Add(
                new InspectItemData
                {
                    DeviceSN = oData.DeviceSN,
                    ItemCode = ItemCodes.humidityCode,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(14, 4)) / 10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );
            
            return itemDatas;
        }
    }
}
