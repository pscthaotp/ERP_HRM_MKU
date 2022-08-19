using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn hình ảnh cập nhật")]
    public class HoSo_ChonHinhAnh : BaseObject, ICongTy
    {
        //
        private CongTy _CongTy;            
       
        [ModelDefault("Caption", "Công ty")]
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

        [Size(500)]
        [ModelDefault("Caption", "Lưu ý")]
        public string GhiChu
        {
            get
            {
                return "Hình 3x4 hoặc 4x6 được đặt tên theo Mã nhân viên hoặc Mã tập đoàn. \nDung lượng hình dưới 500KB, đẹp, rõ nét. \nCho phép chọn một hoặc nhiều hình để upload.";
            }           
        }

        public HoSo_ChonHinhAnh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            CongTy = Common.CongTy(Session);
        }        
    }

}
