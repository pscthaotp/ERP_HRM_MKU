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
    [ModelDefault("Caption", "Danh sách môn học theo tkb")]
    [DefaultProperty("LopHocPhan")]
    public class dsMonHocTheoTKB : BaseObject
    {
        //private string _NamHoc;
        //private string _HocKy;
        private string _LopHocPhan;
        private string _MaHocPhan;
        private string _TenHocPhan;

       
        //[ModelDefault("Caption", "Năm học")]
        //public string NamHoc
        //{
        //    get { return _NamHoc; }
        //    set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        //}

        //[ModelDefault("Caption", "Học kỳ")]
        //public string HocKy
        //{
        //    get { return _HocKy; }
        //    set { SetPropertyValue("HocKy", ref _HocKy, value); }
        //}

        [ModelDefault("Caption", "Mã học phần")]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("MaHocPhan", ref _MaHocPhan, value); }
        }

        [ModelDefault("Caption", "Tên học phần")]
        public string TenHocPhan
        {
            get { return _TenHocPhan; }
            set { SetPropertyValue("TenHocPhan", ref _TenHocPhan, value); }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        public dsMonHocTheoTKB(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
