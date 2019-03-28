using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ViewVariantsModule;
using Exercise2Solution.Module.BusinessObjects;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;

namespace Exercise2Solution.Module.Controllers
{
    public partial class AdminViewVariantController : ViewController
    {
        //private ChoiceActionItem _viewVariantSwap;

        public AdminViewVariantController()
        {
            InitializeComponent();
            TargetObjectType = typeof(Orders);
            TargetViewType = ViewType.ListView;

            ChooseViewVariantAction.Items.Clear();
        }
        protected override void OnActivated()
        {
            base.OnActivated();

        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }        

        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            Frame.GetController<AdminViewVariantController>().ChooseViewVariantAction.ExecuteCompleted += new EventHandler<ActionBaseEventArgs>(ChooseViewVariantAction_ExecuteCompleted);
        }

        void ChooseViewVariantAction_ExecuteCompleted(object sender, ActionBaseEventArgs e)
        {
            if(e.ShowViewParameters.CreatedView.ObjectTypeInfo.Type == typeof(Orders))
            {
                SingleChoiceAction changeVariantAction = Frame.GetController<ChangeVariantController>().ChangeVariantAction;
                if (changeVariantAction.SelectedIndex == 0)
                {
                    changeVariantAction.DoExecute(changeVariantAction.Items[1]);
                }
                else
                {
                    changeVariantAction.DoExecute(changeVariantAction.Items[0]);
                }
            }
        }
    }
}
