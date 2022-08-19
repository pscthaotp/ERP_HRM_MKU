using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.GiayTo;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;

namespace ERP.Module.DanhMuc.NhanSu
{
    [ImageName("BO_ChungChi")]
    [DefaultProperty("TenChungChi")]
    [ModelDefault("Caption", "Chứng chỉ")]
    public class ChungChi : BaseObject
    {
        //
        private string _TenChungChi;
        private LoaiChungChi _LoaiChungChi;
        private HoSo _HoSo;
        private decimal _Diem;
        private XepLoaiChungChiEnum _XepLoai = XepLoaiChungChiEnum.KhongChon;
        private string _NoiCap;
        private DateTime _NgayCap;
        private string _GhiChu;
        private QuyetDinh _QuyetDinh;

        [Browsable(false)]
        [ImmediatePostData]
        [Association("HoSo-ListChungChi")]
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

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Loại chứng chỉ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiChungChi LoaiChungChi
        {
            get
            {
                return _LoaiChungChi;
            }
            set
            {
                SetPropertyValue("LoaiChungChi", ref _LoaiChungChi, value);
            }
        }

        [ModelDefault("Caption", "Tên chứng chỉ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChungChi
        {
            get
            {
                return _TenChungChi;
            }
            set
            {
                SetPropertyValue("TenChungChi", ref _TenChungChi, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap
        {
            get
            {
                return _NgayCap;
            }
            set
            {
                SetPropertyValue("NgayCap", ref _NgayCap, value);
            }
        }

        [ModelDefault("Caption", "Nơi cấp")]
        public string NoiCap
        {
            get
            {
                return _NoiCap;
            }
            set
            {
                SetPropertyValue("NoiCap", ref _NoiCap, value);
            }
        }

        [ModelDefault("Caption", "Điểm")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal Diem
        {
            get
            {
                return _Diem;
            }
            set
            {
                SetPropertyValue("Diem", ref _Diem, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại")]
        public XepLoaiChungChiEnum XepLoai
        {
            get
            {
                return _XepLoai;
            }
            set
            {
                SetPropertyValue("XepLoai", ref _XepLoai, value);
            }
        }

        [Size(300)]
        [ModelDefault("Caption", "Ghi chú")]
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
       

        public ChungChi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
