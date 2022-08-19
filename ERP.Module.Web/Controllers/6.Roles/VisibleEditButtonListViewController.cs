using System;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.SystemModule;

namespace ERP.Module.Web.Controllers.Roles
{
    public partial class VisibleEditButtonController : ViewController
    {
        public VisibleEditButtonController()
        {
            InitializeComponent();
            TargetViewType = ViewType.ListView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            //
            ListViewController controller = Frame.GetController<ListViewController>();
            if (controller == null) return;
            //  
            string objectSpace = View.ObjectSpace.ToString();
            //
            if (View == null || objectSpace.Equals("DevExpress.ExpressApp.NonPersistentObjectSpace"))
            {
              
                return;
            }
            //
            bool enable = true;
            Session session = ((XPObjectSpace)View.ObjectSpace).Session;
            if (View.Id.Equals("ChuyenDoiFollowUp_ListKhachHang_ListView"))
            {
                Frame.GetController<ResetViewSettingsController>().Active["ViewAllowReset"] = false;
            }
            #region Tuyển sinh
            if (View.ObjectTypeInfo.FullName.Contains("TuyenSinh"))
            {
                /*
                #region 2. Chi tiết tổ chức thi
                if (View.ObjectTypeInfo.Name == "ChiTietToChucThi")
                {
                    ChiTietToChucThi chiTietToChucThi = View.CurrentObject as ChiTietToChucThi;
                    if (chiTietToChucThi != null && (chiTietToChucThi.DaLamHoSo))
                        enable = false;
                    //
                }
                #endregion

                #region 2. Hồ sơ nhập học
                if (View.ObjectTypeInfo.Name == "HoSoNhapHoc")
                {
                    HoSoNhapHoc hoSoNhapHoc = View.CurrentObject as HoSoNhapHoc;
                    if (hoSoNhapHoc != null && (hoSoNhapHoc.DaThuHoSoNhapHoc))
                        enable = false;
                    //
                }
                #endregion */
            }
            #endregion

            // Set giá trị
            controller.EditAction.Active.SetItemValue("EditableListView", enable);
            //
        }
    }
}
