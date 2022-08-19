using Autofac;
using ERP.Module.Commons;
using ERP.Module.HeThong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.XuLyNghiepVu;

namespace ERP.Module.Registers
{
    public static partial class Register
    {

        static IContainer Container { get; set; }

        public static void RegisterAll()
        {
            //Lấy Trường hiện tại
            SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser();
            if (user == null) return;
            //
            CongTy congTyHienTai = user.CongTy;
            if (congTyHienTai == null) return;

            //Đăng ký theo Trường
            Register.Register_TTC();
        }

        public static IQuyetDinhThoiViec Func_QuyetDinhThoiViec
        {
            get
            {
                return Container.Resolve<IQuyetDinhThoiViec>();
            }
        }

        public static IQuyetDinhTuyenDung Func_QuyetDinhTuyenDung
        {
            get
            {
                return Container.Resolve<IQuyetDinhTuyenDung>();
            }
        }
    }
}
