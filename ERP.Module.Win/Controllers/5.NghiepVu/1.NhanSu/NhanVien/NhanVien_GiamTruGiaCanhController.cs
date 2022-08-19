using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Extends;
using ERP.Module.Enum.Systems;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_GiamTruGiaCanhController : ViewController
    {
        public NhanVien_GiamTruGiaCanhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void NhanVien_GiamTruGiaCanhController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = Common.IsWriteGranted<GiamTruGiaCanh>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                IObjectSpace obs = View.ObjectSpace;
                GiamTruGiaCanh giamTru;
                CriteriaOperator filter;
                NhanVien nhanVien = null;

                foreach (QuanHeGiaDinh item in View.SelectedObjects)
                {                    
                    if (!IsExists(item.NhanVien.ListGiamTruGiaCanh, item))
                    {
                        giamTru = obs.CreateObject<GiamTruGiaCanh>();
                        giamTru.QuanHeGiaDinh = item;
                        giamTru.TuNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartYear);
                        item.NhanVien.ListGiamTruGiaCanh.Add(giamTru);
                        nhanVien = item.NhanVien;

                        if (item.QuanHe.TenQuanHe.ToLower().Contains("cha đẻ"))
                        {
                            filter = CriteriaOperator.Parse("TenLoaiGiamTruGiaCanh like ?", "%cha già%");
                            giamTru.LoaiGiamTruGiaCanh = obs.FindObject<LoaiGiamTruGiaCanh>(filter);
                        }
                        else if (item.QuanHe.TenQuanHe.ToLower().Contains("mẹ đẻ"))
                        {
                            filter = CriteriaOperator.Parse("TenLoaiGiamTruGiaCanh like ?", "%mẹ già%");
                            giamTru.LoaiGiamTruGiaCanh = obs.FindObject<LoaiGiamTruGiaCanh>(filter);
                        }
                        else if (item.QuanHe.TenQuanHe.ToLower().Contains("%con%"))
                        {
                            if (item.NgaySinh != DateTime.MaxValue && giamTru.TuNgay.Year - item.NgaySinh.Year < 18)
                                filter = CriteriaOperator.Parse("TenLoaiGiamTruGiaCanh like ?", "Con dưới 18 tuổi");
                            else
                                filter = CriteriaOperator.Parse("TenLoaiGiamTruGiaCanh like ?", "%con trên 18 tuổi%đại học, cao đẳng%");

                            giamTru.LoaiGiamTruGiaCanh = obs.FindObject<LoaiGiamTruGiaCanh>(filter);
                        }
                        else if (item.QuanHe.TenQuanHe.ToLower() == "vợ" ||
                            item.QuanHe.TenQuanHe.ToLower() == "chồng")
                        {
                            if (item.NgaySinh != DateTime.MaxValue && giamTru.TuNgay.Year - item.NgaySinh.Year < 60)
                                filter = CriteriaOperator.Parse("TenLoaiGiamTruGiaCanh like ?", "Vợ/Chồng ngoài độ tuổi lao động");
                            else
                                filter = CriteriaOperator.Parse("TenLoaiGiamTruGiaCanh like ?", "Vợ/chồng trong độ tuổi lao động nhưng bị tàn tật, không có khả năng lao động");

                            giamTru.LoaiGiamTruGiaCanh = obs.FindObject<LoaiGiamTruGiaCanh>(filter);
                        }
                        else if (item.QuanHe.TenQuanHe.ToLower().Contains("ông") ||
                            item.QuanHe.TenQuanHe.ToLower().Contains("bà"))
                        {
                            filter = CriteriaOperator.Parse("TenLoaiGiamTruGiaCanh like ?", "Ông/bà ngoài độ tuổi lao động");

                            giamTru.LoaiGiamTruGiaCanh = obs.FindObject<LoaiGiamTruGiaCanh>(filter);
                        }
                    }
                }

                if (nhanVien != null)
                {
                    int soNguoi = 0, soThang = 0, temp;

                    //Tính toán số người phụ thuộc và số tháng giảm trừ
                    foreach (GiamTruGiaCanh item in nhanVien.ListGiamTruGiaCanh)
                    {
                        soNguoi++;
                        if (item.TuNgay != DateTime.MinValue &&
                            item.DenNgay != DateTime.MinValue)
                        {
                            temp = item.TuNgay.GetMonthNumber(item.DenNgay) + 1;
                            if (temp > 12 || temp < 0)
                                temp = 12;
                        }
                        else if (item.TuNgay != DateTime.MinValue)
                        {
                            temp = item.TuNgay.GetMonthNumber(item.TuNgay.SetTime(SetTimeEnum.EndYear)) + 1;
                        }
                        else if (item.DenNgay != DateTime.MinValue)
                        {
                            temp = item.DenNgay.SetTime(SetTimeEnum.StartYear).GetMonthNumber(item.DenNgay) + 1;
                        }
                        else
                        {
                            temp = 12;
                        }
                        //
                        soThang += temp;
                    }
                    nhanVien.NhanVienThongTinLuong.SoNguoiPhuThuoc = soNguoi;
                    nhanVien.NhanVienThongTinLuong.SoThangGiamTru = soThang;
                }
                View.Refresh();
            }
        }

        private static bool IsExists(XPCollection<GiamTruGiaCanh> gdList, QuanHeGiaDinh qh)
        {
            foreach (GiamTruGiaCanh item in gdList)
            {
                if (item.QuanHeGiaDinh.Oid == qh.Oid)
                    return true;
            }
            return false;
        }
    }
}
