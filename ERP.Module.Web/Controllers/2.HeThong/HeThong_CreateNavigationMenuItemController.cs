using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Web;
using ERP.Module.HeThong;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.CauHinhChungs;
using ERP.Module.BaoCao.Custom;
using DevExpress.ExpressApp.Security;
using ERP.Module.DanhMuc.System;
using System.Data;
using System.Data.SqlClient;

namespace ERP.Module.Web.Controllers.HeThong
{
    public partial class HeThong_CreateNavigationMenuItemController : WindowController
    {
        IObjectSpace _obs = null;
        WebApplication _application = WebApplication.Instance;

        public HeThong_CreateNavigationMenuItemController()
        {
            InitializeComponent();
            RegisterActions(components);
            //
            TargetWindowType = WindowType.Main;
        }

        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            ShowNavigationItemController nav = Frame.GetController<ShowNavigationItemController>();
            if (nav != null)
            {
                nav.NavigationItemCreated += new EventHandler<NavigationItemCreatedEventArgs>(nav_NavigationItemCreated);
                nav.CustomShowNavigationItem += new EventHandler<CustomShowNavigationItemEventArgs>(nav_CustomShowNavigationItem);
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            //
            Frame.GetController<MyDetailsController>().Actions["MyDetails"].Caption = SecuritySystem.CurrentUserName;
        }

        void nav_NavigationItemCreated(object sender, NavigationItemCreatedEventArgs e)
        {
            //
            _obs = _application.CreateObjectSpace();
            //
            if (!e.NavigationItem.Id.Contains("Nav_Report"))
            {
                CriteriaOperator filter = CriteriaOperator.Parse("MaQuanLy like ?", e.NavigationItem.Id.ToString().Trim());
                PhanHe phanHe = _obs.FindObject<PhanHe>(filter);

                //Tất cả phân hệ
                if (phanHe != null)
                {
                    //
                    filter = CriteriaOperator.Parse("SuDung and PhanHe = ? and LaThuMuc and LoaiPhanMen = 1", phanHe.Oid);
                    XPCollection<AppMenu> thuMucList = new XPCollection<AppMenu>(((XPObjectSpace)_obs).Session, filter, new SortProperty("SoThuTu", DevExpress.Xpo.DB.SortingDirection.Ascending));
                    //
                    if (thuMucList.Count > 0)
                        GetNavigationItemfromAppMenu(e, thuMucList, phanHe.Oid);
                }
            }

            //Báo cáo
            if (e.NavigationItem.Id.Contains("Nav_Report"))
            {
                //Chỗ này tạo khác
                GetNavigationItemOfGroupReport(e);
            }
        }

