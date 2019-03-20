using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using DevExpress.Persistent.Validation;
using System.Linq;
using Exercise2Solution.Module.BusinessObjects;
using System.ComponentModel;

namespace Exercise2Solution.Module.BusinessObjects
{
    [DefaultClassOptions, ImageName("Telephone")]
    public class Telephone : BaseObject
    {

        public Telephone(Session session) : base(session) { }

        private TelephoneType telephoneType;
        private string telephoneNumber;
        private bool active;
        private DateTime dateCreated = DateTime.Now;
        private Profile customer;

        public DateTime DateCreated
        {
            get { return dateCreated; }
            set { SetPropertyValue("DateCreated", ref dateCreated, value); }
        }

        public bool Active
        {
            get { return active; }
            set { SetPropertyValue("Active", ref active, value); }
        }

        [ImmediatePostData]
        public TelephoneType TelephoneType
        {
            get { return telephoneType; }
            set
            {
                telephoneType = value;
                OnChanged("ExtNum");
            }
        }

        [ImmediatePostData]
        public string TelephoneNumber
        {
            get { return telephoneNumber; }
            set
            {
                if(SetPropertyValue("TelephoneNumber", ref telephoneNumber, value))
                {
                    OnChanged("ExtNum");
                }
            }
        }
        
        [PersistentAlias("Iif(TelephoneType = 'W','('+customer.BuildingNumber+')'+ Substring(TelephoneNumber,11),'')")]
        public string ExtNum
        {
            get
            {
                return (string)EvaluateAlias("ExtNum");
            }
        }

        /** Here we are setting a one to many relationship with 'Profile'. This is the 'one' part*/
        [Browsable(false)]
        [Association("Profile-Telephone")]
        public Profile Customer
        {
            get { return customer; }
            set { SetPropertyValue("Customer", ref customer, value); }
        }  
    }
}