using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EC_C2C_BanQuanAo.Areas.NguoiBan.Models;
using EC_C2C_BanQuanAo.Models.Principal;
using EC_C2C_BanQuanAo.Filter;
using System.Data.Entity;

namespace EC_C2C_BanQuanAo.Areas.NguoiBan.Controllers
{
    [BasicAuthFilter]
    public class GoiTinController : Controller
    {
        EC_C2C_BanQuanAoDBContext db = new EC_C2C_BanQuanAoDBContext();
        // GET: NguoiBan/GoiTin

        //Mua gói tin
        #region
        public ActionResult MuaGoiTin()
        {
            ViewBag.MaGoi = new SelectList(db.GoiTins, "MaGoi", "TenGoi");

            MuaGoiTinViewModel mgtvm = new MuaGoiTinViewModel();
            mgtvm.SoTinDaMua = db.TaiKhoans.Find((User as DangNhapPrincipal).TaiKhoan.MaTK).TongTinDaMua ?? 0;
            mgtvm.SoTinConLai = db.TaiKhoans.Find((User as DangNhapPrincipal).TaiKhoan.MaTK).TongTinConLai ?? 0;
            mgtvm.KhuyenMai = (mgtvm.SoTinDaMua/500) * 10;
            if(mgtvm.SoTinDaMua == 0)
            {
                mgtvm.KhuyenMai = 20;
            }

            return View(mgtvm);
        }

        [HttpPost]
        public ActionResult MuaGoiTin(MuaGoiTinViewModel mgtvm)
        {
            if (ModelState.IsValid) //xác nhận thông tin
            {
                //tạo 1 hóa đơn bán tin
                HoaDon_BanTin hdbt = new HoaDon_BanTin();
                hdbt.MaGoi = mgtvm.MaGoi;
                hdbt.MaTK = (User as DangNhapPrincipal).TaiKhoan.MaTK;
                hdbt.Ngay = DateTime.Now;

                //cập nhật số lượng tin trong tài khoản
                TaiKhoan tk = db.TaiKhoans.Find((User as DangNhapPrincipal).TaiKhoan.MaTK);
                int tinkhuyenmai = (mgtvm.KhuyenMai * mgtvm.SoLuongTin ?? 0) / 100; // ?? 0 -- mặc định cho kiểu int?
                tk.TongTinDaMua += mgtvm.SoLuongTin;
                tk.TongTinConLai += tinkhuyenmai + mgtvm.SoLuongTin;
                tk.TongTinKM += tinkhuyenmai;

                //lưu vào csdl
                db.HoaDon_BanTin.Add(hdbt);
                db.Entry(tk).State = EntityState.Modified;

                ModelState.Clear();
                db.SaveChanges();

                ViewBag.MaGoi = new SelectList(db.GoiTins, "MaGoi", "TenGoi");
                ViewBag.ThongBaoThanhCong = 1;

                mgtvm.SoTinDaMua = tk.TongTinDaMua?? default(int);
                mgtvm.SoTinConLai = tk.TongTinConLai ?? default(int);
                mgtvm.KhuyenMai = (mgtvm.SoTinDaMua / 500) * 10;
                return View(mgtvm);
            }

            ViewBag.MaGoi = new SelectList(db.GoiTins, "MaGoi", "TenGoi");

            return View(mgtvm);
        }

        public JsonResult layGoiTin(int MaGoi)
        {
            var goitin = (from l in db.GoiTins
                             where l.MaGoi == MaGoi
                             select new { 
                                 l.SoLuongTin,
                                 l.Gia
                             }).SingleOrDefault();
            return Json(goitin, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //Thống kê hóa đơn bán tin
        #region
        public ActionResult ThongKeHoaDonBanTin()
        {
            ViewBag.MaGoi = new SelectList(db.GoiTins, "MaGoi", "TenGoi");

            MuaGoiTinViewModel mgtvm = new MuaGoiTinViewModel();
            mgtvm.SoTinDaMua = db.TaiKhoans.Find((User as DangNhapPrincipal).TaiKhoan.MaTK).TongTinDaMua ?? 0;
            mgtvm.SoTinConLai = db.TaiKhoans.Find((User as DangNhapPrincipal).TaiKhoan.MaTK).TongTinConLai ?? 0;
            mgtvm.KhuyenMai = (mgtvm.SoTinDaMua / 500) * 10;
            if (mgtvm.SoTinDaMua == 0)
            {
                mgtvm.KhuyenMai = 20;
            }

            return View(mgtvm);
        }

        #endregion
    }
}