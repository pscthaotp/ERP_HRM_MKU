﻿using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_DanToc")]
    [DefaultProperty("TenDanToc")]
    [ModelDefault("Caption", "Dân tộc")]
    public class DanToc : BaseObject
    {
        private string _MaQuanLy;
        private string _TenDanToc;

        [ModelDefault("Caption", "Mã Quản Lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên Dân Tộc")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenDanToc
        {
            get
            {
                return _TenDanToc;
            }
            set
            {
                SetPropertyValue("TenDanToc", ref _TenDanToc, value);
            }
        }
        private decimal _CapDo;
        [ModelDefault("Caption", "Cấp độ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal CapDo
        {
            get
            {
                return _CapDo;
            }
            set
            {
                SetPropertyValue("CapDo", ref _CapDo, value);
            }
        }

        public DanToc(Session session) : base(session) { }

    }

}
