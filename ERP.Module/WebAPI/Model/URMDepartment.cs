using System;

namespace ERP.Module.WebAPI.Models
{
    public class URMDepartment
    {
        public int ID { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int ParentID { get; set; }
        public byte[] Image { get; set; }
        public string UpdateStaff { get; set; }
        public string ID_ERP { get; set; }
        public int DepartmentType { get; set; }
        public bool DepartmentStatus { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}