using System;
using System.Web.Mvc;

using HtmlTags;

namespace VoteSystem.Clients.MVC.Infrastructure.Extensions
{
    public static class CustomHtmlHelpers
    {
        public static string TimeDisplay(this HtmlHelper helper, DateTime endDate)
        {
            if (endDate == null)
            {
                throw new ArgumentNullException(nameof(endDate));
            }

            TimeSpan ts = endDate - DateTime.Now;

            string timeAsString = string.Empty;

            if (ts.Hours == 0 && ts.Days == 0)
            {
                timeAsString = string.Format(
                                           "{0} минут{1}",                                         
                                           ts.Minutes,
                                           ts.Minutes == 1 ? "а" : "и");
            }
            else if (ts.Days == 0)
            {
                timeAsString = string.Format(
                                            "{0} час{1} {2} минут{3}",
                                            ts.Hours,
                                            ts.Hours == 1 ? string.Empty : "а",
                                            ts.Minutes,
                                            ts.Minutes == 1 ? "а" : "и");
            }
            else
            {
                timeAsString = string.Format(
                                            "{4} д{5} {0} час{1} {2} минут{3}",
                                            ts.Hours,
                                            ts.Hours == 1 ? string.Empty : "а",
                                            ts.Minutes,
                                            ts.Minutes == 1 ? "а" : "и",
                                            ts.Days,
                                            ts.Days == 1 ? "ен" : "ни");
            }

            return timeAsString;
        }

        public static HtmlTag VoteSystemStatus(this HtmlHelper helper, DateTime startDate, DateTime endDate)
        {
            if (startDate == null)
            {
                throw new ArgumentNullException(nameof(startDate));
            }

            if (endDate == null)
            {
                throw new ArgumentNullException(nameof(endDate));
            }
            
            HtmlTag status = new HtmlTag("span");
           
            if (startDate <= DateTime.Now && DateTime.Now <= endDate)
            {
                status.Text("Active").AddClass("btn btn-success btn-xs");
            }
            else if (startDate > DateTime.Now)
            {
                status.Text("Upcoming").AddClass("btn btn-warning btn-xs");
            }
            else if (endDate < DateTime.Now)
            {
                status.Text("Past").AddClass("btn btn-danger btn-xs");
            }

            return status;
        }
    }
}
