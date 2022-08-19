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
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using System.Windows.Forms;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Win.Editors;
using ERP.Module.HeThong;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.CauHinhChungs;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Win.Templates.ActionContainers;
using DevExpress.XtraNavBar;
using DevExpress.XtraTreeList.Nodes;
using System.Drawing;
using ERP.Module.Properties;
using System.Data;
using System.Reflection;
using System.IO;
using System.Reflection.Emit;
using System.Resources;
using ERP.NormalizationData;
using ERP.Module.NonPersistentObjects.NhanSu;
using DevExpress.ExpressApp.ScriptRecorder.Win;
using DevExpress.XtraRichEdit.API.Word;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.NonPersistentObjects.ReportCustom;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_CreateNavigationMenuItemController : ShowNavigationItemController
    {
        IObjectSpace _obs = null;
        Session session;
        bool laQuanTri;
        SecuritySystemUser_Custom currentUser;
        string _group = "";
         XPCollection<getThuMuc> listThuMuc;
         XPCollection<getChucNang> listChucNang;

        public HeThong_CreateNavigationMenuItemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            //Khởi tạo giá trị cho các biến
            _obs = Application.CreateObjectSpace();
            session = ((XPObjectSpace)_obs).Session;
            laQuanTri = Common.QuanTriToanHeThong();
            currentUser = Common.SecuritySystemUser_GetCurrentUser();

            listThuMuc = new XPCollection<getThuMuc>(session, false);
            listChucNang = new XPCollection<getChucNang>(session, false);
            //Viết lại hàm này để load Navigation Menu
            Frame.ProcessActionContainer += Frame_ProcessActionContainer;

            #region Hỗ trợ HeThong_ScriptRecorderController
            //Tắt nút show và pause
            WinScriptRecorderController _script = Frame.GetController<WinScriptRecorderController>();
            _script.ShowScriptAction.Active["TruyCap"] = false;
            _script.PauseRecordAction.Active["TruyCap"] = false;

            //Replace password lại
            string _text = DevExpress.ExpressApp.ScriptRecorder.Logger.Instance.Script.ScriptLog;
            try
            {
                _text = _text.Remove(0, _text.LastIndexOf("*Action Đăng nhập"));
            }
            catch (Exception)
            {

                _text = _text.Remove(0, _text.LastIndexOf("*Action Log On"));
            }
            DevExpress.ExpressApp.ScriptRecorder.Logger.Instance.Script.ScriptLog = _text;
            #endregion
        }


        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            Frame.ProcessActionContainer -= Frame_ProcessActionContainer;
        }

        void Frame_ProcessActionContainer(object sender, ProcessActionContainerEventArgs e)
        {
            //Phải kiểm tra kiểu của e.ActionContainer
            if (e.ActionContainer is NavigationActionContainer)
            {
                NavigationActionContainer container = ((NavigationActionContainer)e.ActionContainer);
                NavBarControl navBar = ((NavBarControl)container.NavigationControl);

                //Hàm này để gọi khi chọn group phân hệ
                navBar.ActiveGroupChanged += navBar_ActiveGroupChanged;

                //Nếu group đã load rồi thì không load nữa
                if (navBar.Groups.Count == 0)
                {
                    //
                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("@User", currentUser.Oid);
                    SqlCommand cmd = DataProvider.GetCommand("spd_HeThong_GetMenu_New", CommandType.StoredProcedure, p);
                    DataSet dataset = DataProvider.GetDataSet(cmd);
                    //DataTable tb = DataProvider.GetDataTable("spd_HeThong_GetMenuGroup", System.Data.CommandType.StoredProcedure, p);
                    if (dataset != null)
                    {

                        #region Table Thư mục
                        DataTable dtThuMuc = dataset.Tables[1];
                        foreach (DataRow iThuMuc in dtThuMuc.Rows)
                        {
                            getThuMuc tm = new getThuMuc(session);
                            tm.TenThuMuc = iThuMuc["TenChucNang"].ToString();
                            tm.TenPhanHe = iThuMuc["TenPhanHe"].ToString();
                            tm.Oid = new Guid(iThuMuc["Oid"].ToString());
                            listThuMuc.Add(tm);
                        }
                        #endregion

                        #region Table Chức năng
                        DataTable dtChucNang = dataset.Tables[2];
                        foreach (DataRow iChucNang in dtChucNang.Rows)
                        {
                            getChucNang cNang = new getChucNang(session);
                            cNang.TenChucNang = iChucNang["TenChucNang"].ToString();
                            cNang.TenPhanHe = iChucNang["TenPhanHe"].ToString();
                            cNang.LoaiView = iChucNang["LoaiView"].ToString();
                            cNang.LoaiObject = iChucNang["LoaiObject"].ToString();
                            cNang.KeyObject = iChucNang["KeyObject"].ToString();
                            if (iChucNang["ListViewId"].ToString() != string.Empty)
                                cNang.ListViewId = iChucNang["ListViewId"].ToString();
                            cNang.LoaiCustom = iChucNang["LoaiCustom"].ToString();
                            if (iChucNang["ThuMucQuanLy"].ToString() != "")
                                cNang.ThuMucQuanLy = new Guid(iChucNang["ThuMucQuanLy"].ToString());
                            cNang.Oid = new Guid(iChucNang["OidApp"].ToString());
                            listChucNang.Add(cNang);
                        }

                        #endregion

                        #region Tạo menu PhanHe
                        DataTable dtPhanHe = dataset.Tables[0];
                        foreach (DataRow item in dtPhanHe.Rows)
                        {
                            NavBarGroup group = new NavBarGroup();
                            group.Caption = item["TenPhanHe"].ToString();
                            if (item["MaQuanLy"].ToString() == "Nav_Admissions")
                                group.Name = "Nav_Admission";
                            else
                                group.Name = item["MaQuanLy"].ToString();

                            #region Load hình
                            ResourceManager rm = Resources.ResourceManager;
                            group.SmallImage = (Bitmap)rm.GetObject(group.Name);
                            group.LargeImage = (Bitmap)rm.GetObject(group.Name);
                            if(group.SmallImage==null)
                            {
                                group.SmallImage = (Bitmap)rm.GetObject("Nav_Asset");
                            }
                            if (group.LargeImage == null)
                            {
                                group.LargeImage = (Bitmap)rm.GetObject("Nav_Asset");
                            }
                            #endregion

                            navBar.Groups.Add(group);

                            #region Gắn thư mục
                            if (group.Tag == null)
                            {
                                //Set thuộc tính Navigation
                                group.GroupStyle = NavBarGroupStyle.ControlContainer;
                                TreeListNavigationControl treeControl = new TreeListNavigationControl();

                                //Thêm Resources hình cho Navigation
                                ResourceManager rm1 = Resources.ResourceManager;
                                ((DevExpress.Utils.ImageCollection)treeControl.StateImageList).AddImage((Bitmap)rm1.GetObject("BO_Folder"));
                                ((DevExpress.Utils.ImageCollection)treeControl.StateImageList).AddImage((Bitmap)rm1.GetObject("BO_List"));
                                ((DevExpress.Utils.ImageCollection)treeControl.StateImageList).AddImage((Bitmap)rm1.GetObject("BO_Report"));

                                //Add loại Navigation là tree vào
                                group.ControlContainer = new NavBarGroupControlContainer ();//
                                group.ControlContainer.Controls.Add(treeControl);

                                //Kiểm tra nếu Group là report thì sẽ load kiểu khác
                                if (!group.Name.ToString().Contains("Nav_Report"))
                                {
                                    treeControl.BeforeExpand += thuMuc_BeforeExpand; //Hàm giải nén thư mục
                                    treeControl.FocusedNodeChanged += thuMuc_FocusedNodeChanged; //Hàm check mở chức năng
                                    List<getThuMuc> l = (from d in listThuMuc
                                                         where d.TenPhanHe.ToString() == item["TenPhanHe"].ToString()
                                                         select d).ToList();
                                    foreach (getThuMuc itemThuMuc in l)
                                    {                                        
                                        if (itemThuMuc.TenPhanHe == group.Caption)
                                        {//Tạo mới add từng node vào tree
                                            TreeListNode node = treeControl.AppendNode(new object[] { itemThuMuc.TenThuMuc.ToString() }, null);
                                            node.Tag = itemThuMuc.Oid.ToString();
                                            node.StateImageIndex = 0; //Chọn index của hình là 0 (thư mục)
                                            //
                                            node.HasChildren = true; //Gắn thuộc tính có danh sách con cho thư mục
                                            group.Tag = "true"; //Gắn Tag để sau này check là Tag != null nghĩa là đã load rồi thì sẽ không load nữa
                                        }

                                    }
                                }
                                else
                                {
                                    treeControl.BeforeExpand += report_BeforeExpand;
                                    treeControl.FocusedNodeChanged += report_FocusedNodeChanged;

                                    SqlParameter[] px = new SqlParameter[1];
                                    DataTable tb = DataProvider.GetDataTable("spd_HeThong_GetMenuReport", System.Data.CommandType.StoredProcedure, px);

                                    foreach (DataRow itemThuMuc in tb.Rows)
                                    {
                                        TreeListNode node = treeControl.AppendNode(new object[] { itemThuMuc["TenPhanHe"].ToString() }, null);
                                        node.Tag = itemThuMuc["Oid"];
                                        node.StateImageIndex = 0;
                                        //
                                        node.HasChildren = true;
                                    }
                                    group.Tag = "true";

                                }
                            }
                            #endregion
                        }
                        NavBarGroup groupReport = new NavBarGroup();
                        groupReport.Caption = "Báo cáo";
                        groupReport.Name = "Nav_Report";

                        //Load hình
                        ResourceManager rm1x = Resources.ResourceManager;
                        groupReport.SmallImage = (Bitmap)rm1x.GetObject(groupReport.Name);
                        groupReport.LargeImage = (Bitmap)rm1x.GetObject(groupReport.Name);

                        navBar.Groups.Add(groupReport);
                        #endregion
                    }
                }
            }
        }

        void navBar_ActiveGroupChanged(object sender, NavBarGroupEventArgs e)
        {
            _group = e.Group.Caption;

            //Nếu group đã load rồi thì không load nữa - Đã bỏ
            if (e.Group.Tag == null)
            {
                //Set thuộc tính Navigation
                e.Group.GroupStyle = NavBarGroupStyle.ControlContainer;
                TreeListNavigationControl treeControl = new TreeListNavigationControl();

                //Thêm Resources hình cho Navigation
                ResourceManager rm1 = Resources.ResourceManager;
                ((DevExpress.Utils.ImageCollection)treeControl.StateImageList).AddImage((Bitmap)rm1.GetObject("BO_Folder"));
                ((DevExpress.Utils.ImageCollection)treeControl.StateImageList).AddImage((Bitmap)rm1.GetObject("BO_List"));
                ((DevExpress.Utils.ImageCollection)treeControl.StateImageList).AddImage((Bitmap)rm1.GetObject("BO_Report"));

                //Add loại Navigation là tree vào
                e.Group.ControlContainer.Controls.Add(treeControl);

                //Kiểm tra nếu Group là report thì sẽ load kiểu khác
                if (!e.Group.Name.ToString().Contains("Nav_Report"))
                {
                    treeControl.BeforeExpand += thuMuc_BeforeExpand; //Hàm giải nén thư mục
                    treeControl.FocusedNodeChanged += thuMuc_FocusedNodeChanged; //Hàm check mở chức năng
                    List<getThuMuc> l = (from d in listThuMuc
                                         where d.TenPhanHe.ToString() == e.Group.Caption.ToString()
                                         select d).ToList();
                    foreach (getThuMuc itemThuMuc in l)
                    {
                        var ij = itemThuMuc.TenThuMuc;
                        if (itemThuMuc.TenPhanHe == e.Group.Caption)
                        {//Tạo mới add từng node vào tree
                            TreeListNode node = treeControl.AppendNode(new object[] { itemThuMuc.TenThuMuc.ToString() }, null);
                            node.Tag = itemThuMuc.Oid.ToString();
                            node.StateImageIndex = 0; //Chọn index của hình là 0 (thư mục)
                            //
                            node.HasChildren = true; //Gắn thuộc tính có danh sách con cho thư mục
                            e.Group.Tag = "true"; //Gắn Tag để sau này check là Tag != null nghĩa là đã load rồi thì sẽ không load nữa
                        }

                    }
                }
                else
                {

                    treeControl.BeforeExpand += report_BeforeExpand;
                    treeControl.FocusedNodeChanged += report_FocusedNodeChanged;

                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("@User", currentUser.Oid);
                    DataTable tb = DataProvider.GetDataTable("spd_HeThong_GetMenuReport", System.Data.CommandType.StoredProcedure, p);

                    foreach (DataRow itemThuMuc in tb.Rows)
                    {
                        TreeListNode node = treeControl.AppendNode(new object[] { itemThuMuc["TenPhanHe"].ToString() }, null);
                        node.Tag = itemThuMuc["Oid"];
                        node.StateImageIndex = 0;
                        //
                       // node.HasChildren = true;//Report - code cũ
                    }
                    e.Group.Tag = "true";

                }
            }
        }
        void thuMuc_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node.HasChildren == false)
            { 
                string clasCaption = e.Node.GetDisplayText(0);
                //
                if (e.Node.ParentNode != null)
                    DevExpress.ExpressApp.ScriptRecorder.Logger.Instance.Script.ScriptLog += "\r\n*Action Navigation (" + _group + "." + e.Node.ParentNode.GetDisplayText(0) + "." + clasCaption + ")";
                //
                using (DialogUtil.Wait(null,clasCaption, "Đang truy cập chức năng" ))
                {
                    //Tạo bộ nhớ mới
                    _obs = Application.CreateObjectSpace();

                    //Tắt ghi nhận truy cập thông tin nhân viên lần đâu
                    Common.AcessedCategorizedListEditor = false;
                    var a = e.Node.Tag;
                    #region ListView
                    if (e.Node.Tag.ToString().Contains("_ListView")
                        && !e.Node.Tag.ToString().Contains("_ListViewCustom"))
                    {
                        string objectName = e.Node.Tag.ToString().ToString().Substring(0, e.Node.Tag.ToString().Length - 9);

                        //Tìm object trong source
                        Type objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == objectName);
                        if (objecttype == null)
                            return;

                        string listViewId = Application.FindListViewId(objecttype);
                        CollectionSourceBase cs = Application.CreateCollectionSource(_obs, objecttype, listViewId);

                        ShowViewParameters showPara = new ShowViewParameters();
                        showPara.CreatedView = Application.CreateListView(listViewId, cs, true);
                        showPara.Context = TemplateContext.View;
                        showPara.TargetWindow = TargetWindow.Current;

                        Application.ShowViewStrategy.ShowView(showPara, new ShowViewSource(Frame, null));

                    }
                    #endregion

                    #region ListViewCustom
                    if (e.Node.Tag.ToString().Contains("_ListViewCustom"))
                    {
                        string objectName = e.Node.Tag.ToString().ToString().Split('_')[0];

                        //Tìm object trong source
                        Type objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == objectName);
                        if (objecttype == null)
                            return;

                        string listViewId = e.Node.Tag.ToString().ToString().Substring(objectName.Length + 1, e.Node.Tag.ToString().Length - objectName.Length - 7); //Application.FindListViewId(objecttype);
                        CollectionSourceBase cs = Application.CreateCollectionSource(_obs, objecttype, listViewId);
                        //
                        ShowViewParameters showPara = new ShowViewParameters();
                        showPara.CreatedView = Application.CreateListView(listViewId, cs, true);
                        showPara.Context = TemplateContext.View;
                        showPara.TargetWindow = TargetWindow.Current;

                        Application.ShowViewStrategy.ShowView(showPara, new ShowViewSource(Frame, null));
                    }
                    #endregion

                    #region DetailView
                    if (e.Node.Tag.ToString().Contains("_DetailView"))
                    {
                        DetailView view = null;
                        if (e.Node.Tag.ToString().Contains("CauHinhChung"))
                        {
                            //
                            CauHinhChung cauHingChung = _obs.GetObjectByKey<CauHinhChung>(Common.CauHinhChung_GetCauHinhChung.Oid);
                            view = Application.CreateDetailView(_obs, cauHingChung, true);
                        }
                        else
                        {
                            if(clasCaption.Contains("Nhân viên đang làm việc (New)"))
                            {
                                NhanSuCustomView._DaNghiViec = 0;
                            }
                            else if (clasCaption.Contains("Nhân viên đã nghỉ việc (New)"))
                            {
                                NhanSuCustomView._DaNghiViec = 1;
                            }
                            else if (clasCaption.Contains("Không nhận việc"))
                            {
                                NhanSuCustomView._DaNghiViec = 2;
                            }

                            if (clasCaption.Contains("Giảng viên thỉnh giảng (New)"))
                            {
                                ThinhGiangCustomView._DaNghiViec = 0;
                            }
                            else if (clasCaption.Contains("Giảng viên thỉnh giảng đã nghỉ việc (New)"))
                            {
                                ThinhGiangCustomView._DaNghiViec = 1;
                            }

                            //
                            string objectName = e.Node.Tag.ToString().ToString().Substring(0, e.Node.Tag.ToString().Length - 11);
                            //Tìm object trong source
                            Type objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == objectName);
                            if (objecttype == null)
                                return;
                            //
                            object obj = _obs.CreateObject(objecttype);
                            //
                            view = Application.CreateDetailView(_obs, obj, true);
                        }
                        //
                        view.ViewEditMode = ViewEditMode.Edit;

                        ShowViewParameters showPara = new ShowViewParameters();
                        showPara.CreatedView = view;
                        showPara.Context = TemplateContext.View;
                        showPara.TargetWindow = TargetWindow.Current;

                        Application.ShowViewStrategy.ShowView(showPara, new ShowViewSource(Frame, null));
                    }
                    #endregion

                    #region DashboardView
                    if (e.Node.Tag.ToString().Contains("_DashboardView"))
                    {
                        ShowViewParameters showPara = new ShowViewParameters();
                        showPara.Context = TemplateContext.View;
                        showPara.TargetWindow = TargetWindow.Current;

                        if (e.Node.Tag.ToString().Contains("QuyTrinh"))
                        {
                            showPara.CreatedView = Application.CreateDashboardView(_obs, "QuyTrinh_DashboardView", true);
                        }
                        else
                        {
                            if (e.Node.Tag.ToString() == "ThongKeChucVu_DashboardView")
                            {
                                ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.ThongKeFactory.Type = ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.LoaiThongKeEnum.ThongKeChucVu;
                            }
                            if (e.Node.Tag.ToString() == "ThongKeDoTuoi_DashboardView")
                            {
                                ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.ThongKeFactory.Type = ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.LoaiThongKeEnum.ThongKeDoTuoi;
                            }
                            if (e.Node.Tag.ToString() == "ThongKeGioiTinh_DashboardView")
                            {
                                ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.ThongKeFactory.Type = ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.LoaiThongKeEnum.ThongKeGioiTinh;
                            }
                            if (e.Node.Tag.ToString() == "ThongKeThamNien_DashboardView")
                            {
                                ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.ThongKeFactory.Type = ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.LoaiThongKeEnum.ThongKeThamNien;
                            }
                            if (e.Node.Tag.ToString() == "ThongKeLoaiNhanSu_DashboardView")
                            {
                                ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.ThongKeFactory.Type = ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.LoaiThongKeEnum.ThongKeLoaiNhanSu;
                            }
                            if (e.Node.Tag.ToString() == "ThongKeSoLuongNhanSu_DashboardView")
                            {
                                ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.ThongKeFactory.Type = ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.LoaiThongKeEnum.ThongKeSoLuongNhanSu;
                            }
                            if (e.Node.Tag.ToString() == "ThongKeLoaiHopDong_DashboardView")
                            {
                                ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.ThongKeFactory.Type = ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.LoaiThongKeEnum.ThongKeLoaiHopDong;
                            }
                            if (e.Node.Tag.ToString() == "ThongKeNgachLuong_DashboardView")
                            {
                                ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.ThongKeFactory.Type = ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.LoaiThongKeEnum.ThongKeNgachLuong;
                            }
                            if (e.Node.Tag.ToString() == "ThongKeTrinhDoChuyenMon_DashboardView")
                            {
                                ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.ThongKeFactory.Type = ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.LoaiThongKeEnum.ThongKeTrinhDoChuyenMon;
                            }
                            if (e.Node.Tag.ToString() == "ThongKeTyLeThoiViec_DashboardView")
                            {
                                ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.ThongKeFactory.Type = ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.LoaiThongKeEnum.ThongKeTyLeThoiViec;
                            }
                            if (e.Node.Tag.ToString() == "ThongKeBienDongSoLuongNhanSu_DashboardView")
                            {
                                ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.ThongKeFactory.Type = ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.LoaiThongKeEnum.ThongKeBienDongSoLuongNhanSu;
                            }
                            if (e.Node.Tag.ToString() == "ThongKeTongHopBienDongNhanSu_DashboardView")
                            {
                                ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.ThongKeFactory.Type = ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe.LoaiThongKeEnum.ThongKeTongHopBienDongNhanSu;
                            }

                            showPara.CreatedView = Application.CreateDashboardView(_obs, "ThongKe_DashboardView", true);
                        }

                        Application.ShowViewStrategy.ShowView(showPara, new ShowViewSource(Frame, null));
                    }
                    #endregion

                }
            }
        }

        void thuMuc_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
                return;

            //using (DialogUtil.Wait(null, "Đang xử lý...", "Vui lòng chờ"))
            {
                List<getChucNang> list = (from d in listChucNang
                                       where d.ThuMucQuanLy.ToString() == e.Node.Tag.ToString()
                                       select d).ToList();

                string LoaiObject = "";
                foreach (getChucNang i in list)
                {
                    #region Cũ
                    //AppMenu itemChucNang = session.GetObjectByKey<AppMenu>(new Guid(i.Oid.ToString()));
                    //TreeListNode node = null;

                    //#region 1. Persitent
                    //if (itemChucNang.LoaiObject == LoaiObjectEnum.Persistent)
                    //{
                    //    string Id = "";

                    //    string loaiView = string.Empty;
                    //    if (itemChucNang.LoaiView == LoaiViewEnum.ListView)
                    //    {
                    //        loaiView = "ListView";
                    //        Id = string.Format("{0}_{1}", itemChucNang.AppObject.KeyObject, loaiView);
                    //    }
                    //    else if (itemChucNang.LoaiView == LoaiViewEnum.DetailView)
                    //    {
                    //        loaiView = "DetailView";
                    //        Id = string.Format("{0}_{1}", itemChucNang.AppObject.KeyObject, loaiView);
                    //    }
                    //    else if (itemChucNang.LoaiView == LoaiViewEnum.ListViewCustom)
                    //    {
                    //        loaiView = "ListViewCustom";
                    //        Id = string.Format("{0}_{1}Custom", itemChucNang.AppObject.KeyObject, itemChucNang.ListViewId, loaiView);
                    //    }
                    //    else
                    //    {
                    //        string loaiCustom = string.Empty;
                    //        if (itemChucNang.LoaiCustom == LoaiCustomEnum.DangLamViec)
                    //        {
                    //            loaiCustom = "DangLamViec";
                    //        }
                    //        else if (itemChucNang.LoaiCustom == LoaiCustomEnum.DaNghiViec)
                    //        {
                    //            loaiCustom = "DaNghiViec";
                    //        }
                    //        else if (itemChucNang.LoaiCustom == LoaiCustomEnum.KhongNhanViec)
                    //        {
                    //            loaiCustom = "KhongNhanViec";
                    //        }
                    //        else if (itemChucNang.LoaiCustom == LoaiCustomEnum.Category1)
                    //        {
                    //            loaiCustom = "Category1";
                    //        }
                    //        else if (itemChucNang.LoaiCustom == LoaiCustomEnum.Category2)
                    //        {
                    //            loaiCustom = "Category2";
                    //        }
                    //        else if (itemChucNang.LoaiCustom == LoaiCustomEnum.GiangVienThinhGiang)
                    //        {
                    //            loaiCustom = "GiangVienThinhGiang";
                    //        }
                    //        else if (itemChucNang.LoaiCustom == LoaiCustomEnum.GiangVienThinhGiangDaNghiViec)
                    //        {
                    //            loaiCustom = "GiangVienThinhGiangDaNghiViec";
                    //        }

                    //        loaiView = string.Format("{0}_CategorizedListEditor", loaiCustom);
                    //        Id = string.Format("{0}_{1}", itemChucNang.AppObject.KeyObject, loaiView);

                    //    }
                    //    node = ((TreeListNavigationControl)sender).AppendNode(new object[] { itemChucNang.TenChucNang }, e.Node);
                    //    node.Tag = Id;
                    //    node.StateImageIndex = 1;
                    //    node.HasChildren = false;
                    //}
                    //#endregion

                    //#region 2. NonPersitent
                    //if (itemChucNang.LoaiObject == LoaiObjectEnum.NonPersistent)
                    //{
                    //    string Id = string.Format("{0}_{1}", itemChucNang.AppObject.KeyObject, itemChucNang.LoaiView);
                    //    node = ((TreeListNavigationControl)sender).AppendNode(new object[] { itemChucNang.TenChucNang }, e.Node);
                    //    node.Tag = Id;
                    //    node.StateImageIndex = 1;
                    //    node.HasChildren = false;

                    //}
                    //#endregion
                    #endregion

                    #region Mới
                    
                          //AppMenu itemChucNang = session.GetObjectByKey<AppMenu>(new Guid(i.Oid.ToString()));
                    TreeListNode node = null;
                    LoaiObject = i.LoaiObject;

                    #region 1. Persitent
                    if (LoaiObject == "Persistent")
                    {
                        string Id = "";

                        string loaiView = i.LoaiView;
                        if (loaiView == "ListViewCustom")
                            Id = string.Format("{0}_{1}Custom", i.KeyObject, i.ListViewId, loaiView);
                        else
                            if (!loaiView.Contains("CategorizedListEditor"))
                            {
                                Id = string.Format("{0}_{1}", i.KeyObject, loaiView);
                            }
                            else
                            {
                                string loaiCustom = string.Empty;
                                if (Convert.ToInt32(i.LoaiCustom) == 0)
                                {
                                    loaiCustom = "DangLamViec";
                                }
                                else if (Convert.ToInt32(i.LoaiCustom) == 1)
                                {
                                    loaiCustom = "DaNghiViec";
                                }
                                else if (Convert.ToInt32(i.LoaiCustom) == 2)
                                {
                                    loaiCustom = "KhongNhanViec";
                                }
                                else if (Convert.ToInt32(i.LoaiCustom) == 3)
                                {
                                    loaiCustom = "GiangVienThinhGiang";
                                }
                                else if (Convert.ToInt32(i.LoaiCustom) == 4)
                                {
                                    loaiCustom = "Category1";
                                }
                                else if (Convert.ToInt32(i.LoaiCustom) == 5)
                                {
                                    loaiCustom = "Category2";
                                }
                                else if (Convert.ToInt32(i.LoaiCustom) == 6)
                                {
                                    loaiCustom = "GiangVienThinhGiangDaNghiViec";
                                }

                                loaiView = string.Format("{0}_CategorizedListEditor", loaiCustom);
                                Id = string.Format("{0}_{1}", i.KeyObject, loaiView);

                            }
                        node = ((TreeListNavigationControl)sender).AppendNode(new object[] { i.TenChucNang }, e.Node);
                        node.Tag = Id;
                        node.StateImageIndex = 1;
                        node.HasChildren = false;
                    }
                    #endregion

                    #region 2. NonPersitent
                    if (LoaiObject == "NonPersistent")
                    {
                        string Id = string.Format("{0}_{1}", i.KeyObject, i.LoaiView);
                        node = ((TreeListNavigationControl)sender).AppendNode(new object[] { i.TenChucNang }, e.Node);
                        node.Tag = Id;
                        node.StateImageIndex = 1;
                        node.HasChildren = false;
                    }
                    #endregion

                    #endregion
                }
            }
        }

        void report_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node.HasChildren == false)
            {
                using (DialogUtil.Wait())
                {
                    if (e.Node.ParentNode != null)
                        DevExpress.ExpressApp.ScriptRecorder.Logger.Instance.Script.ScriptLog += "\r\n*Action Navigation (" + _group + e.Node.ParentNode.GetDisplayText(0) + "." + e.Node.GetDisplayText(0) + ")";

                    _obs = Application.CreateObjectSpace();
                    Type objecttype;
                    string objectName;
                    ShowViewParameters showPara;

                    #region Mới

                    DetailView view;
                    objectName = e.Node.Tag.ToString().ToString().Substring(0, e.Node.Tag.ToString().Length - 11);
                    var phanhe = e.Node.Tag;
                    //Tìm object trong source
                    objecttype = typeof(ReportCustomView);
                    if (objecttype == null)
                        return;
                    //
                    object obj = _obs.CreateObject(objecttype);
                    ((ReportCustomView)obj).PhanHe = new Guid(phanhe.ToString());
                    //
                    view = Application.CreateDetailView(_obs, obj, true);
                    view.ViewEditMode = ViewEditMode.Edit;

                    showPara = new ShowViewParameters();
                    showPara.CreatedView = view;
                    showPara.Context = TemplateContext.View;
                    showPara.TargetWindow = TargetWindow.Current;

                    Application.ShowViewStrategy.ShowView(showPara, new ShowViewSource(Frame, null));
                    #endregion
                    //

                    #region code cũ

                    //bool isAdmin = Common.QuanTriToanHeThong();

                    //string[] oidList = null;
                    //if (!isAdmin)
                    //    oidList = currentUser.SecuritySystemRole_Report.Quyen.ToString().Split(';');

                    ////
                    //IModelListView listViewNode = (IModelListView)Application.Model.Views[string.Format("ReportData_Custom_{0}_ListView", e.Node.Tag)];
                    //if (listViewNode == null)
                    //{
                    //    listViewNode = Application.Model.Views.AddNode<IModelListView>(string.Format("ReportData_Custom_{0}_ListView", e.Node.Tag));
                    //    listViewNode.ModelClass = Application.Model.BOModel.GetClass(typeof(ReportData_Custom));
                    //    listViewNode.Caption = string.Format("Báo cáo {0}", e.Node.ParentNode.GetDisplayText(e.Node.ParentNode.Id));
                    //    listViewNode.MasterDetailMode = MasterDetailMode.ListViewAndDetailView;
                    //    listViewNode.DetailView = Application.Model.Views["ReportData_Custom_Short_DetailView"] as IModelDetailView;
                    //    listViewNode.MasterDetailView = Application.Model.Views["ReportData_Custom_Short_DetailView"] as IModelDetailView;
                    //    listViewNode.ImageName = "BO_Report";
                    //    listViewNode.AllowNew = false;
                    //}

                    //IModelColumn column = listViewNode.Columns["ReportName"];
                    //if (column == null)
                    //{
                    //    column = listViewNode.Columns.AddNode<IModelColumn>("ReportName");
                    //    column.PropertyName = "ReportName";
                    //    column.Caption = "Tên báo cáo";
                    //    column.SortIndex = 0;
                    //    column.Width = 200;
                    //    column.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    //    column.PropertyEditorType = typeof(StringPropertyEditor);
                    //}


                    ////Tìm object trong source
                    //objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == "ReportData_Custom");
                    //if (objecttype == null)
                    //    return;

                    //string listViewId = string.Format("ReportData_Custom_{0}_ListView", e.Node.Tag);
                    //CollectionSourceBase cs = Application.CreateCollectionSource(_obs, objecttype, listViewId);
                    //cs.Criteria.Add("Filter", CriteriaOperator.Parse("GroupReport =?", e.Node.Tag));
                    //if (!isAdmin)
                    //{
                    //    cs.Criteria.Add("Filter1", new InOperator("Oid", oidList));
                    //}

                    //showPara = new ShowViewParameters();
                    //showPara.CreatedView = Application.CreateListView(string.Format("ReportData_Custom_{0}_ListView", e.Node.Tag), cs, true);
                    //showPara.Context = TemplateContext.View;
                    //showPara.TargetWindow = TargetWindow.Current;

                    //Application.ShowViewStrategy.ShowView(showPara, new ShowViewSource(Frame, null));

                    #endregion
                }
            }
        }

        void report_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
                return;

            using (DialogUtil.Wait())
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@User", currentUser.Oid);
                p[1] = new SqlParameter("@PhanHe", e.Node.Tag.ToString());
                SqlCommand cmd = DataProvider.GetCommand("spd_HeThong_GetMenuReport", CommandType.StoredProcedure, p);
                DataSet dts = DataProvider.GetDataSet(cmd);

                foreach (DataRow item in dts.Tables[0].Rows)
                {
                    TreeListNode node = ((TreeListNavigationControl)sender).AppendNode(new object[] { item["TenNhom"] }, e.Node);
                    node.Tag = item["Oid"];
                    node.StateImageIndex = 2;
                    node.HasChildren = false;
                }
            }
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

    }
}
