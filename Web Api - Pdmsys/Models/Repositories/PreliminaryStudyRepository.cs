using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.Interfaces;

namespace Web_Api___Pdmsys.Models.Repositories
{
    public class PreliminaryStudyRepository : IPreliminaryStudyRepository
    {
        private pdmsysEntities db = new pdmsysEntities();

        public project_effort_estimations getEffortEstimation(int projectId)
        {
            var query = (from model in db.project_effort_estimations
                         where model.Project_FK == projectId
                         select model).AsQueryable();
            if (query.Count() == 0)
                return null;
            return query.First<project_effort_estimations>();
        }

        public project_descriptions GetProjectDescription(int projectId)
        {
            var query = (from model in db.project_descriptions
                        where model.Project_FK == projectId
                        select model).AsQueryable();

            if (query.Count() == 0)
                return null;

            return query.First<project_descriptions>();
        }

        public IQueryable GetProjectRisks(int projectId)
        {
            var query = (from model in db.project_risks
                         where model.Project_FK == projectId
                         select new
                         {
                             id = model.Id,
                             name = model.name,
                             content = model.content
                         }).AsQueryable();

            return query;
        }

        public bool UpdateProjectDescription(project_descriptions model, int projectId)
        {
            var query = (from m in db.project_descriptions
                         where m.Project_FK == projectId
                         select m).AsQueryable();

            if (query.Count() == 0)
                return false;

            project_descriptions newDescription = query.First<project_descriptions>();
            newDescription.description = model.description;
            db.Entry(newDescription).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool UpdateProjectEffortEstimation(String content, int projectId)
        {
            var query = (from m in db.project_effort_estimations
                         where m.Project_FK == projectId
                         select m).AsQueryable();

            if (query.Count() == 0)
                return false;

            project_effort_estimations newDescription = query.First<project_effort_estimations>();
            newDescription.content = content;
            db.Entry(newDescription).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }


    }
}