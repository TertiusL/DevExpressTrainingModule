using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace Exercise2Solution.Module.BusinessObjects
{
    public enum Status
    {
        OnOrder,
        Processed,
        Delivered
    }

    [DefaultClassOptions, ImageName("Orders")]
    [System.ComponentModel.DefaultProperty("CreatedBy")]
    public class Orders : BaseObject
    {
        public Orders(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            createdBy = Session.GetObjectByKey<PermissionPolicyUser>(SecuritySystem.CurrentUserId);
        }

        private DateTime order = DateTime.Now;
        private DateTime delivery;
        private DateTime delivered;
        private Status status;
        private decimal totalVat;
        private decimal totalExclVat;
        PermissionPolicyUser createdBy;     
        
        public PermissionPolicyUser CreatedBy
        {
            get { return createdBy; }
            set
            {
                SetPropertyValue("CreatedBy", ref createdBy, value);
            }
        }

        public DateTime Order
        {
            get { return order; }
            set { SetPropertyValue("Order", ref order, value); }
        }

        public DateTime Delivery
        {
            get { return delivery; }
            set { SetPropertyValue("Delivery", ref delivery, value); }
        }

        public DateTime Delivered
        {
            get { return delivered; }
            set
            {
                SetPropertyValue("Delivered", ref delivered, value);
            }
        }

        public Status Status
        {
            get { return status; }
            set
            {
                if (SetPropertyValue("Status", ref status, value) && !IsLoading && !IsSaving)
                {
                    if(Status == Status.Delivered)
                    {
                        Delivered = DateTime.Now.Date;
                    }
                    else
                    {
                        if (Delivered != null || Delivered != DateTime.MinValue)
                        {
                            Delivered = DateTime.MinValue;
                        }
                    }
                }
            }
        }

        [ImmediatePostData]
        public decimal TotalExclVat
        {
            get { return totalExclVat; }
            set
            {
                if (SetPropertyValue("TotalExclVat", ref totalExclVat, value))
                {
                    OnChanged("TotalOrder");
                }
            }
        }

        [ImmediatePostData]
        public decimal TotalVat
        {
            get { return totalVat; }
            set
            {
                if (SetPropertyValue("TotalVat", ref totalVat, value))
                {
                    OnChanged("TotalOrder");
                }
            }
        }

        [PersistentAlias("TotalVat + TotalExclVat")]
        public decimal TotalOrder
        {
            get
            {
                object tempObj = EvaluateAlias("TotalOrder");

                if (tempObj != null)
                {
                    return ((decimal)tempObj);
                }
                else return 0;
            }
        }

        [Association("Order-OrderLine"), Aggregated]
        public XPCollection<OrderLines> OrderLine
        {
            get
            {
               return GetCollection<OrderLines>("OrderLine");
            }         
        }
    }
}