using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using Exercise2Solution.Module.BusinessObjects;

namespace Exercise2Solution.Module.BusinessObjects
{
    [DefaultClassOptions, ImageName("Department")]
    [System.ComponentModel.DefaultProperty("Description")]
    public class Department : BaseObject
    {
        public Department(Session session) : base(session) { }

        private string description;
        private string manager;

        public string Description
        {
            get { return description; }
            set { SetPropertyValue("Description", ref description, value); }
        }
        public string Manager
        {
            get { return manager; }
            set { SetPropertyValue("Manager", ref manager, value); }
        }
    }
}