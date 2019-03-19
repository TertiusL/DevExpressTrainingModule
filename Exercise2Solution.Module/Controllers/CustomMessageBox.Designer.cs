namespace Exercise2Solution.Module.Controllers
{
    partial class CustomMessageBox
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
            this.CustomMessageAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // CustomMessageAction
            // 
            this.CustomMessageAction.AcceptButtonCaption = null;
            this.CustomMessageAction.CancelButtonCaption = null;
            this.CustomMessageAction.Caption = "Custom Message Action";
            this.CustomMessageAction.ConfirmationMessage = null;
            this.CustomMessageAction.Id = "CustomMessageAction";
            this.CustomMessageAction.ToolTip = null;
            this.CustomMessageAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CustomMessageAction_Execute);
            // 
            // CustomMessageBox
            // 
            this.Actions.Add(this.CustomMessageAction);
            this.TargetObjectType = typeof(Exercise2Solution.Module.BusinessObjects.Orders);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CustomMessageAction;
    }
}
