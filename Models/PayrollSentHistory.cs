//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VolanteNominaRC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PayrollSentHistory
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }
        public string PayrollCycle { get; set; }
        public string PayrollType { get; set; }
        public System.DateTime Sent { get; set; }
        public string SentBy { get; set; }
    }
}
