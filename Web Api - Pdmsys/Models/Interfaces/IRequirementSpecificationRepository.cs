using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Api___Pdmsys.Models.data;

namespace Web_Api___Pdmsys.Models.Interfaces
{
    public interface IRequirementSpecificationRepository
    {
        project_introductions GetProjectIntroduction(int projectId);
        bool UpdateProjectIntroduction(project_introductions model, int projectId);
        bool UpdateProjectResult(project_results model, int projectId);
        project_results GetProjectResult(int projectId);
        bool UpdateProjectUse(project_uses model, int projectId);
        project_uses GetProjectUse(int projectId);
        project_data GetProductData(int projectId);
        bool UpdateProductData(project_data model, int projectId);
        project_qualities GetProjectQuality(int projectId);
        bool UpdateProjectQuality(project_qualities model, int projectId);
        IQueryable GetProjectneedToHaves(int projectId);
        IQueryable GetProjectniceToHaves(int projectId);
        project_actual_states GetactualState(int projectId);
        bool UpdateactualState(project_actual_states model, int projectId);
        project_target_states GettargetState(int projectId);
        bool UpdatetargetState(project_target_states model, int projectId);
        IQueryable GetProjectnonFunctionalRequirements(int projectId);
    }
}