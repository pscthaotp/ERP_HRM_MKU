using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.BusinessObjects._6.NghiepVu.NghiPhep
{
    [DefaultClassOptions]
    [DefaultProperty("NienDoTaiChinh")]
    [ModelDefault("Caption", "Quản lý nghỉ phép")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    public class CC_QuanLyNghiPhep : BaseObject, ICongTy
    {
        private NamHoc _NamHoc;
        private CongTy _CongTy;
        private NienDoTaiChinh _NienDoTaiChinh;

        [Browsable(false)]
        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Công ty")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                //if (!IsLoading)
                //{
                //    //
                //    NamHoc = Session.FindObject<NamHoc>(CriteriaOperator.Parse("Thang=? and Nam=? and CongTy.Oid=?", NgayLap.Month, NgayLap.Year, CongTy != null ? CongTy.Oid : Guid.Empty));
                //}
            }
        }

        [ModelDefault("Caption", "Niên độ tài chính")]
        public NienDoTaiChinh NienDoTaiChinh
        {
            get
            {
                return _NienDoTaiChinh;
            }
            set
            {
                SetPropertyValue("NienDoTaiChinh", ref _NienDoTaiChinh, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết nghỉ phép")]
        [Association("QuanLyNghiPhep-ListChiTietNghiPhep")]
        public XPCollection<CC_ChiTietNghiPhep> ListChiTietNghiPhep
        {
            get
            {
                //
                return GetCollection<CC_ChiTietNghiPhep>("ListChiTietNghiPhep");
            }
        }

        public CC_QuanLyNghiPhep(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}