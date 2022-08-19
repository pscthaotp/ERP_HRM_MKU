using System;

namespace ERP.Module.WebAPI.Models
{
    public class URMUserGroup
    {
        public int ID { get; set; }
        public string UserGroupID { get; set; }
        public string UserGroupName { get; set; }
        public int ParentID { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErpID { get; set; }
        public int LoaiBoPhan { get; set; }

        public URMUserGroup() { }
    }
}
