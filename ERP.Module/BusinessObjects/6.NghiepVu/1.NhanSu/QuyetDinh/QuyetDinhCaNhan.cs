using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định cá nhân")]
    public class QuyetDinhCaNhan : QuyetDinh, IBoPhan
    {
        private CongTy _CongTyCu;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private bool _JobUpdated;

        [Browsable(false)]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Trường cũ")]        
        public CongTy CongTyCu
        {
            get
            {
                return _CongTyCu;
            }
            set
            {
                SetPropertyValue("CongTyCu", ref _CongTyCu, value);
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
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                {
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                    {
                        BoPhan = value.BoPhan;
                        CongTyCu = value.CongTy;
                    }
                    AfterNhanVienChanged();
                }
            }
        }

        [Browsable(false)]
        public bool JobUpdated
        {
            get
            {
                return _JobUpdated;
            }
            set
            {
                SetPropertyValue("JobUpdated", ref _JobUpdated, value);
            }
        }
    
        public QuyetDinhCaNhan(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
            //
            JobUpdated = false;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNhanVienList();
        }

        protected virtual void AfterNhanVienChanged()  {}        

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }
       
        /// <summary>
        /// Cập nhật danh sách nhân viên
        /// </summary>
        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
                NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }     

    }

}
