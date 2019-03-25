using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using Exercise2Solution.Module.BusinessObjects;

namespace Exercise2Solution.Module
{
    public class ValidateExtNumOperator :ICustomFunctionOperator
    {
        public string Name
        {
            get { return "ValidateExtNum"; }
        }

        public object Evaluate(params object[] operands)
        {
            return operands;
        }

        public Type ResultType(params Type[] operands)
        {
            return typeof(object);
        }

        static ValidateExtNumOperator()
        {
            ValidateExtNumOperator instance = new ValidateExtNumOperator();

            if (CriteriaOperator.GetCustomFunction(instance.Name) == null)
            {
                CriteriaOperator.RegisterCustomFunction(instance);
            }
        }

        public static void Register()
        {

        }
    }
}
