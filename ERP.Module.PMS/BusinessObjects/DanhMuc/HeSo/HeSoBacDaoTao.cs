using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ERP.Module.PMS.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.CauHinh.HeSo
{
    [ModelDefault("Caption", "Hệ số bậc đào tạo")]
    public class HeSoBacDaoTao : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoBacDaoTao")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }

        private BacDaoTao _BacDaoTao;
        private decimal _HeSo;
        
        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }


        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSoBacDaoTao", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo
        {
            get { return _HeSo; }
            set { SetPropertyValue("HeSo", ref _HeSo, value); }
        }


        public HeSoBacDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
