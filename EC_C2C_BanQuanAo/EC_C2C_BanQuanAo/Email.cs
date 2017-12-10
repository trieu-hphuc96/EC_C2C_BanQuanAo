using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace EC_C2C_BanQuanAo
{
    public class Email
    {
        public static void GuiEmail_XacNhanTaiKhoan(TaiKhoan tk,string url)
        {
            //tạo 1 email
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(tk.Email));  // replace with valid value 
            mail.From = new MailAddress("test.send.mail1996@gmail.com");  // replace with valid value
            mail.Subject = "Bán Quần Áo - Xác nhận email!";
            mail.Body = string.Format("Xin chào {0}!"
            + "<BR/>Cảm ơn bạn đã đăng ký dịch vụ của chúng tôi, vui lòng nhấn vào link bên dưới để xác nhận email và hoàn thành việc đăng ký:"
            + "<BR/> <a href=\"{1}\" title=\"Xác Nhận Email\">{1}</a>",
            tk.TenDayDu, url);
            mail.IsBodyHtml = true;

            //gửi email
            using (var smtp = new SmtpClient())
            {
                smtp.Credentials = new NetworkCredential("test.send.mail1996@gmail.com", "123456789phuc");
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
        }
    }
}