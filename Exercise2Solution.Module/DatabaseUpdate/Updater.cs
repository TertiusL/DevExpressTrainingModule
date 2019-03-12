using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using Exercise2Solution.Module.BusinessObjects;
using System;
using System.Linq;

namespace Exercise2Solution.Module.DatabaseUpdate
{

    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            PermissionPolicyRole adminRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
            PermissionPolicyRole userRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Users"));
            PermissionPolicyRole canteenRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "CanteenUsers"));
            PermissionPolicyRole staffRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "StaffUsers"));
            PermissionPolicyUser admin = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "Tertius"));
            PermissionPolicyUser baseUser = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "Sara"));
            PermissionPolicyUser canteenUser = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "Frik"));
            PermissionPolicyUser staffUser = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "Piet"));

            Telephone rdTelephoneNumber = ObjectSpace.FindObject<Telephone>(CriteriaOperator.Parse("TelephoneNumber == '0814669402'"));
            if (rdTelephoneNumber == null)
            {
                rdTelephoneNumber = ObjectSpace.CreateObject<Telephone>();
                rdTelephoneNumber.TelephoneNumber = "0814669402";
                rdTelephoneNumber.DateCreated = DateTime.Now;
                rdTelephoneNumber.Active = true;
                rdTelephoneNumber.TelephoneType = ((TelephoneType)1);
                rdTelephoneNumber.Customer = null;
            }

            if (admin == null)
            {
                admin = ObjectSpace.CreateObject<PermissionPolicyUser>();
                admin.UserName = "Tertius";
                admin.SetPassword("1234");
            }

            if (baseUser == null)
            {
                baseUser = ObjectSpace.CreateObject<PermissionPolicyUser>();
                baseUser.UserName = "Sara";
                baseUser.SetPassword("");
            }

            if (canteenUser == null)
            {
                baseUser = ObjectSpace.CreateObject<PermissionPolicyUser>();
                baseUser.UserName = "Frik";
                baseUser.SetPassword("Cantina");
            }

            if (staffUser == null)
            {
                baseUser = ObjectSpace.CreateObject<PermissionPolicyUser>();
                baseUser.UserName = "Piet";
                baseUser.SetPassword("Staffer");
            }

            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = SecurityStrategy.AdministratorRoleName;
                /** Setting the 'IsAdministrative = true ' allows the user with Admin role to have full access to all objects of all types.*/
                adminRole.IsAdministrative = true;
            }

            if (userRole == null)
            {
                userRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                userRole.Name = "Users";
                userRole.PermissionPolicy = SecurityPermissionPolicy.AllowAllByDefault;
                userRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyUser>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                userRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.ReadOnlyAccess, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                userRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", null, SecurityPermissionState.Allow);
                userRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", null, SecurityPermissionState.Allow);
                userRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);
                userRole.AddTypePermission<PermissionPolicyTypePermissionObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyMemberPermissionsObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyObjectPermissionsObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
            }

            if (canteenRole == null)
            {
                userRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                userRole.Name = "CanteenUsers";
                userRole.PermissionPolicy = SecurityPermissionPolicy.AllowAllByDefault;
                userRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyUser>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                userRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.ReadOnlyAccess, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                userRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", null, SecurityPermissionState.Allow);
                userRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", null, SecurityPermissionState.Allow);
                userRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);
                userRole.AddTypePermission<PermissionPolicyTypePermissionObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyMemberPermissionsObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyObjectPermissionsObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
            }

            if (staffRole == null)
            {
                userRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                userRole.Name = "StaffUsers";
                userRole.PermissionPolicy = SecurityPermissionPolicy.AllowAllByDefault;
                userRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyUser>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                userRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.ReadOnlyAccess, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                userRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", null, SecurityPermissionState.Allow);
                userRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", null, SecurityPermissionState.Allow);
                userRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);
                userRole.AddTypePermission<PermissionPolicyTypePermissionObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyMemberPermissionsObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyObjectPermissionsObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
            }
            else
            {
                /** Testing to see if it stops creating thousands of records */
            }

            /** Calls the DefaultRole method, which is depricated in our project at this point. Keeping it as example code */
            //CreateDefaultRole();          

            admin.Roles.Add(adminRole);
            baseUser.Roles.Add(userRole);
            canteenUser.Roles.Add(canteenRole);
            staffUser.Roles.Add(staffRole);
            ObjectSpace.CommitChanges();
        }
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
        /** private PermissionPolicyRole CreateDefaultRole() {
             PermissionPolicyRole defaultRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default"));
             if(defaultRole == null) {
                 defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                 defaultRole.Name = "Default";

                 defaultRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                 defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                 defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                 defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                 defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                 defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                 defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                 defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                 defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);                
             }
             return defaultRole;
         }
             */
    }
}
