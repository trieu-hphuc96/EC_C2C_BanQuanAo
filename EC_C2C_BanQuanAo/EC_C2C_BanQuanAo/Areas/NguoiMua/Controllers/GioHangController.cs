using EC_C2C_BanQuanAo.Areas.NguoiMua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PayPal.Api;
using EC_C2C_BanQuanAo.Models;
using System.Windows.Forms;

namespace EC_C2C_BanQuanAo.Areas.NguoiMua.Controllers
{
    public class GioHangController : Controller
    {
        EC_C2C_BanQuanAoDBContext db = new EC_C2C_BanQuanAoDBContext();

        // GET: NguoiMua/GioHang
        public ActionResult TrangChu()
        {
            return View();
        }

        public ActionResult TinhTien()
        {
            var giohang = db.GioHangs.Include(ma => ma.Tin).Where(m => m.MaTK_Mua == 7);

            var ttvms = new List<TinhTienViewModel>();

            foreach(var item in giohang)
            {
                TinhTienViewModel ttvm = new TinhTienViewModel();
                ttvm.GioHang = item;

                var hinhanh = db.HinhAnhSanPhams.Where(m => m.MaTin == item.MaTin).FirstOrDefault();

                ttvm.HinhAnh = hinhanh;

                ttvms.Add(ttvm);
            }

            Session["GioHang"] = ttvms;

            return View(ttvms);
        }

        // Work with Paypal Payment
        private Payment payment;

        // Create a payment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var giohang = Session["GioHang"] as List<TinhTienViewModel>;

            double sub_total = 0;

            double total = 0;

            var listItems = new ItemList()
            {
                items = new List<Item>()
            };

            foreach(var item in giohang)
            {
                listItems.items.Add(new Item()
                {
                    name = item.GioHang.Tin.TenSP,
                    currency = "USD",
                    price = item.GioHang.Gia.ToString(),
                    quantity = item.GioHang.SoLuong.ToString(),
                    sku = item.GioHang.Tin.MaSKU
                });

                sub_total += item.GioHang.Gia * item.GioHang.SoLuong ?? 0;
            }

            

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
                subtotal = sub_total.ToString()
            };

            //Create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (sub_total + 1 + 2).ToString(),
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

            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
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
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/NguoiMua/GioHang/PaymentWithPaypal?";
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
                        Session["CheckOut"] = 0;
                        return RedirectToAction("TrangChu","TaiKhoan_NguoiMua");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                PaypalLogger.Log("Error:" + ex.Message);

                Session["CheckOut"] = 0;
                return RedirectToAction("TrangChu", "TaiKhoan_NguoiMua");
            }

            Session["CheckOut"] = 1;
            return RedirectToAction("TrangChu", "TaiKhoan_NguoiMua");
        }


    }
}