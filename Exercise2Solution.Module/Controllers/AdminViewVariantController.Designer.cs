namespace Exercise2Solution.Module.Controllers
{
    partial class AdminViewVariantController
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
            this.ChooseViewVariantAction = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // ChooseViewVariantAction
            // 
            this.ChooseViewVariantAction.Caption = "Choose View Variant Action";
            this.ChooseViewVariantAction.ConfirmationMessage = null;
            this.ChooseViewVariantAction.Id = "ChooseViewVariantAction";
            this.ChooseViewVariantAction.TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueForAll;
            this.ChooseViewVariantAction.TargetObjectType = typeof(Exercise2Solution.Module.BusinessObjects.Orders);
            this.ChooseViewVariantAction.TargetViewNesting = DevExpress.ExpressApp.Nesting.Nested;
            this.ChooseViewVariantAction.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.ChooseViewVariantAction.ToolTip = null;
            this.ChooseViewVariantAction.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            // 
            // AdminViewVariantController
            // 
            this.Actions.Add(this.ChooseViewVariantAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction ChooseViewVariantAction;
    }
}
