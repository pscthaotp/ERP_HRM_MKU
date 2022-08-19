using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.PMS;
using ERP.Module.NghiepVu.PMS.QuanLy;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.PMS.DanhMuc;

namespace ERP.Module.NghiepVu.PMS.HeSo
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Hệ số ngôn ngữ")]
    public class HeSoNgonNgu : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        private NgonNguGiangDay _NgonNguGiangDay;
        private decimal _HeSo;

        [Association("QuanLyHeSo-ListHeSoNgonNgu")]
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý hệ số")]
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

        [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgonNguGiangDay NgonNguGiangDay
        {
            get
            {
                return _NgonNguGiangDay;
            }
            set
            {
                SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value);
            }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo
        {
            get { return _HeSo; }
            set { SetPropertyValue("HeSo", ref _HeSo, value); }
        }

        public HeSoNgonNgu(Session session)
            : base(session)
        {
        }


    }
}