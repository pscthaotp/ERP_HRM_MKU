using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Chuyên đề giảng dạy")]
    [DefaultProperty("TenChuyenDeGiangDay")]
    [RuleCombinationOfPropertiesIsUnique("ChuyenDeGiangDay.Identifier", DefaultContexts.Save, "MaQuanLy;TenChuyenDeGiangDay")]
    public class ChuyenDeGiangDay : BaseObject
    {
        private string _MaQuanLy;
        private string _TenChuyenDeGiangDay;
        private string _NoiLuuTruTaiLieu;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên chuyên đề")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChuyenDeGiangDay
        {
            get
            {
                return _TenChuyenDeGiangDay;
            }
            set
            {
                SetPropertyValue("TenChuyenDeGiangDay", ref _TenChuyenDeGiangDay, value);
            }
        }

        [ModelDefault("Caption", "Nơi lưu trữ tài liệu")]
        public string NoiLuuTruTaiLieu
        {
            get
            {
                return _NoiLuuTruTaiLieu;
            }
            set
            {
                SetPropertyValue("NoiLuuTruTaiLieu", ref _NoiLuuTruTaiLieu, value);
            }
        }
        

        public ChuyenDeGiangDay(Session session) : base(session) { }
    }

}
