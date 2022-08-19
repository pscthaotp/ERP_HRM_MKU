using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.NhanSu.DaoTao
{
    [ImageName("BO_QuanLyDaoTao")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết duyệt đăng ký đào tạo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "DuyetDangKyDaoTao;ThongTinNhanVien")]
    public class ChiTietDuyetDangKyDaoTao : BaseObject, IBoPhan
    {       
        private DuyetDangKyDaoTao _DuyetDangKyDaoTao;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _GhiChu;

        [Browsable(false)]
        [Association("DuyetDangKyDaoTao-ListChiTietDuyetDangKyDaoTao")]
        public DuyetDangKyDaoTao DuyetDangKyDaoTao
        {
            get
            {
                return _DuyetDangKyDaoTao;
            }
            set
            {
                SetPropertyValue("DuyetDangKyDaoTao", ref _DuyetDangKyDaoTao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nhân viên")]
        [DataSourceProperty("NhanVienList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
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

        public ChiTietDuyetDangKyDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NhanVienList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NhanVienList == null)
                NhanVienList = new XPCollection<ThongTinNhanVien>(Session);
            NhanVienList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }
    }

}
