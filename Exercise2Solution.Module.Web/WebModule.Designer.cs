﻿namespace Exercise2Solution.Module.Web {
    partial class Exercise2SolutionAspNetModule {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            // 
            // Exercise2SolutionAspNetModule
            // 
            this.RequiredModuleTypes.Add(typeof(Exercise2Solution.Module.Exercise2SolutionModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule));

        }

        #endregion
    }
}