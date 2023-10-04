using System.Collections.Generic;

namespace MVCReport.Models
{

    public class TradeViewModel
    {
        public List<ReportNode> ReportData { get; set; }
        public int GrandTotal { get; set; }
    }
}