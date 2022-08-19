using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.NhanSu;
using DevExpress.Persistent.Base.General;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.DanhMuc.System;

namespace ERP.Module.HeThong
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Danh sách chức năng")]
    [ImageName("Nav_HeThong")]
    [DefaultProperty("Caption")]
    [Appearance("LaThuMuc", TargetItems = "AppObject;LoaiView;ThuMucQuanLy", Visibility = ViewItemVisibility.Hide, Criteria = "LaThuMuc")]
    [Appearance("KhongLaThuMuc", TargetItems = "PhanHe", Visibility = ViewItemVisibility.Hide, Criteria = "!LaThuMuc")]
    [Appearance("LoaiView", TargetItems = "LoaiCustom", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiView != 3")]
    [Appearance("ListViewId", TargetItems = "ListViewId", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiView != 4")]
    public class AppMenu : BaseObject,ITreeNode, ILoaiPhanMen
    {
        private decimal _SoThuTu;
        private string _TenChucNang;
        private PhanHe _PhanHe;
        private AppObject _AppObject;
        private LoaiViewEnum _LoaiView;
        private LoaiCustomEnum _LoaiCustom;
        private string _ListViewId;
        private bool _LaThuMuc;
        private string _HinhAnh;
        private AppMenu _ThuMucQuanLy;
        private bool _SuDung;
        private LoaiPhanMenEnum _LoaiPhanMen;
        private LoaiObjectEnum _LoaiObject;

        [ModelDefault("Caption", "Số thứ tự")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "n3")]
        [ModelDefault("EditMask", "n3")]
        public decimal SoThuTu
        {
            get
            {
                return _SoThuTu;
            }
            set
            {
                SetPropertyValue("SoThuTu", ref _SoThuTu, value);
            }
        }

        [ModelDefault("Caption", "Tên chức năng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public String TenChucNang
        {
            get
            {
                return _TenChucNang;
            }
            set
            {
                SetPropertyValue("TenChucNang", ref _TenChucNang, value);
                if (!IsLoading)
                    if (TenChucNang != string.Empty)
                        if (AppObject == null)
                        {
                            AppObject = Session.FindObject<AppObject>(CriteriaOperator.Parse("Caption =?", TenChucNang));
                        }
            }
        }

        [ModelDefault("Caption", "Là thư mục")]
        [ModelDefault("AllowEdit","false")]
        [ImmediatePostData]
        public bool LaThuMuc
        {
            get
            {
                return _LaThuMuc;
            }
            set
            {
                SetPropertyValue("LaThuMuc", ref _LaThuMuc, value);
                if (LaThuMuc)
                {
                    AppObject = null;
                    HinhAnh = "BO_Folder";
                }
                else
                    HinhAnh = "BO_List";
            }
        }

        [ModelDefault("Caption", "Đối tượng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!LaThuMuc")]
        [ImmediatePostData]
        public AppObject AppObject
        {
            get
            {
                return _AppObject;
            }
            set
            {
                SetPropertyValue("AppObject", ref _AppObject, value);
                if (!IsLoading)
                {
                    if (AppObject != null && TenChucNang == string.Empty)
                        TenChucNang = AppObject.Caption;
                }
            }
        }

        [ModelDefault("Caption", "Loại đối tượng")]
        public LoaiObjectEnum LoaiObject
        {
            get
            {
                return _LoaiObject;
            }
            set
            {
                SetPropertyValue("LoaiObject", ref _LoaiObject, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại View")]
        public LoaiViewEnum LoaiView
        {
            get
            {
                return _LoaiView;
            }
            set
            {
                SetPropertyValue("LoaiView", ref _LoaiView, value);
            }
        }

        [ModelDefault("Caption", "ListView ID")]
        public string ListViewId
        {
            get
            {
                return _ListViewId;
            }
            set
            {
                SetPropertyValue("ListViewId", ref _ListViewId, value);
            }
        }

        [ModelDefault("Caption", "Loại custom")]
        public LoaiCustomEnum LoaiCustom
        {
            get
            {
                return _LoaiCustom;
            }
            set
            {
                SetPropertyValue("LoaiCustom", ref _LoaiCustom, value);
            }
        }

        [ModelDefault("Caption", "Thư mục quản lý")]
        [DataSourceProperty("AppMenuParentList")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!LaThuMuc")]
        [Association("ThuMucQuanLy-ListChildAppMenu")]
        [ImmediatePostData]
        public AppMenu ThuMucQuanLy
        {
            get
            {
                return _ThuMucQuanLy;
            }
            set
            {
                SetPropertyValue("ThuMucQuanLy", ref _ThuMucQuanLy, value);
                if (!IsLoading)
                {
                    if (ThuMucQuanLy != null)
                    {
                        PhanHe = ThuMucQuanLy.PhanHe;
                        SoThuTu = ThuMucQuanLy.SoThuTu;
                    }
                }
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Phân hệ")]
        public PhanHe PhanHe
        {
            get
            {
                return _PhanHe;
            }
            set
            {
                SetPropertyValue("PhanHe", ref _PhanHe, value);
            }
        }

        [ModelDefault("Caption", "Hình ảnh")]
        public String HinhAnh
        {
            get
            {
                return _HinhAnh;
            }
            set
            {
                SetPropertyValue("HinhAnh", ref _HinhAnh, value);
            }
        }

        [ModelDefault("Caption", "Sử dụng")]
        public bool SuDung
        {
            get
            {
                return _SuDung;
            }
            set
            {
                SetPropertyValue("SuDung", ref _SuDung, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Tên chức năng")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public String Caption
        {
            get
            {
                 return String.Format("{0} - {1}", TenChucNang, PhanHe != null ? PhanHe.TenPhanHe : string.Empty);
            }

        }

        [Browsable(false)]
        [ModelDefault("Caption", "Loại phần mềm")]
        public LoaiPhanMenEnum LoaiPhanMen
        {
            get
            {
                return _LoaiPhanMen;
            }
            set
            {
                SetPropertyValue("LoaiPhanMen", ref _LoaiPhanMen, value);
            }
        }

        [ModelDefault("Caption", "List menu")]
        [Browsable(false)]
        [Aggregated]
        [Association("ThuMucQuanLy-ListChildAppMenu")]
        public XPCollection<AppMenu> ListChildAppMenu
        {
            get
            {
                return GetCollection<AppMenu>("ListChildAppMenu");
            }
        }

        public AppMenu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            LoaiView = LoaiViewEnum.ListView;
            SuDung = true;
            if (Config.TypeApplication.Equals("WinForm"))
            {
                LoaiPhanMen = LoaiPhanMenEnum.Win;
            }
            else
            {
                LoaiPhanMen = LoaiPhanMenEnum.Web;
            }
            //
            UpdateAppMenuParentList();
        }
        //protected override void OnLoaded()
        //{
        //    base.OnLoaded();
        //    //
        //    UpdateAppMenuParentList();
        //}

        [Browsable(false)]
        public XPCollection<AppMenu> AppMenuParentList { get; set; }

        public void UpdateAppMenuParentList()
        {
            if (AppMenuParentList == null)
                AppMenuParentList = new XPCollection<AppMenu>(Session);
            //
            if (AppMenuParentList != null)
                AppMenuParentList.Criteria = CriteriaOperator.Parse("LaThuMuc");
        }

        string ITreeNode.Name
        {
            get { return TenChucNang; }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return ThuMucQuanLy; }
        }

        [ImmediatePostData]
        IBindingList ITreeNode.Children
        {
            get { return ListChildAppMenu; }
        }

    }

}