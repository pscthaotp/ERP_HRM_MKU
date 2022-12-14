using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultProperty("Caption")]
    [ImageName("Action_ReportTemplate")]
    [ModelDefault("Caption", "Mốc quy đổi thu nhập không thuế")]
    public class MocQuyDoiThuNhapKhongThue : BaseObject
    {

        private int _Muc7;
        private int _Muc6;
        private int _Muc5;
        private int _Muc4;
        private int _Muc3;
        private int _Muc2;
        private int _Muc1;
       
        private int _Moc1;
        private decimal _Tru1;
        private int _Moc2;
        private decimal _Tru2;
        private int _Moc3;
        private decimal _Tru3;
        private int _Moc4;
        private decimal _Tru4;
        private int _Moc5;
        private decimal _Tru5;
        private int _Moc6;
        private decimal _Tru6;
        private int _Moc7;
        private decimal _Tru7;

        [Browsable(false)]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("Mốc 1: {Moc1:N} - Mốc 2: {Moc2:N} ...", this);
            }
        }

        [ModelDefault("Caption", "Từ")]
        public int Moc1
        {
            get
            {
                return _Moc1;
            }
            set
            {
                SetPropertyValue("Moc1", ref _Moc1, value);
            }
        }

        [ModelDefault("Caption", "Đến")]
        [ImmediatePostData]
        public int Moc2
        {
            get
            {
                return _Moc2;
            }
            set
            {
                SetPropertyValue("Moc2", ref _Moc2, value);
                OnChanged("Moc2Mirror");
            }
        }

        [ModelDefault("Caption", "Mức quy đổi 1 (%)")]
        public int Muc1
        {
            get
            {
                return _Muc1;
            }
            set
            {
                SetPropertyValue("Muc1", ref _Muc1, value);
            }
        }

        [ModelDefault("Caption", "Trừ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru1
        {
            get
            {
                return _Tru1;
            }
            set
            {
                SetPropertyValue("Tru1", ref _Tru1, value);
            }
        }

        [ModelDefault("Caption", "Từ2")]
        [NonPersistent]
        public int Moc2Mirror
        {
            get
            {
                return _Moc2;
            }
        }

        [ModelDefault("Caption", "Đến2")]
        [ImmediatePostData]
        public int Moc3
        {
            get
            {
                return _Moc3;
            }
            set
            {
                SetPropertyValue("Moc3", ref _Moc3, value);
                OnChanged("Moc3Mirror");
            }
        }

        [ModelDefault("Caption", "Mức quy đổi 2 (%)")]
        public int Muc2
        {
            get
            {
                return _Muc2;
            }
            set
            {
                SetPropertyValue("Muc2", ref _Muc2, value);
            }
        }

        [ModelDefault("Caption", "Trừ2")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru2
        {
            get
            {
                return _Tru2;
            }
            set
            {
                SetPropertyValue("Tru2", ref _Tru2, value);
            }
        }

        [ModelDefault("Caption", "Từ3")]
        [NonPersistent]
        public int Moc3Mirror
        {
            get
            {
                return _Moc3;
            }
        }

        [ModelDefault("Caption", "Đến3")]
        [ImmediatePostData]
        public int Moc4
        {
            get
            {
                return _Moc4;
            }
            set
            {
                SetPropertyValue("Moc4", ref _Moc4, value);
                OnChanged("Moc4Mirror");
            }
        }

        [ModelDefault("Caption", "Mức quy đổi 3 (%)")]       
        public int Muc3
        {
            get
            {
                return _Muc3;
            }
            set
            {
                SetPropertyValue("Muc3", ref _Muc3, value);
            }
        }

        [ModelDefault("Caption", "Trừ3")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru3
        {
            get
            {
                return _Tru3;
            }
            set
            {
                SetPropertyValue("Tru3", ref _Tru3, value);
            }
        }

        [ModelDefault("Caption", "Từ4")]
        [NonPersistent]
        public int Moc4Mirror
        {
            get
            {
                return _Moc4;
            }
        }

        [ModelDefault("Caption", "Đến4")]
        [ImmediatePostData]
        public int Moc5
        {
            get
            {
                return _Moc5;
            }
            set
            {
                SetPropertyValue("Moc5", ref _Moc5, value);
                OnChanged("Moc5Mirror");
            }
        }

        [ModelDefault("Caption", "Mức quy đổi 4 (%)")]      
        public int Muc4
        {
            get
            {
                return _Muc4;
            }
            set
            {
                SetPropertyValue("Muc4", ref _Muc4, value);
            }
        }

        [ModelDefault("Caption", "Trừ4")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru4
        {
            get
            {
                return _Tru4;
            }
            set
            {
                SetPropertyValue("Tru4", ref _Tru4, value);
            }
        }

        [ModelDefault("Caption", "Từ5")]
        [NonPersistent]
        public int Moc5Mirror
        {
            get
            {
                return _Moc5;
            }
        }

        [ModelDefault("Caption", "Đến5")]
        [ImmediatePostData]
        public int Moc6
        {
            get
            {
                return _Moc6;
            }
            set
            {
                SetPropertyValue("Moc6", ref _Moc6, value);
                OnChanged("Moc6Mirror");
            }
        }

        [ModelDefault("Caption", "Mức quy đổi 5 (%)")]       
        public int Muc5
        {
            get
            {
                return _Muc5;
            }
            set
            {
                SetPropertyValue("Muc5", ref _Muc5, value);
            }
        }

        [ModelDefault("Caption", "Trừ5")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru5
        {
            get
            {
                return _Tru5;
            }
            set
            {
                SetPropertyValue("Tru5", ref _Tru5, value);
            }
        }

        [ModelDefault("Caption", "Từ6")]
        [NonPersistent]
        public int Moc6Mirror
        {
            get
            {
                return _Moc6;
            }
        }

        [ModelDefault("Caption", "Đến6")]
        [ImmediatePostData]
        public int Moc7
        {
            get
            {
                return _Moc7;
            }
            set
            {
                SetPropertyValue("Moc7", ref _Moc7, value);
                OnChanged("Moc7Mirror");
            }
        }

        [ModelDefault("Caption", "Mức quy đổi 6 (%)")]    
        public int Muc6
        {
            get
            {
                return _Muc6;
            }
            set
            {
                SetPropertyValue("Muc6", ref _Muc6, value);
            }
        }

        [ModelDefault("Caption", "Trừ6")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru6
        {
            get
            {
                return _Tru6;
            }
            set
            {
                SetPropertyValue("Tru6", ref _Tru6, value);
            }
        }

        [ModelDefault("Caption", "Từ7")]
        [NonPersistent]
        public int Moc7Mirror
        {
            get
            {
                return _Moc7;
            }
        }

        [ModelDefault("Caption", "Mức quy đổi 7 (%)")]       
        public int Muc7
        {
            get
            {
                return _Muc7;
            }
            set
            {
                SetPropertyValue("Muc7", ref _Muc7, value);
            }
        }

        [ModelDefault("Caption", "Trừ7")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal Tru7
        {
            get
            {
                return _Tru7;
            }
            set
            {
                SetPropertyValue("Tru7", ref _Tru7, value);
            }
        }

        public MocQuyDoiThuNhapKhongThue(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Moc1 = 0;
            Moc2 = 4750000;
            Moc3 = 9250000;
            Moc4 = 16050000;
            Moc5 = 27250000;
            Moc6 = 42250000;
            Moc7 = 61850000;
            Muc1 = 95;
            Muc2 = 90;
            Muc3 = 85;
            Muc4 = 80;
            Muc5 = 75;
            Muc6 = 70;
            Muc7 = 65;
            Tru1 = 0;
            Tru2 = 250000;
            Tru3 = 750000;
            Tru4 = 1650000;
            Tru5 = 3250000;
            Tru6 = 5850000;
            Tru7 = 9850000;
           
        }
    }

}
