using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;

using ERP.Module.NghiepVu.TuyenSinh;

using DevExpress.Xpo;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using ERP.Module.NghiepVu.HocSinh.HocSinhs;
using DevExpress.ExpressApp.Xpo;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_CapNhatThonTinHocSinhController : ViewController
    {
        public TuyenSinh_CapNhatThonTinHocSinhController()
        {
            InitializeComponent();
        }
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            if (View.SelectedObjects.Count > 0)
            {
                foreach (ThongTinKhachHang item in View.SelectedObjects)
                {
                    string thongtin = "";
                    if (item != null)
                    {
                        XPCollection<HoSoNhapHoc> hoSoNhapHocList = new XPCollection<HoSoNhapHoc>(((XPObjectSpace)View.ObjectSpace).Session, CriteriaOperator.Parse("ThongTinKhachHang = ?", item.Oid));
                        if (hoSoNhapHocList != null && hoSoNhapHocList.Count > 0)
                        {
                           
                            foreach (HoSoNhapHoc hs in hoSoNhapHocList)
                            {
                                if (hs != null)
                                {
                                    Module.NghiepVu.HocSinh.HocSinhs.HocSinh hocSinh = View.ObjectSpace.FindObject<Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(CriteriaOperator.Parse("HoSoNhapHoc = ?", hs.Oid));
                                    if (hocSinh != null)
                                    {
                                        if (string.IsNullOrEmpty(thongtin))
                                        {
                                            thongtin = hocSinh.MaQuanLy + "|" + hocSinh.HoTen + "|" + hocSinh.NgaySinh.ToShortDateString() + "\n";
                                        }
                                        else
                                        {
                                            thongtin += hocSinh.MaQuanLy + "|" + hocSinh.HoTen + "|" + hocSinh.NgaySinh.ToShortDateString() + "\n";
                                        }
                                    }
                                }
                            }
                        }
                        item.ThongTinHocSinh = thongtin;
                    }
                }
                View.ObjectSpace.CommitChanges();
                View.Refresh();
            }
        }

        private void TuyenSinh_DuyetThongBaoTuyenSinhController_Activated(object sender, EventArgs e)
        {

            if (View.Id.Equals("ThongTinKhachHang_ListView"))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            else
                simpleAction1.Active["TruyCap"] = false;

        }
    }
}
