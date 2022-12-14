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

namespace ERP.Module.Report.TienLuong
{
    [NonPersistent]
    [ModelDefault("Caption", "Biến động thay đổi tình trạng - Tiền lương")]
    public class BaoCao_HoSoLuong_DanhSachThayDoiTinhTrang : StoreProcedureReport,ICongTy
    {
        //
        private CongTy _CongTy;
        private DateTime _ThangThuNhat;
        private DateTime _ThangThuHai;

        //
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

        [ModelDefault("Caption", "Tháng thứ nhất")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime ThangThuNhat
        {
            get
            {
                return _ThangThuNhat;
            }
            set
            {
                SetPropertyValue("ThangThuNhat", ref _ThangThuNhat, value);
            }
        }

        [ModelDefault("Caption", "Tháng thứ hai")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime ThangThuHai
        {
            get
            {
                return _ThangThuHai;
            }
            set
            {
                SetPropertyValue("ThangThuHai", ref _ThangThuHai, value);
            }
        }

        public BaoCao_HoSoLuong_DanhSachThayDoiTinhTrang(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            //
            SqlCommand cmd = new SqlCommand("spd_Rpt_HoSoLuong_BienDongTinhTrangGiuaHaiThang", (SqlConnection)Session.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            //
            cmd.Parameters.AddWithValue("@ThangThuNhat", ThangThuNhat);
            cmd.Parameters.AddWithValue("@ThangThuHai", ThangThuHai);
            cmd.Parameters.AddWithValue("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);

            return cmd;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ThangThuHai = Common.GetServerCurrentTime().Date;
            ThangThuNhat = ThangThuHai.AddMonths(-1);
        }
    }

}
