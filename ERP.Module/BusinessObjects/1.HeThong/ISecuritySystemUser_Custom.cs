using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;

namespace ERP.Module.HeThong
{
    public interface ISecuritySystemUser_Custom
    {
        SecuritySystemUser_Custom SecuritySystemUser { get; set; }
    }
}
