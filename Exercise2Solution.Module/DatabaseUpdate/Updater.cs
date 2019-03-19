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
            Profile admin = ObjectSpace.FindObject<Profile>(new BinaryOperator("UserName", "Tertius"));
            Profile baseUser = ObjectSpace.FindObject<Profile>(new BinaryOperator("UserName", "Sara"));
            Profile canteenUser = ObjectSpace.FindObject<Profile>(new BinaryOperator("UserName", "Frik"));
            Profile staffUser = ObjectSpace.FindObject<Profile>(new BinaryOperator("UserName", "Piet"));

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
                admin = ObjectSpace.CreateObject<Profile>();
                admin.UserName = "Tertius";
                admin.SetPassword("1234");

                //Profile Class Fields
                admin.Surname = "Liebenberg";
                admin.Title = "Mr.";
                admin.Initials = "T";
                admin.EmailAddress = "tester@wester.com";
                admin.BuildingNumber = 2;
                admin.Floor = 2;
            }

            if (baseUser == null)
            {
                baseUser = ObjectSpace.CreateObject<Profile>();
                baseUser.UserName = "Sara";
                baseUser.SetPassword("");

                //Profile Class Fields
                baseUser.Surname = "Verster";
                baseUser.Title = "Ms.";
                baseUser.Initials = "S";
                baseUser.EmailAddress = "test@west.com";
                baseUser.BuildingNumber = 1;
                baseUser.Floor = 2;
            }

            if (canteenUser == null)
            {
                canteenUser = ObjectSpace.CreateObject<Profile>();
                canteenUser.UserName = "Frik";
                canteenUser.SetPassword("Cantina");

                //Profile Class Fields
                canteenUser.Surname = "Pogenpoel";
                canteenUser.Title = "Mr.";
                canteenUser.Initials = "FG";
                canteenUser.EmailAddress = "testing@westing.com";
                canteenUser.BuildingNumber = 1;
                canteenUser.Floor = 1;
            }

            if (staffUser == null)
            {
                staffUser = ObjectSpace.CreateObject<Profile>();
                staffUser.UserName = "Piet";
                staffUser.SetPassword("Staffer");

                //Profile Class Fields
                staffUser.Surname = "Boela";
                staffUser.Title = "Mr.";
                staffUser.Initials = "P";
                staffUser.EmailAddress = "tested@wested.com";
                staffUser.BuildingNumber = 2;
                staffUser.Floor = 1;
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
                userRole.AddTypePermission<Profile>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                userRole.AddObjectPermission<Profile>(SecurityOperations.ReadOnlyAccess, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                userRole.AddMemberPermission<Profile>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", null, SecurityPermissionState.Allow);
                userRole.AddMemberPermission<Profile>(SecurityOperations.Write, "StoredPassword", null, SecurityPermissionState.Allow);
                userRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);
                userRole.AddTypePermission<PermissionPolicyTypePermissionObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyMemberPermissionsObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                userRole.AddTypePermission<PermissionPolicyObjectPermissionsObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
            }

            if (canteenRole == null)
            {
                canteenRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                canteenRole.Name = "CanteenUsers";
                canteenRole.PermissionPolicy = SecurityPermissionPolicy.AllowAllByDefault;
                canteenRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                canteenRole.AddTypePermission<Profile>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                canteenRole.AddObjectPermission<Profile>(SecurityOperations.ReadOnlyAccess, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                canteenRole.AddMemberPermission<Profile>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", null, SecurityPermissionState.Allow);
                canteenRole.AddMemberPermission<Profile>(SecurityOperations.Write, "StoredPassword", null, SecurityPermissionState.Allow);
                canteenRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);
                canteenRole.AddTypePermission<PermissionPolicyTypePermissionObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                canteenRole.AddTypePermission<PermissionPolicyMemberPermissionsObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                canteenRole.AddTypePermission<PermissionPolicyObjectPermissionsObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
            }

            if (staffRole == null)
            {
                staffRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                staffRole.Name = "StaffUsers";
                staffRole.PermissionPolicy = SecurityPermissionPolicy.AllowAllByDefault;
                staffRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                staffRole.AddTypePermission<Profile>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                staffRole.AddObjectPermission<Profile>(SecurityOperations.ReadOnlyAccess, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                staffRole.AddMemberPermission<Profile>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", null, SecurityPermissionState.Allow);
                staffRole.AddMemberPermission<Profile>(SecurityOperations.Write, "StoredPassword", null, SecurityPermissionState.Allow);
                staffRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);
                staffRole.AddTypePermission<PermissionPolicyTypePermissionObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                staffRole.AddTypePermission<PermissionPolicyMemberPermissionsObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
                staffRole.AddTypePermission<PermissionPolicyObjectPermissionsObject>("Write;Delete;Navigate;Create", SecurityPermissionState.Deny);
            }         

            admin.Roles.Add(adminRole);
            baseUser.Roles.Add(userRole);
            canteenUser.Roles.Add(canteenRole);
            staffUser.Roles.Add(staffRole);
            ObjectSpace.CommitChanges();
        }
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
        }
    }
}
