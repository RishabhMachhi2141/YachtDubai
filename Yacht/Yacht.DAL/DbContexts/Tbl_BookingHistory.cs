//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yacht.DAL.DbContexts
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_BookingHistory
    {
        public long numID { get; set; }
        public string OrderCode { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public Nullable<long> YatchID { get; set; }
        public string YatchName { get; set; }
        public string YatchDescription { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> OrderStatus { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
