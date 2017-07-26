// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IAnnotationSortingView.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/07/26  10:34
// 更新时间 :  2017/07/26  10:34

using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Pipeline.Editor.Views
{
    public interface IAnnotationSortingView : IMenuProvider
    {
        void Initialize(IAppContext context);
    }
}