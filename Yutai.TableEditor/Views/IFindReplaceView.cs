// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IFindReplaceView.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/13  15:31
// 更新时间 :  2017/06/13  15:31

using System;
using System.Windows.Forms;
using Yutai.Plugins.TableEditor.Enums;

namespace Yutai.Plugins.TableEditor.Views
{
    public interface IFindReplaceView
    {
        string SearchContent { get; }
        MatchType MatchType { get; }
        string FieldName { get; }
        DataGridViewCell CurrentCell { get; }
        DataGridViewCell MoveToNextCell(int columnIndex = -1);
        DataGridViewCell GetNextCell(DataGridViewCell cell, int columnIndex = -1);
        void Find(DataGridViewCell cell, bool isClick = false);
        void Close();
    }
}