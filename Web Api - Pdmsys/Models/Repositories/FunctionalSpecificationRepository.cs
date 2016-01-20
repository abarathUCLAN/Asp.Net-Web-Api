using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.Interfaces;

namespace Web_Api___Pdmsys.Models.Repositories
{
    public class FunctionalSpecificationRepository : IFunctionalSpecificationRepository
    {
        pdmsysEntities db = new pdmsysEntities();

        public IQueryable GetProjectfunctionalRequirements(int projectId)
        {
            var query = (from model in db.project_functional_requirements
                         where model.Project_FK == projectId
                         select new
                         {
                             id = model.Id,
                             name = model.name,
                             content = model.content
                         }).AsQueryable();

            return query;
        }

        public project_implementations GetProjectImplementation(int projectId)
        {
            var query = (from model in db.project_implementations
                         where model.Project_FK == projectId
                         select model).AsQueryable();

            if (query.Count() == 0)
                return null;

            return query.First<project_implementations>();
        }

        public bool UpdateProjectImplementation(project_implementations model, int projectId)
        {
            var query = (from m in db.project_implementations
                         where m.Project_FK == projectId
                         select m).AsQueryable();

            if (query.Count() == 0)
                return false;

            project_implementations newDescription = query.First<project_implementations>();
            newDescription.content = model.content;
            db.Entry(newDescription).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
    }
}