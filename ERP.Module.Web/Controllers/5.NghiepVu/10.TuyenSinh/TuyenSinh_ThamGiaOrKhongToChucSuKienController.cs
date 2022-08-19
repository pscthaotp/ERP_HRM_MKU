using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ThamGiaOrKhongToChucSuKienController : ViewController<ListView>
    {
        public TuyenSinh_ThamGiaOrKhongToChucSuKienController()
        {
            InitializeComponent();
            RegisterActions(components);

        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            bool daChinhSua = false;
            var chiTietList = View.SelectedObjects;
            foreach (var item in chiTietList)
            {
                ChiTietToChucSuKien chiTiet = item as ChiTietToChucSuKien;
                if (chiTiet != null)
                {
                    //1. Kiểm tra sự kiện này đã duyệt hay chưa
                    if (chiTiet.ToChucSuKien != null && !chiTiet.ToChucSuKien.DaDuyet)
                    {
                        WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Sự kiện chưa được duyệt!!!')");
                        break;
                    }
                    else
                    {
                        if (chiTiet.DaThamGia)
                            chiTiet.DaThamGia = false;
                        else
                        {
                            chiTiet.DaThamGia = true;
                        }
                    }
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

        private void TuyenSinh_ThamGiaOrKhongToChucSuKienController_Activated(object sender, EventArgs e)
        {
            //
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<ToChucSuKien>();

        }
    }
}
