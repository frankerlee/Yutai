using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public class MxDocument
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private object object_0 = null;
        private string string_0 = "新文档.mxd";

        public MxDocument(object object_1)
        {
            this.object_0 = object_1;
        }

        public bool Close()
        {
            if (this.bool_0)
            {
                switch (MessageBox.Show("是否将改动保存到 " + Path.GetFileNameWithoutExtension(this.string_0), "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk))
                {
                    case DialogResult.Cancel:
                        this.bool_1 = true;
                        return true;

                    case DialogResult.No:
                        return false;
                }
                if (this.Save(""))
                {
                    this.bool_1 = true;
                    return true;
                }
            }
            this.bool_1 = false;
            return this.bool_1;
        }

        private void method_0(IMapDocument imapDocument_0)
        {
            if (this.object_0 is IPageLayoutControl2)
            {
                imapDocument_0.ReplaceContents(((IPageLayoutControl2) this.object_0).PageLayout as IMxdContents);
                try
                {
                    imapDocument_0.Save(true, true);
                }
                catch (Exception)
                {
                }
            }
            else if (this.object_0 is IMapControl2)
            {
                imapDocument_0.ReplaceContents(((IMapControl2) this.object_0).Map as IMxdContents);
                try
                {
                    imapDocument_0.Save(true, true);
                }
                catch (Exception)
                {
                }
            }
            else if (this.object_0 is MapAndPageLayoutControls)
            {
                imapDocument_0.ReplaceContents((this.object_0 as MapAndPageLayoutControls).PageLayoutControl.PageLayout as IMxdContents);
                imapDocument_0.Save(true, true);
            }
        }

        private void method_1()
        {
            if (this.object_0 is IMapControl2)
            {
                IMapControl2 control = this.object_0 as IMapControl2;
                control.ClearLayers();
                IMap map = control.Map;
                (map as IStandaloneTableCollection).RemoveAllStandaloneTables();
                (map as IGraphicsContainer).DeleteAllElements();
                control.Extent = control.FullExtent;
                control.ActiveView.Refresh();
            }
            else if (this.object_0 is IPageLayoutControl2)
            {
                IPageLayoutControl2 control2 = this.object_0 as IPageLayoutControl2;
                (control2.PageLayout as IGraphicsContainer).DeleteAllElements();
            }
            else if (this.object_0 is MapAndPageLayoutControls)
            {
                ((this.object_0 as MapAndPageLayoutControls).PageLayoutControl.PageLayout as IGraphicsContainer).DeleteAllElements();
            }
            //if (ApplicationRef.Application != null)
            //{
            //    ApplicationRef.Application.MapDocumentChanged();
            //}
        }

        public bool NewDocument()
        {
            if (this.bool_0)
            {
                DialogResult result = MessageBox.Show("是否将改动保存到 " + Path.GetFileNameWithoutExtension(this.string_0), "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Cancel)
                {
                    return true;
                }
                if ((result == DialogResult.Yes) && this.Save(""))
                {
                    return true;
                }
            }
            this.string_0 = "新文档.mxd";
            this.method_1();
            this.bool_0 = false;
            return false;
        }

        public bool Open(string string_1)
        {
            if (this.bool_0)
            {
                DialogResult result = MessageBox.Show("是否将改动保存到 " + Path.GetFileNameWithoutExtension(this.string_0), "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Cancel)
                {
                    return true;
                }
                if ((result == DialogResult.Yes) && this.Save(""))
                {
                    return true;
                }
            }
            this.string_0 = string_1;
            this.bool_0 = false;
            if (this.object_0 is IMapControl2)
            {
                (this.object_0 as IMapControl3).LoadMxFile(string_1, Type.Missing, "");
            }
            else if (this.object_0 is IPageLayoutControl2)
            {
                (this.object_0 as IPageLayoutControl2).LoadMxFile(string_1, "");
            }
            else if (this.object_0 is MapAndPageLayoutControls)
            {
                IMapDocument document = new MapDocument();
                if (document.get_IsPresent(string_1) && !document.get_IsPasswordProtected(string_1))
                {
                    document.Open(string_1, string.Empty);
                    IMaps maps =new Maps();
                    for (int i = 0; i < document.MapCount; i++)
                    {
                        IMap map = document.get_Map(i);
                        maps.Add(map);
                    }
                    IMap focusMap = document.ActiveView.FocusMap;
                    (this.object_0 as MapAndPageLayoutControls).PageLayoutControl.LoadMxFile(string_1, Missing.Value);
                    document.Close();
                }
            }
            return false;
        }

        public bool Save(string string_1)
        {
            IMapDocument document;
            if ((string_1 == null) || (string_1.Length == 0))
            {
                if (this.string_0 == "新文档.mxd")
                {
                    SaveFileDialog dialog = new SaveFileDialog {
                        OverwritePrompt = true,
                        Title = "保存为",
                        Filter = "ArcMap Document (*.mxd)|*.mxd|ArcMap Template (*.mxt)|*.mxt|Published Maps (*.pmf)|*.pmf|所有支持Map格式|*.mxd;*.mxt;*.pmf",
                        FilterIndex = 0,
                        RestoreDirectory = true,
                        FileName = this.string_0
                    };
                    switch (dialog.ShowDialog())
                    {
                        case DialogResult.Cancel:
                            return true;

                        case DialogResult.OK:
                            document = new MapDocument();
                            try
                            {
                                this.string_0 = dialog.FileName;
                                document.New(dialog.FileName);
                                this.method_0(document);
                                document.Close();
                            }
                            catch (Exception)
                            {
                            }
                            break;
                    }
                }
                else
                {
                    if (!this.bool_0)
                    {
                        return false;
                    }
                    try
                    {
                        if (File.Exists(this.string_0))
                        {
                            File.Delete(this.string_0);
                        }
                    }
                    catch
                    {
                    }
                    document = new MapDocument();
                    document.New(this.string_0);
                    this.method_0(document);
                    document.Close();
                }
                this.bool_0 = false;
            }
            else
            {
                try
                {
                    if (File.Exists(string_1))
                    {
                        File.Delete(string_1);
                    }
                }
                catch
                {
                }
                document = new MapDocument();
                this.string_0 = string_1;
                document.New(this.string_0);
                this.method_0(document);
                document.Close();
                this.bool_0 = false;
            }
            return false;
        }

        public string DocumentFilename
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public object Hook
        {
            get
            {
                return this.object_0;
            }
        }

        public bool IsDocumentChange
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }
    }
}

