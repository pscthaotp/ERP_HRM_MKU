using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using System.Data;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NonPersistentObjects.NhanSu;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HoSoLuong
{
    public partial class HoSoLuong_CopyChiTietLuongController : ViewController
    {
        IObjectSpace _obs;
        HoSoTinhLuong _hoSoTinhLuong;
        HoSoLuong_ChonNhanVien _source;

        public HoSoLuong_CopyChiTietLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HoSoLuong_CopyChiTietLuongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<HoSoTinhLuong>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            // Không cần lưu vì đợt 1 đã lưu
            //View.ObjectSpace.CommitChanges();
            //

            //
            _hoSoTinhLuong = View.CurrentObject as HoSoTinhLuong;
            if (_hoSoTinhLuong != null)
            {
                //
                _obs = Application.CreateObjectSpace();
                _source = _obs.CreateObject<HoSoLuong_ChonNhanVien>();
                e.View = Application.CreateDetailView(_obs, _source);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_source != null && _source.ThongTinNhanVien != null)
            {
                if (_hoSoTinhLuong.KhoaSo || _hoSoTinhLuong.KyTinhLuong.KhoaSo)
                {
                    DialogUtil.ShowWarning("Hồ sơ tính lương hoặc Kỳ tính lương đã khóa sổ.");
                    return;
                }
                using (DialogUtil.AutoWait())
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@HoSoTinhLuong", _hoSoTinhLuong.Oid);
                    param[1] = new SqlParameter("@NhanVien", _source.ThongTinNhanVien.Oid);
                    //
                    int sucess = DataProvider.ExecuteNonQuery("spd_HoSoLuong_CopyChiTietLuong", CommandType.StoredProcedure, param);
                    //
                    if (sucess != -1)
                    {
                        View.ObjectSpace.Refresh();
                        View.Refresh();
                        //
                        DialogUtil.ShowInfo("Copy chi tiết lương thành công.");
                    }
                    else
                        DialogUtil.ShowError("Copy chi tiết tính lương thất bại.");
                    //
                }              
            }
            else
            {
                DialogUtil.ShowWarning("Chưa chọn dữ liệu chốt.");
            }
        }
    }
}
