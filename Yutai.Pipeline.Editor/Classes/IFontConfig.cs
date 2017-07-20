// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IFontConfig.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/07/20  10:59
// 更新时间 :  2017/07/20  10:59

using System.Drawing;

namespace Yutai.Pipeline.Editor.Classes
{
    public interface IFontConfig
    {
        Font Font { get; }
        Color Color { get; }
    }
}