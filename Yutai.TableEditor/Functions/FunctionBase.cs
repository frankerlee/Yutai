using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseUI;
using Yutai.Plugins.TableEditor.Functions.Conversion;
using Yutai.Plugins.TableEditor.Functions.Date;
using Yutai.Plugins.TableEditor.Functions.Math;
using Yutai.Plugins.TableEditor.Functions.String;

namespace Yutai.Plugins.TableEditor.Functions
{
    class FunctionBase : IFunction
    {
        protected string _caption;
        protected string _category;
        protected string _key;
        protected string _expression;
        protected List<Parameter> _parameters;
        protected string _description;
        protected List<IFunction> _functions;

        public string Caption
        {
            get { return _caption; }
        }

        public string Category
        {
            get { return _category; }
        }

        public string Key
        {
            get { return _key; }
        }

        public string Expression
        {
            get { return _expression; }
        }

        public string Description
        {
            get { return _description; }
        }

        public List<Parameter> Parameters
        {
            get { return _parameters; }
        }
        public List<IFunction> GetFunctions()
        {
            _functions = new List<IFunction>()
            {
                new FunctionAbs(),
                new FunctionAtn(),
                new FunctionCos(),
                new FunctionExp(),
                new FunctionFix(),
                new FunctionInt(),
                new FunctionLog(),
                new FunctionRnd(),
                new FunctionSgn(),
                new FunctionSin(),
                new FunctionSqr(),
                new FunctionTan(),
                
                new FunctionInStr(),
                new FunctionLCase(),
                new FunctionLeft(),
                new FunctionLen(),
                new FunctionLTrim(),
                new FunctionMid(),
                new FunctionReplace(),
                new FunctionRight(),
                new FunctionRTrim(),
                new FunctionSpace(),
                new FunctionString(),
                new FunctionStrReverse(),
                new FunctionUCase(),

                new FunctionAsc(),
                new FunctionCBool(),
                new FunctionCDate(),
                new FunctionCDbl(),
                new FunctionChr(),
                new FunctionCInt(),
                new FunctionCLng(),
                new FunctionCSng(),
                new FunctionCStr(),
                new FunctionHex(),
                new FunctionOct(),

                new FunctionDate(),
                new FunctionDateAdd(),
                new FunctionDateDiff(),
                new FunctionDatePart(),
                new FunctionNow(),
            };
            return _functions;
        }

        public IFunction GetFunction(string key)
        {
            return _functions?.FirstOrDefault(c => c.Key == key);
        }

        public void Calculate(ITable table, string fieldName, string expression)
        {
        }

        public string GetDescription()
        {
            string des = _description;

            if (Parameters != null && Parameters.Count > 0)
            {
                for (int i = 0; i < Parameters.Count; i++)
                {
                    des += $"\r\n{Parameters[i].Name}:{Parameters[i].Description}";
                }
            }
            return des;
        }
    }
}
