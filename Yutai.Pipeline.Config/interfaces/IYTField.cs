﻿// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IYTField.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/22  16:59
// 更新时间 :  2017/06/22  16:59

using System.Xml;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Config.interfaces
{
    public interface IYTField
    {
        string Name { get; set; }
        string AliasName { get; set; }
        string AutoNames { get; set; }
        int Length { get; set; }
        int Precision { get; set; }
        esriFieldType FieldType { get; set; }
        bool AllowNull { get; set; }

        void ReadFromXml(XmlNode xml);
        XmlNode ToXml();
    }
}