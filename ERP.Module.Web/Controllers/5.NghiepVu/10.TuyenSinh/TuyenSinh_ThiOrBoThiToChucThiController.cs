using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ThiOrBoThiToChucThiController : ViewController<ListView>
    {
        public TuyenSinh_ThiOrBoThiToChucThiController()
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
                    ToChucThi toChucThi = View.ObjectSpace.GetObjectByKey<ToChucThi>(item);
                    if (toChucThi != null)
                    {
                        if (toChucThi.DaDongLePhi)
                        {
                            if (toChucThi.DaThi)
                            {
                                toChucThi.DaThi = false;
                                toChucThi.HuyThi = true;
                            }
                            else
                            {
                                toChucThi.DaThi = true;
                                toChucThi.HuyThi = false;
                            }
                            //
                            daChinhSua = true;
                        }
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

        private void TuyenSinh_ThiOrBoThiToChucThiController_Activated(object sender, EventArgs e)
        {
            //
            if (View.Id.Equals("ToChucThi_ListView")
             || View.Id.Equals("ThongBaoDongLePhi_ListChiTietThongBaoDongLePhi_ListView"))
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
