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
using ERP.Module.DanhMuc.TSCD;
using ERP.Module.NghiepVu.QuanLyKho.HangHoas;
using ERP.Module.NghiepVu.TSCD;
using ERP.Module.Enum.TSCD;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.TSCD
{
    [NonPersistent]
    [ModelDefault("Caption", "Hiện trạng tài sản - TSCD")]
    public class Report_HienTrangTaiSan : StoreProcedureReport,ICongTy
    {
        private TinhTrangHHEnum? _TinhTrang;
        private CongTy _CongTy;
        private BoPhanTS _BoPhanTS;
        private bool _TaiSan = true;
        private bool _CongCu = true;

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrangHHEnum? TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Công ty")]
        [ImmediatePostData]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading && value != null)
                {
                    UpdateBP();
                }
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [DataSourceProperty("BoPhanList")]
        public BoPhanTS BoPhanTS
        {
            get
            {
                return _BoPhanTS;
            }
            set
            {
                SetPropertyValue("BoPhanTS", ref _BoPhanTS, value);
            }
        }

        [ModelDefault("Caption", "Tài sản")]
        public bool TaiSan
        {
            get
            {
                return _TaiSan;
            }
            set
            {
                SetPropertyValue("TaiSan", ref _TaiSan, value);
            }
        }

        [ModelDefault("Caption", "Công cụ")]
        public bool CongCu
        {
            get
            {
                return _CongCu;
            }
            set
            {
                SetPropertyValue("CongCu", ref _CongCu, value);
            }
        }

        [Browsable(false)]
        XPCollection<BoPhanTS> BoPhanList { get;set; }

        public Report_HienTrangTaiSan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
        }

        public void UpdateBP()
        {
            CriteriaOperator filter = new InOperator("BoPhan", Common.Department_GetRoledDepartmentList_ByCurrentUser());
            BoPhanList = new XPCollection<BoPhanTS>(Session, filter);
        }

        public override SqlCommand CreateCommand()
        {
            int temp = -1;

            if(TinhTrang != null)
            {
                switch (TinhTrang)
                {
                    case TinhTrangHHEnum.Tot :
                        temp = 0;
                    break;
                    case TinhTrangHHEnum.BinhThuong:
                        temp = 1;
                        break;
                    case TinhTrangHHEnum.HuHong:
                        temp = 2;
                        break;
                    case TinhTrangHHEnum.ChoThanhLy:
                        temp = 3;
                        break;
                    case TinhTrangHHEnum.ThanhLy:
                        temp = 4;
                        break;
                    case TinhTrangHHEnum.ChoThue:
                        temp = 5;
                        break;
                    case TinhTrangHHEnum.Thue:
                        temp = 6;
                        break;
                    case TinhTrangHHEnum.DaTra:
                        temp = 7;
                        break;
                }

            }

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@TinhTrang", temp);
            param[1] = new SqlParameter("@CongTy", CongTy.Oid);
            param[2] = new SqlParameter("@TaiSan", TaiSan);
            param[3] = new SqlParameter("@CongCu", CongCu);
            param[4] = new SqlParameter("@BoPhanTS", BoPhanTS == null ? Guid.Empty : BoPhanTS.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TSCD_HienTrangTS", CommandType.StoredProcedure, param);
            //
            return cmd;

        }

    }
}
