using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Yacht.DAL.DbContexts;

namespace Yacht.Models
{
    public partial class YachtDetailsViewModel
    {

        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Nullable<decimal> Price { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public HttpPostedFileBase postedFile { get; set; }
        public HttpPostedFileBase[] UploadYatchImagesViews { get; set; }
        public string Cover_Img_Url { get; set; }
        public Nullable<bool> Isavaible { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> Create_date { get; set; }
        public string Create_by { get; set; }
        public Nullable<System.DateTime> Update_date { get; set; }
        public Nullable<System.DateTime> Delete_date { get; set; }

        public List<Tbl_Yacht_Details> _Yacht_DetailsList { get; set; }
        public List<Tbl_Yachts_Images> _YachtImagesList { get; set; }
    }
}