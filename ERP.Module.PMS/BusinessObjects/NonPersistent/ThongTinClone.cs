using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Enum.NhanSu;
using ERP.Module.Enum.TuyenSinh_PT;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NonPersistent
{
    [ModelDefault("Caption", "Thông tin trường")]   
    [NonPersistent]
    public class ThongTinClone : BaseObject
    {
        private CongTy _CongTy;
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public CongTy CongTy
        {
            get;
            set;
        }

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }
       
        public ThongTinClone(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            
        }
    }
}
