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

namespace ERP.Module.BaoCao.TuyenSinh_TP
{
    [NonPersistent]
    [ModelDefault("Caption", "Phiếu in giấy nhập học - Thu phí - Tuyển sinh")]
    public class BaoCao_TuyenSinh_PhieuDongHocPhi : StoreProcedureReport
    {
        //
        private HoSoXetTuyen _HoSoXetTuyen;
        private CongTy _CongTy;
        private string _StudentId;
        private int _Nam;
        private int _Thang;
        private string _UpdateStaff;

        //
        [ModelDefault("Caption", "Hồ sơ xét tuyển")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HoSoXetTuyen HoSoXetTuyen
        {
            get
            {
                return _HoSoXetTuyen;
            }
            set
            {
                SetPropertyValue("HoSoXetTuyen", ref _HoSoXetTuyen, value);
            }
        }

        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Mã xét tuyển")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string StudentId
        {
            get
            {
                return _StudentId;
            }
            set
            {
                SetPropertyValue("StudentId", ref _StudentId, value);
            }
        }


        [ModelDefault("Caption", "Tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }

        [ModelDefault("Caption", "Năm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        [ModelDefault("Caption", "Người in phiếu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string UpdateStaff
        {
            get
            {
                return _UpdateStaff;
            }
            set
            {
                SetPropertyValue("UpdateStaff", ref _UpdateStaff, value);
            }
        }

        public BaoCao_TuyenSinh_PhieuDongHocPhi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            UpdateStaff = Common.SecuritySystemUser_GetCurrentUser().UserName;
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@CongTy", CongTy.Oid);
            param[1] = new SqlParameter("@HoSoXetTuyen", HoSoXetTuyen.Oid);
            if (HoSoXetTuyen.MAHOCSINH != null)
                param[2] = new SqlParameter("@_StudentId", HoSoXetTuyen.MAHOCSINH);
            else
                param[2] = new SqlParameter("@_StudentId", HoSoXetTuyen.MaXetTuyen);

            param[3] = new SqlParameter("@_Nam", Nam);
            param[4] = new SqlParameter("@_Thang", Thang);
            param[5] = new SqlParameter("@_UpdateStaff", UpdateStaff);
  
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_TP_PhieuDongHocPhi", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
