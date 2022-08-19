
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Module.Win.Diagrams
{
    public class QuyTrinhFactory
    {
        private QuyTrinhFactory()
        { }
        public static LoaiQuyTrinhEnum Type { get; set; }

        public static QuyTrinhBaseController CreateControl(XafApplication app, IObjectSpace obs)
        {
            switch (Type)
            {
                case LoaiQuyTrinhEnum.QuyTrinhNghiViec:
                    return new QuyTrinhNghiViecController(app);
                case LoaiQuyTrinhEnum.QuyTrinhNangLuong:
                    return new QuyTrinhNangLuongController(app);
                default:
                    return new QuyTrinhNangLuongController(app);
            }
        }
    }
}
