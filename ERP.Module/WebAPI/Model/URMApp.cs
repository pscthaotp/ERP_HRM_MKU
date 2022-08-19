using System.Collections.Generic;

namespace ERP.Module.WebAPI.Models
{
    public class URMApp
    {
        public int ID { get; set; }
        public string AppID { get; set; }
        public string AppName { get; set; }
        public string Descriptions { get; set; }
        public int MaBoPhan { get; set; }
        public string Username { get; set; }
        public int ObjectID { get; set; }
        public List<URMModule> ListModules { get; set; }
        public URMApp() { }
    }
}
