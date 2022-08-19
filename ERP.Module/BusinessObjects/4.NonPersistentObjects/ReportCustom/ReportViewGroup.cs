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
using DevExpress.Xpo.Metadata;

namespace ERP.Module.NonPersistentObjects.ReportCustom
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Danh sách báo cáo")]
    public class ReportViewGroup : BaseObject, ITreeNode, ITreeNodeImageProvider
    {
        private ReportViewGroup _ReportCha;
        private string _TenReport; private string _TenNhom;

        private int _ID;
        private Guid _Oid;
        private Guid _Group;
        private ReportCustomView _ReportCustomView;

        [Browsable(false)]
        public ReportCustomView ReportCustomView
        {
            get
            {
                return _ReportCustomView;
            }
            set
            {
                SetPropertyValue("ReportCustomView", ref _ReportCustomView, value);
            }
        }
        [Browsable(false)]
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                SetPropertyValue("ID", ref _ID, value);
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
        public Guid Group
        {
            get
            {
                return _Group;
            }
            set
            {
                SetPropertyValue("Group", ref _Group, value);
            }
        }

        [ModelDefault("Caption", "Báo cáo")]
        public string TenReport
        {
            get
            {
                return _TenReport;
            }
            set
            {
                SetPropertyValue("TenReport", ref _TenReport, value);
            }
        }
        [ModelDefault("Caption", "TenNhom")]
        [Browsable(false)]
        public string TenNhom
        {
            get
            {
                return _TenNhom;
            }
            set
            {
                SetPropertyValue("TenNhom", ref _TenNhom, value);
            }
        }
        [Browsable(false)]
        public ReportViewGroup ReportCha
        {
            get
            {
                return _ReportCha;
            }
            set
            {
                SetPropertyValue("ReportCha", ref _ReportCha, value);
            }
        }
        

        [Aggregated]
        [ModelDefault("Caption", "Danh sách Report")]
        public XPCollection<ReportViewGroup> ListGroup { get; set; }

        public ReportViewGroup(Session session) : base(session) { }

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
                return ListGroup;
            }
        }

        string ITreeNode.Name
        {
            get { return TenReport; }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return ReportCha; }
        }
    }
}
