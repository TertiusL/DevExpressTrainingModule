using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

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
        public string TelephoneNumber
        {
            get { return telephoneNumber; }
            set { SetPropertyValue("TelephoneNumber", ref telephoneNumber, value); }
        }
        public TelephoneType TelephoneType
        {
            get { return telephoneType; }
            set { telephoneType = value; }
        }

        /** Here we are setting a one to many relationship with 'Profile'. This is the 'one' part*/
        [Association("Profile-Telephone", typeof(Profile)), ImmediatePostData]
        public Profile Customer
        {
            get { return customer; }
            set { SetPropertyValue("Customer", ref customer, value); }
        }
    }
}