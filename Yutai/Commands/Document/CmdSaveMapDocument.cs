using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Services.Serialization;

namespace Yutai.Commands.Document
{
    public class CmdSaveMapDocument : YutaiCommand
    {
        private bool _enabled;

        public override bool Enabled
        {
            get
            {
                return _context.MainView.ControlType == GISControlType.MapControl ||
                       _context.MainView.ControlType == GISControlType.PageLayout;
               
            }
        }

        public CmdSaveMapDocument(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            ISecureContext secureContext=_context as ISecureContext;
            if (secureContext.YutaiProject == null || string.IsNullOrEmpty(secureContext.YutaiProject.MapDocumentName))
            {
                SaveFileDialog dialog =new SaveFileDialog();
                dialog.Title = "选择要保存的位置和文档名称";
                DialogResult result = dialog.ShowDialog();
                if (result != DialogResult.OK)
                    return;
                secureContext.YutaiProject.MapDocumentName = dialog.FileName;
            }

            if (_context.MainView.ControlType == GISControlType.MapControl)
            {
                if (_context.MainView.MapControl.CheckMxFile(secureContext.YutaiProject.MapDocumentName))
                {
                    //create a new instance of a MapDocument
                    IMapDocument mapDoc = new MapDocumentClass();
                    mapDoc.Open(secureContext.YutaiProject.MapDocumentName, string.Empty);

                    //Make sure that the MapDocument is not readonly
                    if (mapDoc.get_IsReadOnly(secureContext.YutaiProject.MapDocumentName))
                    {
                        MessageBox.Show("Map document is read only!");
                        mapDoc.Close();
                        return;
                    }

                    //Replace its contents with the current map
                    mapDoc.ReplaceContents((IMxdContents)_context.MainView.MapControl.Map);

                    //save the MapDocument in order to persist it
                    mapDoc.Save(mapDoc.UsesRelativePaths, false);

                    //close the MapDocument
                    mapDoc.Close();
                    return;
                }

            }
            else if (_context.MainView.ControlType == GISControlType.PageLayout)
            {
                if (_context.MainView.MapControl.CheckMxFile(secureContext.YutaiProject.MapDocumentName))
                {
                    //create a new instance of a MapDocument
                    IMapDocument mapDoc = new MapDocumentClass();
                    mapDoc.Open(secureContext.YutaiProject.MapDocumentName, string.Empty);

                    //Make sure that the MapDocument is not readonly
                    if (mapDoc.get_IsReadOnly(secureContext.YutaiProject.MapDocumentName))
                    {
                        MessageBox.Show("Map document is read only!");
                        mapDoc.Close();
                        return;
                    }

                    //Replace its contents with the current map
                    mapDoc.ReplaceContents((IMxdContents)_context.MainView.PageLayoutControl);

                    //save the MapDocument in order to persist it
                    mapDoc.Save(mapDoc.UsesRelativePaths, false);

                    //close the MapDocument
                    mapDoc.Close();
                    return;
                }

            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "保存二维文档";
            base.m_category = "Document";
            base.m_bitmap = Properties.Resources.icon_save;
            base.m_name = "File.Mxd.SaveMXD";
            base._key = "File_Mxd_SaveMXD";
            base.m_toolTip = "保存二维文档";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}