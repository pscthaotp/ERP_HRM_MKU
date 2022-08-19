using System.Collections.Generic;

namespace ERP.Module.WebAPI.Models
{
    public class URMForm
    {
        public int ID { get; set; }
        public string FormID { get; set; }
        public string FormName { get; set; }
        public string Descriptions { get; set; }
        public int ObjectID { get; set; }
        public List<URMControl> ListControls { get; set; }

        public URMForm() { }
    }
}
