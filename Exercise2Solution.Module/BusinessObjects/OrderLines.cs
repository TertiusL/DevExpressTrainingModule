using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace Exercise2Solution.Module.BusinessObjects
{
    [DefaultClassOptions, ImageName("OrderLine")]
    [System.ComponentModel.DefaultProperty("OrderItem")]
    [RuleCriteria("RuleCriteria for Quantity", DefaultContexts.Save, "Quantity <= 10 && Quantity > 0")]
    public class OrderLines : BaseObject
    {
        public OrderLines(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        private Orders _order;
        private Meals orderItem;
        private int quantity;
        private decimal unitPrice;
        //private decimal exclVat;
        //private decimal inclVat;

        //private void VatStuffs()
        //{
        //    if ((OrderItem.Vatable))
        //    {
        //        SetPropertyValue("InclVat", ref inclVat, (Quantity * UnitPrice) * (decimal)0.15); 
        //    }
        //    else
        //    {
        //        SetPropertyValue("ExclVat", ref exclVat, Quantity * UnitPrice); 
        //    }
        //}

        //public decimal ExclVat
        //{
        //    get { return exclVat; }
            
        //}

        //public decimal InclVat
        //{
        //    get { return inclVat; }
        //}

        [ImmediatePostData]
        public Meals OrderItem
        {
            get { return orderItem; }
            set
            {
                if(SetPropertyValue("OrderItem", ref orderItem, value) && !IsLoading && !IsSaving)
                {
                    unitPrice += OrderItem.Price;
                    Quantity = 1;
                }
            }
        }

        [ImmediatePostData]
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (SetPropertyValue("Quantity", ref quantity, value))
                {
                    OnChanged("Amount");
                }
            }
        }

        [ImmediatePostData]
        public decimal UnitPrice
        {
            get { return unitPrice; }
        }

        [PersistentAlias("Quantity * UnitPrice")]
        public decimal Amount
        {
            get

            {
                object tempObj = EvaluateAlias("Amount");

                if (tempObj != null)
                {
                    return ((decimal)tempObj);
                }
                else return 0;
            }
        }

        [Association("Order-OrderLine")]
        public Orders Order
        {
            get { return _order; }
            set { SetPropertyValue("Order", ref _order, value); }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            //VatStuffs();
        }
    }
}