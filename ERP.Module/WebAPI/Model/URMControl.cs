namespace ERP.Module.WebAPI.Models
{
    public class URMControl
    {
        public int ObjectID { get; set; }
        public string ControlID { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public string Descriptions { get; set; }
        public int? ParentID { get; set; }
        public bool IsModule { get; set; }
        public bool IsForm { get; set; }
        public bool IsControl { get; set; }
        public bool Enable { get; set; }
        public bool Visible { get; set; }
        public bool ReadOnly { get; set; }

        public URMControl() { }
    }
}
