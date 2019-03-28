using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using Exercise2Solution.Module.BusinessObjects;
using Exercise2Solution.Module.Reports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise2Solution.Module
{

    public sealed partial class Exercise2SolutionModule : ModuleBase
    {
        public Exercise2SolutionModule()
        {
            InitializeComponent();
            ValidateExtNumOperator.Register();
            BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            PredefinedReportsUpdater predefinedReportsUpdater = new PredefinedReportsUpdater(Application, objectSpace, versionFromDB);
            predefinedReportsUpdater.AddPredefinedReport<PastOrdersReport>("Past Orders Report", typeof(Orders));

            return new ModuleUpdater[] { updater, predefinedReportsUpdater };
        }
        public override void Setup(XafApplication application)
        {
            base.Setup(application);
            Application.LinkNewObjectToParentImmediately = true;
        }
        public override void CustomizeTypesInfo(ITypesInfo typesInfo)
        {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
        }
    }
}
