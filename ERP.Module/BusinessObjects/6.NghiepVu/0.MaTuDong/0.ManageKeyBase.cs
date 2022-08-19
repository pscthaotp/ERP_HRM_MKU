using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.NghiepVu.MaTuDong
{
    public abstract class ManageKeyBase
    {
        public abstract string ManageKey(params SqlParameter[] args);        
    }
}
