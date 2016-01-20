using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.Interfaces;

namespace Web_Api___Pdmsys.Models.Repositories
{
    public class RequirementSpecificationRepository : IRequirementSpecificationRepository
    {

        private pdmsysEntities db = new pdmsysEntities();

        public project_actual_states GetactualState(int projectId)
        {
            var query = (from model in db.project_actual_states
                         where model.Project_FK == projectId
                         select model).AsQueryable();

            if (query.Count() == 0)
                return null;

            return query.First<project_actual_states>();
        }

        public project_data GetProductData(int projectId)
        {
            var query = (from model in db.project_data
                         where model.Project_FK == projectId
                         select model).AsQueryable();

            if (query.Count() == 0)
                return null;

            return query.First<project_data>();
        }

        public project_introductions GetProjectIntroduction(int projectId)
        {
            var query = (from model in db.project_introductions
                         where model.Project_FK == projectId
                         select model).AsQueryable();

            if (query.Count() == 0)
                return null;

            return query.First<project_introductions>();
        }

        public IQueryable GetProjectneedToHaves(int projectId)
        {
            var query = (from model in db.project_need_to_haves
                         where model.Project_FK == projectId
                         select new
                         {
                             id = model.Id,
                             name = model.name,
                             content = model.content
                         }).AsQueryable();

            return query;
        }

        public IQueryable GetProjectniceToHaves(int projectId)
        {
            var query = (from model in db.project_nice_to_haves
                         where model.Project_FK == projectId
                         select new
                         {
                             id = model.Id,
                             name = model.name,
                             content = model.content
                         }).AsQueryable();

            return query;
        }

        public IQueryable GetProjectnonFunctionalRequirements(int projectId)
        {
            var query = (from model in db.project_non_functional_requirements
                         where model.Project_FK == projectId
                         select new
                         {
                             id = model.Id,
                             name = model.name,
                             content = model.content
                         }).AsQueryable();

            return query;
        }

        public project_qualities GetProjectQuality(int projectId)
        {
            var query = (from model in db.project_qualities
                         where model.Project_FK == projectId
                         select model).AsQueryable();

            if (query.Count() == 0)
                return null;

            return query.First<project_qualities>();
        }

        public project_results GetProjectResult(int projectId)
        {
            var query = (from model in db.project_results
                         where model.Project_FK == projectId
                         select model).AsQueryable();

            if (query.Count() == 0)
                return null;

            return query.First<project_results>();
        }

        public project_uses GetProjectUse(int projectId)
        {
            var query = (from model in db.project_uses
                         where model.Project_FK == projectId
                         select model).AsQueryable();

            if (query.Count() == 0)
                return null;

            return query.First<project_uses>();
        }

        public project_target_states GettargetState(int projectId)
        {
            var query = (from model in db.project_target_states
                         where model.Project_FK == projectId
                         select model).AsQueryable();

            if (query.Count() == 0)
                return null;

            return query.First<project_target_states>();
        }

        public bool UpdateactualState(project_actual_states model, int projectId)
        {
            var query = (from m in db.project_actual_states
                         where m.Project_FK == projectId
                         select m).AsQueryable();

            if (query.Count() == 0)
                return false;

            project_actual_states newDescription = query.First<project_actual_states>();
            newDescription.content = model.content;
            db.Entry(newDescription).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool UpdateProductData(project_data model, int projectId)
        {
            var query = (from m in db.project_data
                         where m.Project_FK == projectId
                         select m).AsQueryable();

            if (query.Count() == 0)
                return false;

            project_data newDescription = query.First<project_data>();
            newDescription.content = model.content;
            db.Entry(newDescription).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool UpdateProjectIntroduction(project_introductions model, int projectId)
        {
            var query = (from m in db.project_introductions
                         where m.Project_FK == projectId
                         select m).AsQueryable();

            if (query.Count() == 0)
                return false;

            project_introductions newDescription = query.First<project_introductions>();
            newDescription.content = model.content;
            db.Entry(newDescription).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool UpdateProjectQuality(project_qualities model, int projectId)
        {
            var query = (from m in db.project_qualities
                         where m.Project_FK == projectId
                         select m).AsQueryable();

            if (query.Count() == 0)
                return false;

            project_qualities newDescription = query.First<project_qualities>();
            newDescription.content = model.content;
            db.Entry(newDescription).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool UpdateProjectResult(project_results model, int projectId)
        {
            var query = (from m in db.project_results
                         where m.Project_FK == projectId
                         select m).AsQueryable();

            if (query.Count() == 0)
                return false;

            project_results newDescription = query.First<project_results>();
            newDescription.content = model.content;
            db.Entry(newDescription).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool UpdateProjectUse(project_uses model, int projectId)
        {
            var query = (from m in db.project_uses
                         where m.Project_FK == projectId
                         select m).AsQueryable();

            if (query.Count() == 0)
                return false;

            project_uses newDescription = query.First<project_uses>();
            newDescription.content = model.content;
            db.Entry(newDescription).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool UpdatetargetState(project_target_states model, int projectId)
        {
            var query = (from m in db.project_target_states
                         where m.Project_FK == projectId
                         select m).AsQueryable();

            if (query.Count() == 0)
                return false;

            project_target_states newDescription = query.First<project_target_states>();
            newDescription.content = model.content;
            db.Entry(newDescription).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
    }
}