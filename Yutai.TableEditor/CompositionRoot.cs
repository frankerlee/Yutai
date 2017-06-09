// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CompositionRoot.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/06  11:05
// 更新时间 :  2017/06/06  11:05

using Yutai.Plugins.Mvp;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor
{
    internal static class CompositionRoot
    {
        public static void Compose(IApplicationContainer container)
        {
            container.RegisterService<ITableEditorView, TableEditorDockPanel>()
                .RegisterSingleton<TableEditorPresenter>();
        }
    }
}