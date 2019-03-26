﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private Profile _orderedBy;
        
        public Profile OrderedBy
        {
            get { return _orderedBy; }
            set { SetPropertyValue("OrderedBy", ref _orderedBy, value); }
        }
        
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

        //[Appearance("PriorityBackColorPink", AppearanceItemType = "ViewItem", Context = "Any", Criteria = "Delivered == null AND Delivery < DateTime.Now", BackColor = "255, 240, 240")]
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
                    if (Status == Status.Delivered)
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

        [ModelDefault("PropertyEditorType", "Exercise2Solution.Module.Web.Editors.CurrencyPropertyEditor")]
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

        [ModelDefault("PropertyEditorType", "Exercise2Solution.Module.Web.Editors.CurrencyPropertyEditor")]
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

        [ModelDefault("PropertyEditorType", "Exercise2Solution.Module.Web.Editors.CurrencyPropertyEditor")]
        [PersistentAlias("TotalVat + TotalExclVat")]
        public decimal TotalOrder
        {
            get
            {
                return (decimal)EvaluateAlias("TotalOrder");
            }
        }

        [Association("Order-OrderLine"), Aggregated, RuleRequiredField(DefaultContexts.Save)]
        public XPCollection<OrderLines> OrderLine
        {
            get
            {
                return GetCollection<OrderLines>("OrderLine");
            }
        }

        public void AddCalcTotal(List<OrderLines> orderList)
        {          
            if (orderList == null)
            {
                orderList = OrderLine.ToList();
            }

            decimal totalVatableWithVat = orderList.Where(w => w.OrderItem.Vatable == true).Sum(s => (s.OrderItem.Price * s.Quantity)) * (orderList.Sum(v => ((v.OrderItem.AppliedVat) / 100) / OrderLine.Count));

            decimal totalVatableWithoutVat = orderList.Where(w => w.OrderItem.Vatable == true).Sum(s => (s.OrderItem.Price * s.Quantity));

            decimal totalNonVatableWithoutVat = orderList.Where(w => w.OrderItem.Vatable == false).Sum(s => (s.OrderItem.Price * s.Quantity));

            totalVat = totalVatableWithVat;
            totalExclVat = totalVatableWithoutVat + totalNonVatableWithoutVat;
        }

        protected override void OnSaving()
        {
            AddCalcTotal(null);
            base.OnSaving();
        }

    }
}