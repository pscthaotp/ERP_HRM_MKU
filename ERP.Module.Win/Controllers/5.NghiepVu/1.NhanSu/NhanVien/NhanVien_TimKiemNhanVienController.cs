using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using System.Data.SqlClient;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_TimKiemNhanVienController : ViewController
    {
        public NhanVien_TimKiemNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void NhanVien_TimKiemNhanVienController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            List<ThongTinNhanVien> nvList = new List<ThongTinNhanVien>();
            //
            if (view != null)
            {
                ControlViewItem item = ((DetailView)View).FindItem("btnSearch") as ControlViewItem;
                //
                if (item != null)
                {
                    SimpleButton btnSearch = item.Control as SimpleButton;
                    if (btnSearch != null)
                    {
                        btnSearch.Text = "Tìm kiếm";
                        btnSearch.Width = 80;
                        btnSearch.Click += (obj, ea) =>
                        {
                            using (DialogUtil.AutoWait())
                            {
                               
                                TimKiemNhanVien search = view.CurrentObject as TimKiemNhanVien;
                                if (search != null)
                                {

                                    //
                                    IObjectSpace obs = Application.CreateObjectSpace();
                                    //
                                    DateTime current = Common.GetServerCurrentTime();

                                    //Lấy danh sách thỏa điều kiện
                                    List<Guid> list = GetStaffList_ByCondition(obs, "spd_NhanVien_TimKiemDanhSachNhanVien", CommandType.StoredProcedure, search.DieuKienTimKiem, new object[] { current.SetTime(SetTimeEnum.StartYear), current.SetTime(SetTimeEnum.EndMonth) });
                                    
                                    //Lấy danh sách tất cả nhân viên
                                    XPCollection<ThongTinNhanVien> nhanVienList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)obs).Session);

                                    //Lọc lại danh sách nhân viên thỏa điều kiện
                                    nvList = (from x in nhanVienList
                                                where list.Contains(x.Oid)
                                                select x).ToList();                                  
                                }

                                if (search.ListChiTietTimKiemNhanVien == null)
                                    search.ListChiTietTimKiemNhanVien = new XPCollection<ChiTietTimKiemNhanVien>(((XPObjectSpace)View.ObjectSpace).Session, false);
                                else
                                    search.ListChiTietTimKiemNhanVien.Reload();
                                //
                                foreach (ThongTinNhanVien nvItem in nvList)
                                {
                                    ChiTietTimKiemNhanVien chiTietTimKiemNhanVien = new ChiTietTimKiemNhanVien(((XPObjectSpace)View.ObjectSpace).Session);
                                    chiTietTimKiemNhanVien.NhanVien = View.ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nvItem.Oid);

                                    search.ListChiTietTimKiemNhanVien.Add(chiTietTimKiemNhanVien);
                                }
                                View.Refresh();
                            }         
                        };
                    }
                }
            }
        }

        private List<Guid> GetStaffList_ByCondition(IObjectSpace obs, string query, CommandType type, string criteria, params object[] args)
        {
            List<Guid> resultList = new List<Guid>();
            //
            SqlParameter param = new SqlParameter("@Criteria", criteria.XuLyDieuKien(obs, true, args));
            //
            resultList = DataProvider.GetGuidList(query, type, param);
            //
            return resultList;
        }
    }
}
