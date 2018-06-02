using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvMII.VO;

namespace EnvMII.SSAppServer.TH11SBRS485
{
    /// <summary>
    /// TH11S-B RS485通讯型温湿度变送器数据解析
    /// </summary>
    public class TH11SDataResolver
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

            //温度H
            itemDatas.Add(
                new InspectItemData {
                    DeviceSN=oData.DeviceSN,
                    ItemCode = ItemCodes.temperatureCodeHigh,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(6, 2))/10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );

            //温度L
            itemDatas.Add(
                new InspectItemData
                {
                    DeviceSN = oData.DeviceSN,
                    ItemCode = ItemCodes.temperatureCodeLow,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(8, 2)) / 10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );

            //湿度H
            itemDatas.Add(
                new InspectItemData
                {
                    DeviceSN = oData.DeviceSN,
                    ItemCode = ItemCodes.humidityCodeHigh,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(10, 2)) / 10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );

            //湿度L
            itemDatas.Add(
                new InspectItemData
                {
                    DeviceSN = oData.DeviceSN,
                    ItemCode = ItemCodes.humidityCodeLow,
                    InspectData = (Tools.HexStrToDecimal(oData.InspectData.Substring(12, 2)) / 10).ToString(),
                    InspectTime = oData.InspectTime
                }
            );

            return itemDatas;
        }
    }
}
