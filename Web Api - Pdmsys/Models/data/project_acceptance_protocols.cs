//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web_Api___Pdmsys.Models.data
{
    using System;
    using System.Collections.Generic;
    
    public partial class project_acceptance_protocols
    {
        public int Id { get; set; }
        public string criteria { get; set; }
        public int Project_FK { get; set; }
        public string criteriaName { get; set; }
        public string fulfilled { get; set; }
        public string note { get; set; }
        public string requirement { get; set; }
    
        public virtual Projects Projects { get; set; }
    }
}