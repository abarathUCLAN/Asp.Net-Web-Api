using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Web_Api___Pdmsys.Models.Repositories;

namespace Web_Api___Pdmsys.Controllers
{
    public class MemberAndSpectatorActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            object projectid;
            actionContext.ActionArguments.TryGetValue("projectId", out projectid);
            int result = new UserProjectRel().GetProjectRightsByProjectId(Convert.ToInt32(projectid));
            if (result < 1)
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}