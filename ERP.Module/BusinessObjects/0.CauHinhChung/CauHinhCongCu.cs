using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.CauHinhChungs
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình CCDC")]
    public class CauHinhCongCu : BaseObject
    {
        private int _SoBatDauDieuChuyen;
        private string _MauSoDieuChuyen;
        private bool _TuDongTaoSoDieuChuyen = true;
        //
        private int _SoBatDauChungTuGhiTang;
        private string _MauSoChungTuGhiTang;
        private bool _TuDongTaoSoChungTuGhiTang = true;
        //
        private int _SoBatDauDeNghiCC;
        private string _MauSoDeNghiCC;
        private bool _TuDongTaoSoDeNghiCC = true;
        //
        private int _SoBatDauDeNghiThanhLy;
        private string _MauSoDeNghiThanhLy;
        private bool _TuDongTaoSoDeNghiThanhLy = true;
        //
        private int _SoBatDauToTrinhThanhLy;
        private string _MauSoToTrinhThanhLy;
        private bool _TuDongTaoSoToTrinhThanhLy = true;
        //
        private int _SoBatDauBBThanhLy;
        private string _MauSoBBThanhLy;
        private bool _TuDongTaoSoBBThanhLy = true;
        //
        private int _SoBatDauBBNghiemThu;
        private string _MauSoBBNghiemThu;
        private bool _TuDongTaoSoBBNghiemThu = true;
        //
        private int _SoBatDauBBSuaChua;
        private string _MauSoBBSuaChua;
        private bool _TuDongTaoSoBBSuaChua = true;
        //
        private int _SoBatDauYCSuaChua;
        private string _MauSoYCSuaChua;
        private bool _TuDongTaoSoYCSuaChua = true;
        //
        private int _SoBatDauDeNghiVT;
        private string _MauSoDeNghiVT;
        private bool _TuDongTaoSoDeNghiVT = true;


        [ModelDefault("Caption", "Số bắt đầu điều chuyển")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDieuChuyen")]
        public int SoBatDauDieuChuyen
        {
            get
            {
                return _SoBatDauDieuChuyen;
            }
            set
            {
                SetPropertyValue("SoBatDauDieuChuyen", ref _SoBatDauDieuChuyen, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số điều chuyển")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDieuChuyen")]
        public string MauSoDieuChuyen
        {
            get
            {
                return _MauSoDieuChuyen;
            }
            set
            {
                SetPropertyValue("MauSoDieuChuyen", ref _MauSoDieuChuyen, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số điều chuyển")]
        public bool TuDongTaoSoDieuChuyen
        {
            get
            {
                return _TuDongTaoSoDieuChuyen;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDieuChuyen", ref _TuDongTaoSoDieuChuyen, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu chứng từ ghi tăng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoChungTuGhiTang")]
        public int SoBatDauChungTuGhiTang
        {
            get
            {
                return _SoBatDauChungTuGhiTang;
            }
            set
            {
                SetPropertyValue("SoBatDauChungTuGhiTang", ref _SoBatDauChungTuGhiTang, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số chứng từ ghi tăng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoChungTuGhiTang")]
        public string MauSoChungTuGhiTang
        {
            get
            {
                return _MauSoChungTuGhiTang;
            }
            set
            {
                SetPropertyValue("MauSoChungTuGhiTang", ref _MauSoChungTuGhiTang, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số chứng từ ghi tăng")]
        public bool TuDongTaoSoChungTuGhiTang
        {
            get
            {
                return _TuDongTaoSoChungTuGhiTang;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoChungTuGhiTang", ref _TuDongTaoSoChungTuGhiTang, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu đề nghị CC")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDeNghiCC")]
        public int SoBatDauDeNghiCC
        {
            get
            {
                return _SoBatDauDeNghiCC;
            }
            set
            {
                SetPropertyValue("SoBatDauDeNghiCC", ref _SoBatDauDeNghiCC, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đề nghị CC")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDeNghiCC")]
        public string MauSoDeNghiCC
        {
            get
            {
                return _MauSoDeNghiCC;
            }
            set
            {
                SetPropertyValue("MauSoDeNghiCC", ref _MauSoDeNghiCC, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đề nghị CC")]
        public bool TuDongTaoSoDeNghiCC
        {
            get
            {
                return _TuDongTaoSoDeNghiCC;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDeNghiCC", ref _TuDongTaoSoDeNghiCC, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu đề nghị thanh lý")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDeNghiThanhLy")]
        public int SoBatDauDeNghiThanhLy
        {
            get
            {
                return _SoBatDauDeNghiThanhLy;
            }
            set
            {
                SetPropertyValue("SoBatDauDeNghiThanhLy", ref _SoBatDauDeNghiThanhLy, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đề nghị thanh lý")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDeNghiThanhLy")]
        public string MauSoDeNghiThanhLy
        {
            get
            {
                return _MauSoDeNghiThanhLy;
            }
            set
            {
                SetPropertyValue("MauSoDeNghiThanhLy", ref _MauSoDeNghiThanhLy, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đề nghị thanh lý")]
        public bool TuDongTaoSoDeNghiThanhLy
        {
            get
            {
                return _TuDongTaoSoDeNghiThanhLy;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDeNghiThanhLy", ref _TuDongTaoSoDeNghiThanhLy, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu tờ trình thanh lý")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoToTrinhThanhLy")]
        public int SoBatDauToTrinhThanhLy
        {
            get
            {
                return _SoBatDauToTrinhThanhLy;
            }
            set
            {
                SetPropertyValue("SoBatDauToTrinhThanhLy", ref _SoBatDauToTrinhThanhLy, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số tờ trình thanh lý")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoToTrinhThanhLy")]
        public string MauSoToTrinhThanhLy
        {
            get
            {
                return _MauSoToTrinhThanhLy;
            }
            set
            {
                SetPropertyValue("MauSoToTrinhThanhLy", ref _MauSoToTrinhThanhLy, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số tờ trình thanh lý")]
        public bool TuDongTaoSoToTrinhThanhLy
        {
            get
            {
                return _TuDongTaoSoToTrinhThanhLy;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoToTrinhThanhLy", ref _TuDongTaoSoToTrinhThanhLy, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu biên bản thanh lý")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoBBThanhLy")]
        public int SoBatDauBBThanhLy
        {
            get
            {
                return _SoBatDauBBThanhLy;
            }
            set
            {
                SetPropertyValue("SoBatDauBBThanhLy", ref _SoBatDauBBThanhLy, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số biên bản thanh lý")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoBBThanhLy")]
        public string MauSoBBThanhLy
        {
            get
            {
                return _MauSoBBThanhLy;
            }
            set
            {
                SetPropertyValue("MauSoBBThanhLy", ref _MauSoBBThanhLy, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số biên bản thanh lý")]
        public bool TuDongTaoSoBBThanhLy
        {
            get
            {
                return _TuDongTaoSoBBThanhLy;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoBBThanhLy", ref _TuDongTaoSoBBThanhLy, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu đề nghị VT")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDeNghiVT")]
        public int SoBatDauDeNghiVT
        {
            get
            {
                return _SoBatDauDeNghiVT;
            }
            set
            {
                SetPropertyValue("SoBatDauDeNghiVT", ref _SoBatDauDeNghiVT, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đề nghị VT")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDeNghiVT")]
        public string MauSoDeNghiVT
        {
            get
            {
                return _MauSoDeNghiVT;
            }
            set
            {
                SetPropertyValue("MauSoDeNghiVT", ref _MauSoDeNghiVT, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đề nghị VT")]
        public bool TuDongTaoSoDeNghiVT
        {
            get
            {
                return _TuDongTaoSoDeNghiVT;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDeNghiVT", ref _TuDongTaoSoDeNghiVT, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu biên bản nghiệm thu sửa chữa")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoBBNghiemThu")]
        public int SoBatDauBBNghiemThu
        {
            get
            {
                return _SoBatDauBBNghiemThu;
            }
            set
            {
                SetPropertyValue("SoBatDauBBNghiemThu", ref _SoBatDauBBNghiemThu, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số biên bản nghiệm thu sửa chữa")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoBBNghiemThu")]
        public string MauSoBBNghiemThu
        {
            get
            {
                return _MauSoBBNghiemThu;
            }
            set
            {
                SetPropertyValue("MauSoBBNghiemThu", ref _MauSoBBNghiemThu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số biên bản nghiệm thu sửa chữa")]
        public bool TuDongTaoSoBBNghiemThu
        {
            get
            {
                return _TuDongTaoSoBBNghiemThu;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoBBNghiemThu", ref _TuDongTaoSoBBNghiemThu, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu biên bản sửa chữa")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoBBSuaChua")]
        public int SoBatDauBBSuaChua
        {
            get
            {
                return _SoBatDauBBSuaChua;
            }
            set
            {
                SetPropertyValue("SoBatDauBBSuaChua", ref _SoBatDauBBSuaChua, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số biên bản sửa chữa")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoBBSuaChua")]
        public string MauSoBBSuaChua
        {
            get
            {
                return _MauSoBBSuaChua;
            }
            set
            {
                SetPropertyValue("MauSoBBSuaChua", ref _MauSoBBSuaChua, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số biên bản sửa chữa")]
        public bool TuDongTaoSoBBSuaChua
        {
            get
            {
                return _TuDongTaoSoBBSuaChua;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoBBSuaChua", ref _TuDongTaoSoBBSuaChua, value);
            }
        }
        [ModelDefault("Caption", "Số bắt đầu yêu cầu sửa chữa")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoYCSuaChua")]
        public int SoBatDauYCSuaChua
        {
            get
            {
                return _SoBatDauYCSuaChua;
            }
            set
            {
                SetPropertyValue("SoBatDauYCSuaChua", ref _SoBatDauYCSuaChua, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số yêu cầu sửa chữa")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoYCSuaChua")]
        public string MauSoYCSuaChua
        {
            get
            {
                return _MauSoYCSuaChua;
            }
            set
            {
                SetPropertyValue("MauSoYCSuaChua", ref _MauSoYCSuaChua, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số yêu cầu sửa chữa")]
        public bool TuDongTaoSoYCSuaChua
        {
            get
            {
                return _TuDongTaoSoYCSuaChua;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoYCSuaChua", ref _TuDongTaoSoYCSuaChua, value);
            }
        }

        public CauHinhCongCu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            _SoBatDauDieuChuyen = 1;
            _MauSoDieuChuyen = "MaTruong" + "BCC" + "mmyy" + "{0000#}";
            //
            _SoBatDauChungTuGhiTang = 1;
            _MauSoChungTuGhiTang = "MaTruong" + "CTC" + "mmyy" + "{0000#}";
            //
            _SoBatDauDeNghiCC = 1;
            _MauSoDeNghiCC = "MaTruong" + "DCC" + "mmyy" + "{0000#}";
            //
            _SoBatDauDeNghiThanhLy = 1;
            _MauSoDeNghiThanhLy = "MaTruong" + "DTC" + "mmyy" + "{0000#}";
            //
            _SoBatDauToTrinhThanhLy = 1;
            _MauSoToTrinhThanhLy = "MaTruong" + "TTC" + "mmyy" + "{0000#}";
            //
            _SoBatDauToTrinhThanhLy = 1;
            _MauSoToTrinhThanhLy = "MaTruong" + "BTC" + "mmyy" + "{0000#}";
            //
            _SoBatDauBBNghiemThu = 1;
            _MauSoBBNghiemThu = "MaTruong" + "BNC" + "mmyy" + "{0000#}";
            //
            _SoBatDauBBSuaChua = 1;
            _MauSoBBSuaChua = "MaTruong" + "YSC" + "mmyy" + "{0000#}";
            //
            _SoBatDauYCSuaChua = 1;
            _MauSoYCSuaChua = "MaTruong" + "BTC" + "mmyy" + "{0000#}";
            //
            _SoBatDauDeNghiVT = 1;
            _MauSoDeNghiVT = "MaTruong" + "DCV" + "mmyy" + "{0000#}";
        }
    }
}
