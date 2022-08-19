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
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Text;
using System.Collections.Generic;

namespace ERP.Module.BaoCao.TuyenSinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách học sinh có anh/chị/em học cùng trường - Tuyển sinh")]
    [Appearance("DanhSachAnhChiEm.TatCaTruong", TargetItems = "CongTy", Enabled = false, Criteria = "TatCa")]
    public class BaoCao_TuyenSinh_DanhSachHocSinhCoAnhChiEmCungTruong : StoreProcedureReport,ICongTy
    {
        private bool _TatCa = true;
        private CongTy _CongTy;
        private DateTime _Thang;

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

       [ModelDefault("Caption", "Tháng")]
       [ModelDefault("DisplayFormat", "MM/yyyy")]
       [ModelDefault("EditMask", "MM/yyyy")]      
       public DateTime Thang
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

        public BaoCao_TuyenSinh_DanhSachHocSinhCoAnhChiEmCungTruong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Thang = Common.GetServerCurrentTime();
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
            param[1] = new SqlParameter("@NgayNhapHoc", Thang);

            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_DanhSachHocSinhCoAnhChiEmHocCungTruong", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
