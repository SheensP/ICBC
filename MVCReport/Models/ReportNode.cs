using System.Collections.Generic;

namespace MVCReport.Models
{
    public class ReportNode
    {
        public string GroupingName { get; set; }
        public LabelValue Current { get; set; }
        public List<ReportNode> Children { get; set; }
    }
}

public class LabelValue
{
    public string Label { get; set; }
    public int Value { get; set; }
}