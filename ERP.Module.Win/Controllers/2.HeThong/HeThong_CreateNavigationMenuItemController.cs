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

            //Kh???i t???o gi?? tr??? cho c??c bi???n
            _obs = Application.CreateObjectSpace();
            session = ((XPObjectSpace)_obs).Session;
            laQuanTri = Common.QuanTriToanHeThong();
            currentUser = Common.SecuritySystemUser_GetCurrentUser();

            listThuMuc = new XPCollection<getThuMuc>(session, false);
            listChucNang = new XPCollection<getChucNang>(session, false);
            //Vi???t l???i h??m n??y ????? load Navigation Menu
            Frame.ProcessActionContainer += Frame_ProcessActionContainer;

            #region H??? tr??? HeThong_ScriptRecorderController
            //T???t n??t show v?? pause
            WinScriptRecorderController _script = Frame.GetController<WinScriptRecorderController>();
            _script.ShowScriptAction.Active["TruyCap"] = false;
            _script.PauseRecordAction.Active["TruyCap"] = false;

            //Replace password l???i
            string _text = DevExpress.ExpressApp.ScriptRecorder.Logger.Instance.Script.ScriptLog;
            try
            {
                _text = _text.Remove(0, _text.LastIndexOf("*Action ????ng nh???p"));
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
            //Ph???i ki???m tra ki???u c???a e.ActionContainer
            if (e.ActionContainer is NavigationActionContainer)
            {
                NavigationActionContainer container = ((NavigationActionContainer)e.ActionContainer);
                NavBarControl navBar = ((NavBarControl)container.NavigationControl);

                //H??m n??y ????? g???i khi ch???n group ph??n h???
                navBar.ActiveGroupChanged += navBar_ActiveGroupChanged;

                //N???u group ???? load r???i th?? kh??ng load n???a
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

                        #region Table Th?? m???c
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

                        #region Table Ch???c n??ng
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

                        #region T???o menu PhanHe
                        DataTable dtPhanHe = dataset.Tables[0];
                        foreach (DataRow item in dtPhanHe.Rows)
                        {
                            NavBarGroup group = new NavBarGroup();
                            group.Caption = item["TenPhanHe"].ToString();
                            if (item["MaQuanLy"].ToString() == "Nav_Admissions")
                                group.Name = "Nav_Admission";
                            else
                                group.Name = item["MaQuanLy"].ToString();

                            #region Load h??nh
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

                            #region G???n th?? m???c
                            if (group.Tag == null)
                            {
                                //Set thu???c t??nh Navigation
                                group.GroupStyle = NavBarGroupStyle.ControlContainer;
                                TreeListNavigationControl treeControl = new TreeListNavigationControl();

                                //Th??m Resources h??nh cho Navigation
                                ResourceManager rm1 = Resources.ResourceManager;
                                ((DevExpress.Utils.ImageCollection)treeControl.StateImageList).AddImage((Bitmap)rm1.GetObject("BO_Folder"));
                                ((DevExpress.Utils.ImageCollection)treeControl.StateImageList).AddImage((Bitmap)rm1.GetObject("BO_List"));
                                ((DevExpress.Utils.ImageCollection)treeControl.StateImageList).AddImage((Bitmap)rm1.GetObject("BO_Report"));

                                //Add lo???i Navigation l?? tree v??o
                                group.ControlContainer = new NavBarGroupControlContainer ();//
                                group.ControlContainer.Controls.Add(treeControl);

                                //Ki???m tra n???u Group l?? report th?? s??? load ki???u kh??c
                                if (!group.Name.ToString().Contains("Nav_Report"))
                                {
                                    treeControl.BeforeExpand += thuMuc_BeforeExpand; //H??m gi???i n??n th?? m???c
                                    treeControl.FocusedNodeChanged += thuMuc_FocusedNodeChanged; //H??m check m??? ch???c n??ng
                                    List<getThuMuc> l = (from d in listThuMuc
                                                         where d.TenPhanHe.ToString() == item["TenPhanHe"].ToString()
                                                         select d).ToList();
                                    foreach (getThuMuc itemThuMuc in l)
                                    {                                        
                                        if (itemThuMuc.TenPhanHe == group.Caption)
                                        {//T???o m???i add t???ng node v??o tree
                                            TreeListNode node = treeControl.AppendNode(new object[] { itemThuMuc.TenThuMuc.ToString() }, null);
                                            node.Tag = itemThuMuc.Oid.ToString();
                                            node.StateImageIndex = 0; //Ch???n index c???a h??nh l?? 0 (th?? m???c)
                                            //
                                            node.HasChildren = true; //G???n thu???c t??nh c?? danh s??ch con cho th?? m???c
                                            group.Tag = "true"; //G???n Tag ????? sau n??y check l?? Tag != null ngh??a l?? ???? load r???i th?? s??? kh??ng load n???a
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
                        groupReport.Caption = "B??o c??o";
                        groupReport.Name = "Nav_Report";

                        //Load h??nh
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

            //N???u group ???? load r???i th?? kh??ng load n???a - ???? b???
            if (e.Group.Tag == null)
            {
                //Set thu???c t??nh Navigation
                e.Group.GroupStyle = NavBarGroupStyle.ControlContainer;
                TreeListNavigationControl treeControl = new TreeListNavigationControl();

                //Th??m Resources h??nh cho Navigation
                ResourceManager rm1 = Resources.ResourceManager;
                ((DevExpress.Utils.ImageCollection)treeControl.StateImageList).AddImage((Bitmap)rm1.GetObject("BO_Folder"));
                ((DevExpress.Utils.ImageCollection)treeControl.StateImageList).AddImage((Bitmap)rm1.GetObject("BO_List"));
                ((DevExpress.Utils.ImageCollection)treeControl.StateImageList).AddImage((Bitmap)rm1.GetObject("BO_Report"));

                //Add lo???i Navigation l?? tree v??o
                e.Group.ControlContainer.Controls.Add(treeControl);

                //Ki???m tra n???u Group l?? report th?? s??? load ki???u kh??c
                if (!e.Group.Name.ToString().Contains("Nav_Report"))
                {
                    treeControl.BeforeExpand += thuMuc_BeforeExpand; //H??m gi???i n??n th?? m???c
                    treeControl.FocusedNodeChanged += thuMuc_FocusedNodeChanged; //H??m check m??? ch???c n??ng
                    List<getThuMuc> l = (from d in listThuMuc
                                         where d.TenPhanHe.ToString() == e.Group.Caption.ToString()
                                         select d).ToList();
                    foreach (getThuMuc itemThuMuc in l)
                    {
                        var ij = itemThuMuc.TenThuMuc;
                        if (itemThuMuc.TenPhanHe == e.Group.Caption)
                        {//T???o m???i add t???ng node v??o tree
                            TreeListNode node = treeControl.AppendNode(new object[] { itemThuMuc.TenThuMuc.ToString() }, null);
                            node.Tag = itemThuMuc.Oid.ToString();
                            node.StateImageIndex = 0; //Ch???n index c???a h??nh l?? 0 (th?? m???c)
                            //
                            node.HasChildren = true; //G???n thu???c t??nh c?? danh s??ch con cho th?? m???c
                            e.Group.Tag = "true"; //G???n Tag ????? sau n??y check l?? Tag != null ngh??a l?? ???? load r???i th?? s??? kh??ng load n???a
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
                       // node.HasChildren = true;//Report - code c??
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
                using (DialogUtil.Wait(null,clasCaption, "??ang truy c???p ch???c n??ng" ))
                {
                    //T???o b??? nh??? m???i
                    _obs = Application.CreateObjectSpace();

                    //T???t ghi nh???n truy c???p th??ng tin nh??n vi??n l???n ????u
                    Common.AcessedCategorizedListEditor = false;
                    var a = e.Node.Tag;
                    #region ListView
                    if (e.Node.Tag.ToString().Contains("_ListView")
                        && !e.Node.Tag.ToString().Contains("_ListViewCustom"))
                    {
                        string objectName = e.Node.Tag.ToString().ToString().Substring(0, e.Node.Tag.ToString().Length - 9);

                        //T??m object trong source
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

                        //T??m object trong source
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
                            if(clasCaption.Contains("Nh??n vi??n ??ang l??m vi???c (New)"))
                            {
                                NhanSuCustomView._DaNghiViec = 0;
                            }
                            else if (clasCaption.Contains("Nh??n vi??n ???? ngh??? vi???c (New)"))
                            {
                                NhanSuCustomView._DaNghiViec = 1;
                            }
                            else if (clasCaption.Contains("Kh??ng nh???n vi???c"))
                            {
                                NhanSuCustomView._DaNghiViec = 2;
                            }

                            if (clasCaption.Contains("Gi???ng vi??n th???nh gi???ng (New)"))
                            {
                                ThinhGiangCustomView._DaNghiViec = 0;
                            }
                            else if (clasCaption.Contains("Gi???ng vi??n th???nh gi???ng ???? ngh??? vi???c (New)"))
                            {
                                ThinhGiangCustomView._DaNghiViec = 1;
                            }

                            //
                            string objectName = e.Node.Tag.ToString().ToString().Substring(0, e.Node.Tag.ToString().Length - 11);
                            //T??m object trong source
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

            //using (DialogUtil.Wait(null, "??ang x??? l??...", "Vui l??ng ch???"))
            {
                List<getChucNang> list = (from d in listChucNang
                                       where d.ThuMucQuanLy.ToString() == e.Node.Tag.ToString()
                                       select d).ToList();

                string LoaiObject = "";
                foreach (getChucNang i in list)
                {
                    #region C??
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

                    #region M???i
                    
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

                    #region M???i

                    DetailView view;
                    objectName = e.Node.Tag.ToString().ToString().Substring(0, e.Node.Tag.ToString().Length - 11);
                    var phanhe = e.Node.Tag;
                    //T??m object trong source
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

                    #region code c??

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
                    //    listViewNode.Caption = string.Format("B??o c??o {0}", e.Node.ParentNode.GetDisplayText(e.Node.ParentNode.Id));
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
                    //    column.Caption = "T??n b??o c??o";
                    //    column.SortIndex = 0;
                    //    column.Width = 200;
                    //    column.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    //    column.PropertyEditorType = typeof(StringPropertyEditor);
                    //}


                    ////T??m object trong source
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
