using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Api___Pdmsys.Models.Interfaces
{
    interface IInvitationRepository
    {
        bool checkForAvailableEmail(String email);
        IQueryable GetInvitations(int projectId);
        void RemoveInvitationByEmail(string email);
    }
}
