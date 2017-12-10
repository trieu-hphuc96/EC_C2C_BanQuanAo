using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Security;
namespace EC_C2C_BanQuanAo.Models.Principal
{
    public class DangNhapPrincipal : IPrincipal
    {
        public TaiKhoan TaiKhoan { get; set; }

        public DangNhapPrincipal(TaiKhoan tk)
        {
            this.TaiKhoan = tk;
            this.Identity = new GenericIdentity(TaiKhoan.TenNguoiDung);
        }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            return TaiKhoan.Roles.Any(r => r.Equals(role));
        }
    }
}