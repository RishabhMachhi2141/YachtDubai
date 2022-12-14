using System;
using System.Collections.Generic;
using System.Linq;
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




        public int YachtBookingStoreInDb(Tbl_BookingHistory _Yacht_Details)
        {

           
                _db.Tbl_BookingHistory.Add(_Yacht_Details);
                _db.SaveChanges();
           
         

            return Convert.ToInt32(_Yacht_Details.numID);

        }

        public List<Tbl_BookingHistory> GetBookingHistoryList()
        {
            List<Tbl_BookingHistory> _List = new List<Tbl_BookingHistory>();
            _List = _db.Tbl_BookingHistory.Where(x => x.CreatedBy != "A").OrderByDescending(x => x.CreatedDate).ToList();
            return _List;
        }
    }
}
