using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Security;
using DevExpress.Utils;
using System.Windows.Forms;
using System.Data;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HoSoLuong
{
    public partial class HoSoLuong_ChonCongThucLuongController : ViewController
    {
        Luong_ChonCongThucLuong _congThucTinhLuong = null;
        IObjectSpace _obs = null;
        PhanQuyenTinhLuong _phanQuyenTinhLuongCurrent = null;

        public HoSoLuong_ChonCongThucLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HoSoLuong_ChonCongThucLuongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<ChiTietPhanQuyenTinhLuong>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            //
            _phanQuyenTinhLuongCurrent = View.CurrentObject as PhanQuyenTinhLuong;
            //
            if (_phanQuyenTinhLuongCurrent != null)
            {
                _obs = Application.CreateObjectSpace();
                //
                _congThucTinhLuong = _obs.CreateObject<Luong_ChonCongThucLuong>();
                e.View = Application.CreateDetailView(_obs, _congThucTinhLuong);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            if (_congThucTinhLuong != null && _congThucTinhLuong.CongThucTinhLuongList.Count > 0)
            {
                foreach (var item in _congThucTinhLuong.CongThucTinhLuongList)
                {
                    if (item.Chon)
                    {
                        _phanQuyenTinhLuongCurrent.CreateChiTietPhanQuyenTinhLuong(item.CongThucTinhLuong.Oid);
                    }
                }
            }
        }
    }
}
