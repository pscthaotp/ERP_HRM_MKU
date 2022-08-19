using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using ERP.Module.Commons;
using System.ComponentModel;
using DevExpress.Persistent.Base.General;
using System.Drawing;
using DevExpress.ExpressApp.Utils;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Danh sách bộ phận")]
    public class BoPhanView : BaseObject, ITreeNode, ITreeNodeImageProvider
    {
        private BoPhanView _BoPhanCha;
        private string _TenBoPhan;
        private Guid _Oid;
        private NhanSuCustomView _NhanSuCustomView;
        private LoaiBoPhanEnum _LoaiBoPhan;

        [Browsable(false)]
        public NhanSuCustomView NhanSuCustomView
        {
            get
            {
                return _NhanSuCustomView;
            }
            set
            {
                SetPropertyValue("NhanSuCustomView", ref _NhanSuCustomView, value);
            }
        }

        [Browsable(false)]
        public Guid Oid
        {
            get
            {
                return _Oid;
            }
            set
            {
                SetPropertyValue("Oid", ref _Oid, value);
            }
        }
        [Browsable(false)]
        public LoaiBoPhanEnum LoaiBoPhan
        {
            get
            {
                return _LoaiBoPhan;
            }
            set
            {
                SetPropertyValue("LoaiBoPhan", ref _LoaiBoPhan, value);
            }
        }
        [ModelDefault("Caption", "Bộ phận")]
        public string TenBoPhan
        {
            get
            {
                return _TenBoPhan;
            }
            set
            {
                SetPropertyValue("TenBoPhan", ref _TenBoPhan, value);
            }
        }

        [Browsable(false)]
        public BoPhanView BoPhanCha
        {
            get
            {
                return _BoPhanCha;
            }
            set
            {
                SetPropertyValue("BoPhanCha", ref _BoPhanCha, value);
            }
        }


        [Aggregated]
        [ModelDefault("Caption", "Danh sách bộ phận")]
        public XPCollection<BoPhanView> ListBoPhan { get; set; }

        public BoPhanView(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        public Image GetImage(out string imageName)
        {
            imageName = "BO_GiaDinh_32x32";
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }

        [ImmediatePostData]
        IBindingList ITreeNode.Children
        {
            get
            {
                return ListBoPhan;
            }
        }

        string ITreeNode.Name
        {
            get { return TenBoPhan; }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return BoPhanCha; }
        }
    }
}
