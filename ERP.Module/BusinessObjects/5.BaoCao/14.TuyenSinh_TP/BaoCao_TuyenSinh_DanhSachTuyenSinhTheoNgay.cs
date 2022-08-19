using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using System.Data;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.HocSinh;
using ERP.Module.Commons;
using ERP.Module.Enum.TuyenSinh;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using ERP.Module.NghiepVu.TuyenSinh_TP;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.HocSinh.Lops;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Text;

namespace ERP.Module.BaoCao.TuyenSinh_TP
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách tuyển sinh theo ngày - Tân phú")]
    [Appearance("BaoCaoTuVan", TargetItems = "CongTy", Enabled = false, Criteria = "TatCa")]
    public class BaoCao_TuyenSinh_DanhSachTuyenSinhTheoNgay : StoreProcedureReport
    {
        //
        private bool _TatCa = false;
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        //
        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả")]
        public bool TatCa
        {
            get
            {
                return _TatCa;
            }
            set
            {
                SetPropertyValue("TatCa", ref _TatCa, value);
                if (!IsLoading)
                {
                    CongTy = null;
                }
            }
        }

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCa")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }
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

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Editmask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Editmask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        public BaoCao_TuyenSinh_DanhSachTuyenSinhTheoNgay(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
        }
        public override SqlCommand CreateCommand()
        {
            List<string> listBP = new List<string>();
            //
            if (TatCa)
                listBP = Common.Department_GetRoledDepartmentList_ByCurrentUser();
            else
                listBP = Common.Department_GetRoledDepartmentList_ByDepartment(CongTy);
            //
            StringBuilder roled = new StringBuilder();
            foreach (string item in listBP)
            {
                roled.Append(String.Format("{0};", item));
            }
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CongTy", roled.ToString());
            param[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[2] = new SqlParameter("@TuNgay", TuNgay);
            param[3] = new SqlParameter("@DenNgay", DenNgay);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_TanPhu_DanhSachTuyenSinhTheoNgay", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
