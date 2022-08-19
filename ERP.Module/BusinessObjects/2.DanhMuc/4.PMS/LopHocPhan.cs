using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.DanhMuc.PMS
{
    [DefaultProperty("TenLopHocPhan")]
    [ModelDefault("Caption", "Lớp học phần")]
    public class LopHocPhan : BaseObject
    {
        private string _MaHocPhan;
        private string _MaLopHocPhan;
        private string _TenLopHocPhan;

        [ModelDefault("Caption", "Mã học phần")]
        [Size(-1)]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set
            {
                SetPropertyValue("MaHocPhan ", ref _MaHocPhan, value);
            }
        }
        [ModelDefault("Caption", "Mã lớp học phần")]
        [Size(-1)]
        public string MaLopHocPhan
        {
            get { return _MaLopHocPhan; }
            set
            {
                SetPropertyValue("MaLopHocPhan ", ref _MaLopHocPhan, value);
            }
        }
        [ModelDefault("Caption", "Tên lớp học phần")]
        [Size(-1)]
        public string TenLopHocPhan
        {
            get { return _TenLopHocPhan; }
            set
            {
                SetPropertyValue("TenLopHocPhan ", ref _TenLopHocPhan, value);
            }
        }

        public LopHocPhan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
