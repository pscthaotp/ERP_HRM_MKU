using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using ERP.Module.HeThong;
using ERP.Module.Commons;
using System.Data;

namespace ERP.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

         
            ////Kiểm tra nhóm quyền không có tạo mới
            SecuritySystemRole adminRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                adminRole.Name = SecurityStrategy.AdministratorRoleName;
                adminRole.IsAdministrative = true;
                //Lưu dữ liệu lại
                ObjectSpace.CommitChanges();
            }

            //Kiểm tra tài khoản không có tạo mới
            SecuritySystemUser_Custom admin = ObjectSpace.FindObject<SecuritySystemUser_Custom>(new BinaryOperator("UserName", "psc"));
            if (admin == null)
            {
                admin = ObjectSpace.CreateObject<SecuritySystemUser_Custom>();
                admin.UserName = "psc";
                admin.SetPassword("pscvietnam");
                admin.Roles.Add(adminRole);
                //Lưu dữ liệu lại
                ObjectSpace.CommitChanges();
                string sql = "update SecuritySystemUser SET StoredPassword=null FROM dbo.SecuritySystemUser WHERE Oid='" + admin.Oid.ToString() + "'";
                DataProvider.ExecuteNonQuery(sql, CommandType.Text);
            } 
        }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
    }
}
