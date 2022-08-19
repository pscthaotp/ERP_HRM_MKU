using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.TuyenDung
{
    public partial class TuyenDung_LapQuyetDinhThanhLapHoiDongTuyenDungController : ViewController
    {
        public TuyenDung_LapQuyetDinhThanhLapHoiDongTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TuyenDung_TrungTuyenController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<QuyetDinhThanhLapHoiDongTuyenDung>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            QuanLyTuyenDung qlTuyenDung = View.CurrentObject as QuanLyTuyenDung;
            if (qlTuyenDung != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                QuyetDinhThanhLapHoiDongTuyenDung quyetDinh = obs.FindObject<QuyetDinhThanhLapHoiDongTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung.oid=?", qlTuyenDung.Oid));
                if (quyetDinh == null)
                {
                    qlTuyenDung = obs.GetObjectByKey<QuanLyTuyenDung>(qlTuyenDung.Oid);
                    quyetDinh = obs.CreateObject<QuyetDinhThanhLapHoiDongTuyenDung>();
                    quyetDinh.QuanLyTuyenDung = qlTuyenDung;

                    //e.ShowViewParameters.Context = TemplateContext.View;
                    //e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                    //e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, quyetDinh);
                    //obs.Committed += obs_Committed;
                }
                e.ShowViewParameters.Context = TemplateContext.View;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, quyetDinh);
                obs.Committed += obs_Committed;

                //Application.ShowView<QuyetDinhThanhLapHoiDongTuyenDung>(obs, quyetDinh);
            }
        }

        void obs_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.Refresh();
            View.Refresh();
        }
    }
}
