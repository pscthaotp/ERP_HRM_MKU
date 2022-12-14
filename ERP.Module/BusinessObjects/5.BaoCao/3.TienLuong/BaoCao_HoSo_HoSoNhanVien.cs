using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using System.Collections.Generic;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Text;
using DevExpress.ExpressApp.Model;
using System.Data;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using System.ComponentModel;
using DevExpress.Data.Filtering;

namespace ERP.Module.Report.TienLuong
{
    [NonPersistent]
    [ModelDefault("Caption", "Hồ sơ nhân viên - Hồ sơ")]
    public class BaoCao_HoSo_HoSoNhanVien : StoreProcedureReport, ICongTy, IBoPhan
    {
        //
        private CongTy _CongTy;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        //
        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty / Trường")]
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
                if(!IsLoading)
                {
                    BoPhan = null;
                    ThongTinNhanVien = null;
                    UpdateBoPhanList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ phận")]
        [DataSourceProperty("BPList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    if (BoPhan == null)
                        ThongTinNhanVien = null;
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nhân viên")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && BoPhan == null && ThongTinNhanVien != null)
                    BoPhan = ThongTinNhanVien.BoPhan;
            }
        }

        public BaoCao_HoSo_HoSoNhanVien(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            //
            SqlCommand cmd = new SqlCommand("spd_Rpt_HoSo_HoSoNhanVien", (SqlConnection)Session.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            //
            cmd.Parameters.AddWithValue("@ThongTinNhanVien", ThongTinNhanVien.Oid);

            return cmd;
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        [Browsable(false)]
        public XPCollection<BoPhan> BPList { get; set; }

        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
                NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        private void UpdateBoPhanList()
        {
            //
            if (BPList == null)
                BPList = new XPCollection<BoPhan>(Session);
            //
            if(CongTy != null)
                BPList.Criteria = CriteriaOperator.Parse("CongTy = ?", CongTy.Oid);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
        }
    }
}
