using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.Interfaces;
using Web_Api___Pdmsys.Models.Repositories;

namespace Web_Api___Pdmsys.Controllers
{
    [Authorize]
    [RoutePrefix("api/preliminaryStudy")]
    public class PreliminaryStudyController : ApiController
    {
        private pdmsysEntities db = new pdmsysEntities();
        static readonly IPreliminaryStudyRepository _repo = new PreliminaryStudyRepository();

        [HttpGet]
        [Route("projectDescription/{projectId}")]
        public IHttpActionResult GetProjectDescription(int projectId)
        {
            project_descriptions query = _repo.GetProjectDescription(projectId);
            if (query == null)
                return BadRequest();
            return Ok(new { description = query.description });
        }

        [HttpPost]
        [Route("projectDescription/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public async Task<IHttpActionResult> PostProjectDescription(project_descriptions model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool updated = _repo.UpdateProjectDescription(model, projectId);
            

            if(updated == true)
                return Ok();

            model.Project_FK = projectId;
            db.project_descriptions.Add(model);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("projectDescription/delete/{projectId}")]
        [AdminTypeActionFilter]
        public async Task<IHttpActionResult> DeleteProjectDescription(int projectId)
        {
            var delete = from des in db.project_descriptions
                         where des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_descriptions.Remove(delete.FirstOrDefault<project_descriptions>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("risk/{projectId}")]
        public IQueryable GetProjectRisks(int projectId)
        {
            return _repo.GetProjectRisks(projectId);
        }

        [HttpPost]
        [Route("risk/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public async Task<IHttpActionResult> PostProjectRisk(project_risks model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Project_FK = projectId;
            db.project_risks.Add(model);

            await db.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost]
        [Route("risk/delete/{projectId}/{riskId}")]
        [AdminTypeActionFilter]
        public async Task<IHttpActionResult> DeleteProjectRisk(int projectId, int riskId)
        {
            var delete = from des in db.project_risks
                         where des.Id == riskId && des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_risks.Remove(delete.FirstOrDefault<project_risks>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("effortEstimation/{projectId}")]
        public IHttpActionResult GetProjectEffortEstimation(int projectId)
        {
            project_effort_estimations query = _repo.getEffortEstimation(projectId);
            if (query == null)
                return BadRequest();

            return Ok(new { content = query.content });
        }

        [HttpPost]
        [Route("effortEstimation/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public async Task<IHttpActionResult> PostProjectEffortEstimation([FromBody] object content, int projectId)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
            bool updated = _repo.UpdateProjectEffortEstimation(json, projectId);


            if (updated == true)
                return Ok();

            project_effort_estimations es = new project_effort_estimations();

            es.Project_FK = projectId;
            es.content = json;
            db.project_effort_estimations.Add(es);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("effortEstimation/delete/{projectId}")]
        [AdminTypeActionFilter]
        public async Task<IHttpActionResult> DeleteProjectEffortEstimation(int projectId)
        {
            var delete = from des in db.project_effort_estimations
                         where des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_effort_estimations.Remove(delete.FirstOrDefault<project_effort_estimations>());
            await db.SaveChangesAsync();

            return Ok();
        }
    }
}