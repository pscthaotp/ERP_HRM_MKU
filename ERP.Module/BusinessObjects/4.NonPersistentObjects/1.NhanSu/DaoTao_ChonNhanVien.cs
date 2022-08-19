using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn nhân viên")]
    public class DaoTao_ChonNhanVien : BaseObject
    {
        // Fields...
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private bool _Chon;
        private string _GhiChu;


        [ModelDefault("Caption", "Chọn")]
        [ModelDefault("AllowEdit", "True")]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [ModelDefault("AllowEdit", "False")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [Size(300)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public DaoTao_ChonNhanVien(Session session) : base(session) { }
    }

}
