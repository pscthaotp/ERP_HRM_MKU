using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ERP.Module.MailMerge.ChungTuTS_CCDC_VTHH
{
    public class Non_DeNghiFull : Non_DeNghi
    {
        public Non_DeNghiFull()
        {
            Master = new List<Non_DeNghiMaster>();
            Detail = new List<Non_DeNghiDetail>();
        }

    }
}
