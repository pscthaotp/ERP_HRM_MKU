using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using System.Data;
using ERP.Module.Commons;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Report.HocSinh.DanhGia
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo tình trạng dinh dưỡng theo thời gian")]
    public class Report_DanhGia_TongHopTinhTrangDinhDuong : StoreProcedureReport
    {
        private NamHoc _NamHoc;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

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
                if(!IsLoading && value != null)
                {
                    TuNgay = value.NgayBatDau;
                    DenNgay = value.NgayKetThuc;
                }
            }
        }

        [ModelDefault("Caption", "Từ tháng")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Đến tháng")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        public Report_DanhGia_TongHopTinhTrangDinhDuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = Common.GetCurrentNamHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@TuNgay", TuNgay);
            parameter[1] = new SqlParameter("@DenNgay", DenNgay);
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_DanhGia_TongHopTinhTrangDinhDuong", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }
}
