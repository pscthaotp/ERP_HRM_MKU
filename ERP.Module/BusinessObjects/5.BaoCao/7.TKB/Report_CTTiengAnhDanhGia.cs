using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.BepAn.ThucDonMonAn;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.NghiepVu.TKB.ChuongTrinhTiengAnh;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.ThoiKhoaBieu
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Chương trình tiếng anh đánh giá")]
    public class Report_CTTiengAnhDanhGia : StoreProcedureReport
    {
        private ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh _HocSinh;
        private Lop _Lop;
        private NamHoc _NamHoc;
        private Unit _Unit;
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Lớp")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Lop Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
                if (!IsLoading && value != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("Lop =?", Lop.Oid);
                    DSHocSinh = new XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(Session, filter);
                    UpdateUnitList();
                }
            }
        }
       

        [ModelDefault("Caption", "Học sinh")]
        [DataSourceProperty("DSHocSinh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh HocSinh
        {
            get
            {
                return _HocSinh;
            }
            set
            {
                SetPropertyValue("HocSinh", ref _HocSinh, value);
            }
        }

        [ModelDefault("Caption", "Unit")]
        [DataSourceProperty("UnitList")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Unit Unit
        {
            get
            {
                return _Unit;
            }
            set
            {
                SetPropertyValue("Unit", ref _Unit, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách học sinh")]
        public XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh> DSHocSinh { get; set; }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<Unit> UnitList
        {
            get; set;
        }

        public Report_CTTiengAnhDanhGia(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            NamHoc = Common.GetCurrentNamHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@Unit", Unit.Oid);
            parameter[1] = new SqlParameter("@Lop", Lop.Oid);
            parameter[2] = new SqlParameter("@NamHoc", NamHoc.Oid);
            parameter[3] = new SqlParameter("@HocSinh", HocSinh.Oid);
            SqlCommand cmd = DataProvider.GetCommand("spd_Web_MamNon_DanhGiaHocSinh_CTTiengAnh", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }

        private void UpdateUnitList()
        {
            if (UnitList != null)
                UnitList.Reload();
            else
                UnitList = new XPCollection<Unit>(Session, false);
            CriteriaOperator filter = CriteriaOperator.Parse("QuanLyCTTiengAnh.CongTy=? and QuanLyCTTiengAnh.Lop=?", Lop.CongTy.Oid, Lop.LopCha.Oid);
            XPCollection<CTTiengAnh> cttienganh = new XPCollection<CTTiengAnh>(Session, filter);
            foreach (var item in cttienganh)
            {
                UnitList.Add(item.Unit);
            }
        }
    }

}
