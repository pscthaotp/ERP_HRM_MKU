﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Web;
using ERP.Module.HeThong;
using ERP.Module.DanhMuc.System;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
   public partial class TuyenSinh_DuyetThongBaoTuyenSinhController : ViewController
    {
        public TuyenSinh_DuyetThongBaoTuyenSinhController()
        {
            InitializeComponent();
        }
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            List<Guid> oidList = Common.OidCustomList;
            //
            if (oidList.Count > 0)
            {
                foreach (var item in oidList)
                {
                    ThongBaoTuyenSinh thongbao = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<ThongBaoTuyenSinh>(item);
                    if(thongbao != null)
                    {
                        thongbao.DaDuyet = true;
                    }
                    View.ObjectSpace.CommitChanges();
                    View.Refresh();
                    //
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Đã duyệt Thông báo tuyển sinh thành công !!!')");
                }
            }
        }

        private void TuyenSinh_DuyetThongBaoTuyenSinhController_Activated(object sender, EventArgs e)
        {
            string objectSpace = View.ObjectSpace.ToString();
            //
            if (View == null || objectSpace.Equals("DevExpress.ExpressApp.NonPersistentObjectSpace"))
                return;

            SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser(((XPObjectSpace)View.ObjectSpace).Session);
            PhanHe phanHe = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<PhanHe>(CriteriaOperator.Parse("Oid = ?", Config.KeyTuyenSinh));
            if (Common.KiemTraPhanQuyenDuyet(((XPObjectSpace)View.ObjectSpace).Session, phanHe != null ? phanHe.Oid : Guid.Empty, user.CongTy != null ? user.CongTy.Oid : Guid.Empty, user.Oid))
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
