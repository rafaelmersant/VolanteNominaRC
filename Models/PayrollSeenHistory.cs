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
    
    public partial class PayrollSeenHistory
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string PayrollCycle { get; set; }
        public System.DateTime SeenTime { get; set; }
        public string SeenBy { get; set; }
        public string Machine { get; set; }
    }
}
