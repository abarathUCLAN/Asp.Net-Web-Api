using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.helpers;

namespace Web_Api___Pdmsys.Models.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private PdmsysContext context;

        private pdmsysEntities db;

        private UserManager<IdentityUser> _userManager;

        public ProjectRepository()
        {
            context = new PdmsysContext();
            db = new pdmsysEntities();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

        public void Add(Projects item)
        {

            db.Projects.Add(item);
            db.SaveChangesAsync();
        }

        public bool AddMemberToProject(MemberModel member)
        {
            throw new NotImplementedException();
        }

        public bool findProjectByName(string name)
        {
            var query = from result in db.Projects
                        where result.name == name
                        select result;

            if (query.Count() == 0)
                return false;
            return true;
        }

        public int getFinalizationPercent(int projectId)
        {
            int count = 0;
            var acceptanceProtocolQuery = from model in db.project_acceptance_protocols
                                          where model.Project_FK == projectId
                                          select model;

            if (acceptanceProtocolQuery.Count() > 0)
                count++;           

            db.project_manuals.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count++;
                        return;
                    }
                });

            return count;
        }

        public int getFunctionalSpecificationPercent(int projectId)
        {
            int count = 0;
            db.project_functional_requirements.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count = 1;
                        return;
                    }
                });

            db.project_implementations.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count++;
                        return;
                    }
                });

            return count;
        }

        public int getPreliminaryStudyPercent(int projectId)
        {
            int count = 0;
            db.project_risks.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count = 1;
                        return;
                    }
                });

            db.project_descriptions.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count++;
                        return;
                    }
                });

            db.project_effort_estimations.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count++;
                        return;
                    }
                });

            return count;
        }

        public IQueryable<MemberModel> GetProjectMembers()
        {
            throw new NotImplementedException();
        }

        public String GetProjectName(int id)
        {
            var query = from p in db.Projects
                        where p.Id == id
                        select new
                        {
                            name = p.name
                        };
            return query.AsQueryable().First().name;
        }

        public int getRequirementSpecificationPercent(int projectId)
        {
            int count = 0;

            db.project_introductions.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count++;
                        return;
                    }
                });

            db.project_results.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count++;
                        return;
                    }
                });

            db.project_uses.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count++;
                        return;
                    }
                });

            db.project_actual_states.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count++;
                        return;
                    }
                });

            db.project_target_states.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count++;
                        return;
                    }
                });

            db.project_data.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count++;
                        return;
                    }
                });

            db.project_qualities.AsParallel().ForAll(
                p =>
                {
                    if (p.Project_FK == projectId)
                    {
                        count++;
                        return;
                    }
                });

            var niceToHaveQuery = from model in db.project_nice_to_haves
                                   where model.Project_FK == projectId
                                   select model;

            if (niceToHaveQuery.Count() > 0)
                count++;

            var needToHaveQuery = from model in db.project_need_to_haves
                                  where model.Project_FK == projectId
                                  select model;

            if (needToHaveQuery.Count() > 0)
                count++;

            var nonFunctionalQuery = from model in db.project_non_functional_requirements
                                  where model.Project_FK == projectId
                                  select model;

            if (nonFunctionalQuery.Count() > 0)
                count++;

            return count;
        }

        public IQueryable GetUserProjects()
        {
            IdentityUser user = _userManager.FindByName(ClaimsPrincipal.Current.Claims.ToList().First().Value);
            var query = (from m in db.User_Project_Rel
                         join p in db.Projects on m.Project_FK equals p.Id
                         where m.User_FK == user.Id
                         select new
                         {
                             id = p.Id,
                             name = p.name,
                             description = p.description,
                             acronym = p.acronym
                         }).AsQueryable();
            return query;

        }

        public void RemoveProjectMember(string email)
        {
            throw new NotImplementedException();
        }
    }
}