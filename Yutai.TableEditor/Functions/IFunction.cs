// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IFunction.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/15  16:29
// 更新时间 :  2017/06/15  16:29

using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Plugins.TableEditor.Functions
{
    public interface IFunction
    {
        string Caption { get; }
        string Category { get; }
        string Key { get; }
        string Expression { get; }
        string Description { get; }
        List<Parameter> Parameters { get; }
        List<IFunction> GetFunctions();
        IFunction GetFunction(string key);
        void Calculate(ITable table, string fieldName, string expression);
        string GetDescription();
    }
}