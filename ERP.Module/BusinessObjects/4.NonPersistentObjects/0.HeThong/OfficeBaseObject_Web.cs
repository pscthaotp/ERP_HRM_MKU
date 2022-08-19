using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using ERP.Module.Enum.Systems;

namespace ERP.Module.NonPersistentObjects.HeThong
{

    [ModelDefault("Caption", "Loại nhập")]
    [NonPersistent]
    public class OfficeBaseObject_Web : BaseObject
    {
        private LoaiOfficeEnum _LoaiOffice = LoaiOfficeEnum.Office2010;
        private FileData _File;

        //
        [ModelDefault("Caption", "Loại Office")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiOfficeEnum LoaiOffice
        {
            get
            {
                return _LoaiOffice;
            }
            set
            {
                SetPropertyValue("LoaiOffice", ref _LoaiOffice, value);
            }
        }

        [ModelDefault("Caption", "Lưu trữ")]
        [ImmediatePostData]
        public FileData File
        {
            get
            {
                return _File;
            }
            set
            {
                SetPropertyValue("File", ref _File, value);
            }
        }


        public OfficeBaseObject_Web(Session session): base(session)
        {
        }
    }
}
