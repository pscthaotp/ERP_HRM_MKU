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
using DevExpress.ExpressApp.Editors;
using ERP.Module.NonPersistentObjects.TuyenSinh_TP;
using ERP.Module.Enum.TuyenSinh_PT;

namespace ERP.Module.BaoCao.TuyenSinh_TP
{
    [NonPersistent]
    [ModelDefault("Caption", "Thống kê số lượng học sinh chuyển trường/Rút hồ sơ - Tân phú")]
    [Appearance("TatCaKhoi", TargetItems = "KhoiSIS", Enabled = false, Criteria = "TatCa")]
    [Appearance("TheoNam", TargetItems = "HocKy;TuNgay;DenNgay", Visibility = ViewItemVisibility.Hide, Criteria = "DieuKien=1")]
    [Appearance("TheoHocKy", TargetItems = "TuNgay;DenNgay", Visibility = ViewItemVisibility.Hide, Criteria = "DieuKien=2")]
    [Appearance("TheoNgay", TargetItems = "NamHoc;HocKy", Visibility = ViewItemVisibility.Hide, Criteria = "DieuKien=3")]
    public class BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenTruong : StoreProcedureReport
    {
        //
        private CongTy _CongTy;
        private DieuKienLayThoiGianEnum _DieuKien;
        private NamHoc _NamHoc;
        private HocKyEnum _HocKy;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private bool _TatCa;
        //Key Mapping
        private int _ID_KHOI;
        //Mapping SIS.dbo
        private string _KhoiSIS;

        [ModelDefault("Caption", "Trường")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Điều kiện")]
        public DieuKienLayThoiGianEnum DieuKien
        {
            get
            {
                return _DieuKien;
            }
            set
            {
                SetPropertyValue("DieuKien", ref _DieuKien, value);
            }
        }

        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "DieuKien=1 or DieuKien=2")]
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

        [ModelDefault("Caption", "Học kỳ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "DieuKien=2")]
        public HocKyEnum HocKy
        {
            get
            {
                return _HocKy;
            }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "DieuKien=3")]
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
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "DieuKien=3")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả các khối")]
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
                    KhoiSIS = "";
                    ID_KHOI = 0;
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Mã khối")]
        public int ID_KHOI
        {
            get
            {
                return _ID_KHOI;
            }
            set
            {
                SetPropertyValue("ID_KHOI", ref _ID_KHOI, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCa")]
        [ModelDefault("Caption", "Khối dự kiến nhập học")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Web.Editors.ComboBox_Khoi")]
        public string KhoiSIS
        {
            get
            {
                return _KhoiSIS;
            }
            set
            {
                SetPropertyValue("KhoiSIS", ref _KhoiSIS, value);
                if (!IsLoading)
                {
                    if (!string.IsNullOrEmpty(KhoiSIS))
                    {
                        if (ListDanhMucKhoi == null)
                            LoadKhoiSIS();
                        if (ListDanhMucKhoi.Count > 0)
                        {
                            foreach (var item in ListDanhMucKhoi)
                            {
                                if (KhoiSIS.Equals(item.TenKhoi))
                                {
                                    ID_KHOI = item.ID;
                                }
                            }
                        }
                    }
                }
            }
        }

        [Browsable(false)]
        public XPCollection<DanhMucKhoi> ListDanhMucKhoi { get; set; }

        public BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenTruong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
            TuNgay = DateTime.Today.SetTime(Enum.Systems.SetTimeEnum.StartMonth);
            DenNgay = DateTime.Today.SetTime(Enum.Systems.SetTimeEnum.EndMonth);
            DieuKien = DieuKienLayThoiGianEnum.TheoNam;
            HocKy = HocKyEnum.CaNam;
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@CongTy", CongTy.Oid);
            param[1] = new SqlParameter("@DieuKien", DieuKien.GetHashCode());
            param[2] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[3] = new SqlParameter("@HocKy", HocKy.GetHashCode());
            param[4] = new SqlParameter("@TuNgay", TuNgay);
            param[5] = new SqlParameter("@DenNgay", DenNgay);
            param[6] = new SqlParameter("@Khoi", ID_KHOI);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_TP_ThongKeSoLuongHocSinhChuyenTruong", CommandType.StoredProcedure, param);
            //
            return cmd;
        }

        void LoadKhoiSIS()
        {
            ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(Session, false);
            string _connect = DataProvider.GetConnectionString();
            var query = "";
            if (_connect.Contains(Config.KeyServerMamNon))
                query = "select ID , TENKHOI,GIOIHANTUOIDUOI,GIOIHANTUOITREN ,GIOIHANTUOIDUOINU,GIOIHANTUOITRENNU from " + Config.KeyLinkServer + ".SIS.dbo.KHOI";
            else
                query = "select ID , TENKHOI,GIOIHANTUOIDUOI,GIOIHANTUOITREN ,GIOIHANTUOIDUOINU,GIOIHANTUOITRENNU from SIS.dbo.KHOI";

            using (DataTable dt = DataProvider.GetDataTable(query, CommandType.Text))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        DanhMucKhoi obj = new DanhMucKhoi(Session);
                        if (!item.IsNull("ID"))
                        {
                            obj.ID = int.Parse(item["ID"].ToString());
                            obj.TenKhoi = item["TENKHOI"].ToString();
                        }
                        ListDanhMucKhoi.Add(obj);
                    }
                }
            }
        }
    }
}
