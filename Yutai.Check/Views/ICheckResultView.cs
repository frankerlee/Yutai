// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  ICheckResultView.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/08/10  09:59
// 更新时间 :  2017/08/10  09:59

using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.Check.Classes;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Check.Views
{
    public interface ICheckResultView : IMenuProvider
    {
        List<FeatureItem> FeatureItems { get; set; }
        IFeatureLayer FeatureLayer { set; }
        void Initialize(IAppContext context);
        void ReloadData();
    }
}