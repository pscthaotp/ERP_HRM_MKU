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
using ERP.Module.NonPersistentObjects.TuyenSinh_TP;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.HeThong;

namespace ERP.Module.BaoCao.TuyenSinh_TP
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách trúng tuyển - Tuyển sinh")]
    [Appearance("TatCaKhoi", TargetItems = "KhoiSIS", Enabled = false, Criteria = "TatCa")]
    public class BaoCao_TuyenSinh_DanhSachTrungTuyen : StoreProcedureReport
    {
        //
        private NamHoc _NamHoc;
        private CongTy _CongTy;
        private bool _TatCa;
        //Key Mapping
        private int _ID_KHOI;
        //Mapping SIS.dbo
        private string _KhoiSIS;

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

        [Browsable(false)]
        public XPCollection<DanhMucKhoi> ListDanhMucKhoi { get; set; }


        public BaoCao_TuyenSinh_DanhSachTrungTuyen(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CongTy", CongTy.Oid);
            param[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[2] = new SqlParameter("@Khoi", ID_KHOI);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_TP_DanhSachTrungTuyen", CommandType.StoredProcedure, param);
            ////
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
