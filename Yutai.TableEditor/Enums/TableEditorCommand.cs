// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  TableEditorCommand.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/06  15:25
// 更新时间 :  2017/06/06  15:25
namespace Yutai.Plugins.TableEditor.Enums
{
    public enum TableEditorCommand
    {
        Clear = 0,
        Close = 1,

    }

    public enum ItemDisplayStyle
    {
        None,
        Text,
        Image,
        ImageAndText,
    }

    public enum TableType
    {
        All,
        Selected
    }
}