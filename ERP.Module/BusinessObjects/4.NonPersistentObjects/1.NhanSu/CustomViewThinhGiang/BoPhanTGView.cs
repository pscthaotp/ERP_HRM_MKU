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

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Danh sách bộ phận")]
    public class BoPhanTGView : BaseObject, ITreeNode, ITreeNodeImageProvider
    {
        private BoPhanTGView _BoPhanCha;
        private string _TenBoPhan;
        private Guid _Oid;
        private ThinhGiangCustomView _ThinhGiangCustomView;

        [Browsable(false)]
        public ThinhGiangCustomView ThinhGiangCustomView
        {
            get
            {
                return _ThinhGiangCustomView;
            }
            set
            {
                SetPropertyValue("ThinhGiangCustomView", ref _ThinhGiangCustomView, value);
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
        public BoPhanTGView BoPhanCha
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
        public XPCollection<BoPhanTGView> ListBoPhan { get; set; }

        public BoPhanTGView(Session session) : base(session) { }

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
