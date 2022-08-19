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
using ERP.Module.Enum.Systems;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.Enum.TuyenSinh_PT;
using ERP.Module.HeThong;
using System.Collections.Generic;
using System.Text;

namespace ERP.Module.Report.NhanSu.TuyenSinh //Sai k duoc lam theo
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách khách hàng đã tư vấn(New) - Tuyển sinh")]
    [Appearance("DanhSachKhachHang.TatCaTruong", TargetItems = "CongTy", Enabled = false, Criteria = "TatCa")]
    public class BaoCao_TuyenSinh_DanhSachKhachHangDaTuVan : StoreProcedureReport, ICongTy
    {
        //
        private bool _TatCa = false;
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
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
                if (!IsLoading)
                {
                    if (NamHoc != null)
                    {
                        TuNgay = NamHoc.NgayBatDau;
                        DenNgay = NamHoc.NgayKetThuc;
                    }
                    else
                    {
                        TuNgay = DateTime.MinValue;
                        DenNgay = DateTime.MinValue;
                    }
                }

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

        public BaoCao_TuyenSinh_DanhSachKhachHangDaTuVan(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

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
            //SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser();
            //if (user != null && !user.CongTy.Oid.Equals(Config.KeyTanPhu))
            //    param[4] = new SqlParameter("@Type", 1);
            //else
            //    param[4] = new SqlParameter("@Type", 2);
            ////
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_DanhSachKhachHangDaTuVan_19", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
