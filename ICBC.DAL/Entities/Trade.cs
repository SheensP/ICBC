namespace ICBC.DAL.Entities
{
    public partial class Trade
    {
        public int TradeId { get; set; }

        public string Instrument { get; set; }

        public string BusinessUnit { get; set; }

        public int ProfitCentre { get; set; }

        public int ReportingAmount { get; set; }
    }
}