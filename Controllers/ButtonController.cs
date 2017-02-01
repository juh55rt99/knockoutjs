using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using EzyMVC.Models;

namespace EzyMVC.Controllers
{
    public class ButtonController : Controller
    {
        public ActionResult Index()
        {
            return View("");
        }

        public JsonResult GetRates()
        {
            List<string> country = new List<string>() { "EUR", "USD" };
            string xmlData = HttpContext.Server.MapPath("~/App_Data/CurrencyRates.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(xmlData);
            var rates = new List<Row>();
            rates = (from rows in ds.Tables[0].AsEnumerable()
                     where country.Contains(rows.Field<string>("swift_code"))
                     select new Row
                     {
                         Swift_code = rows[0].ToString(),
                         Swift_name = rows[1].ToString(),
                         Multiply = rows[2].ToString(),
                         Buy_cash = rows[3].ToString(),
                         Buy_tc = rows[4].ToString(),
                         Sell_cash = rows[5].ToString(),
                         Sell_tc = rows[6].ToString(),
                         CurrencyGuide = rows[7].ToString(),
                         Base_swift = rows[8].ToString()
                     }).ToList();
            ds.Dispose();

            return Json(rates, JsonRequestBehavior.AllowGet);
        }
    }
}
