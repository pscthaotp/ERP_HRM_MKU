using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using System;
using System.Collections.Generic;
using System.Linq;

//
namespace ERP.Module.NghiepVu.NhanSu.XuLyNghiepVu
{
    public interface IQuyetDinhTuyenDung
    {
         void Save(Session session, ChiTietQuyetDinhTuyenDung obj);

         void Delete(Session session, ChiTietQuyetDinhTuyenDung obj);
    }
}
