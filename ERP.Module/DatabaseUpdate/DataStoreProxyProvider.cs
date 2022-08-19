using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.DatabaseUpdate
{
    public class DataStoreProxyProvider : IXpoDataStoreProvider {
        private readonly DataStoreProxy proxy;
        public DataStoreProxyProvider() {
            proxy = new DataStoreProxy();
        }
        public DevExpress.Xpo.DB.IDataStore CreateUpdatingStore(bool allowUpdateSchema, out IDisposable[] disposableObjects) {
            disposableObjects = null;
            return proxy;
        }
        public DevExpress.Xpo.DB.IDataStore CreateWorkingStore(out IDisposable[] disposableObjects) {
            disposableObjects = null;
            return proxy;
        }
        public DevExpress.Xpo.DB.IDataStore CreateSchemaCheckingStore(out IDisposable[] disposableObjects) {
            disposableObjects = null;
            return proxy;
        }
        public XPDictionary XPDictionary {
            get { return null; }
        }
        public string ConnectionString {
            get { return null; }
        }
        public bool IsInitialized {
            get;
            private set;
        }
        public void Initialize(XPDictionary dictionary, string connectionString1, string connectionString2, string connectionString3) {
            proxy.Initialize(dictionary, connectionString1, connectionString2, connectionString3);
            IsInitialized = true;
        }
        public void Initialize(XPDictionary dictionary, string connectionString1, string connectionString2)
        {
            proxy.Initialize(dictionary, connectionString1, connectionString2);
            IsInitialized = true;
        }
    }
}
