using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectRequest.Models;


namespace ProjectRequest.Models
{
    public class AuthorizeUser : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);

            ProjectRequestEntities request = new ProjectRequestEntities();

            var staffList = request.Staffs.Select(s => s.staffID).ToList();

            string name = Convert.ToString(httpContext.User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            if (!authorized)
            {
                // The user is not authenticated
                return false;
            }

            if(staffList.Contains(name))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}