using System.Collections.Generic;

namespace ERP.Module.WebAPI.Models
{
    public class URMModule
    {
        public int ID { get; set; }
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string Descriptions { get; set; }
        public int ObjectID { get; set; }
        public List<URMForm> ListForms { get; set; }
        public int AppID { get; set; }
        public string NguoiTao { get; set; }

        public URMModule() { }
    }
}
