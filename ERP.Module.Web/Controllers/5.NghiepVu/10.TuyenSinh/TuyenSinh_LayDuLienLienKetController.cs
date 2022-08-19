using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.TreeListEditors.Web;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_LayDuLienLienKetController : ViewController<DetailView>
    {
        public TuyenSinh_LayDuLienLienKetController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TuyenSinh_LayDuLienLienKetController_ViewControlsCreated(object sender, EventArgs e)
        {
            if (View != null && View.Id.Equals("HoSoNhapHoc_DetailView"))
            {
                Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                //
                try
                {
                    HoSoNhapHoc hoSoNhapHoc = View.CurrentObject as HoSoNhapHoc;
                    //Lưu ý: Lấy dữ liệu báo cáo phải lấy như code ở đây vì chưa lưu cho liên kết
                    if (hoSoNhapHoc != null && hoSoNhapHoc.ThongTinKhachHang != null && !hoSoNhapHoc.DaTaoLienKet)
                    {
                        //
                        CriteriaOperator filter = CriteriaOperator.Parse("ThongTinKhachHang=? and Oid != ?", hoSoNhapHoc.ThongTinKhachHang.Oid, hoSoNhapHoc.Oid);
                        XPCollection<HoSoNhapHoc> lienKetList = new XPCollection<HoSoNhapHoc>(session, filter);
                        foreach (var item in lienKetList)
                        {
                            filter = CriteriaOperator.Parse("HoSoNhapHoc=? and LienKet=?",hoSoNhapHoc.Oid,item.Oid);
                            HoSoNhapHoc_LienKet lienKetHoSo = session.FindObject<HoSoNhapHoc_LienKet>(filter);
                            if (lienKetHoSo == null)
                            {
                                lienKetHoSo = new HoSoNhapHoc_LienKet(session);
                                lienKetHoSo.HoSoNhapHoc = hoSoNhapHoc;
                                lienKetHoSo.LienKet = item;
                                lienKetHoSo.Thu = item.Thu;
                                //
                                hoSoNhapHoc.LienKetList.Add(lienKetHoSo);
                                //
                                hoSoNhapHoc.DaTaoLienKet = true;
                            }
                        }
                        //
                        View.Refresh();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
