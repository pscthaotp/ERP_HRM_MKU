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
using ERP.Module.Enum.Systems;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Text;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.Report.TienLuong
{
    [NonPersistent]
    [ModelDefault("Caption", "Bảng tổng hợp quỹ lương - Tiền lương")]
    [Appearance("BaoCao_Luong_BangTongHopQuyLuong.TatCaDonVi", TargetItems = "CongTy", Enabled = false, Criteria = "TatCa")]
    [Appearance("BaoCao_Luong_BangTongHopQuyLuong.CaNam", TargetItems = "Thang", Enabled = false, Criteria = "CaNam")]
    public class BaoCao_Luong_BangTongHopQuyLuong : StoreProcedureReport
    {      
        private bool _TatCa = true;
        private bool _CaNam = true;
        private CongTy _CongTy;
        private int _Nam;
        private int? _Thang;

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
            }
        }
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty/Trường")]
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

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
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

        //
        [ImmediatePostData]
        [ModelDefault("Caption", "Cả năm")]
        public bool CaNam
        {
            get
            {
                return _CaNam;
            }
            set
            {
                SetPropertyValue("CaNam", ref _CaNam, value);
                if (!IsLoading && value == true)
                    Thang = 0;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int? Thang
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

        public BaoCao_Luong_BangTongHopQuyLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();           
            //            
            Nam = Common.GetServerCurrentTime().Year;
            Thang = Common.GetServerCurrentTime().Month;
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
            //
            SqlCommand cmd = new SqlCommand("spd_Rpt_Luong_BangTongHopQuyLuong");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;       
            cmd.Parameters.AddWithValue("@BoPhanPhanQuyen", roled.ToString());
            cmd.Parameters.AddWithValue("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            cmd.Parameters.AddWithValue("@Nam", Nam);
            cmd.Parameters.AddWithValue("@Thang", CaNam == true ? 0 : Thang);
            return cmd;
        }       
    }
}
