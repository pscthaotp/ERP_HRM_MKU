using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using System.Drawing;
//
namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_Bank")]
    [DefaultProperty("TenNganHang")]
    [ModelDefault("Caption", "Ngân hàng")]
    public class NganHang : BaseObject, ITreeNode, ITreeNodeImageProvider
    {
        private NganHang _TrucThuoc;
        private string _MaQuanLy;
        private string _TenNganHang;
        private string _TenVietTat;
        private decimal _PhanTramPhiChietKhau;
        //
        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên ngân hàng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNganHang
        {
            get
            {
                return _TenNganHang;
            }
            set
            {
                SetPropertyValue("TenNganHang", ref _TenNganHang, value);
            }
        }

        [ModelDefault("Caption", "Tên viết tắt")]
        public string TenVietTat
        {
            get
            {
                return _TenVietTat;
            }
            set
            {
                SetPropertyValue("TenVietTat", ref _TenVietTat, value);
            }
        }

        [ModelDefault("Caption", "Phần trăm phí chiết khấu")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal PhanTramPhiChietKhau
        {
            get
            {
                return _PhanTramPhiChietKhau;
            }
            set
            {
                SetPropertyValue("PhanTramPhiChietKhau", ref _PhanTramPhiChietKhau, value);
            }
        }

        [VisibleInListView(false)]
        [Association("NganHang-ListChiNhanh")]
        [ModelDefault("Caption", "Trực thuộc")]
        public NganHang TrucThuoc
        {
            get
            {
                return _TrucThuoc;
            }
            set
            {
                SetPropertyValue("TrucThuoc", ref _TrucThuoc, value);
            }
        }

        [Association("NganHang-ListChiNhanh")]
        [ModelDefault("Caption", "Chi nhánh")]
        public XPCollection<NganHang> ListChiNhanh
        {
            get
            {
                return GetCollection<NganHang>("ListChiNhanh");
            }
        }

        IBindingList ITreeNode.Children
        {
            get { return ListChiNhanh; }
        }

        string ITreeNode.Name
        {
            get { return TenNganHang; }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return TrucThuoc; }
        }

        public Image GetImage(out string imageName)
        {
            imageName = "BO_Bank";
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }


        public NganHang(Session session) : base(session) { }
    }

}
