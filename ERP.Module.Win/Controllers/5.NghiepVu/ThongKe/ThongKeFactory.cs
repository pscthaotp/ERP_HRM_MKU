
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe
{
    public class ThongKeFactory
    {
        private ThongKeFactory()
        { }
        public static LoaiThongKeEnum Type { get; set; }

        public static ThongKeBaseController CreateControl(XafApplication app, IObjectSpace obs)
        {
            switch (Type)
            {
                case LoaiThongKeEnum.ThongKeChucVu:
                    return new ThongKeChucVuController(app);
                case LoaiThongKeEnum.ThongKeDoTuoi:
                    return new ThongKeDoTuoiController(app);
                case LoaiThongKeEnum.ThongKeGioiTinh:
                    return new ThongKeGioiTinhController(app);
                case LoaiThongKeEnum.ThongKeThamNien:
                    return new ThongKeThamNienController(app);
                case LoaiThongKeEnum.ThongKeLoaiNhanSu:
                    return new ThongKeLoaiNhanSuController(app);
                case LoaiThongKeEnum.ThongKeSoLuongNhanSu:
                    return new ThongKeSoLuongNhanSuController(app);
                case LoaiThongKeEnum.ThongKeLoaiHopDong:
                    return new ThongKeLoaiHopDongController(app);
                case LoaiThongKeEnum.ThongKeNgachLuong:
                    return new ThongKeNgachLuongController(app);
                case LoaiThongKeEnum.ThongKeTrinhDoChuyenMon:
                    return new ThongKeTrinhDoChuyenMonController(app);
                case LoaiThongKeEnum.ThongKeTyLeThoiViec:
                    return new ThongKeTyLeThoiViecController(app);
                case LoaiThongKeEnum.ThongKeBienDongSoLuongNhanSu:
                    return new ThongKeBienDongSoLuongNhanSuController(app);
                case LoaiThongKeEnum.ThongKeTongHopBienDongNhanSu:
                    return new ThongKeTongHopBienDongNhanSuController(app);
                default:
                    return new ThongKeGioiTinhController(app);
            }
        }
    }
}
