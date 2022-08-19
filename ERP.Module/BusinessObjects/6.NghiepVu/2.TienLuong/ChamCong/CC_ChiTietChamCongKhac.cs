using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using DevExpress.Data.Filtering;

namespace ERP.Module.NghiepVu.TienLuong.ChamCong
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Chi tiết chấm công khác")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "ChamCongKhac;ThongTinNhanVien")]
    public class CC_ChiTietChamCongKhac : BaseObject,IBoPhan
    {
        private CC_ChamCongKhac _ChamCongKhac;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        //
        private int _Ngay1;
        private int _Ngay2;
        private int _Ngay3;
        private int _Ngay4;
        private int _Ngay5;
        private int _Ngay6;
        private int _Ngay7;
        private int _Ngay8;
        private int _Ngay9;
        private int _Ngay10;
        private int _Ngay11;
        private int _Ngay12;
        private int _Ngay13;
        private int _Ngay14;
        private int _Ngay15;
        private int _Ngay16;
        private int _Ngay17;
        private int _Ngay18;
        private int _Ngay19;
        private int _Ngay20;
        private int _Ngay21;
        private int _Ngay22;
        private int _Ngay23;
        private int _Ngay24;
        private int _Ngay25;
        private int _Ngay26;
        private int _Ngay27;
        private int _Ngay28;
        private int _Ngay29;
        private int _Ngay30;
        private int _Ngay31;
        //
        private int _TongCong;

        //
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("ChamCongKhac-ListChiTietChamCongKhac")]
        [ModelDefault("Caption", "Bảng chấm công khác")]
        public CC_ChamCongKhac ChamCongKhac
        {
            get
            {
                return _ChamCongKhac;
            }
            set
            {
                SetPropertyValue("ChamCongKhac", ref _ChamCongKhac, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading)
                {
                    if (value != null && value.BoPhan != BoPhan)
                        BoPhan = value.BoPhan;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 1")]
        public int Ngay1
        {
            get
            {
                return _Ngay1;
            }
            set
            {
                SetPropertyValue("Ngay1", ref _Ngay1, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 2")]
        public int Ngay2
        {
            get
            {
                return _Ngay2;
            }
            set
            {
                SetPropertyValue("Ngay2", ref _Ngay2, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 3")]
        public int Ngay3
        {
            get
            {
                return _Ngay3;
            }
            set
            {
                SetPropertyValue("Ngay3", ref _Ngay3, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 4")]
        public int Ngay4
        {
            get
            {
                return _Ngay4;
            }
            set
            {
                SetPropertyValue("Ngay4", ref _Ngay4, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 5")]
        public int Ngay5
        {
            get
            {
                return _Ngay5;
            }
            set
            {
                SetPropertyValue("Ngay5", ref _Ngay5, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 6")]
        public int Ngay6
        {
            get
            {
                return _Ngay6;
            }
            set
            {
                SetPropertyValue("Ngay6", ref _Ngay6, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 7")]
        public int Ngay7
        {
            get
            {
                return _Ngay7;
            }
            set
            {
                SetPropertyValue("Ngay7", ref _Ngay7, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 8")]
        public int Ngay8
        {
            get
            {
                return _Ngay8;
            }
            set
            {
                SetPropertyValue("Ngay8", ref _Ngay8, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 9")]
        public int Ngay9
        {
            get
            {
                return _Ngay9;
            }
            set
            {
                SetPropertyValue("Ngay9", ref _Ngay9, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 10")]
        public int Ngay10
        {
            get
            {
                return _Ngay10;
            }
            set
            {
                SetPropertyValue("Ngay10", ref _Ngay10, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 11")]
        public int Ngay11
        {
            get
            {
                return _Ngay11;
            }
            set
            {
                SetPropertyValue("Ngay11", ref _Ngay11, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 12")]
        public int Ngay12
        {
            get
            {
                return _Ngay12;
            }
            set
            {
                SetPropertyValue("Ngay12", ref _Ngay12, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 13")]
        public int Ngay13
        {
            get
            {
                return _Ngay13;
            }
            set
            {
                SetPropertyValue("Ngay13", ref _Ngay13, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 14")]
        public int Ngay14
        {
            get
            {
                return _Ngay14;
            }
            set
            {
                SetPropertyValue("Ngay14", ref _Ngay14, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 15")]
        public int Ngay15
        {
            get
            {
                return _Ngay15;
            }
            set
            {
                SetPropertyValue("Ngay15", ref _Ngay15, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 16")]
        public int Ngay16
        {
            get
            {
                return _Ngay16;
            }
            set
            {
                SetPropertyValue("Ngay16", ref _Ngay16, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 17")]
        public int Ngay17
        {
            get
            {
                return _Ngay17;
            }
            set
            {
                SetPropertyValue("Ngay17", ref _Ngay17, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 18")]
        public int Ngay18
        {
            get
            {
                return _Ngay18;
            }
            set
            {
                SetPropertyValue("Ngay18", ref _Ngay18, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 19")]
        public int Ngay19
        {
            get
            {
                return _Ngay19;
            }
            set
            {
                SetPropertyValue("Ngay19", ref _Ngay19, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 20")]
        public int Ngay20
        {
            get
            {
                return _Ngay20;
            }
            set
            {
                SetPropertyValue("Ngay20", ref _Ngay20, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 21")]
        public int Ngay21
        {
            get
            {
                return _Ngay21;
            }
            set
            {
                SetPropertyValue("Ngay21", ref _Ngay21, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 22")]
        public int Ngay22
        {
            get
            {
                return _Ngay22;
            }
            set
            {
                SetPropertyValue("Ngay22", ref _Ngay22, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 23")]
        public int Ngay23
        {
            get
            {
                return _Ngay23;
            }
            set
            {
                SetPropertyValue("Ngay23", ref _Ngay23, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 24")]
        public int Ngay24
        {
            get
            {
                return _Ngay24;
            }
            set
            {
                SetPropertyValue("Ngay24", ref _Ngay24, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 25")]
        public int Ngay25
        {
            get
            {
                return _Ngay25;
            }
            set
            {
                SetPropertyValue("Ngay25", ref _Ngay25, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 26")]
        public int Ngay26
        {
            get
            {
                return _Ngay26;
            }
            set
            {
                SetPropertyValue("Ngay26", ref _Ngay26, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 27")]
        public int Ngay27
        {
            get
            {
                return _Ngay27;
            }
            set
            {
                SetPropertyValue("Ngay27", ref _Ngay27, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 28")]
        public int Ngay28
        {
            get
            {
                return _Ngay28;
            }
            set
            {
                SetPropertyValue("Ngay28", ref _Ngay28, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 29")]
        public int Ngay29
        {
            get
            {
                return _Ngay29;
            }
            set
            {
                SetPropertyValue("Ngay29", ref _Ngay29, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 30")]
        public int Ngay30
        {
            get
            {
                return _Ngay30;
            }
            set
            {
                SetPropertyValue("Ngay30", ref _Ngay30, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày 31")]
        public int Ngay31
        {
            get
            {
                return _Ngay31;
            }
            set
            {
                SetPropertyValue("Ngay31", ref _Ngay31, value);
                if (!IsLoading)
                {
                    TinhTong();
                }
            }
        }

        [ImmediatePostData]
        [ReadOnly(true)]
        [ModelDefault("Caption", "Tổng cộng")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public int TongCong
        {
            get
            {
                return _TongCong;
            }
            set
            {
                SetPropertyValue("TongCong", ref _TongCong, value);
                if (IsLoading)
                {
                    Focus = true;
                }
            }
        }

        //
        private bool _Focus;
        [NonPersistent]
        [Browsable(false)]
        public bool Focus
        {
            get
            {
                return _Focus;
            }
            set
            {
                SetPropertyValue("Focus", ref _Focus, value);
            }
        }

        public CC_ChiTietChamCongKhac(Session session) : base(session) { }


        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
                NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNVList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNVList();
        }

        void TinhTong()
        {
            int tong = Ngay1 + Ngay2 + Ngay3 + Ngay4 + Ngay5 + Ngay6 + Ngay7 + Ngay8 + Ngay9 + Ngay10 + Ngay11 + Ngay12 + Ngay13
                       + Ngay14 + Ngay15 + Ngay16 + Ngay17 + Ngay18 + Ngay19 + Ngay20 + Ngay21 + Ngay22 + Ngay23 + Ngay24
                       + Ngay25 + Ngay26 + Ngay27 + Ngay28 + Ngay29 + Ngay30 + Ngay31;
            //

            this.TongCong = tong;
        }
    }
}
