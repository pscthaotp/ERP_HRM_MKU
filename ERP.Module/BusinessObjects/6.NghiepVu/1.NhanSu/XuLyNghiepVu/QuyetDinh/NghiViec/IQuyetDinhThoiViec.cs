using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using System;
using System.Collections.Generic;
using System.Linq;

//
namespace ERP.Module.NghiepVu.NhanSu.XuLyNghiepVu
{
    public interface IQuyetDinhThoiViec
    {
         void Save(Session session, QuyetDinhThoiViec obj);

         void Delete(Session session, QuyetDinhThoiViec obj);
    }
}
