using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace Exercise2Solution.Module.BusinessObjects
{
    public enum TelephoneType
    {
        C,
        W,
        H
    }

    [DefaultClassOptions, ImageName("Profile")]
    [System.ComponentModel.DefaultProperty("FullName")]
    [RuleCriteria("RuleCriteria for BuildingNumber", DefaultContexts.Save, "BuildingNumber >= 1 && BuildingNumber <= 5", CustomMessageTemplate ="Building Number has to be between 1 and 5!")]
    public class Profile : PermissionPolicyUser
    {
        public Profile(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        private string title;
        private string initials;
        private string surname;
        private string name;
        private string emailAddress;
        private Department department;
        private int buildingNumber = 1;
        private int floor = 1;

        [RuleRequiredField(DefaultContexts.Save)]
        public string Title
        {
            get { return title; }
            set { SetPropertyValue("Title", ref title, value); }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public string Initials
        {
            get { return initials; }
            set { SetPropertyValue("Initials", ref initials, value); }
        }

        [RuleRequiredField(DefaultContexts.Save), ImmediatePostData]
        public string Surname
        {
            get { return surname; }
            set
            {
                if (SetPropertyValue("Surname", ref surname, value))
                {
                    OnChanged("FullName");
                }
            }
        }

        [ImmediatePostData]
        public string Name
        {
            get { return name; }
            set
            {
                if (SetPropertyValue("Name", ref name, value))
                {
                    OnChanged("FullName");
                }
            }
        }

        [PersistentAlias("Concat([Name],' ',[Surname])")]
        public string FullName
        {
            get
            {
                return (string)EvaluateAlias("FullName");
            }
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set { SetPropertyValue("EmailAddress", ref emailAddress, value); }
        }

        public Department Department
        {
            get { return department; }
            set { SetPropertyValue("Department", ref department, value); }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public int BuildingNumber
        {
            get { return buildingNumber; }
            set { SetPropertyValue("BuildingNumber", ref buildingNumber, value); }
        }

        [RuleRequiredField(DefaultContexts.Save), RuleRange(1, 2, CustomMessageTemplate = "Must be either 1 or 2")]
        public int Floor
        {
            get { return floor; }
            set { SetPropertyValue("Floor", ref floor, value); }
        }

        /** Here we are setting a one to many relationship with 'Telephone'. This is the 'many' part*/
        [Association("Profile-Telephone")]
        public XPCollection<Telephone> TelephoneNumbers
        {
            get { return GetCollection<Telephone>("TelephoneNumbers"); }
        }

    }
}