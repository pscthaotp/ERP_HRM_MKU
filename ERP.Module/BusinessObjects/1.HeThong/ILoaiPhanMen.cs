using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.Enum.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
namespace ERP.Module.HeThong
{
    public interface ILoaiPhanMen
    {
        LoaiPhanMenEnum LoaiPhanMen { get; set; }
    }
}
