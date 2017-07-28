// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  ICodeSetting.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/07/28  12:29
// 更新时间 :  2017/07/28  12:29

using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.Pipeline.Editor.Classes
{
    public interface ICodeSetting
    {
        string Code { get; set; }
        void Next();
        DialogResult ShowDialog();
    }
}