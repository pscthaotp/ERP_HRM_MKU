using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NonPersistentObjects.NhanSu;
//
namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HopDongs
{
    public partial class HopDong_TaoHopDongController : ViewController
    {
        private IObjectSpace _obs;
        private HopDong_ChonLoaiHopDong _chonLoaiHopDong;
        private QuanLyHopDong quanLyHopDong;

        public HopDong_TaoHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //lưu Quản lý hợp đồng
            View.ObjectSpace.CommitChanges();
            _obs = Application.CreateObjectSpace();
            //
            quanLyHopDong = View.CurrentObject as QuanLyHopDong;
            _chonLoaiHopDong = _obs.CreateObject<HopDong_ChonLoaiHopDong>();
            e.View = Application.CreateDetailView(_obs, _chonLoaiHopDong);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            e.PopupWindow.View.ObjectSpace.CommitChanges();
            _obs = Application.CreateObjectSpace();
            //
            HopDong hopDong = null;
            
            if (_chonLoaiHopDong.LoaiHopDong != null && (_chonLoaiHopDong.LoaiHopDong.TenLoaiHopDong.Contains("khoán") || _chonLoaiHopDong.LoaiHopDong.TenLoaiHopDong.Contains("thời vụ")))
            {
                hopDong = _obs.CreateObject<HopDongKhoan>();
                hopDong.LoaiHopDong = _obs.GetObjectByKey<LoaiHopDong>(_chonLoaiHopDong.LoaiHopDong.Oid);
            }
            else
            {
                hopDong = _obs.CreateObject<HopDongLamViec>();
                hopDong.LoaiHopDong = _obs.GetObjectByKey<LoaiHopDong>(_chonLoaiHopDong.LoaiHopDong.Oid);
            }
            //
            if (hopDong != null)
            {
                hopDong.QuanLyHopDong = _obs.GetObjectByKey<QuanLyHopDong>(quanLyHopDong.Oid);
                //
                e.ShowViewParameters.Context = TemplateContext.View;
                e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, hopDong);
                e.ShowViewParameters.CreatedView.ObjectSpace.Committed += ObjectSpace_Committed;
            }
        }

        //
        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void HopDong_TaoHopDongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<HopDongLamViec>();
        }
    }
}
