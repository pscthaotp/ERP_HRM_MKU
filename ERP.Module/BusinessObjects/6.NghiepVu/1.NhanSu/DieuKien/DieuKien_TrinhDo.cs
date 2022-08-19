using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;

//
namespace ERP.Module.NghiepVu.NhanSu.DieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Trình độ nhân viên")]
    public class DieuKien_TrinhDo : BaseObject
    {
        [ModelDefault("Caption", "Trình độ văn hóa")]
        public TrinhDoVanHoa TrinhDoVanHoa { get; set; }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon { get; set; }

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        public ChuyenNganhDaoTao ChuyenNganhDaoTao { get; set; }

        [ModelDefault("Caption", "Trường đào tạo")]
        public TruongDaoTao TruongDaoTao { get; set; }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        public HinhThucDaoTao HinhThucDaoTao { get; set; }

        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiep { get; set; }

        [ModelDefault("Caption", "Trình độ tin học")]
        public TrinhDoTinHoc TrinhDoTinHoc { get; set; }

        [ModelDefault("Caption", "Ngoại ngữ")]
        public NgoaiNgu NgoaiNgu { get; set; }

        [ModelDefault("Caption", "Trình độ ngoại ngữ")]
        public TrinhDoNgoaiNgu TrinhDoNgoaiNgu { get; set; }

        public DieuKien_TrinhDo(Session session) : base(session) { }
    }

}
