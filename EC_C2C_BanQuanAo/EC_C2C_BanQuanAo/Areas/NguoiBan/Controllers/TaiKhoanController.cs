using EC_C2C_BanQuanAo.Areas.NguoiBan.Models;
using EC_C2C_BanQuanAo.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EC_C2C_BanQuanAo.Areas.NguoiBan.Controllers
{
    public class TaiKhoanController : Controller
    {
        EC_C2C_BanQuanAoDBContext db = new EC_C2C_BanQuanAoDBContext();

        [BasicAuthFilter]
        public ActionResult TrangChu()
        {
            return View("TrangChu");
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(DangNhapViewModels dnvm)
        {
            if (ModelState.IsValid)
            {
                //Xác thực người dùng
                //tkvm.TaiKhoan.Roles = new string[] { "admin" };
                if (db.TaiKhoans.Where(ten => ten.TenNguoiDung == dnvm.TenNguoiDung).FirstOrDefault() != null)
                {
                    var password = EncyptPasswordHelper.EncodePassword(dnvm.MatKhau, db.TaiKhoans.Where(ten => ten.TenNguoiDung == dnvm.TenNguoiDung).FirstOrDefault().VCode);
                    if (password == db.TaiKhoans.Where(ten => ten.TenNguoiDung == dnvm.TenNguoiDung).FirstOrDefault().MatKhau)
                    {
                        //gán mã tk cho viewmodel
                        TaiKhoan tk = new TaiKhoan();
                        tk = db.TaiKhoans.Where(ma => ma.TenNguoiDung == dnvm.TenNguoiDung).FirstOrDefault();
                        dnvm.MaTK = tk.MaTK;

                        //chặn lỗi của hàm formauthenticationticket -- , new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }
                        //Tạo ủy quyền và cookie
                        FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1, dnvm.TenNguoiDung, DateTime.Now, DateTime.Now.AddMinutes(30), false, JsonConvert.SerializeObject(dnvm));

                        HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat));
                        ck.Expires = DateTime.Now.AddMinutes(30);
                        Response.Cookies.Add(ck);

                        return RedirectToAction("TrangChu", "TaiKhoan");

                    }
                }

            }

            ViewBag.Loi = "Tài khoản không hợp lệ! Vui lòng nhập lại!";
            return View();
        }

        public ActionResult DangXuat()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap", "TaiKhoan");
        }

        public ActionResult DangKy()
        {
            return View("DangKy");
        }

        [HttpPost]
        public ActionResult DangKy(DangKyViewModels dkvm)
        {
            if (ModelState.IsValid) //xác nhận thông tin
            {
                try
                {
                    var ktTenNguoiDung = db.TaiKhoans.Where(m => m.TenNguoiDung == dkvm.TenNguoiDung).FirstOrDefault();
                    if (ktTenNguoiDung == null)
                    {
                        //mã hóa mật khẩu
                        var keyNew = EncyptPasswordHelper.GeneratePassword(10);
                        var password = EncyptPasswordHelper.EncodePassword(dkvm.MatKhau, keyNew);

                        //gán thông tin vào 1 tài khoản
                        TaiKhoan tk = new TaiKhoan();
                        tk.LoaiTK = 2;
                        tk.TenDayDu = dkvm.TenDayDu;
                        tk.TenNguoiDung = dkvm.TenNguoiDung;
                        tk.MatKhau = password;
                        tk.Email = dkvm.Email;
                        tk.NgayDangKy = DateTime.Now;
                        tk.NgayDanhGia = DateTime.Now.AddDays(60);
                        tk.TongTinDaMua = 0;
                        tk.TongTinConLai = 0;
                        tk.VCode = keyNew;

                        //lưu vào csdl
                        db.TaiKhoans.Add(tk);
                        db.SaveChanges();
                        ModelState.Clear();

                        //var code = EncyptPasswordHelper.GeneratePassword(10);
                        Email.GuiEmail_XacNhanTaiKhoan(tk, Url.Action("XacNhanEmail", "TaiKhoan", new { MaTK = tk.MaTK, code = EncyptPasswordHelper.GeneratePassword(10) }, protocol: Request.Url.Scheme));


                        return RedirectToAction("TrangChu");
                    }

                    ViewBag.Loi = "Tên đăng nhập đã tồn tại!";
                    return View("DangKy");
                }
                catch (Exception e)
                {
                    ViewBag.Loi = "Xảy ra lỗi ngoài ý muốn! <br/>" + e;
                    return View("DangKy");
                }
            }
            return View("DangKy");
        }

        public ActionResult XacNhanEmail(int MaTK, string code)
        {
            if (ModelState.IsValid) //xác nhận thông tin
            {
                TaiKhoan tk = db.TaiKhoans.Find(MaTK);
                tk.XacNhanEmail = 1;

                db.Entry(tk).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.XacNhanEmail = 1;
                return View("TrangChu");
            }

            return View("DangNhap");
        }
    }
}