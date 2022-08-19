//using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.PMS.NghiepVu;
using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace ERP.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách giảng viên thỉnh giảng được tính thù lao")]
    public class Report_PMS_DanhSachGVTG_TinhThuLao : StoreProcedureReport
    {        
        private BangChotThuLao_ThinhGiang _BangChotThuLao_ThinhGiang;
        private GiangVienThinhGiang _NhanVien;       
        [RuleRequiredField(DefaultContexts.Save)]      
        [ModelDefault("Caption", "Bảng chốt thù lao")]
      
        public BangChotThuLao_ThinhGiang BangChotThuLao_ThinhGiang
        {
            get { return _BangChotThuLao_ThinhGiang; }
            set
            {
                SetPropertyValue("BangChotThuLao_ThinhGiang", ref _BangChotThuLao_ThinhGiang, value);              
            }
        }

        [ModelDefault("Caption", "Nhân Viên")]       
        [ImmediatePostData]
        public GiangVienThinhGiang NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        public Report_PMS_DanhSachGVTG_TinhThuLao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();       
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@BangChotThuLao_ThinhGiang", BangChotThuLao_ThinhGiang.Oid);
            parameter[1] = new SqlParameter("@User", "");
            parameter[2] = new SqlParameter("@NhanVien", NhanVien != null ? NhanVien.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_PMS_BangChotThuLao_LayKhoiLuong_ThinhGiang", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;         
        }
    }
}
