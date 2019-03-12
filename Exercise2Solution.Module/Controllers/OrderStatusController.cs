using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using Exercise2Solution.Module.BusinessObjects;
using DevExpress.ExpressApp.Security;
using System.Collections;

namespace Exercise2Solution.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class OrderStatusController : ViewController
    {
        private ChoiceActionItem setOrderStatusItem;

        public OrderStatusController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            OrderStatusAction.Items.Clear();

            setOrderStatusItem = new ChoiceActionItem(CaptionHelper.GetMemberCaption(typeof(Orders), "Status"), null);
            OrderStatusAction.Items.Add(setOrderStatusItem);
            FillItemWithEnumValues(setOrderStatusItem, typeof(Status));

        }

        private void FillItemWithEnumValues(ChoiceActionItem parentItem, Type enumType)
        {
            foreach (object current in Enum.GetValues(enumType))
            {
                /**We are building our enum here with values. */
                EnumDescriptor ed = new EnumDescriptor(enumType);
                ChoiceActionItem item = new ChoiceActionItem(ed.GetCaption(current), current);
                item.ImageName = ImageLoader.Instance.GetEnumValueImageName(current);
                parentItem.Items.Add(item);
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void OrderStatusAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = View is ListView ? Application.CreateObjectSpace(typeof(Orders)) : View.ObjectSpace;
            ArrayList objectsToProcess = new ArrayList(e.SelectedObjects);

            foreach (Object obj in objectsToProcess)
            {
                Orders objInNewObjectSpace = (Orders)objectSpace.GetObject(obj);
                /**We use 'SelectedChoiceActionItem' parameter to grap the value selected in our dropdown. */
                objInNewObjectSpace.Status = (Status)e.SelectedChoiceActionItem.Data;
            }

            if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
            {
                objectSpace.CommitChanges();
            }
            if (View is ListView)
            {
                objectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            }



        }

        private void OrderStatusController_Activated(object sender, EventArgs e)
        {
            View.SelectionChanged += new EventHandler(View_SelectionChanged);
            UpdateSetOrderStatusState();
        }

        void View_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSetOrderStatusState();
        }

        private void UpdateSetOrderStatusState()
        {
            bool isGranted = true;

            foreach (object selectedObject in View.SelectedObjects)
            {
                bool isStatusGranted = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace, typeof(Orders), SecurityOperations.Write, selectedObject, "Status"));

                if (!isStatusGranted)
                {
                    isGranted = false;
                }
            }
            OrderStatusAction.Enabled.SetItemValue("SecurityAllowance", isGranted);
        }

    }
}
