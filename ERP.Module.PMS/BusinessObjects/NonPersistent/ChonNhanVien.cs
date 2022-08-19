using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NonPersistentObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn nhân viên")]
    [Appearance("Khoa", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCa")]
    public class ChonNhanVien : BaseObject
    {
        private bool _TatCa;
        private BoPhan _BoPhan;
        private bool _ThinhGiang;


        [ModelDefault("Caption", "Tất cả")]
        [ImmediatePostData]
        public bool TatCa
        {
            get { return _TatCa; }
            set
            {
                SetPropertyValue("TatCa", ref _TatCa, value);
                if (!IsLoading)
                {
                    if (TatCa)
                        BoPhan = null;
                }
            }
        }

        [ModelDefault("Caption", "Thỉnh giảng")]
        [Browsable(false)]
        [ImmediatePostData]
        public bool ThinhGiang
        {
            get { return _ThinhGiang; }
            set
            {
                SetPropertyValue("ThinhGiang", ref _ThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "Công ty")]
        [ModelDefault("AllowEdit", "False")]
        public CongTy CongTy
        {
            get;
            set;
        }

        [ModelDefault("Caption", "Bộ phận")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    using (DialogUtil.AutoWait("Đang lấy danh sách nhân viên"))
                    //if (BoPhan != null)
                    {
                        CapNhatNV();
                    }
                }
            }
        }
        [ModelDefault("Caption", "Danh sách nhân viên")]
        public XPCollection<dsThongTinNhanVien> listNhanVien
        {
            get;
            set;
        }
        public ChonNhanVien(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            listNhanVien = new XPCollection<dsThongTinNhanVien>(Session, false);
            TatCa = true;
            ThinhGiang = false;
        }

        public void CapNhatNV()
        {
            listNhanVien.Reload();
            //CongTy ttt = Common.CongTy(Session);
            SqlParameter[] param = new SqlParameter[3]; /*Số parameter trên Store Procedure*/
            param[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            param[1] = new SqlParameter("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty);
            param[2] = new SqlParameter("@ThinhGiang", ThinhGiang.GetHashCode());
            DataTable dt = DataProvider.GetDataTable("spd_PMS_HoSo_LayThongTinHoSoNhanVien", System.Data.CommandType.StoredProcedure, param);
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    dsThongTinNhanVien ttnv = new dsThongTinNhanVien(Session);
                    ttnv.OidThongTinNhanVien = new Guid(item["Oid"].ToString());
                    ttnv.MaNhanVien = item["MaNhanVien"].ToString();
                    ttnv.HoTen = item["HoTen"].ToString();
                    if (item["TenBoPhan"].ToString() != string.Empty)
                        ttnv.BoPhan = item["TenBoPhan"].ToString();
                    ttnv.Chon = true;
                    listNhanVien.Add(ttnv);
                }
            }
        }
    }
}
