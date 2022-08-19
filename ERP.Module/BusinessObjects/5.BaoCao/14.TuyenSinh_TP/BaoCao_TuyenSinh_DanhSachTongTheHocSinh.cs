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
    [ModelDefault("Caption", "Danh sách tổng thể học sinh theo năm học - Tân phú")]
    [Appearance("BaoCaoTuVan", TargetItems = "CongTy", Enabled = false, Criteria = "TatCa")]
    //[Appearance("TheoSoLuongThucTe", TargetItems = "NamHoc", Enabled = false, Criteria = "TheoSoLuongThucTe")]
    public class BaoCao_TuyenSinh_DanhSachTongTheHocSinh : StoreProcedureReport
    {
        //
        private bool _TatCa = false;
        private CongTy _CongTy;
        //private bool _TheoSoLuongThucTe = false;
        private NamHoc _NamHoc;

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

    
        public BaoCao_TuyenSinh_DanhSachTongTheHocSinh(Session session) : base(session) { }
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
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CongTy", roled.ToString());
            param[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_TanPhu_DanhSachTongHopHocSinh", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
