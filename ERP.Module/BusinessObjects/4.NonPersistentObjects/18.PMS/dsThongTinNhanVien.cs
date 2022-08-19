using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách giảng viên")]
    [DefaultProperty("HoTen")]
    public class dsThongTinNhanVien : BaseObject
    {
        private Guid _OidThongTinNhanVien;
        private bool _Chon;
        private string _MaNhanVien;
        private string _HoTen;
        private string _BoPhan;

        [ModelDefault("Caption", "Chọn")]

        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [ModelDefault("Caption", "Thông tin NV")]
        [Browsable(false)]
        public Guid OidThongTinNhanVien
        {
            get { return _OidThongTinNhanVien; }
            set { SetPropertyValue("OidThongTinNhanVien", ref _OidThongTinNhanVien, value); }
        }

        [ModelDefault("Caption", "Mã nhân viên")]
        public string MaNhanVien
        {
            get { return _MaNhanVien; }
            set { SetPropertyValue("MaNhanVien", ref _MaNhanVien, value); }
        }

        [ModelDefault("Caption", "Họ tên")]
        public string HoTen
        {
            get { return _HoTen; }
            set { SetPropertyValue("HoTen", ref _HoTen, value); }
        }

        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        public dsThongTinNhanVien(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Chon = true;
        }
    }
}
