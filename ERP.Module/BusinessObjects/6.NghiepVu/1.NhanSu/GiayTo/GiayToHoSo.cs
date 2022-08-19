using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using DevExpress.Persistent.Base.General;

namespace ERP.Module.NghiepVu.NhanSu.GiayTo
{
    //[ImageName("BO_GiayToHoSo")]
    [DefaultProperty("SoGiayTo")]
    [ModelDefault("Caption", "Giấy tờ hồ sơ")]
    public class GiayToHoSo : BaseObject, ITreeNode
    {
        //
        private decimal _STT;
        private string _TenGiayTo;
        private GiayToHoSo _LoaiGiayTo;
        private DateTime _NgayLap;
        private string _GhiChu;
        private HoSo _HoSo;
        private string _DuongDanFile;
        private string _DuongDanFileWeb;

        [Browsable(false)]
        [Association("HoSo-ListGiayToHoSo")]
        public HoSo HoSo
        {
            get
            {
                return _HoSo;
            }
            set
            {
                SetPropertyValue("HoSo", ref _HoSo, value);
            }
        }

        [ModelDefault("Caption", "Thứ tự")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        [ModelDefault("Caption", "Tên giấy tờ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public string TenGiayTo
        {
            get
            {
                return _TenGiayTo;
            }
            set
            {
                SetPropertyValue("TenGiayTo", ref _TenGiayTo, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }

        [ModelDefault("Caption", "Tên loại giấy tờ")]
        [Association("GiayToHoSo-ChildGiayToHoSo")]
        public GiayToHoSo LoaiGiayTo
        {
            get
            {
                return _LoaiGiayTo;
            }
            set
            {
                SetPropertyValue("LoaiGiayTo", ref _LoaiGiayTo, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(500)]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [Browsable(false)]
        [Size(500)]
        public string DuongDanFile
        {
            get
            {
                return _DuongDanFile;
            }
            set
            {
                SetPropertyValue("DuongDanFile", ref _DuongDanFile, value);
            }
        }

        [Browsable(false)]
        [Size(500)]
        public string DuongDanFileWeb
        {
            get
            {
                return _DuongDanFileWeb;
            }
            set
            {
                SetPropertyValue("DuongDanFileWeb", ref _DuongDanFileWeb, value);
            }
        }

        [Browsable(false)]
        [Association("GiayToHoSo-ChildGiayToHoSo")]
        [ModelDefault("Caption", "")]
        [Aggregated]
        public XPCollection<GiayToHoSo> ListGiayToHoSo
        {
            get
            {
                return GetCollection<GiayToHoSo>("ListGiayToHoSo");
            }
        }

        public GiayToHoSo(Session session) : base(session) { }

        [ImmediatePostData]
        IBindingList ITreeNode.Children
        {
            get { return ListGiayToHoSo; }
        }

        string ITreeNode.Name
        {
            get { return TenGiayTo; }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return LoaiGiayTo; }
        }
    }

}
