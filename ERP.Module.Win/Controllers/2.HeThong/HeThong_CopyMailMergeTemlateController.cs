using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using System.Data;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.MailMerge;
using ERP.Module.NonPersistentObjects.HeThong;
using System.Text;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_CopyMailMergeTemlateController : ViewController
    {
        IObjectSpace _obs;
        MailMergeTemplate _mailMerge;
        ChonCongTyTruong _source;

        public HeThong_CopyMailMergeTemlateController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HeThong_CopyMailMergeTemlateController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = (Common.IsWriteGranted<MailMergeTemplate>() && Common.SecuritySystemUser_GetCurrentUser().LoaiTaiKhoan == Enum.Systems.LoaiTaiKhoanEnum.QuanTriHeThong);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {           
            _mailMerge = View.CurrentObject as MailMergeTemplate;
            if (_mailMerge != null)
            {
                //
                _obs = Application.CreateObjectSpace();
                _source = _obs.CreateObject<ChonCongTyTruong>();
                e.View = Application.CreateDetailView(_obs, _source);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_source != null)
            {              
                using (DialogUtil.AutoWait())
                {
                    List<string> lstBP;
                    if (_source.TatCaDonVi)
                        lstBP = Common.Department_GetRoledDepartmentList_ByDepartment(null);
                    else
                        lstBP = Common.Department_GetRoledDepartmentList_ByDepartment(_source.CongTy);

                    StringBuilder sb = new StringBuilder();
                    foreach (string item in lstBP)
                    {
                        sb.Append(String.Format("{0},", item));
                    }

                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@BoPhanDuocPhanQuyenList", sb.ToString());
                    param[1] = new SqlParameter("@MailMerge", _mailMerge.Oid); 
                    //
                    int sucess = DataProvider.ExecuteNonQuery("spd_HeThong_CopyMailMergeTemlate", CommandType.StoredProcedure, param);
                    //
                    if (sucess != 1000)
                    {
                        View.ObjectSpace.Refresh();
                        View.Refresh();
                        //
                        DialogUtil.ShowInfo("Copy biểu mẫu thành công.");
                    }
                    else
                        DialogUtil.ShowError("Copy biểu mẫu thất bại.");
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
