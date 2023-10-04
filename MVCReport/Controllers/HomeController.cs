using ICBC.DAL.Context;
using ICBC.DAL.UnitOfWork;
using MVCReport.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVCReport.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _conn;
        private readonly UnitOfWork _uow;
        public HomeController() {

            _conn = ConfigurationManager.ConnectionStrings["ICBCData"].ConnectionString;
            ICBCContext context = new ICBCContext(_conn);
            _uow = new UnitOfWork(context);

        }
        public ActionResult Index()
        {

            var tradesData = _uow.TradeRepository.GetList();

            var vm = new TradeViewModel();


            vm.ReportData = tradesData.GroupBy(x => x.Instrument).Select(instrGroup => new ReportNode()
            {
                GroupingName = "Instrument",
                Current = new LabelValue()
                {
                    Label = instrGroup.Key,
                    Value = instrGroup.Select(x => x.ReportingAmount).Sum()
                },
                Children = instrGroup.GroupBy(x => x.BusinessUnit).Select(businessGroup => new ReportNode()
                {
                    GroupingName = "BusinessUnit",
                    Current = new LabelValue()
                    {
                        Label = businessGroup.Key,
                        Value = businessGroup.Select(x => x.ReportingAmount).Sum()
                    },
                    Children = businessGroup.Select(profitItems => new ReportNode()
                    {
                        GroupingName = "",
                        Current = new LabelValue()
                        {
                            Label = profitItems.ProfitCentre.ToString(),
                            Value = profitItems.ReportingAmount
                        },
                        Children = new List<ReportNode>()
                    }).ToList()
                }).ToList()
            }).ToList();

            vm.GrandTotal = tradesData.Sum(x => x.ReportingAmount);

            return View(vm);
        }

        public ActionResult LoadData() {
            _uow.LoadData();
            return RedirectToAction("Index");
        }
    }
}