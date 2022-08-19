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
    [ModelDefault("Caption", "Cấu hình tài sản")]
    public class CauHinhTaiSan : BaseObject
    {
        private int _SoBatDauDieuChuyen;
        private string _MauSoDieuChuyen;
        private bool _TuDongTaoSoDieuChuyen = true;
        //
        private int _SoBatDauDenBu;
        private string _MauSoDenBu;
        private bool _TuDongTaoSoDenBu = true;
        //
        private int _SoBatDauTraTS;
        private string _MauSoTraTS;
        private bool _TuDongTaoSoTraTS = true;
        //
        private int _SoBatDauNghiemThuTS;
        private string _MauSoNghiemThuTS;
        private bool _TuDongTaoSoNghiemThuTS = true;
        //
        private int _SoBatDauBBSuaChua;
        private string _MauSoBBSuaChua;
        private bool _TuDongTaoSoBBSuaChua = true;
        //
        private int _SoBatDauBBThanhLy;
        private string _MauSoBBThanhLy;
        private bool _TuDongTaoSoBBThanhLy = true;
        //
        private int _SoBatDauChungTuGhiTang;
        private string _MauSoChungTuGhiTang;
        private bool _TuDongTaoSoChungTuGhiTang = true;
        //
        private int _SoBatDauDeNghiCC;
        private string _MauSoDeNghiCC;
        private bool _TuDongTaoSoDeNghiCC = true;
        //
        private int _SoBatDauDeNghiThue;
        private string _MauSoDeNghiThue;
        private bool _TuDongTaoSoDeNghiThue = true;
        //
        private int _SoBatDauDeNghiChoThue;
        private string _MauSoDeNghiChoThue;
        private bool _TuDongTaoSoDeNghiChoThue = true;
        //
        private int _SoBatDauDeNghiThanhLy;
        private string _MauSoDeNghiThanhLy;
        private bool _TuDongTaoSoDeNghiThanhLy = true;
        //
        private int _SoBatDauToTrinhThanhLy;
        private string _MauSoToTrinhThanhLy;
        private bool _TuDongTaoSoToTrinhThanhLy = true;
        //
        private int _SoBatDauYeuCauSC;
        private string _MauSoYeuCauSC;
        private bool _TuDongTaoSoYeuCauSC = true;

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


        [ModelDefault("Caption", "Số bắt đầu đền bù")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDenBu")]
        public int SoBatDauDenBu
        {
            get
            {
                return _SoBatDauDenBu;
            }
            set
            {
                SetPropertyValue("SoBatDauDenBu", ref _SoBatDauDenBu, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đền bù")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDenBu")]
        public string MauSoDenBu
        {
            get
            {
                return _MauSoDenBu;
            }
            set
            {
                SetPropertyValue("MauSoDenBu", ref _MauSoDenBu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đền bù")]
        public bool TuDongTaoSoDenBu
        {
            get
            {
                return _TuDongTaoSoDenBu;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDenBu", ref _TuDongTaoSoDenBu, value);
            }
        }


        [ModelDefault("Caption", "Số bắt đầu trả TS")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoTraTS")]
        public int SoBatDauTraTS
        {
            get
            {
                return _SoBatDauTraTS;
            }
            set
            {
                SetPropertyValue("SoBatDauTraTS", ref _SoBatDauTraTS, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số trả TS")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoTraTS")]
        public string MauSoTraTS
        {
            get
            {
                return _MauSoTraTS;
            }
            set
            {
                SetPropertyValue("MauSoTraTS", ref _MauSoTraTS, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số trả TS")]
        public bool TuDongTaoSoTraTS
        {
            get
            {
                return _TuDongTaoSoTraTS;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoTraTS", ref _TuDongTaoSoTraTS, value);
            }
        }


        [ModelDefault("Caption", "Số bắt đầu nghiệm thu TS")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoNghiemThuTS")]
        public int SoBatDauNghiemThuTS
        {
            get
            {
                return _SoBatDauNghiemThuTS;
            }
            set
            {
                SetPropertyValue("SoBatDauNghiemThuTS", ref _SoBatDauNghiemThuTS, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số nghiệm thu TS")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoNghiemThuTS")]
        public string MauSoNghiemThuTS
        {
            get
            {
                return _MauSoNghiemThuTS;
            }
            set
            {
                SetPropertyValue("MauSoNghiemThuTS", ref _MauSoNghiemThuTS, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số nghiệm thu TS")]
        public bool TuDongTaoSoNghiemThuTS
        {
            get
            {
                return _TuDongTaoSoNghiemThuTS;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoNghiemThuTS", ref _TuDongTaoSoNghiemThuTS, value);
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


        [ModelDefault("Caption", "Số bắt đầu đề nghị thuê")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDeNghiThue")]
        public int SoBatDauDeNghiThue
        {
            get
            {
                return _SoBatDauDeNghiThue;
            }
            set
            {
                SetPropertyValue("SoBatDauDeNghiThue", ref _SoBatDauDeNghiThue, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đề nghị thuê")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDeNghiThue")]
        public string MauSoDeNghiThue
        {
            get
            {
                return _MauSoDeNghiThue;
            }
            set
            {
                SetPropertyValue("MauSoDeNghiThue", ref _MauSoDeNghiThue, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đề nghị thuê")]
        public bool TuDongTaoSoDeNghiThue
        {
            get
            {
                return _TuDongTaoSoDeNghiThue;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDeNghiThue", ref _TuDongTaoSoDeNghiThue, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu đề nghị cho thuê")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDeNghiChoThue")]
        public int SoBatDauDeNghiChoThue
        {
            get
            {
                return _SoBatDauDeNghiChoThue;
            }
            set
            {
                SetPropertyValue("SoBatDauDeNghiChoThue", ref _SoBatDauDeNghiChoThue, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số đề nghị cho thuê")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoDeNghiChoThue")]
        public string MauSoDeNghiChoThue
        {
            get
            {
                return _MauSoDeNghiChoThue;
            }
            set
            {
                SetPropertyValue("MauSoDeNghiChoThue", ref _MauSoDeNghiChoThue, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số đề nghị cho thuê")]
        public bool TuDongTaoSoDeNghiChoThue
        {
            get
            {
                return _TuDongTaoSoDeNghiChoThue;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoDeNghiChoThue", ref _TuDongTaoSoDeNghiChoThue, value);
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

        [ModelDefault("Caption", "Số bắt đầu yêu cầu SC")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoYeuCauSC")]
        public int SoBatDauYeuCauSC
        {
            get
            {
                return _SoBatDauYeuCauSC;
            }
            set
            {
                SetPropertyValue("SoBatDauYeuCauSC", ref _SoBatDauYeuCauSC, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số yêu cầu SC")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoYeuCauSC")]
        public string MauSoYeuCauSC
        {
            get
            {
                return _MauSoYeuCauSC;
            }
            set
            {
                SetPropertyValue("MauSoYeuCauSC", ref _MauSoYeuCauSC, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số yêu cầu SC")]
        public bool TuDongTaoSoYeuCauSC
        {
            get
            {
                return _TuDongTaoSoYeuCauSC;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoYeuCauSC", ref _TuDongTaoSoYeuCauSC, value);
            }
        }


        public CauHinhTaiSan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            _SoBatDauDieuChuyen = 1;
            _MauSoDieuChuyen = "MaTruong" + "BCT" + "mmyy" + "{0000#}";
            //
            _SoBatDauDenBu = 1;
            _MauSoDenBu = "MaTruong" + "BDT" + "mmyy" + "{0000#}";
            //
            _SoBatDauTraTS = 1;
            _MauSoTraTS = "MaTruong" + "BHT" + "mmyy" + "{0000#}";
            //
            _SoBatDauNghiemThuTS = 1;
            _MauSoNghiemThuTS = "MaTruong" + "BNT" + "mmyy" + "{0000#}";
            //
            _SoBatDauBBSuaChua = 1;
            _MauSoBBSuaChua = "MaTruong" + "BST" + "mmyy" + "{0000#}";
            //
            _SoBatDauBBThanhLy = 1;
            _MauSoBBThanhLy = "MaTruong" + "BTT" + "mmyy" + "{0000#}";
            //
            _SoBatDauChungTuGhiTang = 1;
            _MauSoChungTuGhiTang = "MaTruong" + "CTS" + "mmyy" + "{0000#}";
            //
            _SoBatDauDeNghiCC = 1;
            _MauSoDeNghiCC = "MaTruong" + "DCT" + "mmyy" + "{0000#}";
            //
            _SoBatDauDeNghiThue = 1;
            _MauSoDeNghiThue = "MaTruong" + "DMT" + "mmyy" + "{0000#}";
            //
            _SoBatDauDeNghiChoThue = 1;
            _MauSoDeNghiChoThue = "MaTruong" + "DTT" + "mmyy" + "{0000#}";
            //
            _SoBatDauDeNghiThanhLy = 1;
            _MauSoDeNghiThanhLy = "MaTruong" + "DTL" + "mmyy" + "{0000#}";
            //
            _SoBatDauToTrinhThanhLy = 1;
            _MauSoToTrinhThanhLy = "MaTruong" + "TTS" + "mmyy" + "{0000#}";
            //
            _SoBatDauYeuCauSC = 1;
            _MauSoYeuCauSC = "MaTruong" + "YST" + "mmyy" + "{0000#}";
        }
    }
}
