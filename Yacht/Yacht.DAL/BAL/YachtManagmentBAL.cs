using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Yacht.DAL.DbContexts;

namespace Yacht.DAL.BAL
{
    public class YachtManagmentBAL
    {
        YatchDb _db = new YatchDb();

        public int YachtStoreInDb(Tbl_Yacht_Details _Yacht_Details)
        {

            if (_Yacht_Details.Id == 0)
            {
                _db.Tbl_Yacht_Details.Add(_Yacht_Details);
                _db.SaveChanges();
            }
            else
            {
                var yacht = _db.Tbl_Yacht_Details.Where(x => x.Id == _Yacht_Details.Id).FirstOrDefault();
                yacht.Name = _Yacht_Details.Name;
                if (_Yacht_Details.Cover_Img_Url != null && _Yacht_Details.Cover_Img_Url != "")
                {
                    yacht.Cover_Img_Url = _Yacht_Details.Cover_Img_Url;
                }
                yacht.Price = _Yacht_Details.Price;
                yacht.Update_date = DateTime.Now;
                yacht.Isavaible = _Yacht_Details.Isavaible;
                yacht.IsActive = _Yacht_Details.IsActive;
                yacht.Description = _Yacht_Details.Description;
                _db.SaveChanges();
            }

            return Convert.ToInt32(_Yacht_Details.Id);

        }


        public bool YachtImageSave(Tbl_Yachts_Images _Yachts_Images)
        {
            bool IsInserted = false;


            if (_Yachts_Images != null)
            {
                _db.Tbl_Yachts_Images.Add(_Yachts_Images);
                _db.SaveChanges();
                IsInserted = true;
            }

            return IsInserted;
        }

        public List<Tbl_Yacht_Details> GetYachtList()
        {
            List<Tbl_Yacht_Details> _List = new List<Tbl_Yacht_Details>();
            _List = _db.Tbl_Yacht_Details.OrderByDescending(x => x.Create_date).ToList();
            return _List;
        }

        public Tbl_Yacht_Details GetYachtDetail(int Id)
        {
            return _db.Tbl_Yacht_Details.Where(x => x.Id == Id).FirstOrDefault();
        }

        public int YachtBookingStoreInDb(Tbl_BookingHistory _Yacht_Details, string FilePath)
        {
            TimeSpan days = ((TimeSpan)(_Yacht_Details.Booking_To - _Yacht_Details.Booking_From));
            var i = days.TotalDays;
            _Yacht_Details.Price = _Yacht_Details.Price * Convert.ToDecimal(i);
            _db.Tbl_BookingHistory.Add(_Yacht_Details);
            _db.SaveChanges();
            string Subject = "Krisnal Yacht Booking";
            SendMail(_Yacht_Details, FilePath, Subject, "Booking");
         
            return Convert.ToInt32(_Yacht_Details.numID);
        }

        public string CheckYachtBooking(int id, string fromdate, string Todate)
        {
            var F = Convert.ToDateTime(fromdate);
            var T = Convert.ToDateTime(Todate);

            var result = _db.Tbl_BookingHistory.Where(x => x.YatchID == id && x.Booking_To <= F && x.OrderStatus == 1).FirstOrDefault();
            if (result != null)
            {
                return "This Date already book Yacht. Please Select another date.";
            }
            else
            {
                return "1";
            }
        }

        public List<Tbl_BookingHistory> GetBookingHistoryList()
        {
            List<Tbl_BookingHistory> _List = new List<Tbl_BookingHistory>();
            _List = _db.Tbl_BookingHistory.Where(x => x.CreatedBy != "A").OrderByDescending(x => x.CreatedDate).ToList();
            return _List;
        }

        public string EmailBody(string FilePath, Tbl_BookingHistory _Yacht_Details, string Subject, string Process)
        {
            string Body = System.IO.File.ReadAllText(FilePath);
            Body = Body.Replace("{YachtName}", _Yacht_Details.YatchName);
            Body = Body.Replace("{FromDate}", _Yacht_Details.Booking_From?.ToString("dd-MM-yyyy"));
            Body = Body.Replace("{Remarks}", _Yacht_Details.Remarks1);
            Body = Body.Replace("{Price}", _Yacht_Details.Price.ToString());
            Body = Body.Replace("{ToDate}", _Yacht_Details.Booking_To?.ToString("dd-MM-yyyy"));
            Body = Body.Replace("{Title}", Subject);
            if(Process=="Booking")
            Body = Body.Replace("{Paymentstatus}", _Yacht_Details.OrderStatus == 1 ? "Done" : "Fail");
            else
            Body = Body.Replace("{Paymentstatus}", "Cancel");

            Body = Body.Replace("{YachtId}", _Yacht_Details.numID.ToString());

            return Body;
        }

        public void SendMail(Tbl_BookingHistory _Yacht_Details, string FilePath, string Subject,string Process)
        {
            List<string> MsgList = new List<string>();
            MsgList.Add(_Yacht_Details.EmailID);
            MsgList.Add("krisnalboat@gmail.com");
            foreach (var i in MsgList)
            {
            string from = "krisnal@krisnalboatsandyachtsrental.com";
            MailMessage message = new MailMessage(from,i);
            message.Subject = Subject;
            message.Body = EmailBody(FilePath, _Yacht_Details, Subject, Process);
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("krisnalboatsandyachtsrental.com");
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("krisnal@krisnalboatsandyachtsrental.com", "Oddd88~6");
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
                try
                {
                    client.Send(message);
                }

                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }
    }
}
