using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Api___Pdmsys.Models.Interfaces
{
    interface IMiscellaneousRepository
    {
        IQueryable GetProjectpresentations(int projectId);
        IQueryable GetProjectchangeRequests(int projectId);
        IQueryable GetProjectreports(int projectId);
        IQueryable GetProjectstyleguides(int projectId);
    }
}
