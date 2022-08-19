using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.GiayTo;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.DanhMuc.NhanSu
{
    [ImageName("BO_NgoaiNguKhac")]
    [DefaultProperty("NgoaiNgu")]
    [ModelDefault("Caption", "Trình độ ngoại ngữ")]
    public class TrinhDoNgoaiNguKhac : BaseObject
    {
        //
        private decimal _Diem;
        private HoSo _HoSo;
        private NgoaiNgu _NgoaiNgu;
        private TrinhDoNgoaiNgu _TrinhDoNgoaiNgu;
        private XepLoaiChungChiEnum _XepLoai = XepLoaiChungChiEnum.KhongChon;
        private string _NoiCap;
        private DateTime _NgayCap;
        private string _GhiChu;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("HoSo-ListNgoaiNgu")]
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

        [ModelDefault("Caption", "Ngoại ngữ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgoaiNgu NgoaiNgu
        {
            get
            {
                return _NgoaiNgu;
            }
            set
            {
                SetPropertyValue("NgoaiNgu", ref _NgoaiNgu, value);
            }
        }

        [ModelDefault("Caption", "Trình độ ngoại ngữ")]
        public TrinhDoNgoaiNgu TrinhDoNgoaiNgu
        {
            get
            {
                return _TrinhDoNgoaiNgu;
            }
            set
            {
                SetPropertyValue("TrinhDoNgoaiNgu", ref _TrinhDoNgoaiNgu, value);
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

        public TrinhDoNgoaiNguKhac(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<GiayToHoSo> GiayToList { get; set; }

        private void UpdateGiayToList()
        {
            if (GiayToList == null)
                GiayToList = new XPCollection<GiayToHoSo>(Session);

            GiayToList.Criteria = CriteriaOperator.Parse("HoSo=?", HoSo);
        }
     
        protected override void OnDeleting()
        {
            TrinhDoNgoaiNguKhac trinhDogoaiNguKhac = Session.FindObject<TrinhDoNgoaiNguKhac>(CriteriaOperator.Parse("Oid=?", this.Oid));
            if (trinhDogoaiNguKhac != null)
            {
                NhanVien nhanVien = Session.FindObject<NhanVien>(CriteriaOperator.Parse("Oid=?", trinhDogoaiNguKhac.HoSo.Oid));

                //Nếu ngoại ngữ vừa xóa là ngoại ngữ chính thì cập nhật lại ngoại ngữ chinh trong nhân viên trình độ
                if (trinhDogoaiNguKhac.NgoaiNgu != null && trinhDogoaiNguKhac.TrinhDoNgoaiNgu != null)
                {
                    
                    if (nhanVien.NhanVienTrinhDo != null
                        && nhanVien.NhanVienTrinhDo.NgoaiNgu != null
                        && nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu != null
                        && nhanVien.NhanVienTrinhDo.NgoaiNgu.Oid == trinhDogoaiNguKhac.NgoaiNgu.Oid
                        && nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu.Oid == trinhDogoaiNguKhac.TrinhDoNgoaiNgu.Oid)
                    {
                        nhanVien.NhanVienTrinhDo.NgoaiNgu = null;
                        nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu = null;
                    }
                     
                }
            }

            base.OnDeleting();
        }
    }

}
