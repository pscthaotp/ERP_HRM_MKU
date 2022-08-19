using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Commons;

namespace ERP.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Đợt tính PMS")]
    [DefaultProperty("TenKy")]
    public class DotTinhPMS : BaseObject
    {
        private NamHoc _NamHoc;
        //private HocKy _HocKy;
        private int _Dot;
        private DateTime _NgayBatDau;
        private DateTime _NgayKetThuc;
        private DateTime _NgayMoXacNhan;
        private DateTime _NgayDongXacNhan;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
         [ImmediatePostData]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
                    XPCollection<DotTinhPMS> listKyTinh = new XPCollection<DotTinhPMS>(Session, filter);
                    if (listKyTinh == null)
                        Dot = 1;
                    else
                        Dot = listKyTinh.Count + 1;
                }
            }
        }

        //[ModelDefault("Caption", "Học kỳ")]
        //[DataSourceProperty("NamHoc.ListHocKy")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[ImmediatePostData]
        //public HocKy HocKy
        //{
        //    get
        //    {
        //        return _HocKy;
        //    }
        //    set
        //    {
        //        SetPropertyValue("HocKy", ref _HocKy, value);
        //    }
        //}

        [ModelDefault("Caption", "Đợt")]
        //[ModelDefault("AllowEdit", "False")]
        public int Dot
        {
            get { return _Dot; }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu")]
        public DateTime NgayBatDau
        {
            get { return _NgayBatDau; }
            set
            {
                SetPropertyValue("NgayBatDau", ref _NgayBatDau, value);
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc")]
        public DateTime NgayKetThuc
        {
            get { return _NgayKetThuc; }
            set
            {
                SetPropertyValue("NgayKetThuc", ref _NgayKetThuc, value);
            }
        }

        [ModelDefault("Caption", "Ngày mở xác nhận")]
        public DateTime NgayMoXacNhan
        {
            get { return _NgayMoXacNhan; }
            set
            {
                SetPropertyValue("NgayMoXacNhan", ref _NgayMoXacNhan, value);
            }
        }

        [ModelDefault("Caption", "Ngày đóng xác nhận")]
        public DateTime NgayDongXacNhan
        {
            get { return _NgayDongXacNhan; }
            set
            {
                SetPropertyValue("NgayDongXacNhan", ref _NgayDongXacNhan, value);
            }
        }
        
       
        [ModelDefault("Caption", "Tên đợt tính")]
        public string TenKy
        {
            get
            {
                //return String.Format("Đợt {0} {1} {2}", Dot.ToString(), " - " + NamHoc != null ? NamHoc.TenNamHoc : "", " - " + HocKy != null ? HocKy.MaQuanLy : "" );
                return String.Format("Đợt {0} {1}", Dot.ToString(), " - " + NamHoc != null ? NamHoc.TenNamHoc : "");
            }
        }
        public DotTinhPMS(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = Common.GetCurrentNamHoc(Session);            
        }
    }
}