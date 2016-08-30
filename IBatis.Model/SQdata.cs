using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBatis.Model
{
    public class SQdata
    {
        #region 个人接待实体
        public string Reception { get; set; }
        public string CPH { set; get; }
        public string Conversion { get; set; }
        public string FrTime { get; set; }
        public string AvgTime { get; set; }
        public string ARTime { get; set; }
        #endregion

        #region 团队接待实体
        public string TD_Reception { get; set; }
        public string TD_CPH { set; get; }
        public string TD_Conversion { get; set; }
        public string TD_FrTime { get; set; }
        public string TD_AvgTime { get; set; }
        public string TD_ARTime { get; set; }
        #endregion
    }
}
