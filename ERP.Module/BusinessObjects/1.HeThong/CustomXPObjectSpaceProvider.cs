using System.Data;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;

namespace ERP.Module.HeThong
{
    public class CustomXPObjectSpaceProvider : XPObjectSpaceProvider {
        public CustomXPObjectSpaceProvider(string connectionString, IDbConnection connection) :
            base(connectionString, connection) { }
        protected override UnitOfWork CreateUnitOfWork(IDataLayer dataLayer) {
            UnitOfWork result = new UnitOfWork(dataLayer);
            result.TrackPropertiesModifications = true;
            return result;
        }
    }
}
