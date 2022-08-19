using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;
using DevExpress.Xpo.DB;
using System.Data.SqlClient;
using System.Data;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết bổ nhiệm kiêm nhiệm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhBoNhiemKiemNhiem;BoPhan")]
    public class ChiTietBoNhiemKiemNhiem : BaseObject
    {
        private QuyetDinhBoNhiemKiemNhiem _QuyetDinhBoNhiemKiemNhiem;
        //
        private BoPhan _BoPhan;
        private BoPhan _BoPhanCu;

        [Browsable(false)]
        [Association("QuyetDinhBoNhiemKiemNhiem-ListChiTietBoNhiemKiemNhiem")]
        public QuyetDinhBoNhiemKiemNhiem QuyetDinhBoNhiemKiemNhiem
        {
            get
            {
                return _QuyetDinhBoNhiemKiemNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiemKiemNhiem", ref _QuyetDinhBoNhiemKiemNhiem, value);
            }
        }
        
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
                if (IsLoading)
                {
                    SetPropertyValue("BoPhan", ref _BoPhan, value);
                }
                else
                {
                    BoPhanCu = BoPhan;
                    SetPropertyValue("BoPhan", ref _BoPhan, value);
                }
            }
        }

        [NonPersistent]
        [Browsable(false)]
        [ModelDefault("Caption", "Đơn vị cũ")]
        public BoPhan BoPhanCu
        {
            get
            {
                return _BoPhanCu;
            }
            set
            {
                SetPropertyValue("BoPhanCu", ref _BoPhanCu, value);
            }
        }

        public ChiTietBoNhiemKiemNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (QuyetDinhBoNhiemKiemNhiem != null
                && !IsLoading
                && !QuyetDinhBoNhiemKiemNhiem.IsDirty)
                QuyetDinhBoNhiemKiemNhiem.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            BoPhanCu = BoPhan;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted && Oid != Guid.Empty)
            {
                //SqlParameter[] param = new SqlParameter[1];
                //param[0] = new SqlParameter("@QuyetDinh", Oid);
                //DataProvider.ExecuteNonQuery("dbo.spd_WebChamCong_CreateWebUser_BoPhan", CommandType.StoredProcedure, param);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
            }
            base.OnDeleting();
        }
    }

}
