using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.Data.SqlClient;

namespace ERP.Module.NghiepVu.MaTuDong
{
    public abstract class ManageKeyCompanyBase
    {        
        public abstract string ManageKeyCompany(CongTy congTy, params SqlParameter[] args);
    }
}
