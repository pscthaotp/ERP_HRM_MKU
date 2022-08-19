using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ERP.Module.PMS.CauHinh.HeSo;
using ERP.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.CauHinh.HeSo
{
    [ModelDefault("Caption","Hệ số giảng dạy theo dự án")]
    public class HeSoGiangDayTheoDuAn : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        #region Key 
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoGiangDayTheoDuAn")]
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
        #endregion
        private LoaiGiangVienEnum? _LoaiGiangVien; 
        private decimal _HeSo; 
        private bool _TrongDaLat; 
        private bool _NgoaiDaLat; 

        [ModelDefault("Caption", "Loại giảng viên")]
        public LoaiGiangVienEnum? LoaiGiangVien 
        {
            get {return _LoaiGiangVien;}
            set { SetPropertyValue("LoaiGiangVien", ref _LoaiGiangVien, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSoGiangDayTheoDuAn", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo
        {
            get { return _HeSo; }
            set { SetPropertyValue("HeSo", ref _HeSo, value); }
        }

        [ModelDefault("Caption", "Trong ĐL")]
        [ImmediatePostData]
        public bool TrongDaLat
        {
            get { return _TrongDaLat; }
            set 
            {
                SetPropertyValue("TrongDaLat", ref _TrongDaLat, value);
                if (!IsLoading && value == true)
                {
                    NgoaiDaLat = false;
                }
            }
        }
        [ModelDefault("Caption", "Ngoài ĐL")]
        [ImmediatePostData]
        public bool NgoaiDaLat
        {
            get { return _NgoaiDaLat; }
            set 
            {
                SetPropertyValue("NgoaiDaLat", ref _NgoaiDaLat, value);
                if (!IsLoading && value == true)
                {
                    TrongDaLat = false;
                }
            }
        }


        public HeSoGiangDayTheoDuAn(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
