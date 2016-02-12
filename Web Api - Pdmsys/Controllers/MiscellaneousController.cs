using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.helpers;
using Web_Api___Pdmsys.Models.Interfaces;
using Web_Api___Pdmsys.Models.Repositories;

namespace Web_Api___Pdmsys.Controllers
{
    [Authorize]
    [RoutePrefix("api/miscellaneous")]
    public class MiscellaneousController : ApiController
    {
        private pdmsysEntities db = new pdmsysEntities();
        static readonly IMiscellaneousRepository _repo = new MiscellaneousRepository();

        [HttpGet]
        [Route("presentation/{projectId}")]
        public IQueryable GetProjectpresentations(int projectId)
        {
            return _repo.GetProjectpresentations(projectId);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("presentation/download/{presentationId}")]
        public HttpResponseMessage DownloadPresentation(int presentationId)
        {
            project_presentations pre = db.project_presentations.Find(presentationId);
            var localFilePath = HttpContext.Current.Server.MapPath("~/App_Data/presentations/" + pre.file);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = pre.file;

            return response;
        }

        [HttpPost]
        [Route("presentation/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public async Task<IHttpActionResult> PostProjectpresentation( int projectId)
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count == 1)
            {
                string name = httpRequest.Form.GetValues("name").First().ToString();
                string content = httpRequest.Form.GetValues("content").First().ToString();
                project_presentations model = new project_presentations();
                model.name = name;
                model.content = content;
                model.Project_FK = projectId;
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    model.file = postedFile.FileName;
                    var filePath = HttpContext.Current.Server.MapPath("~/App_Data/presentations/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                }
                db.project_presentations.Add(model);
                await db.SaveChangesAsync();
                return Ok(model);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("presentation/delete/{projectId}/{presentationId}")]
        [AdminTypeActionFilter]
        public async Task<IHttpActionResult> DeleteProjectpresentation(int projectId, int presentationId)
        {
            var delete = from des in db.project_presentations
                         where des.Id == presentationId && des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            project_presentations pres = delete.FirstOrDefault<project_presentations>();

            File.Delete(HttpContext.Current.Server.MapPath("~/App_Data/presentations/" + pres.file));

            db.project_presentations.Remove(pres);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("changeRequest/{projectId}")]
        public IQueryable GetProjectchangeRequests(int projectId)
        {
            return _repo.GetProjectchangeRequests(projectId);
        }

        [HttpPost]
        [Route("changeRequest/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public async Task<IHttpActionResult> PostProjectchangeRequest(project_change_requests model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Project_FK = projectId;
            db.project_change_requests.Add(model);

            await db.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost]
        [Route("changeRequest/delete/{projectId}/{changeRequestId}")]
        [AdminTypeActionFilter]
        public async Task<IHttpActionResult> DeleteProjectchangeRequest(int projectId, int changeRequestId)
        {
            var delete = from des in db.project_change_requests
                         where des.Id == changeRequestId && des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_change_requests.Remove(delete.FirstOrDefault<project_change_requests>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("styleGuide/{projectId}")]
        public IQueryable GetProjectstyleGuides(int projectId)
        {
            return _repo.GetProjectreports(projectId);
        }

        [HttpPost]
        [Route("styleGuide/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public async Task<IHttpActionResult> PostProjectstyleGuide(project_style_guides model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Project_FK = projectId;
            db.project_style_guides.Add(model);

            await db.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost]
        [Route("styleGuide/delete/{projectId}/{styleGuideId}")]
        [AdminTypeActionFilter]
        public async Task<IHttpActionResult> DeleteProjectstyleGuide(int projectId, int styleGuideId)
        {
            var delete = from des in db.project_style_guides
                         where des.Id == styleGuideId && des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_style_guides.Remove(delete.FirstOrDefault<project_style_guides>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("report/{projectId}")]
        public IQueryable GetProjectreports(int projectId)
        {
            return _repo.GetProjectreports(projectId);
        }

        [HttpPost]
        [Route("report/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public async Task<IHttpActionResult> PostProjectreport(project_reports model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Project_FK = projectId;
            db.project_reports.Add(model);

            await db.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost]
        [Route("report/delete/{projectId}/{reportId}")]
        [AdminTypeActionFilter]
        public async Task<IHttpActionResult> DeleteProjectreports(int projectId, int reportId)
        {
            var delete = from des in db.project_reports
                         where des.Id == reportId && des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_reports.Remove(delete.FirstOrDefault<project_reports>());
            await db.SaveChangesAsync();

            return Ok();
        }


    }
}