﻿using System;
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

namespace ERP.Module.Report.NhanSu.NhanVien
{
    [NonPersistent]
    [ModelDefault("Caption", "Sơ yếu lý lịch (Mẫu 2A-BNV/2007) - Nhân sự")]
    public class Report_SoYeuLyLich_2A_BNV_2007 : StoreProcedureReport, IBoPhan
    {
        //
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("Department", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
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
                if (!IsLoading && value != null)
                    if (BoPhan == null|| value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        public Report_SoYeuLyLich_2A_BNV_2007(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ThongTinNhanVien",ThongTinNhanVien.Oid);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_NhanVien_SoYeuLyLich_2A_BNV_2007", CommandType.StoredProcedure, param);
            //
            return cmd;
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }
    }

}