        private void GetNavigationItemfromAppMenu(NavigationItemCreatedEventArgs e, XPCollection<AppMenu> thuMucList, Guid phanHe)
        {

            foreach (AppMenu itemThuMuc in thuMucList)
            {
                bool daThemChucNang = false;
                //
                ChoiceActionItem thuMuc = new ChoiceActionItem();
                thuMuc.ImageName = itemThuMuc.HinhAnh;
                thuMuc.Caption = itemThuMuc.TenChucNang;
                //
                CriteriaOperator filter = CriteriaOperator.Parse("SuDung and PhanHe = ? and !LaThuMuc and ThuMucQuanLy = ?", phanHe, itemThuMuc.Oid);
                XPCollection<AppMenu> chucNangList = new XPCollection<AppMenu>(((XPObjectSpace)_obs).Session, filter, new SortProperty("SoThuTu", DevExpress.Xpo.DB.SortingDirection.Ascending));

                //Make an array for the list of assemblies.
                System.Reflection.Assembly[] assems = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("DevExpress.ExpressApp.Security") || a.FullName.Contains("ERP")).ToArray();
                //
                foreach (AppMenu itemChucNang in chucNangList)
                {
                    #region 1. Persitent
                    if (itemChucNang.LoaiObject == LoaiObjectEnum.Persistent)
                    {
                        //Tìm object trong source
                        Type objecttype = assems.SelectMany(a => a.GetTypes()).Where(t => t.Name.Equals(itemChucNang.AppObject.KeyObject)).FirstOrDefault();
                        if (objecttype != null)
                        {
                            bool isAccess = Common.TaiKhoanHeThong() ? true : Common.IsAccessGranted(objecttype);
                            var user = Common.SecuritySystemUser_GetCurrentUser();
                            //Là tài khoản hệ thống thì lấy tất cả menu
                            if (isAccess && Common.SecuritySystemUser_GetCurrentUser().LoaiTaiKhoan == LoaiTaiKhoanEnum.QuanTriHeThong)
                            {
                                ChoiceActionItem chucNang = new ChoiceActionItem();
                                chucNang.ImageName = itemChucNang.HinhAnh;
                                chucNang.Caption = itemChucNang.TenChucNang;
                                string loaiView = string.Empty;
                                if (itemChucNang.LoaiView == LoaiViewEnum.ListView)
                                {
                                    loaiView = "ListView";
                                }
                                if (itemChucNang.LoaiView == LoaiViewEnum.DetailView)
                                {
                                    loaiView = "DetailView";
                                }
                                if (itemChucNang.LoaiView == LoaiViewEnum.DashboardView)
                                {
                                    loaiView = "DashboardView";
                                }
                                chucNang.Id = string.Format("{0}_{1}", itemChucNang.AppObject.KeyObject, loaiView);
                                //
                                chucNang.Data = objecttype;
                                //
                                thuMuc.Items.Add(chucNang);
                                //
                                daThemChucNang = true;
                            }
                            ////Ẩn menu Hồ sơ, kiểm tra IQ của trường mầm non  ABI
                            //if (isAccess && user.CongTy.Oid.Equals(Config.KeyTanPhu))
                            //{
                            //    if (itemChucNang.AppObject.KeyObject.Equals("KiemTraIQ")
                            //          || itemChucNang.AppObject.KeyObject.Equals("HoSoNhapHoc")
                            //          || itemChucNang.AppObject.KeyObject.Equals("HoSoBaoLuu")
                            //          || itemChucNang.AppObject.KeyObject.Equals("HoSoThoiHoc")
                            //          || itemChucNang.AppObject.KeyObject.Equals("CauHoiKiemTraIQ")
                            //          || itemChucNang.AppObject.KeyObject.Equals("GiayTo"))
                            //    {
                            //    }
                            //    else
                            //    {
                            //        ChoiceActionItem chucNang = new ChoiceActionItem();
                            //        chucNang.ImageName = itemChucNang.HinhAnh;
                            //        chucNang.Caption = itemChucNang.TenChucNang;
                            //        string loaiView = string.Empty;
                            //        if (itemChucNang.LoaiView == LoaiViewEnum.ListView)
                            //        {
                            //            loaiView = "ListView";
                            //        }
                            //        if (itemChucNang.LoaiView == LoaiViewEnum.DetailView)
                            //        {
                            //            loaiView = "DetailView";
                            //        }
                            //        if (itemChucNang.LoaiView == LoaiViewEnum.DashboardView)
                            //        {
                            //            loaiView = "DashboardView";
                            //        }

                            //        chucNang.Id = string.Format("{0}_{1}", itemChucNang.AppObject.KeyObject, loaiView);
                            //        //
                            //        chucNang.Data = objecttype;
                            //        //
                            //        thuMuc.Items.Add(chucNang);
                            //        //
                            //        daThemChucNang = true;
                            //    }
                            //}

                            else if (isAccess) // Nếu có quyền truy cập or Bàn làm việc  mới tạo menu
                            {
                                //Ẩn Hồ sơ xét tuyển của THCS - THPT Tân phú
                                //if (itemChucNang.AppObject.KeyObject.Equals("HoSoXetTuyen"))
                                //{ }
                                //else
                                //{
                                ChoiceActionItem chucNang = new ChoiceActionItem();
                                chucNang.ImageName = itemChucNang.HinhAnh;
                                chucNang.Caption = itemChucNang.TenChucNang;
                                string loaiView = string.Empty;
                                if (itemChucNang.LoaiView == LoaiViewEnum.ListView)
                                {
                                    loaiView = "ListView";
                                }
                                if (itemChucNang.LoaiView == LoaiViewEnum.DetailView)
                                {
                                    loaiView = "DetailView";
                                }
                                if (itemChucNang.LoaiView == LoaiViewEnum.DashboardView)
                                {
                                    loaiView = "DashboardView";
                                }

                                chucNang.Id = string.Format("{0}_{1}", itemChucNang.AppObject.KeyObject, loaiView);
                                //
                                chucNang.Data = objecttype;
                                //
                                thuMuc.Items.Add(chucNang);
                                //
                                daThemChucNang = true;
                                //}
                            }
                        }
                    }
                    #endregion

                    #region 2. NonPersitent
                    if (itemChucNang.LoaiObject == LoaiObjectEnum.NonPersistent)
                    {
                        //
                        if (!itemChucNang.PhanHe.Oid.Equals("00000000-0000-0000-0000-000000000006"))
                        {
                            //Tìm object trong source
                            Type objecttype = assems.SelectMany(a => a.GetTypes()).Where(t => t.Name.Equals(itemChucNang.AppObject.KeyObject)).FirstOrDefault();
                            if (objecttype != null)
                            {
                                // Nếu có quyền truy cập  mới tạo menu
                                bool isAccess = Common.TaiKhoanHeThong() ? true : Common.CheckRolesBySecuritySystemRole_NonPersitent((((XPObjectSpace)_obs).Session), itemChucNang.PhanHe.Oid, itemChucNang.TenChucNang);

                                //
                                if (isAccess)
                                {
                                    ChoiceActionItem chucNang = new ChoiceActionItem();
                                    chucNang.ImageName = itemChucNang.HinhAnh;
                                    chucNang.Caption = itemChucNang.TenChucNang;
                                    chucNang.Id = string.Format("{0}_{1}", itemChucNang.AppObject.KeyObject, itemChucNang.LoaiView);
                                    //
                                    chucNang.Data = objecttype;
                                    //
                                    thuMuc.Items.Add(chucNang);
                                    //
                                    daThemChucNang = true;
                                }
                            }

                        }
                        else //Bàn làm việc
                        {
                            //
                            ChoiceActionItem chucNang = new ChoiceActionItem();
                            chucNang.ImageName = itemChucNang.HinhAnh;
                            chucNang.Caption = itemChucNang.TenChucNang;
                            chucNang.Id = string.Format("{0}_{1}", itemChucNang.AppObject.KeyObject, itemChucNang.LoaiView);
                            //
                            //chucNang.Data = typeof(StacticBase);
                            //
                            thuMuc.Items.Add(chucNang);
                            //
                            daThemChucNang = true;
                        }
                    }
                    #endregion
                    //
                    if (daThemChucNang)
                        e.NavigationItem.Items.Add(thuMuc);
                }
            }
        }

