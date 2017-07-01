using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Carto.Library;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public sealed class CmdExportClipRegionFeatureClass : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.FocusMap.ClipGeometry != null; }
        }


        public override void OnCreate(object hook)
        {
            this.m_category = "制图工具";
            this.m_caption = "输出";
            this.m_message = "输出裁剪区内要素";
            this.m_toolTip = "输出裁剪区内要素";
            base.m_bitmap = Properties.Resources.icon_clip_cut2;
            base.m_name = "Printing_ExportClipRegionFeatureClass";
            _key = "Printing_ExportClipRegionFeatureClass";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdExportClipRegionFeatureClass(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        private void method_0(IGroupLayer igroupLayer_0, List<IFeatureClass> list_0)
        {
            for (int i = 0; i < (igroupLayer_0 as ICompositeLayer).Count; i++)
            {
                ILayer layer = (igroupLayer_0 as ICompositeLayer).get_Layer(i);
                if (layer.Visible)
                {
                    if (layer is IFeatureLayer)
                    {
                        list_0.Add((layer as IFeatureLayer).FeatureClass);
                    }
                    else if (layer is IGroupLayer)
                    {
                        this.method_0(layer as IGroupLayer, list_0);
                    }
                }
            }
        }

        public override void OnClick()
        {
            frmClipOutSet frmClipOutSet = new frmClipOutSet();
            if (frmClipOutSet.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<IFeatureClass> list = new List<IFeatureClass>();
                for (int i = 0; i < this._context.FocusMap.LayerCount; i++)
                {
                    ILayer layer = this._context.FocusMap.get_Layer(i);
                    if (layer.Visible)
                    {
                        if (layer is IFeatureLayer)
                        {
                            list.Add((layer as IFeatureLayer).FeatureClass);
                        }
                        else if (layer is IGroupLayer)
                        {
                            this.method_0(layer as IGroupLayer, list);
                        }
                    }
                }
                if (frmClipOutSet.Type == 0)
                {
                    SDEToShapefile sde1 = new SDEToShapefile
                    {
                        ClipGeometry = this._context.FocusMap.ClipGeometry,
                        IsClip = true
                    };
                    sde1.AddFeatureClasses(list);
                    sde1.Convert(frmClipOutSet.OutWorspace);
                    ComReleaser.ReleaseCOMObject(frmClipOutSet.OutWorspace);
                }
                else if (frmClipOutSet.Type == 1)
                {
                    new ExportToMiTab
                    {
                        InputFeatureClasses = list,
                        OutPath = frmClipOutSet.OutPath,
                        ClipGeometry = this._context.FocusMap.ClipGeometry,
                        IsClip = true
                    }.Export();
                }
                else if (frmClipOutSet.Type == 2)
                {
                    VCTWrite vCTWrite = new VCTWrite();
                    for (int i = 0; i < list.Count; i++)
                    {
                        vCTWrite.AddDataset(list[i] as IDataset);
                    }
                    vCTWrite.ClipGeometry = this._context.FocusMap.ClipGeometry;
                    vCTWrite.IsClip = true;
                    vCTWrite.Write(frmClipOutSet.OutPath);
                }
            }
        }
    }
}