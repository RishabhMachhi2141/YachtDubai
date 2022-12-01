using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Yacht.DAL.DbContexts;

namespace Yacht.Models
{
    public partial class YachtBookingDetailsViewModel
    {

        public long Id { get; set; }
       
        
        public string OrderCode { get; set; }
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string ContactNo { get; set; }

        public string EmailID { get; set; }

        public string Address { get; set; }
        [Required]
        public long YatchID { get; set; }

        [Required]
        public string YatchName { get; set; }
        public string YatchDescription { get; set; }

        [Required]
        public Nullable<decimal> Price { get; set; }

      
        public Nullable<System.DateTime> OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public string Remarks1 { get; set; }
      
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string Cover_Img_Url { get; set; }
        public List<Tbl_BookingHistory> _Yacht_BookingDetailsList { get; set; }

        //public List<Tbl_Yachts_Images> _YachtImagesList { get; set; }
    }
}