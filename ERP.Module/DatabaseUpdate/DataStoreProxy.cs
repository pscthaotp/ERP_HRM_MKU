using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Helpers;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.DatabaseUpdate
{
    public class DataStoreProxy : IDataStore
    {
        static private string[] dataBase2Tables = new string[] { "tblFeeDetailTypes", "tblDinhPhiHocKy", "tblChinhSach", "tblChiTietChinhSach", "tblGoiPhi",
                                                                 "tblHopDong", "tblChiTietHopDong", "tblPassPort", "tblHinhThucThanhToan", "tblBusFee" };
        static private string[] dataBase3Tables = new string[] { "HeDaoTao", "Khoi","HocSinh","Lop","NamHoc" };
        private IDataStore dataStore1;
        private IDataStore dataStore2;
        private IDataStore dataStore3;
        private SimpleDataLayer dataLayer1;
        private SimpleDataLayer dataLayer2;
        private SimpleDataLayer dataLayer3;


        public static ReflectionDictionary GetDictionaryDB1(XPDictionary dictionary)
        {
            return GetDictionary(1, dictionary);
        }

        public static ReflectionDictionary GetDictionaryDB2(XPDictionary dictionary)
        {
            ReflectionDictionary reflectionDictionary = GetDictionary(2, dictionary);
            reflectionDictionary.QueryClassInfo(typeof(XPObjectType));
            return reflectionDictionary;
        }

        public static ReflectionDictionary GetDictionaryDB3(XPDictionary dictionary)
        {
            ReflectionDictionary reflectionDictionary = GetDictionary(3, dictionary);
            reflectionDictionary.QueryClassInfo(typeof(XPObjectType));
            return reflectionDictionary;
        }

        public void Initialize(XPDictionary dictionary, string connectionString1, string connectionString2, string connectionString3)
        {
            ReflectionDictionary dictionary1 = GetDictionaryDB1(dictionary);
            ReflectionDictionary dictionary2 = GetDictionaryDB2(dictionary);
            ReflectionDictionary dictionary3 = GetDictionaryDB3(dictionary);

            dataStore1 = XpoDefault.GetConnectionProvider(connectionString1, AutoCreateOption.DatabaseAndSchema);
            dataLayer1 = new SimpleDataLayer(dictionary1, dataStore1);

            dataStore2 = XpoDefault.GetConnectionProvider(connectionString2, AutoCreateOption.DatabaseAndSchema);
            dataLayer2 = new SimpleDataLayer(dictionary2, dataStore2);

            dataStore3 = XpoDefault.GetConnectionProvider(connectionString3, AutoCreateOption.DatabaseAndSchema);
            dataLayer3 = new SimpleDataLayer(dictionary3, dataStore3);
        }
        public void Initialize(XPDictionary dictionary, string connectionString1, string connectionString2)
        {
            ReflectionDictionary dictionary1 = GetDictionaryDB1(dictionary);
            ReflectionDictionary dictionary2 = GetDictionaryDB2(dictionary);

            dataStore1 = XpoDefault.GetConnectionProvider(connectionString1, AutoCreateOption.DatabaseAndSchema);
            dataLayer1 = new SimpleDataLayer(dictionary1, dataStore1);

            dataStore2 = XpoDefault.GetConnectionProvider(connectionString2, AutoCreateOption.DatabaseAndSchema);
            dataLayer2 = new SimpleDataLayer(dictionary2, dataStore2);
        }
        public AutoCreateOption AutoCreateOption
        {
            get
            {
                return AutoCreateOption.DatabaseAndSchema;
            }
        }


        public ModificationResult ModifyData(params ModificationStatement[] dmlStatements)
        {
            List<ModificationStatement> changesDB1 = new List<ModificationStatement>(dmlStatements.Length);
            List<ModificationStatement> changesDB2 = new List<ModificationStatement>(dmlStatements.Length);
            //List<ModificationStatement> changesDB3 = new List<ModificationStatement>(dmlStatements.Length);

            foreach (var stm in dmlStatements)
            {
                if (IsDSTable(stm.Table.Name) == 2)
                {
                    changesDB2.Add(stm);
                }/*
                else if (IsDSTable(stm.Table.Name) == 3)
                {
                    changesDB3.Add(stm);
                }*/
                else
                {
                    changesDB1.Add(stm);
                }
            }
            List<ParameterValue> resultSet = new List<ParameterValue>();
            if (changesDB1.Count > 0)
            {
                resultSet.AddRange(dataLayer1.ModifyData(changesDB1.ToArray()).Identities);
            }
            if (changesDB2.Count > 0)
            {
                resultSet.AddRange(dataLayer2.ModifyData(changesDB2.ToArray()).Identities);
            }
            return new ModificationResult(resultSet);
        }

        public SelectedData SelectData(params SelectStatement[] selects)
        {
            List<int> isDb = selects.Select(stmt => IsDSTable(stmt.Table.Name)).ToList();
            //
            List<SelectStatement> selectsDB1 = new List<SelectStatement>(selects.Length);
            List<SelectStatement> selectsDB2 = new List<SelectStatement>(selects.Length);
            //List<SelectStatement> selectsDB3 = new List<SelectStatement>(selects.Length);
            //
            for (int i = 0; i < isDb.Count; i++)
            {
                if (isDb[i] == 1)
                    selectsDB1.Add(selects[i]);
                else if (isDb[i] == 2)
                    selectsDB2.Add(selects[i]);
                //else
                    //selectsDB3.Add(selects[i]);
            }
            //
            var selectsDB1Result = (selectsDB1.Count == 0 ? Enumerable.Empty<SelectStatementResult>() : dataLayer1.SelectData(selectsDB1.ToArray()).ResultSet).GetEnumerator();
            var selectsDB2Result = (selectsDB2.Count == 0 ? Enumerable.Empty<SelectStatementResult>() : dataLayer2.SelectData(selectsDB2.ToArray()).ResultSet).GetEnumerator();
            //var selectsDB3Result = (selectsDB3.Count == 0 ? Enumerable.Empty<SelectStatementResult>() : dataLayer3.SelectData(selectsDB3.ToArray()).ResultSet).GetEnumerator();
            SelectStatementResult[] results = new SelectStatementResult[isDb.Count];
            for (int i = 0; i < results.Length; i++)
            {
                var enumerator = selectsDB1Result;  //Lưu ý
                //
                if (isDb[i] == 1)
                    enumerator = selectsDB1Result;
                else if (isDb[i] == 2)
                    enumerator = selectsDB2Result;
                //else
                    //enumerator = selectsDB3Result;
                //
                enumerator.MoveNext();
                //
                results[i] = enumerator.Current;
            }
            return new SelectedData(results);
        }


        public UpdateSchemaResult UpdateSchema(bool dontCreateIfFirstTableNotExist, params DBTable[] tables)
        {
            
            //Lưu ý chỉ update database mặc định

            List<DBTable> db1Tables = new List<DBTable>();
            List<DBTable> db2Tables = new List<DBTable>();
            List<DBTable> db3Tables = new List<DBTable>();

            foreach (DBTable table in tables)
            {
                if (IsDSTable(table.Name) == 1)
                {
                    db1Tables.Add(table);
                }
                /*
                else if (IsDSTable(table.Name) == 2)
                {
                    db2Tables.Add(table);
                }
                else
                {
                    db3Tables.Add(table);
                }*/
            }
            dataStore1.UpdateSchema(false, db1Tables.ToArray());
            //dataStore2.UpdateSchema(false, db2Tables.ToArray());
            //dataStore3.UpdateSchema(false, db3Tables.ToArray());
            //
            
            return UpdateSchemaResult.SchemaExists;
        }
        static private ReflectionDictionary GetDictionary(int isDb, XPDictionary dictionary)
        {
            ReflectionDictionary dictionaryResult = new ReflectionDictionary();
            foreach (XPClassInfo classInfo in dictionary.Classes)
            {
                if(isDb == 1 && IsDSTable(classInfo.TableName) == 1)
                {
                    dictionaryResult.QueryClassInfo(classInfo.ClassType);
                }
                if (isDb == 2 && IsDSTable(classInfo.TableName) == 2)
                {
                    dictionaryResult.QueryClassInfo(classInfo.ClassType);
                }
                /*
                if (isDb == 3 && IsDSTable(classInfo.TableName) == 3)
                {
                    dictionaryResult.QueryClassInfo(classInfo.ClassType);
                }*/
            }
            return dictionaryResult;
        }
        static private int IsDSTable(string Name)
        {
            int result = 0;
            if (dataBase2Tables.Contains(Name))
                result = 2;
            /*
            else if (dataBase3Tables.Contains(Name))
                result = 3;*/
            else
                result = 1;
            //
            return result;
        }
    }
}
