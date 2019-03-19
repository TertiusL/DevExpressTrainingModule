using DevExpress.ExpressApp.Model;
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
    //[RuleCriteria("RuleCriteria for Quantity", DefaultContexts.Save, "Quantity <= 10 && Quantity > 0")]
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
        private int _vatAmount;

        public int VatAmount
        {
            get { return _vatAmount; }
            set { SetPropertyValue("VatAmount", ref _vatAmount, value); }
        }

        [ImmediatePostData]
        public Meals OrderItem
        {
            get { return orderItem; }
            set
            {
                if (SetPropertyValue("OrderItem", ref orderItem, value) && !IsLoading && !IsSaving)
                {
                    unitPrice = OrderItem.Price;
                    Quantity = 1;
                }
            }
        }

        [ImmediatePostData, RuleRange(1, 10, CustomMessageTemplate = "Has to be between 1 and 10.")]
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

        [ModelDefault("PropertyEditorType", "Exercise2Solution.Module.Web.Editors.CurrencyPropertyEditor")]
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { SetPropertyValue("UnitPrice", ref unitPrice, value); }
        }

        [ModelDefault("PropertyEditorType", "Exercise2Solution.Module.Web.Editors.CurrencyPropertyEditor")]
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

        protected override void OnDeleting()
        {
            var list = Order.OrderLine.Where(w => w.Oid != Oid).ToList();
            Order.AddCalcTotal(list);
        }

    }
}