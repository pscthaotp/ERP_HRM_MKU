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
using ERP.Module.NonPersistentObjects.TuyenSinh;
using DevExpress.Xpo;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ChuyenDoiFollowUpController : ViewController
    {
        private ChuyenDoiFollowUp _ChuyenDoiFollowUp;
        //IObjectSpace _obs;

        public TuyenSinh_ChuyenDoiFollowUpController()
        {
            InitializeComponent();
            RegisterActions(components);

        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Session session = ((XPObjectSpace)View.ObjectSpace).Session;
            string message = "";
            _ChuyenDoiFollowUp = ((DetailView)((View.ObjectSpace).Owner)).CurrentObject as ChuyenDoiFollowUp;
            if (View.SelectedObjects != null)
            {
                if (View.SelectedObjects.Count > 0 && _ChuyenDoiFollowUp.NguoiNhan != null)
                {
                    foreach (var item in View.SelectedObjects)
                    {
                        ChuyenDoiFollowUp_KhachHang kh = item as ChuyenDoiFollowUp_KhachHang;
                        if (kh != null)
                        {
                            kh.ThongTinKhachHang.SecuritySystemUser = _ChuyenDoiFollowUp.NguoiNhan;

                            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinKhachHang.Oid=?", kh.ThongTinKhachHang.Oid);
                            XPCollection<ChiTietTuVanTuyenSinh> listChiTietTuVanTuyenSinh = new XPCollection<ChiTietTuVanTuyenSinh>(session, filter);
                            foreach (var tuVan in listChiTietTuVanTuyenSinh)
                            {
                                tuVan.SecuritySystemUser = _ChuyenDoiFollowUp.NguoiNhan;
                            }
                        }
                    }

                    View.ObjectSpace.CommitChanges();
                    message = "alert('Chuyển thành công !!!.')";
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                    _ChuyenDoiFollowUp.TimKiemKhachHang(_ChuyenDoiFollowUp.NguoiChuyen);
                    ((DetailView)((View.ObjectSpace).Owner)).Refresh();
                }
                else
                {
                    message = "alert('Vui lòng chọn người chuyển và người nhận.')";
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                }
            }
        }

        private void TuyenSinh_ChuyenDoiFollowUpController_Activated(object sender, EventArgs e)
        {
            if (View.Id.Equals("ChuyenDoiFollowUp_ListKhachHang_ListView"))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            else
                simpleAction1.Active["TruyCap"] = false;
        }
    }
}
