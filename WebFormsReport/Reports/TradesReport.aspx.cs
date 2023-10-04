using ICBC.DAL.Context;
using ICBC.DAL.UnitOfWork;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;

namespace WebFormsReport.Reports
{
    public partial class TradesReport : System.Web.UI.Page
    {
        private readonly string _conn;
        public TradesReport()
        {
            _conn = ConfigurationManager.ConnectionStrings["ICBCData"].ConnectionString;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ICBCContext context = new ICBCContext(_conn);

                UnitOfWork uow = new UnitOfWork(context);
                var tradesData = uow.TradeRepository.GetList();

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("TradesReport.rdlc");
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsTrades", tradesData));

            }

        }
    }
}