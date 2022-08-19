using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu;
using ERP.Module.NghiepVu.NhanSu.DaoTao;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.NhanSu
{
    public partial class DaoTao_ImportDangKyDaoTaoController : ViewController
    {
        private IObjectSpace _obs;        
        private OfficeBaseObject _typeOffice;
        private DangKyDaoTao _dangKyDaoTao;

        public DaoTao_ImportDangKyDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void DinhBien_ImportDinhBienChucDanhController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<DangKyDaoTao>();           
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu dữ liệu 
            View.ObjectSpace.CommitChanges();
            //
            _dangKyDaoTao = View.CurrentObject as DangKyDaoTao;
            if (_dangKyDaoTao != null)
            {
                _obs = Application.CreateObjectSpace();   
                _typeOffice = _obs.CreateObject<OfficeBaseObject>();
                e.View = Application.CreateDetailView(_obs, _typeOffice);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_typeOffice != null)
            {
                //
                Imp_DangKyDaoTao.ImportDangKyDaoTao(View.ObjectSpace, _dangKyDaoTao, _typeOffice.LoaiOffice);
                //                   
                //View.ObjectSpace.CommitChanges();              
                View.ObjectSpace.Refresh();
            }
        }
    }
}
