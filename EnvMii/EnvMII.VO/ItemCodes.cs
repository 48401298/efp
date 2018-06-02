using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvMII.VO
{
    public class ItemCodes
    {
        public static string temperatureCode = "temp";

        public static string humidityCode = "hum";

        public static string windSpeed = "windSpeed";

        public static string windDirection = "windDirect";

        //高温
        public static string temperatureCodeHigh = "tempHigh";

        //低温
        public static string temperatureCodeLow = "tempLow";

        //高湿
        public static string humidityCodeHigh = "humHigh";

        //低湿
        public static string humidityCodeLow = "humLow";


        //电导率值
        public static string electricalConductivity = "electCondu";

        //盐度值
        public static string salinity = "salinity";

        //叶绿素值
        public static string chlorophyll = "chlorophyl";

        //浊度值
        public static string turbidity = "turbidity";

        //百叶箱光照传感器
        public static string louverlightSensor = "louligSen";
        
        //清洁刷状态
        public static string S = "S";
    }
}
