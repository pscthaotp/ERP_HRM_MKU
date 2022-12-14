using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Data.SqlClient;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
//
namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [DefaultProperty("TenKy")]
    [ImageName("BO_KyTinhLuong")]
    [ModelDefault("Caption", "Kỳ chấm công")]
    [RuleCombinationOfPropertiesIsUnique("Kỳ chấm công đã tồn tại.", DefaultContexts.Save, "Thang;Nam;CongTy")]
    [Appearance("KyChamCong.KhoaSo", TargetItems = "Thang;Nam;TuNgay;DenNgay;SoNgay;CongTy", Enabled = false, Criteria = "KhoaSo")]
    public class CC_KyChamCong : BaseObject,ICongTy
    {
        private int _Thang;
        private int _Nam;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private decimal _SoNgay;
        private bool _KhoaSo;
        private CongTy _CongTy;

        [ImmediatePostData]
        [ModelDefault("Caption", "Tháng")]
        [RuleRange("", DefaultContexts.Save, 1, 12)]
        public int Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                SetPropertyValue("Thang", ref _Thang, value);
                if (!IsLoading && Nam > 0 && Thang > 0)
                {
                    if (Thang > 1)
                        TuNgay = new DateTime(Nam, Thang - 1, 26);
                    else
                    {
                        TuNgay = new DateTime(Nam - 1, 12, 26);
                    }
                    DenNgay = TuNgay.AddMonths(1).AddDays(-1);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
                if (!IsLoading && Nam > 0 && Thang > 0)
                {
                    if (Thang > 1)
                        TuNgay = new DateTime(Nam, Thang - 1, 26);
                    else
                    {
                        TuNgay = new DateTime(Nam - 1, 12, 26);
                    }
                    DenNgay = TuNgay.AddMonths(1).AddDays(-1);
                }
            }
        }

        [ModelDefault("Caption", "Tên kỳ")]
        public string TenKy
        {
            get
            {
                return String.Format("Tháng {0:0#} năm {1:####}", Thang, Nam);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        //[ModelDefault("AllowEdit", "False")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading)
                {
                    TinhSoNgayCongChuan();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        //[ModelDefault("AllowEdit", "False")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading)
                {
                    TinhSoNgayCongChuan();
                }
            }
        }

        [ModelDefault("Caption", "Số ngày")]
        [ModelDefault("EditMask", "n1")]
        [ModelDefault("DisplayFormat", "n1")]
        public decimal SoNgay
        {
            get
            {
                return _SoNgay;
            }
            set
            {
                SetPropertyValue("SoNgay", ref _SoNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Khóa sổ")]
        public bool KhoaSo
        {
            get
            {
                return _KhoaSo;
            }
            set
            {
                SetPropertyValue("KhoaSo", ref _KhoaSo, value);
            }
        }

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if(!IsLoading)
                {
                    TinhSoNgayCongChuan();
                }
            }
        }

        public CC_KyChamCong(Session session) :base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //
            CongTy = Common.CongTy(Session);
            DateTime currentDate = Common.GetServerCurrentTime();
            SoNgay = 22;
            Thang = currentDate.Month;
            Nam = currentDate.Year;
        }

        void TinhSoNgayCongChuan()
        {
            if (CongTy != null && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
            {
                //Nếu mần non và phổ thông thì tính thứ 7
                if (CongTy.BoPhanCha != null && !CongTy.BoPhanCha.Oid.Equals(Config.KeyDaiHoc_CaoDang))
                {
                    SoNgay = Common.GetDayNumberSubtrackWeekend_ManNonAndPhoThong(TuNgay, DenNgay, Session);
                }
                else
                {
                    SoNgay = Common.GetDayNumberSubtrackWeekend_DaiHoc(TuNgay, DenNgay, Session);
                }
            }
            else
            {
                SoNgay = 0;
            }
        }
    }

}
