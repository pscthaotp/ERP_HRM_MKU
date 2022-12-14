using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using System.ComponentModel;
using ERP.Module.NghiepVu.NhanSu.DinhBien;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn vị trí tuyển dụng")]
    public class TuyenDung_ChonViTriTuyenDung : BaseObject
    {

        [ModelDefault("Caption", "Danh sách vị trí tuyển dụng")]
        public XPCollection<TuyenDung_ChonViTriTuyenDungItem> ChonViTriTuyenDungList { get; set; }

        public TuyenDung_ChonViTriTuyenDung(Session session) : base(session) { }

        public void GetDanhSachViTriTuyenDungAll(QuanLyTuyenDung QuanLyTuyenDung, List<string> maQuanLy)
        {
            ChonViTriTuyenDungList = new XPCollection<TuyenDung_ChonViTriTuyenDungItem>(Session, false);
            //
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            CriteriaOperator filter1 = CriteriaOperator.Parse("QuanLyDinhBienChucDanh.CongTy.Oid=? and QuanLyDinhBienChucDanh.NienDoTaiChinh.Oid=?", QuanLyTuyenDung.CongTy.Oid, QuanLyTuyenDung.NienDoTaiChinh.Oid);
            NotOperator filter2 = new NotOperator(new InOperator("ChucDanh.MaQuanLy", maQuanLy));
            go.Operands.Add(filter1);
            go.Operands.Add(filter2);
            using (XPCollection<DinhBienChucDanh> dsViTriTuyenDungList = new XPCollection<DinhBienChucDanh>(Session, go))
            {            
                foreach (var item in dsViTriTuyenDungList)
                {
                    ChonViTriTuyenDungList.Add(new TuyenDung_ChonViTriTuyenDungItem(Session) { DinhBienChucDanh = item, BoPhan = item.BoPhan, ChucVu = item.ChucVu, ChucDanh = item.ChucDanh, MaQuanLy = item.ChucDanh.MaQuanLy, TenViTriTuyenDung = item.ChucDanh.TenChucDanh });
                }
            }
        }
    }
}
