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
    public partial class TuyenSinh_DongYOrHuyCauTraLoiIQController : ViewController<ListView>
    {
        public TuyenSinh_DongYOrHuyCauTraLoiIQController()
        {
            InitializeComponent();
            RegisterActions(components);

        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            View.ObjectSpace.CommitChanges();
            //
            
            List<Guid> cauTraLoiList = Common.OidCustomList;
            if (cauTraLoiList.Count > 0)
            {
                bool daChinhSua = false;
                foreach (var item in cauTraLoiList)
                {
                    ChiTietKiemTraIQ chiTiet = View.ObjectSpace.GetObjectByKey<ChiTietKiemTraIQ>(item);
                    if (chiTiet != null)
                    {
                        if (chiTiet.ChapNhan)
                            chiTiet.ChapNhan = false;
                        else
                            chiTiet.ChapNhan = true;
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

        private void TuyenSinh_DongYOrHuyCauTraLoiIQController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = false;//Commons.Common.IsWriteGranted<ChiTietKiemTraIQ>();
        }
    }
}
