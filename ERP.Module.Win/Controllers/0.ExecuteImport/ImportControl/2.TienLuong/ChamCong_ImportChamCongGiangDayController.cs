using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.TienLuong
{
    public partial class ChamCong_ImportChamCongGiangDayController : ViewController
    {
        private IObjectSpace _obs;
        private OfficeBaseObject _chonLoaiOffice;
        private CC_QuanLyCongGiangDay _quanLyChamCongGiangDay;

        public ChamCong_ImportChamCongGiangDayController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu Quản lý chấm công
            View.ObjectSpace.CommitChanges();
            _obs = Application.CreateObjectSpace();
            //
            _quanLyChamCongGiangDay = View.CurrentObject as CC_QuanLyCongGiangDay;
            _chonLoaiOffice = _obs.CreateObject<OfficeBaseObject>();
            e.View = Application.CreateDetailView(_obs, _chonLoaiOffice);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //
            if (_quanLyChamCongGiangDay != null)
            {
                //Xử lý
                if (_quanLyChamCongGiangDay.CongTy != null && _quanLyChamCongGiangDay.CongTy.LoaiTruong == Enum.TuyenSinh_PT.LoaiTruongEnum.PT)
                    Imp_ChamCongGiangDayPhoThong.ImportChamCongVuotTietPhoThong(((XPObjectSpace)View.ObjectSpace), _chonLoaiOffice, _quanLyChamCongGiangDay);
                else
                    Imp_ChamCongGiangDay.ImportChamCongGiangDay(((XPObjectSpace)View.ObjectSpace), _chonLoaiOffice, _quanLyChamCongGiangDay.Oid);
                //
                View.ObjectSpace.Refresh();
            }
        }

        private void ChamCong_ImportChamCongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<CC_QuanLyCongGiangDay>();
        }
    }
}
