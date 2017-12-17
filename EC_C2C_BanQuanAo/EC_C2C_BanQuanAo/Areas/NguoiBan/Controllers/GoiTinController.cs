using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EC_C2C_BanQuanAo.Areas.NguoiBan.Models;
using EC_C2C_BanQuanAo.Models.Principal;
using EC_C2C_BanQuanAo.Filter;
using System.Data.Entity;
using PayPal.Api;
using EC_C2C_BanQuanAo.Models;
using System.Windows.Forms;

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
                Session["MuaGoiTinVM"] = mgtvm; 

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

        // Work with Paypal Payment
        private Payment payment;

        // Create a payment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listItems = new ItemList()
            {
                items = new List<Item>()
            };

            listItems.items.Add(new Item()
            {
                name = "Goi 1",
                currency = "USD",
                price = "3.00",
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            //Do the configuation RedirectURLs here with redirectURLs object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            //Create details object
            var detail = new Details()
            {
                tax = "1.00",
                shipping = "2.00",
                subtotal = "3.00"
            };

            //Create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = "6.00",
                details = detail
            };

            //Create transaction
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Phuc testing transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return payment.Create(apiContext);
        }

        // Create Execute Payment method
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            
            payment = new Payment() {id = paymentId};
            return payment.Execute(apiContext,paymentExecution);
        }

        // Create PaymentWithPaypal method
        public ActionResult PaymentWithPaypal()
        {
            // Gettings context from the paypal bases on clientId and clientsecret for payment
            APIContext apiContext = PaypalConfiguration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //Creating a payment
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/NguoiBan/GoiTin/PaymentWithPaypal?";
                    MessageBox.Show("tạo base URI");
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                    MessageBox.Show("tạo links");
                    //Get link returned from paypal respone to create call function
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    //This one will be executed when we have received all the payment params from previous call
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executePayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                PaypalLogger.Log("Error:" + ex.Message);
                return View("Failure");
            }

            return View("Success");
        }
    }
}