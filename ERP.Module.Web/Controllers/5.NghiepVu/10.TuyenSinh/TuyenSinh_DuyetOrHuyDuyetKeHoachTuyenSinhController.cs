using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;
using ERP.Module.Enum.TuyenSinh;
using ERP.Module.HeThong;
using ERP.Module.DanhMuc.System;
using DevExpress.Data.Filtering;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_DuyetOrHuyDuyetKeHoachTuyenSinhController : ViewController<ListView>
    {
        public TuyenSinh_DuyetOrHuyDuyetKeHoachTuyenSinhController()
        {
            InitializeComponent();
            RegisterActions(components);

        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            View.ObjectSpace.CommitChanges();
            //
            
            List<Guid> oidList = Common.OidCustomList;
            if (oidList.Count > 0)
            {
                bool daChinhSua = false;
                foreach (var item in oidList)
                {
                    ChiTietKeHoachTuyenSinh chiTiet = View.ObjectSpace.GetObjectByKey<ChiTietKeHoachTuyenSinh>(item);
                    if (chiTiet != null)
                    {
                        if (chiTiet.TrangThai == TrangThaiKeHoachEnum.DaDuyet)
                            chiTiet.TrangThai = TrangThaiKeHoachEnum.ChuaDuyet;
                        else if (chiTiet.TrangThai == TrangThaiKeHoachEnum.ChuaDuyet)
                            chiTiet.TrangThai = TrangThaiKeHoachEnum.DaDuyet;
                        //
                        daChinhSua = true;
                    }
                }
                //
                if (daChinhSua)
                {
                    View.ObjectSpace.CommitChanges();
                    View.Refresh();
                    //
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Thành công!!!')");
                }
            }
        }

        private void TuyenSinh_DuyetOrHuyDuyetKeHoachTuyenSinhController_Activated(object sender, EventArgs e)
        {
            string objectSpace = View.ObjectSpace.ToString();
            //
            if (View == null || objectSpace.Equals("DevExpress.ExpressApp.NonPersistentObjectSpace"))
                return;

            //
            SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser(((XPObjectSpace)View.ObjectSpace).Session);
            PhanHe phanHe = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<PhanHe>(CriteriaOperator.Parse("Oid = ?",Config.KeyTuyenSinh));
            if (Common.KiemTraPhanQuyenDuyet(((XPObjectSpace)View.ObjectSpace).Session,phanHe!= null ? phanHe.Oid : Guid.Empty,user.CongTy != null ? user.CongTy.Oid : Guid.Empty, user.Oid))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            else
                simpleAction1.Active["TruyCap"] = false;
            //
            if (simpleAction1.Active["TruyCap"] == true)
            {
                //
                Common.OidCustomList = new List<Guid>();
            }
        }
    }
}
