using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class ApplyTFTemplateCommand : ESRI.ArcGIS.ADF.BaseClasses.BaseCommand
    {
        private Hashtable hashtable_0 = new Hashtable();
        private IYTHookHelper _hookHelper;

        public ApplyTFTemplateCommand()
        {
            base.m_caption = "制图模板...";
            base.m_name = "ApplyTFTemplateCommand";
        }

        private void method_0(int int_0, IPageLayout ipageLayout_0)
        {
            ElementsTableStruct struct2 = new ElementsTableStruct();
            ITable table = AppConfigInfo.OpenTable(struct2.TableName);
            IQueryFilter queryFilter = new QueryFilterClass {
                WhereClause = struct2.TemplateIDFieldName + "=" + int_0.ToString()
            };
            ICursor o = table.Search(queryFilter, false);
            for (IRow row = o.NextRow(); row != null; row = o.NextRow())
            {
                IElement element = this.method_3(row, ipageLayout_0);
                if (element != null)
                {
                    (this._hookHelper.PageLayout as IGraphicsContainer).AddElement(element, -1);
                }
            }
            ComReleaser.ReleaseCOMObject(o);
            o = null;
        }

        private string method_1(string string_0)
        {
            string str = "\n";
            int startIndex = 0;
            string str2 = "";
            for (startIndex = 0; startIndex < string_0.Trim().Length; startIndex++)
            {
                str2 = str2 + string_0.Substring(startIndex, 1) + str;
            }
            return str2;
        }

        private IElement method_2(object object_0)
        {
            IMemoryBlobStream o = object_0 as IMemoryBlobStream;
            IPropertySet set = new PropertySetClass();
            IObjectStream pstm = new ObjectStreamClass {
                Stream = o
            };
            ESRI.ArcGIS.esriSystem.IPersistStream stream3 = set as ESRI.ArcGIS.esriSystem.IPersistStream;
            stream3.Load(pstm);
            IElement property = null;
            try
            {
                property = set.GetProperty("Element") as IElement;
            }
            catch (Exception)
            {
            }
            ComReleaser.ReleaseCOMObject(stream3);
            ComReleaser.ReleaseCOMObject(set);
            ComReleaser.ReleaseCOMObject(o);
            set = null;
            return property;
        }

        private IElement method_3(IRow irow_0, IPageLayout ipageLayout_0)
        {
            ITable table = irow_0.Table;
            ElementsTableStruct struct2 = new ElementsTableStruct();
            object obj2 = irow_0.get_Value(table.FindField(struct2.AttributesFieldName));
            IElement element = this.method_2(obj2);
            if (element is ITextElement)
            {
                string text = (element as ITextElement).Text;
                if (text[0] == '=')
                {
                    string[] strArray = text.Substring(1, text.Length - 1).Split(new char[] { '!' });
                    if (strArray[0] == "Field")
                    {
                        string str3;
                        string str4;
                        string str2 = "";
                        string[] strArray2 = strArray[1].Split(new char[] { '.' });
                        if (strArray2.Length > 2)
                        {
                            str2 = strArray2[0];
                            str3 = strArray2[1].Substring(1, strArray2[0].Length - 2);
                            str4 = strArray2[2].Substring(1, strArray2[1].Length - 2);
                        }
                        else
                        {
                            str3 = strArray2[0].Substring(1, strArray2[0].Length - 2);
                            str4 = strArray2[1].Substring(1, strArray2[1].Length - 2);
                        }
                        string str5 = "";
                        IQueryFilter queryFilter = null;
                        if (strArray.Length >= 3)
                        {
                            queryFilter = new QueryFilterClass();
                            string[] strArray3 = strArray[2].Split(new char[] { '#' });
                            int index = 0;
                            bool flag = true;
                            for (index = 0; index < strArray3.Length; index++)
                            {
                                if (flag)
                                {
                                    str5 = str5 + " " + strArray3[index];
                                }
                                else
                                {
                                    str5 = str5 + " " + this.hashtable_0[strArray3[index]].ToString();
                                }
                                flag = !flag;
                            }
                            queryFilter.WhereClause = str5;
                        }
                        try
                        {
                            ITable table2 = ((irow_0.Table as IDataset).Workspace as IFeatureWorkspace).OpenTable(str3);
                            ICursor o = table2.Search(queryFilter, false);
                            IDataStatistics statistics = null;
                            statistics = new DataStatisticsClass {
                                Field = str4,
                                Cursor = o
                            };
                            IStatisticsResults results = statistics.Statistics;
                            string str6 = str2;
                            switch (str6)
                            {
                                case null:
                                    break;

                                case "SUM":
                                    (element as ITextElement).Text = results.Sum.ToString();
                                    goto Label_034E;

                                case "MAX":
                                    (element as ITextElement).Text = results.Maximum.ToString();
                                    goto Label_034E;

                                case "MIN":
                                    (element as ITextElement).Text = results.Minimum.ToString();
                                    goto Label_034E;

                                default:
                                    if (str6 != "STD")
                                    {
                                        if (str6 != "MEAN")
                                        {
                                            break;
                                        }
                                        (element as ITextElement).Text = results.Mean.ToString();
                                    }
                                    else
                                    {
                                        (element as ITextElement).Text = results.StandardDeviation.ToString();
                                    }
                                    goto Label_034E;
                            }
                            IRow row = o.NextRow();
                            if (row != null)
                            {
                                try
                                {
                                    string str7 = row.get_Value(table2.FindField(str4)).ToString();
                                    (element as ITextElement).Text = str7;
                                }
                                catch
                                {
                                }
                            }
                        Label_034E:
                            ComReleaser.ReleaseCOMObject(o);
                        }
                        catch
                        {
                        }
                    }
                    else if (strArray[0] == "Param")
                    {
                        try
                        {
                            (element as ITextElement).Text = this.hashtable_0[strArray[1]].ToString();
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if (element != null)
            {
                int num3;
                double num4;
                double num5;
                IEnvelope envelope;
                object obj3 = irow_0.get_Value(table.FindField(struct2.LocationFieldName)).ToString();
                if (obj3 is DBNull)
                {
                    return element;
                }
                IPoint p = this.method_4(obj3.ToString(), ipageLayout_0, out num3, out num4, out num5);
                if (element is ITextElement)
                {
                    if ((element as IElementProperties).Type == "竖向")
                    {
                        string str8 = this.method_1((element as ITextElement).Text);
                        (element as ITextElement).Text = str8;
                    }
                    element.Geometry = p;
                    return element;
                }
                if (element is IMapSurroundFrame)
                {
                    envelope = element.Geometry.Envelope;
                    envelope.CenterAt(p);
                    element.Geometry = envelope;
                    return element;
                }
                envelope = element.Geometry.Envelope;
                envelope.CenterAt(p);
                element.Geometry = envelope;
            }
            return element;
        }

        private IPoint method_4(string string_0, IPageLayout ipageLayout_0, out int int_0, out double double_0, out double double_1)
        {
            int_0 = 0;
            double_0 = 0.0;
            double_1 = 0.0;
            IPoint point = new PointClass();
            point.PutCoords(0.0, 0.0);
            try
            {
                IPoint upperLeft;
                XmlDocument document = new XmlDocument();
                document.LoadXml(string_0);
                XmlNode node = document.ChildNodes[0];
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    XmlNode node2 = node.ChildNodes[i];
                    string str = node2.Attributes["name"].Value;
                    string s = node2.Attributes["value"].Value;
                    switch (str)
                    {
                        case "position":
                            int_0 = int.Parse(s);
                            break;

                        case "xoffset":
                            double_0 = double.Parse(s);
                            break;

                        case "yoffset":
                            double_1 = double.Parse(s);
                            break;
                    }
                }
                IGraphicsContainer container = ipageLayout_0 as IGraphicsContainer;
                container.Reset();
                IElement element = container.Next();
                IEnvelope bounds = null;
                while (element != null)
                {
                    if ((element is IElementProperties) && ((element as IElementProperties).Type == "图框"))
                    {
                        goto Label_015E;
                    }
                    element = container.Next();
                }
                goto Label_0179;
            Label_015E:
                bounds = new EnvelopeClass();
                element.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
            Label_0179:
                if (bounds == null)
                {
                    container.Reset();
                    IMapFrame frame = container.FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
                    bounds = (frame as IElement).Geometry.Envelope;
                }
                switch (int_0)
                {
                    case 0:
                        upperLeft = bounds.UpperLeft;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 1:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords((bounds.XMin + bounds.XMax) / 2.0, bounds.YMax);
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 2:
                        upperLeft = bounds.UpperRight;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 3:
                        upperLeft = bounds.UpperLeft;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 4:
                        upperLeft = bounds.UpperRight;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 5:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords(bounds.XMin, (bounds.YMin + bounds.YMax) / 2.0);
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 6:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords(bounds.XMax, (bounds.YMin + bounds.YMax) / 2.0);
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 7:
                        upperLeft = bounds.LowerLeft;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 8:
                        upperLeft = bounds.LowerRight;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 9:
                        upperLeft = bounds.LowerLeft;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 10:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords((bounds.XMin + bounds.XMax) / 2.0, bounds.YMin);
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 11:
                        upperLeft = bounds.LowerRight;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;
                }
                return point;
            }
            catch
            {
            }
            return point;
        }

        private void method_5(IActiveView iactiveView_0)
        {
            IGraphicsContainer graphicsContainer = iactiveView_0.GraphicsContainer;
            IMapFrame frame = graphicsContainer.FindFrame(iactiveView_0.FocusMap) as IMapFrame;
            graphicsContainer.Reset();
            IElement item = graphicsContainer.Next();
            List<IElement> list = new List<IElement>();
            while (item != null)
            {
                if (item != frame)
                {
                    list.Add(item);
                }
                item = graphicsContainer.Next();
            }
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    graphicsContainer.DeleteElement(list[i]);
                }
                catch
                {
                }
            }
        }

        public override void OnClick()
        {
            try
            {
                frmSelectCartoTemplateWizard wizard = new frmSelectCartoTemplateWizard {
                    Hashtable = this.hashtable_0
                };
                if (wizard.ShowDialog() == DialogResult.OK)
                {
                    this.method_5(this._hookHelper.ActiveView);
                    wizard.CartoTemplateData.GetTK(this._hookHelper.PageLayout);
                    this.method_0(wizard.CartoTemplateData.OID, this._hookHelper.PageLayout);
                    this._hookHelper.ActiveView.Refresh();
                }
            }
            catch
            {
            }
        }

        public override void OnCreate(object object_0)
        {
            this._hookHelper.Hook = object_0;
        }

        public override bool Enabled
        {
            get
            {
                return (this._hookHelper.ActiveView is IPageLayout);
            }
        }
    }
}

