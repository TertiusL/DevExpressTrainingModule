using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;
using Exercise2Solution.Module.BusinessObjects;

namespace Exercise2Solution.Module.BusinessObjects
{
    [DefaultClassOptions, ImageName("Telephone")]
    public class Telephone : BaseObject
    {

        public Telephone(Session session) : base(session) { }

        private TelephoneType telephoneType;
        private string telephoneNumber;
        private string _extNum;
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

        public TelephoneType TelephoneType
        {
            get { return telephoneType; }
            set { telephoneType = value; }
        }

        public string TelephoneNumber
        {
            get { return telephoneNumber; }
            set { SetPropertyValue("TelephoneNumber", ref telephoneNumber, value); }
        }

        public string ExtNum
        {
            get { return _extNum; }   
            set { SetPropertyValue("ExtNum", ref _extNum, value); }
        }

        /** Here we are setting a one to many relationship with 'Profile'. This is the 'one' part*/
        [Association("Profile-Telephone")]
        public Profile Customer
        {
            get { return customer; }
            set { SetPropertyValue("Customer", ref customer, value); }
        }

        protected override void OnSaving()
        {
            string lastDigits;

            switch (TelephoneType)
            {
                case TelephoneType.C:
                    lastDigits = TelephoneNumber.Substring(11);
                    _extNum = lastDigits;
                    break;

                case TelephoneType.W:
                    lastDigits = TelephoneNumber.Substring(11);
                    _extNum = "(" + Customer.BuildingNumber + ")" + lastDigits;
                    break;

                case TelephoneType.H:
                    lastDigits = TelephoneNumber.Substring(11);
                    _extNum = lastDigits;
                    break;

                default:
                    lastDigits = TelephoneNumber.Substring(11);
                    _extNum = lastDigits;
                    break;
            }


            base.OnSaving();
        }
    }
}