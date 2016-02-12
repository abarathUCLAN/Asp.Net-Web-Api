using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.helpers;
using Web_Api___Pdmsys.Models.Interfaces;

namespace Web_Api___Pdmsys.Models.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {

        private pdmsysEntities db = new pdmsysEntities();
        private AuthRepository _repo = new AuthRepository();

        public bool checkForAvailableEmail(String email)
        {
            var query = from inv in db.invitations
                        where inv.email == email
                        select new
                        {
                            email = inv.email
                        };
            if (query.Count() > 0)
                return true;
            return false;
        }

        public bool checkIfUrlCodeIsValid(string urlcode)
        {
            var query = from model in db.invitations
                        where model.urlcode == urlcode
                        select model;

            if (query.Count() == 1)
                return true;
            return false;
        }

        public IQueryable GetInvitations(int projectId)
        {
            var query = from inv in db.invitations
                        where inv.Project_FK == projectId
                        select new 
                        {
                            Id = inv.Id,
                            type = inv.type,
                            firstname = inv.firstname,
                            lastname = inv.lastname,
                            email = inv.email                            
                        };
            return query.AsQueryable();
        }

        public async Task<IdentityUser> registerUserWithUrlCode(UrlCodeModel code)
        {
            invitations invitation = (from model in db.invitations
                             where model.urlcode == code.urlcode
                             select model).First<invitations>();

            if (invitation == null)
                return null;

            RegisterModel user = new RegisterModel();

            user.Firstname = invitation.firstname;
            user.Lastname = invitation.lastname;
            user.Password = code.password;
            user.email = invitation.email;

            IdentityUser result = await _repo.RegisterUser(user);

            if (result.Id == null)
                return null;

            UserInfos infos = new UserInfos();
            infos.firstname = invitation.firstname;
            infos.lastname = invitation.lastname;
            infos.User_FK = result.Id;

            User_Project_Rel rel = new User_Project_Rel();

            rel.type = invitation.type;
            rel.User_FK = result.Id;
            rel.Project_FK = invitation.Project_FK;

            db.User_Project_Rel.Add(rel);
            db.UserInfos.Add(infos);
            db.invitations.Remove(invitation);

            await db.SaveChangesAsync();

            return result;
        }

        public void RemoveInvitationByEmail(string email)
        {
            var deleteInv = from inv in db.invitations
                            where inv.email == email
                            select inv;

            db.invitations.Remove(deleteInv.FirstOrDefault<invitations>());
            db.SaveChanges();
        }
    }
}