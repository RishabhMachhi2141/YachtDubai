﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yacht.DAL.BAL;
using Yacht.DAL.DbContexts;
using Yacht.Models;

namespace Yacht.Controllers
{
   
    public class HomeController : Controller
    {
        YachtManagmentBAL _Bal = new YachtManagmentBAL();
        public ActionResult Index()
        {
           var id= User.Identity.GetUserId();
            YatchDb _db = new YatchDb();
            var AspNetUsers=_db.AspNetUsers.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult YachtManagment(string id) {

            YachtDetailsViewModel _YachtDetails = new YachtDetailsViewModel();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var detail = _Bal.GetYachtDetail(Convert.ToInt32(id));
                _YachtDetails.Id = detail.Id;
                _YachtDetails.Price = detail.Price;
                _YachtDetails.Name = detail.Name;
                _YachtDetails.IsActive = detail.IsActive ?? false;
                _YachtDetails.Cover_Img_Url = detail.Cover_Img_Url;
                _YachtDetails._YachtImagesList = detail.Tbl_Yachts_Images.ToList();
            }
            
            _YachtDetails._Yacht_DetailsList= _Bal.GetYachtList();

            return View(_YachtDetails);
        }
        [HttpPost]
        public ActionResult YachtManagment(YachtDetailsViewModel _YachtDetails)
        {
            try
            {
                int id = 0;
                string fileName = "";
                string path = "";
                if (_YachtDetails.postedFile != null)
                {

                    fileName = Path.GetFileName(_YachtDetails.postedFile.FileName);
                    path = Server.MapPath("~/Content/Cover_Images/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    _YachtDetails.postedFile.SaveAs(path + fileName);
                }

                
                    Tbl_Yacht_Details _Yacht_Details = new Tbl_Yacht_Details();
                    _Yacht_Details.Id = _YachtDetails.Id;
                    _Yacht_Details.Name = _YachtDetails.Name;
                    _Yacht_Details.Price = _YachtDetails.Price;
                    _Yacht_Details.IsActive = _YachtDetails.IsActive;
                    if(fileName!="" && fileName !=null)
                    _Yacht_Details.Cover_Img_Url = "/Content/Cover_Images/" + fileName;
                    else
                    _Yacht_Details.Cover_Img_Url = "";

                    _Yacht_Details.Create_by = User.Identity.GetUserId();
                    _Yacht_Details.Create_date = DateTime.Now;
                    _Yacht_Details.Update_date = DateTime.Now;
                    id = _Bal.YachtStoreInDb(_Yacht_Details);
                
                if (_YachtDetails.UploadYatchImagesViews.Count() > 0 && _YachtDetails.UploadYatchImagesViews[0] != null)
                {
                    
                    var IsDeleteImage=_Bal.YachtImageDelete(id);
                    for (int i = 0; i <= _YachtDetails.UploadYatchImagesViews.Count(); i++)
                    {
                        if (_YachtDetails.UploadYatchImagesViews[i] != null)
                        {
                             fileName = Path.GetFileName(_YachtDetails.UploadYatchImagesViews[i].FileName);
                             path = Server.MapPath("~/Content/YachtImages/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            _YachtDetails.UploadYatchImagesViews[i].SaveAs(path + fileName);
                            Tbl_Yachts_Images _Yachts_Images = new Tbl_Yachts_Images();
                            _Yachts_Images.Created_date=DateTime.Now;
                            
                            if (fileName != "" && fileName != null)
                                _Yachts_Images.Image_Url= "/Content/YachtImages/" + fileName;
                            
                            _Yachts_Images.IsActive= _YachtDetails.IsActive;
                            _Yachts_Images.Yacht_Id= id;
                            var IsIsertedImage = _Bal.YachtImageSave(_Yachts_Images);
                        }
                    }


                }
            }
            catch (Exception e)
            { 
            
            }

            return RedirectToAction("YachtManagment","Home",new { id=""});
        }



        #region===========Booking================
        [Authorize]
        public ActionResult YachtBooking(string id)
        {

            YachtBookingDetailsViewModel _YachtDetails = new YachtBookingDetailsViewModel();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var detail = _Bal.GetYachtDetail(Convert.ToInt32(id));
                _YachtDetails.Id = detail.Id;
                _YachtDetails.Price = detail.Price;
                _YachtDetails.YatchName = detail.Name;
                
                _YachtDetails.Cover_Img_Url = detail.Cover_Img_Url;
                //_YachtDetails._YachtImagesList = detail.Tbl_Yachts_Images.ToList();
            }

            //_YachtDetails._Yacht_DetailsList = _Bal.GetYachtList();

            return View(_YachtDetails);
        }


        [HttpPost]
        public ActionResult YachtBooking(YachtBookingDetailsViewModel _YachtDetails)
        {
            try
            {
                int id = 0;


                Random generator = new Random();
                int r = generator.Next(1, 1000000);
                string s = r.ToString().PadLeft(6, '0');


                Tbl_BookingHistory _Yacht_Details = new Tbl_BookingHistory();
                _Yacht_Details.OrderCode = s;
                _Yacht_Details.CustomerName = _YachtDetails.CustomerName;
                _Yacht_Details.ContactNo = _YachtDetails.ContactNo;
                _Yacht_Details.EmailID = _YachtDetails.EmailID;
                _Yacht_Details.Address = _YachtDetails.Address;
                _Yacht_Details.YatchID = _YachtDetails.Id;
                _Yacht_Details.YatchName = _YachtDetails.YatchName;
                _Yacht_Details.YatchDescription = _YachtDetails.YatchDescription;
                _Yacht_Details.Remarks1 = _YachtDetails.Remarks1;
                _Yacht_Details.Price = _YachtDetails.Price;
                _Yacht_Details.OrderStatus = 1;
                _Yacht_Details.Address = _YachtDetails.Address;
                _Yacht_Details.OrderDate = DateTime.Now;
                _Yacht_Details.CreatedBy = User.Identity.GetUserId();
                _Yacht_Details.CreatedDate = DateTime.Now;
                //_Yacht_Details.OrderCode = DateTime.Now;
                id = _Bal.YachtBookingStoreInDb(_Yacht_Details);

              
            }
            catch (Exception e)
            {

            }

            return RedirectToAction("YachtManagment", "Home", new { id = "" });
        }

        [Authorize]
        public ActionResult BookingHistory()
        {

            YachtBookingDetailsViewModel _YachtDetails = new YachtBookingDetailsViewModel();
            

            _YachtDetails._Yacht_BookingDetailsList = _Bal.GetBookingHistoryList();

            return View(_YachtDetails);
        }


        #endregion



    }
}