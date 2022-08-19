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
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.Enum.TuyenSinh;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Text;

namespace ERP.Module.BaoCao.TuyenSinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách tư vấn thành công - Tuyển sinh")]
    [Appearance("DanhSachTuVanThanhCong.TatCaTruong", TargetItems = "CongTy", Enabled = false, Criteria = "TatCa")]
    public class BaoCao_TuyenSinh_DanhSachTuVanThanhCong : StoreProcedureReport, ICongTy
    {
        //
        private bool _TatCa = true;
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private HinhThucTuVanEnum _HinhThucTuVan = HinhThucTuVanEnum.TatCa;

        //
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

        [ModelDefault("Caption", "Hình thức tư vấn")]
        public HinhThucTuVanEnum HinhThucTuVan
        {
            get
            {
                return _HinhThucTuVan;
            }
            set
            {
                SetPropertyValue("HinhThucTuVan", ref _HinhThucTuVan, value);
            }
        }
        public BaoCao_TuyenSinh_DanhSachTuVanThanhCong(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //CongTy = Common.CongTy(Session);
            //
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

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CongTy", roled.ToString());
            param[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[2] = new SqlParameter("@HinhThucTuVan", HinhThucTuVan);
            
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_DanhSachKhachHangDaTuVan", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
