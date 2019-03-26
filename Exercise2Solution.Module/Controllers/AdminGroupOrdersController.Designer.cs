namespace Exercise2Solution.Module.Controllers
{
    partial class AdminGroupOrdersController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AdminGroupedOrdersAction = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // AdminGroupedOrdersAction
            // 
            this.AdminGroupedOrdersAction.Caption = "Admin Grouped Orders Action";
            this.AdminGroupedOrdersAction.ConfirmationMessage = null;
            this.AdminGroupedOrdersAction.Id = "AdminGroupedOrdersAction";
            this.AdminGroupedOrdersAction.ToolTip = null;
            // 
            // AdminGroupOrdersController
            // 
            this.Actions.Add(this.AdminGroupedOrdersAction);
            this.TargetObjectType = typeof(Exercise2Solution.Module.BusinessObjects.Orders);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction AdminGroupedOrdersAction;
    }
}
