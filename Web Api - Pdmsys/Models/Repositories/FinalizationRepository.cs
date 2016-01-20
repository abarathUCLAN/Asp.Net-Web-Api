using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.Interfaces;

namespace Web_Api___Pdmsys.Models.Repositories
{
    public class FinalizationRepository : IFinalizationRepository
    {
        pdmsysEntities db = new pdmsysEntities();

        public project_manuals GetprojectManual(int projectId)
        {
            var query = (from model in db.project_manuals
                         where model.Project_FK == projectId
                         select model).AsQueryable();

            if (query.Count() == 0)
                return null;

            return query.First<project_manuals>();
        }

        public IQueryable GetProjectprotocols(int projectId)
        {
            var query = (from model in db.project_acceptance_protocols
                         where model.Project_FK == projectId
                         select new
                         {
                             id = model.Id,
                             requirement = model.requirement,
                             criteriaName = model.criteriaName,
                             criteria = model.criteria,
                             note = model.note,
                             fulfilled = model.fulfilled
                         }).AsQueryable();

            return query;
        }

        public bool UpdateprojectManual(project_manuals model, int projectId)
        {
            var query = (from m in db.project_manuals
                         where m.Project_FK == projectId
                         select m).AsQueryable();

            if (query.Count() == 0)
                return false;

            project_manuals newDescription = query.First<project_manuals>();
            newDescription.content = model.content;
            db.Entry(newDescription).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
    }
}