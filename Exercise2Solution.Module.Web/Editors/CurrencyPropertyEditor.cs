using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Web.TestScripts;
using DevExpress.Web;

namespace Exercise2Solution.Module.Web.Editors
{
    [PropertyEditor(typeof(decimal), false)]
    public class CurrencyPropertyEditor : ASPxPropertyEditor, ITestable
    {
        private const string CurrencyFormat = "{0:c}";

        public CurrencyPropertyEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
        }

        public new ASPxTextBox Editor
        {
            get
            {
                return (ASPxTextBox)base.Editor;
            }
        }

        private void SelectedCurrencyChangedHandler(object source, EventArgs e)
        {
            ASPxEdit Edit = source as ASPxEdit;
            if (Edit.Value != null)
            {
                decimal Num;
                decimal.TryParse(Regex.Replace(Edit.Value.ToString(), @"[^0-9\.\-]", string.Empty), out Num);
                Edit.Value = Num;
            }
            base.EditValueChangedHandler(source, e);
        }

        protected override string GetPropertyDisplayValue()
        {
            return (string.Format(CurrencyFormat, base.PropertyValue));
        }

        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            if (control is ASPxTextBox)
            {
                ASPxTextBox aSPxTextBox = (ASPxTextBox)control;
                ASPxPureTextBoxBase aSPxPureTextBox = (ASPxPureTextBoxBase)control;
                aSPxPureTextBox.HorizontalAlign = HorizontalAlign.Right;
                aSPxTextBox.MaskSettings.Mask = "R <-99999999999..99999999999g>.<00..99>";
                aSPxTextBox.MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.All;
                aSPxTextBox.MaskSettings.AllowMouseWheel = false;
                aSPxTextBox.DisplayFormatString = CurrencyFormat;

                aSPxTextBox.ValueChanged += new EventHandler(this.SelectedCurrencyChangedHandler);
                aSPxTextBox.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
            }
        }

        protected override void SetImmediatePostDataScript(string script)
        {
            this.Editor.ClientSideEvents.ValueChanged = script;  //DX 14.2.4 needs WrapScript here
        }

        protected override WebControl CreateEditModeControlCore()
        {
            return new ASPxTextBox();
        }

        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            if (this.Editor != null)
            {
                this.Editor.ValueChanged -= new EventHandler(this.SelectedCurrencyChangedHandler);
            }
            base.BreakLinksToControl(unwireEventsOnly);
        }
    }
}