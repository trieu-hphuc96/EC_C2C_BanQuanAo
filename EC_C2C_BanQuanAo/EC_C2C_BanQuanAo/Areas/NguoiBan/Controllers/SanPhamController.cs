using EC_C2C_BanQuanAo.Areas.NguoiBan.Models;
using EC_C2C_BanQuanAo.Filter;
using EC_C2C_BanQuanAo.Models;
using EC_C2C_BanQuanAo.Models.Principal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EC_C2C_BanQuanAo.Areas.NguoiBan.Controllers
{
    [BasicAuthFilter]
    public class SanPhamController : Controller
    {
        EC_C2C_BanQuanAoDBContext db = new EC_C2C_BanQuanAoDBContext();

        // GET: NguoiBan/SanPham
        public ActionResult DangTinSanPham()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangTinSanPham(DangTinSanPhamViewModel dtspvm)
        {
            if(ModelState.IsValid)
            {
                if(db.TaiKhoans.Find((User as DangNhapPrincipal).TaiKhoan.MaTK).TongTinConLai <= 0)
                {
                    ViewBag.HetTin = 1;
                    return View();
                }

                Tin tin = new Tin();
                tin.MaSKU = dtspvm.MaSKU;
                tin.MaTK = (User as DangNhapPrincipal).TaiKhoan.MaTK;
                tin.NgayDang = dtspvm.NgayDang;
                tin.NgayKetThuc = dtspvm.NgayKetThuc;
                tin.TenSP = dtspvm.TenSP;
                tin.Gia = dtspvm.Gia;

                db.Tins.Add(tin);
                db.SaveChanges();

                //cập nhật số lượng tin
                TaiKhoan tk = db.TaiKhoans.Find((User as DangNhapPrincipal).TaiKhoan.MaTK);
                tk.TongTinConLai -= 1;

                db.Entry(tk).State = EntityState.Modified;
                db.SaveChanges();

                //lưu hình ảnh vào csdl
                int maTin = db.Tins.Max(ma => ma.MaTin);
                int i = 1;
                foreach(HttpPostedFileBase hinhanh in dtspvm.HinhAnhs)
                {
                    if (hinhanh != null)
                    {
                        string tenfile = Path.GetFileNameWithoutExtension(hinhanh.FileName);
                        string extension = Path.GetExtension(hinhanh.FileName);
                        tenfile = tenfile + DateTime.Now.ToString("yymmssfff") + extension;

                        HinhAnhSanPham hasp = new HinhAnhSanPham();
                        hasp.MaTin = maTin;
                        hasp.MaHinh = i;
                        i++;
                        hasp.DuongDan = "~/HinhAnhSanPham/" + tenfile;

                        tenfile = Path.Combine(Server.MapPath("~/HinhAnhSanPham/"), tenfile);
                        hinhanh.SaveAs(tenfile);


                        db.HinhAnhSanPhams.Add(hasp);
                        db.SaveChanges();
                    }
                }

                ModelState.Clear();

                ViewBag.DangTinThanhCong = 1;
                return View();
            }
            return View();
        }
    }
}