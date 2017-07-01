using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdNewShapefile : YutaiCommand
    {
        public CmdNewShapefile(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_filegdb;
            this.m_caption = "Shape文件";
            this.m_category = "Catalog";
            this.m_message = "创建Shape文件";
            this.m_name = "Catalog_NewShapefile";
            this._key = "Catalog_NewShapefile";
            this.m_toolTip = "创建Shape文件";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.GxSelection != null)
                {
                    flag = ((((IGxSelection) _context.GxSelection).FirstObject is IGxDiskConnection
                        ? false
                        : !(((IGxSelection) _context.GxSelection).FirstObject is IGxFolder))
                        ? false
                        : true);
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmObjectClass _frmObjectClass = new frmObjectClass()
            {
                UseType = enumUseType.enumUTFeatureClass
            };
            IObjectClass objectClass = null;
            IWorkspaceFactory shapefileWorkspaceFactoryClass = new ShapefileWorkspaceFactory();
            IWorkspace workspace =
                shapefileWorkspaceFactoryClass.OpenFromFile(((IGxSelection) _context.GxSelection).FirstObject.FullName,
                    0);
            _frmObjectClass.Dataset = workspace;
            if (_frmObjectClass.ShowDialog() == DialogResult.OK)
            {
                objectClass = _frmObjectClass.ObjectClass;
            }
            if (objectClass != null)
            {
                ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
            }
        }
    }
}