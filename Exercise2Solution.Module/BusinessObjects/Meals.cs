using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace Exercise2Solution.Module.BusinessObjects
{
    [DefaultClassOptions, ImageName("Meals")]
    [System.ComponentModel.DefaultProperty("Description")]
    public class Meals : BaseObject
    {
        public Meals(Session session) : base(session) { }

        private string description;
        private decimal price;
        private bool vatable;
        private bool deliverable;
        private int _appliedVat;

        public int AppliedVat
        {
            get { return _appliedVat; }
            set { SetPropertyValue("AppliedVat", ref _appliedVat, value); }
        }

        public bool Deliverable
        {
            get { return deliverable; }
            set { SetPropertyValue("Deliverable", ref deliverable, value); }
        }

        public bool Vatable
        {
            get { return vatable; }
            set { SetPropertyValue("Vatable", ref vatable, value); }
        }

        [ModelDefault("PropertyEditorType", "Exercise2Solution.Module.Web.Editors.CurrencyPropertyEditor")]
        public decimal Price
        {
            get { return price; }
            set { SetPropertyValue("Price", ref price, value); }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public string Description
        {
            get { return description; }
            set { SetPropertyValue("Description", ref description, value); }
        }
    }
}