        private void nav_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            _obs = _application.CreateObjectSpace();
            //
            //Make an array for the list of assemblies.
            System.Reflection.Assembly[] assems = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("DevExpress.ExpressApp.Security") || a.FullName.Contains("ERP")).ToArray();

            #region ListView
            if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("_ListView"))
            {
                string objectName = e.ActionArguments.SelectedChoiceActionItem.Id.ToString().Substring(0, e.ActionArguments.SelectedChoiceActionItem.Id.Length - 9);

                //Tìm object trong source
                Type objecttype = assems.SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == objectName);
                if (objecttype == null)
                    return;

                string listViewId = Application.FindListViewId(objecttype);
                CollectionSourceBase cs = Application.CreateCollectionSource(_obs, objecttype, listViewId);
                //
                e.ActionArguments.ShowViewParameters.CreatedView = Application.CreateListView(listViewId, cs, true);
                e.ActionArguments.ShowViewParameters.Context = TemplateContext.View;
                e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
            }
            #endregion

            #region DetailView
            if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("_DetailView"))
            {
                DetailView view = null;
                if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("CauHinhChung"))
                {
                    //
                    CauHinhChung cauHingChung = _obs.GetObjectByKey<CauHinhChung>(Common.CauHinhChung_GetCauHinhChung.Oid);
                    view = Application.CreateDetailView(_obs, cauHingChung, true);
                }
                else
                {
                    //
                    string objectName = e.ActionArguments.SelectedChoiceActionItem.Id.ToString().Substring(0, e.ActionArguments.SelectedChoiceActionItem.Id.Length - 11);
                    if(objectName.Contains("BaoCao"))
                    {
                        objectName = e.ActionArguments.SelectedChoiceActionItem.Id.ToString().Substring(0, e.ActionArguments.SelectedChoiceActionItem.Id.Length - 17);
                    }
                    //Tìm object trong source
                    Type objecttype = assems.SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == objectName);
                    if (objecttype == null)
                        return;
                    //
                    object obj = _obs.CreateObject(objecttype);
                    //
                    view = Application.CreateDetailView(_obs, obj, true);
                }
                //
                view.ViewEditMode = ViewEditMode.Edit;
                e.ActionArguments.ShowViewParameters.CreatedView = view;
                e.ActionArguments.ShowViewParameters.Context = TemplateContext.View;
                e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
            }
            #endregion

            #region DashboardView
            if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("_DashboardView"))
            {
                //
                if (e.ActionArguments.SelectedChoiceActionItem.Id.Contains("ThongTinNhanVien"))
                {
                    e.ActionArguments.ShowViewParameters.CreatedView = Application.CreateDashboardView(_obs, "ThongTinNhanVien_DashboardView", true);
                    //
                    e.ActionArguments.ShowViewParameters.Context = TemplateContext.View;
                    e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                    //
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "window.location.reload();");
                }
                else
                {
                    e.ActionArguments.ShowViewParameters.CreatedView = Application.CreateDashboardView(_obs, e.ActionArguments.SelectedChoiceActionItem.Id, true);
                    //
                    e.ActionArguments.ShowViewParameters.Context = TemplateContext.View;
                    e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                    //
                }
            }
            #endregion
            //
        }

        private IModelListView GetListViewModel(object obj, string viewId, string criteria)
        {
            IModelListView modelListView = Application.FindModelView(viewId) as IModelListView;
            if (modelListView == null)
            {
                modelListView = Application.Model.Views.AddNode<IModelListView>(viewId);
                modelListView.ModelClass = Application.Model.BOModel.GetClass(obj.GetType());
                modelListView.MasterDetailMode = MasterDetailMode.ListViewAndDetailView;
                modelListView.Criteria = criteria;
            }
            return modelListView;
        }

        private void GetNavigationItemOfGroupReport(NavigationItemCreatedEventArgs e)
        {
            var groupReport = _obs.GetObjects<GroupReport>().Where(x => x.TenNhom.Equals("Tuyển sinh")
                                                                   || x.TenNhom.Equals("Tuyển sinh")).OrderBy(p => p.STT);
            //
            if (groupReport.Count() > 0)
            {
                ChoiceActionItem navItem = e.NavigationItem;
                SecuritySystemUser_Custom userCurrent = Common.SecuritySystemUser_GetCurrentUser();
                //
                SecuritySystemRole_Report phanQuyenBaoCao = userCurrent.SecuritySystemRole_Report;
                bool isAdmin = false;

                //
                if (userCurrent != null)
                    isAdmin = Common.QuanTriToanHeThong();

                //Make an array for the list of assemblies.
                System.Reflection.Assembly[] assems = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("DevExpress.ExpressApp.Security") || a.FullName.Contains("ERP")).ToArray();
                //

                ChoiceActionItem report = null;
                foreach (GroupReport item in groupReport)
                {
                    bool themGroup = false;
                    //
                    ChoiceActionItem chucNang = new ChoiceActionItem();
                    chucNang.ImageName = "BO_Folder";
                    chucNang.Caption = item.TenNhom;
                    if (phanQuyenBaoCao != null)
                    {
                        //Lấy danh sách báo cáo theo nhóm (chỉ lấy nhóm nào thực sự cho web)
                        //CriteriaOperator filter = CriteriaOperator.Parse("GroupReport = ?", item.Oid);
                        //XPCollection<ReportData_Custom> reportList = new XPCollection<ReportData_Custom>(((XPObjectSpace)_obs).Session, filter);
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@Quyen", phanQuyenBaoCao.Quyen.ToString());
                        param[1] = new SqlParameter("@GroupReport", item.Oid);
                        DataTable dt = DataProvider.GetDataTable("spd_HeThong_GetReport", CommandType.StoredProcedure, param);
                        //
                        ReportData_Custom rpt = null;

                        int i = 0;
                        foreach (DataRow itemReport in dt.Rows)
                        {
                            rpt = ((XPObjectSpace)_obs).Session.FindObject<ReportData_Custom>(CriteriaOperator.Parse("Oid =?", itemReport["Oid"]));
                            bool daThemBaoCao = false;
                            //
                            if (rpt != null)
                            {
                                //if (isAdmin )
                                {
                                    string idReport = rpt.DataTypeName != null ? rpt.DataTypeName.Replace("ERP.Module.Report.NhanSu.TuyenSinh.", "") : string.Empty;
                                    idReport = idReport.Replace("ERP.Module.BaoCao.TuyenSinh.", "");
                                    idReport = idReport.Replace("ERP.Module.BaoCao.TuyenSinh_TP.", "");
                                    //
                                    Type objecttype = assems.SelectMany(a => a.GetTypes()).Where(t => t.Name.Equals(idReport)).FirstOrDefault();
                                    if (objecttype != null)
                                    {
                                        report = new ChoiceActionItem();
                                        report.ImageName = "BO_Report";
                                        report.Caption = rpt.ReportName;
                                        report.Id = idReport + "_DetailView" + "-" + i.ToString("0000#");
                                        report.Data = objecttype;
                                        i++;
                                        //
                                        daThemBaoCao = true;
                                    }
                                }
                            }

                            //
                            if (daThemBaoCao) // Thêm báo cáo vào group
                            {
                                chucNang.Items.Add(report);
                                //
                                themGroup = true;
                            }
                            //
                        }
                    }
                    //
                    if (themGroup) // Thêm group
                    {
                        navItem.Items.Add(chucNang);
                    }
                }
            }
        }

    }
}
