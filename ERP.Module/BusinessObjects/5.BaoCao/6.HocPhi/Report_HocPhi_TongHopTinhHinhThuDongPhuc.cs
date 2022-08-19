using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.HocPhi;
using ERP.Module.NghiepVu.QuanLyKho.HangHoas;
using ERP.Module.Enum.HocPhi;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Tổng hợp tình hình thu đồng phục - học phẩm - Học phí")]
    public class Report_HocPhi_TongHopTinhHinhThuDongPhuc : StoreProcedureReport, ICongTy
    {
        // Fields...
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private DateTime _KyTinh;
        private KyTinhHocPhi _KyTinhHocPhi;
        private DongPhucHocPhamEnum _LoaiHang;

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
        [DataSourceCriteria("LoaiTruong = 1")]
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
                //    updateKyTinh();
                //}
            }
        }
        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                //if (!IsLoading)
                //    updateKyTinh();
            }
        }
        [ModelDefault("Caption", "Kỳ tính học phí")]
        [ImmediatePostData]
        [ModelDefault("Editmask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime KyTinh
        {
            get
            {
                return _KyTinh;
            }
            set
            {
                SetPropertyValue("KyTinh", ref _KyTinh, value);
                //if (!IsLoading)
                //    updateKyTinh();
            }
        }

        [ModelDefault("Caption", "Kỳ tính học phí")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public KyTinhHocPhi KyTinhHocPhi
        {
            get
            {
                return _KyTinhHocPhi;
            }
            set
            {
                SetPropertyValue("KyTinhHocPhi", ref _KyTinhHocPhi, value);
            }
        }

        [ModelDefault("Caption", "Loại hàng")]
        public DongPhucHocPhamEnum LoaiHang
        {
            get
            {
                return _LoaiHang;
            }
            set
            {
                SetPropertyValue("LoaiHang", ref _LoaiHang, value);
            }
        }

        public Report_HocPhi_TongHopTinhHinhThuDongPhuc(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhHocPhi> KyTinhHocPhiList { get; set; }
        public void UpdateKyTinhHocPhi()
        {
            if (KyTinhHocPhiList == null)
                KyTinhHocPhiList = new XPCollection<KyTinhHocPhi>(Session);
            //
            KyTinhHocPhiList.Criteria = CriteriaOperator.Parse("CongTy = ?", CongTy.Oid);
        }
        void updateKyTinh()
        {
            if(CongTy!=null && NamHoc!=null && KyTinh!=DateTime.MinValue)
            {
                KyTinhHocPhi = Session.FindObject<KyTinhHocPhi>(CriteriaOperator.Parse("NamHoc =? and CongTy =? and Thang =? and Nam =?", NamHoc.Oid, CongTy.Oid, KyTinh.Month, KyTinh.Year));
            }
            else
                KyTinhHocPhi=null;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            KyTinh = DateTime.Now;
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {
            updateKyTinh();
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@KyTinhHocPhi", KyTinhHocPhi.Oid);
            parameter[1] = new SqlParameter("@LoaiHang", LoaiHang.GetHashCode());

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocPhi_DanhSachHangHoaBan", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
