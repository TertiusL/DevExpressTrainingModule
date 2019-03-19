using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace Exercise2Solution.Module.BusinessObjects
{
    [DefaultClassOptions, ImageName("Department")]
    [System.ComponentModel.DefaultProperty("Description")]
    public class Department : BaseObject
    {
        public Department(Session session) : base(session) { }

        private string description;
        private Profile manager;

        public string Description
        {
            get { return description; }
            set { SetPropertyValue("Description", ref description, value); }
        }
        
        public Profile Manager
        {
            get { return manager; }
            set { SetPropertyValue("Manager", ref manager, value); }
        }
    }
}