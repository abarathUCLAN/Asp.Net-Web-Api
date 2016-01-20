﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.Interfaces;

namespace Web_Api___Pdmsys.Models.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {

        private pdmsysEntities db = new pdmsysEntities();

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

     /**   public IQueryable GetInvitations(int projectId)
        {
            var query = from inv in db.invitations
                        where inv.Project_FK == projectId
                        select inv;
            return query.AsQueryable();
        }**/
    }
}