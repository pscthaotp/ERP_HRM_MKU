using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Commons;
using ERP.Module.Win.Controllers.NghiepVu.TienLuong.ExecuteClass;
using ERP.Module.NghiepVu.TienLuong.NgoaiGio;
using System.Windows.Forms;
//
namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.NgoaiGio
{
    public partial class NgoaiGio_TinhLuongNgoaiGioController : ViewController
    {
        public NgoaiGio_TinhLuongNgoaiGioController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            BangLuongNgoaiGio bangLuong = View.CurrentObject as BangLuongNgoaiGio;
            if (bangLuong != null)
            {
                if (bangLuong.ListLuongNgoaiGio == null || (bangLuong.ListLuongNgoaiGio != null && bangLuong.ListLuongNgoaiGio.Count == 0))
                {
                    //Lưu dữ liệu trước khi chốt
                    View.ObjectSpace.CommitChanges();
                }

                #region Bắt lỗi trước khi tính
                if (bangLuong.KyTinhLuong.KhoaSo)
                {
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '[{0}]' đã khóa sổ.", bangLuong.KyTinhLuong.TenKy));
                    return;
                }
                if (bangLuong.ChungTu != null)
                {
                    DialogUtil.ShowWarning("Bảng lương đã được lập chứng từ chi tiền.");
                    return;
                }

                if (bangLuong.KyTinhLuong.QuanLyCongNgoaiGio == null && bangLuong.KyTinhLuong.QuanLyCongKhac == null)
                {
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '[{0}]' chưa có bảng chấm công.", bangLuong.KyTinhLuong.TenKy));
                    return;
                }
                
                #endregion

                #region Gọi hàm xử lý tính lương
                //
                string message = string.Empty;
                //
                using (DialogUtil.AutoWait())
                {
                    //Lấy tất cả công thức lương thỏa điều kiện
                    CriteriaOperator filter = CriteriaOperator.Parse("CongTy = ? and LoaiCongThucLuong = 2",bangLuong.CongTy.Oid);
                    XPCollection<CongThucTinhLuong> congThucTinhLuongList = new XPCollection<CongThucTinhLuong>(((XPObjectSpace)View.ObjectSpace).Session, filter);
                    //
                    message = Luong_TinhLuongNgoaiGio.XuLy(View.ObjectSpace, bangLuong, congThucTinhLuongList);
                }
                #endregion

                //
                if (string.IsNullOrEmpty(message))
                {
                    DialogUtil.ShowInfo("Tính lương thành công!!!");
                    //
                    View.ObjectSpace.Refresh();
                }
                else
                {
                    DialogUtil.ShowError(message);
                }
            }
        }

        private void NgoaiGio_TinhLuongNgoaiGioController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<BangLuongNgoaiGio>();
        }
    }
}
