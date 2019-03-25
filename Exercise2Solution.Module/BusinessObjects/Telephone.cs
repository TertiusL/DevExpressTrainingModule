using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using DevExpress.Persistent.Validation;
using System.Linq;
using Exercise2Solution.Module.BusinessObjects;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace Exercise2Solution.Module.BusinessObjects
{


    [DefaultClassOptions, ImageName("Telephone")]
    [RuleCriteria("CheckExtLenAndType", DefaultContexts.Save, "[TelephoneType] == ##Enum#Exercise2Solution.Module.BusinessObjects.TelephoneType,W# AND Len(ExtNum) == 4 ", CustomMessageTemplate = "Work ExtensionNumber has to be 4 digits.")]
    [RuleCriteria("Checasdasd", DefaultContexts.Save, "IsValid", CustomMessageTemplate = "IsValid thingy")]
    public class Telephone : BaseObject
    {
        public Telephone(Session session) : base(session) { }

        private TelephoneType telephoneType;
        private string telephoneNumber;
        private bool active;
        private DateTime dateCreated = DateTime.Now;
        private Profile customer;
        private string _extNum;
        private string _firstValue;


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
                SetPropertyValue("TelephoneType", ref telephoneType, value);

            }
        }

        [Appearance("ShowTeleNumber", Visibility = ViewItemVisibility.Hide, Criteria = "TelephoneType == 'W'", Context = "DetailView")]
        public string TelephoneNumber
        {
            get { return telephoneNumber; }
            set { SetPropertyValue("TelephoneNumber", ref telephoneNumber, value); }
        }

        [Appearance("ShowExtNumber", Visibility = ViewItemVisibility.Hide, Criteria = "TelephoneType != 'W'", Context = "DetailView")]
        [ModelDefault("DisplayFormat", "{0:####}")]
        [ModelDefault("EditMask", "####")]
        //[ImmediatePostData]
        public string ExtNum
        {
            get { return _extNum; }
            set
            {
                SetPropertyValue("ExtNum", ref _extNum, value);
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

        [RuleFromBoolProperty("IsValidExtNumber", DefaultContexts.Save,"ExtNumber has to start with the user's building number.")]
        [NonPersistent]
        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                if (_extNum.Length == 4)
                {
                    _firstValue = _extNum.Substring(0, 1);

                    string z = Customer != null ? Customer.BuildingNumber.ToString() : null;

                    if (_firstValue == z)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool testStuff( bool thingy)
        {
            return thingy;
        }

        protected override void OnSaving()
        {            
            if(Customer == null)
            {

            }

            base.OnSaving();
        }
    }
}