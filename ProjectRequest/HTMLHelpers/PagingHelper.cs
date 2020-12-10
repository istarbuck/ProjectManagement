using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using ProjectRequest.Models;
using System.Text.RegularExpressions;

namespace ProjectRequest.HTMLHelpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            string urlParameters = "";

            Regex reg = new Regex(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$");

            if (pagingInfo.completed == "false")
                urlParameters += "&completed=false";
            else if (pagingInfo.completed == "true")
                urlParameters += "&completed=true";
            else if(pagingInfo.completed == "all")
                urlParameters += "&completed=all";


            if (pagingInfo.location != "00" && pagingInfo.location != null)
                urlParameters += "&location=" + pagingInfo.location;

            //if (pagingInfo.excludeCategory != "00" && pagingInfo.excludeCategory != null && pagingInfo.excludeCategory.Length > 0)
            //    urlParameters += "&excludeCategory=" + pagingInfo.excludeCategory;

            if (pagingInfo.category != "00" && pagingInfo.category != null)
                urlParameters += "&category=" + pagingInfo.category;

            if (pagingInfo.assignedTo != "00" && pagingInfo.assignedTo != null)
                urlParameters += "&assignedTo=" + pagingInfo.assignedTo;

            if (pagingInfo.projectStatus != "00" && pagingInfo.projectStatus != null)
                urlParameters += "&projectStatus=" + pagingInfo.projectStatus;

            //if (pagingInfo.assignedTo != "00" && pagingInfo.assignedTo != null)
            //    urlParameters += "&excludeStatus=" + pagingInfo.projectStatus;

            if (pagingInfo.search != null && pagingInfo.search.Length > 0)
                urlParameters += "&search=" + pagingInfo.search;

            if (pagingInfo.searchPO != null)
                urlParameters += "&searchPO=" + pagingInfo.searchPO;

            if (pagingInfo.priority != null)
                urlParameters += "&priority=" + pagingInfo.priority;

            if (pagingInfo.approvers != null)
                urlParameters += "&approvers=" + pagingInfo.approvers;

            //if (pagingInfo.search != null)
            //    urlParameters += "&projectCompleted=" + pagingInfo.projectCompleted;

            if (pagingInfo.dateStart != null && pagingInfo.dateEnd != null && reg.IsMatch(pagingInfo.dateStart) && reg.IsMatch(pagingInfo.dateEnd))
            {
                urlParameters += "&dateStart=" + pagingInfo.dateStart;
                urlParameters += "&dateEnd=" + pagingInfo.dateEnd;
            }

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", pageUrl(i) + urlParameters);
                tag.InnerHtml = i.ToString();
                //if (i == pagingInfo.CurrentPage)
                //    tag.AddCssClass("selected");
                result.Append(tag.ToString() + "&nbsp;&nbsp;");
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}