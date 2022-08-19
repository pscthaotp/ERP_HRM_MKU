using Autofac;
using ERP.Module.NghiepVu.NhanSu.XuLyNghiepVu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.Registers
{
    public static partial class Register
    {
        public static void Register_YS()
        {
            ContainerBuilder builder = new ContainerBuilder();
            #region 1. Quyết định
         
            builder.RegisterType<QuyetDinhThoiViec_YS>().As<IQuyetDinhThoiViec>();

            #endregion
            Container = builder.Build();
        }
    }
}