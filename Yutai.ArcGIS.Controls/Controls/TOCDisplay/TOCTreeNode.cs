using System;
using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    public class TOCTreeNode
    {
        private Color m_BackColor;
        private Bitmap m_BmpCheck;
        private Bitmap m_BmpCheck_nofc;
        private Bitmap m_BmpCheck_novis;
        private Bitmap m_BmpUnCheck;
        private Bitmap m_BmpUnCheck_nofc;
        private Rectangle m_Bounds;
        private bool m_Checked;
        private Rectangle m_ChectRect;
        private TOCTreeNodeCollection m_ChildNodes;
        private Rectangle m_ExpandRect;
        private Color m_ForeColor;
        private bool m_HasButton;
        private bool m_HasCheck;
        private bool m_HasImage;
        private bool m_HasText;
        private Rectangle m_ImageRect;
        private bool m_IsEditing;
        private bool m_IsExpanded;
        private bool m_IsSelected;
        private bool m_IsVisible;
        private int m_nItemHeight;
        private Font m_NodeFont;
        private NodeType m_NodeType;
        private Pen m_Pen;
        private System.Drawing.Image m_pImage;
        private TOCTreeNode m_pNextVisibleNode;
        private TOCTreeNode m_pParentNode;
        private TOCTreeNode m_pPrevVisibleNode;
        private object m_pTag;
        private TocTreeViewBase m_pTreeView;
        private int m_Space;
        private string m_Text;
        private Rectangle m_TextRect;
        internal int OID;

        public TOCTreeNode()
        {
            this.OID = 0;
            this.m_pImage = null;
            this.m_nItemHeight = 18;
            this.m_Space = 4;
            this.m_Checked = false;
            this.m_Text = "";
            this.m_IsSelected = false;
            this.m_Bounds = new Rectangle(0, 0, 10, 10);
            this.m_TextRect = new Rectangle(0, 0, 0, 0);
            this.m_ExpandRect = new Rectangle(0, 0, 0, 0);
            this.m_ImageRect = new Rectangle(0, 0, 0, 0);
            this.m_ChectRect = new Rectangle(0, 0, 0, 0);
            this.m_HasButton = false;
            this.m_HasCheck = false;
            this.m_HasImage = false;
            this.m_HasText = false;
            this.m_ChildNodes = new TOCTreeNodeCollection();
            this.m_BackColor = Color.White;
            this.m_ForeColor = Color.Black;
            this.m_IsEditing = false;
            this.m_IsExpanded = false;
            this.m_IsVisible = true;
            this.m_pNextVisibleNode = null;
            this.m_pPrevVisibleNode = null;
            this.m_pParentNode = null;
            this.m_NodeFont = new Font("Arial", 8f);
            this.m_Pen = new Pen(Color.Black, 0f);
            this.m_pTreeView = null;
            this.m_pTag = null;
            this.m_NodeType = NodeType.None;
            this.m_ChildNodes.Owner = this;
            this.m_BmpUnCheck = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.uncheck.bmp"));
            this.m_BmpCheck = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.checked.bmp"));
            this.m_BmpUnCheck_nofc = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.unchecked_nofc.bmp"));
            this.m_BmpCheck_nofc = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.checked_nofc.bmp"));
            this.m_BmpCheck_novis = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.checked_novis.bmp"));
        }

        public TOCTreeNode(string name)
        {
            this.OID = 0;
            this.m_pImage = null;
            this.m_nItemHeight = 18;
            this.m_Space = 4;
            this.m_Checked = false;
            this.m_Text = "";
            this.m_IsSelected = false;
            this.m_Bounds = new Rectangle(0, 0, 10, 10);
            this.m_TextRect = new Rectangle(0, 0, 0, 0);
            this.m_ExpandRect = new Rectangle(0, 0, 0, 0);
            this.m_ImageRect = new Rectangle(0, 0, 0, 0);
            this.m_ChectRect = new Rectangle(0, 0, 0, 0);
            this.m_HasButton = false;
            this.m_HasCheck = false;
            this.m_HasImage = false;
            this.m_HasText = false;
            this.m_ChildNodes = new TOCTreeNodeCollection();
            this.m_BackColor = Color.White;
            this.m_ForeColor = Color.Black;
            this.m_IsEditing = false;
            this.m_IsExpanded = false;
            this.m_IsVisible = true;
            this.m_pNextVisibleNode = null;
            this.m_pPrevVisibleNode = null;
            this.m_pParentNode = null;
            this.m_NodeFont = new Font("Arial", 8f);
            this.m_Pen = new Pen(Color.Black, 0f);
            this.m_pTreeView = null;
            this.m_pTag = null;
            this.m_NodeType = NodeType.None;
            this.m_ChildNodes.Owner = this;
            this.m_Text = name;
            if (this.m_Text.Length > 0)
            {
                this.m_HasText = true;
            }
            this.m_BmpUnCheck = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.uncheck.bmp"));
            this.m_BmpCheck = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.checked.bmp"));
            this.m_BmpUnCheck_nofc = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.unchecked_nofc.bmp"));
            this.m_BmpCheck_nofc = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.checked_nofc.bmp"));
            this.m_BmpCheck_novis = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.checked_novis.bmp"));
        }

        public TOCTreeNode(string name, bool HasCheck, bool HasIamge)
        {
            this.OID = 0;
            this.m_pImage = null;
            this.m_nItemHeight = 18;
            this.m_Space = 4;
            this.m_Checked = false;
            this.m_Text = "";
            this.m_IsSelected = false;
            this.m_Bounds = new Rectangle(0, 0, 10, 10);
            this.m_TextRect = new Rectangle(0, 0, 0, 0);
            this.m_ExpandRect = new Rectangle(0, 0, 0, 0);
            this.m_ImageRect = new Rectangle(0, 0, 0, 0);
            this.m_ChectRect = new Rectangle(0, 0, 0, 0);
            this.m_HasButton = false;
            this.m_HasCheck = false;
            this.m_HasImage = false;
            this.m_HasText = false;
            this.m_ChildNodes = new TOCTreeNodeCollection();
            this.m_BackColor = Color.White;
            this.m_ForeColor = Color.Black;
            this.m_IsEditing = false;
            this.m_IsExpanded = false;
            this.m_IsVisible = true;
            this.m_pNextVisibleNode = null;
            this.m_pPrevVisibleNode = null;
            this.m_pParentNode = null;
            this.m_NodeFont = new Font("Arial", 8f);
            this.m_Pen = new Pen(Color.Black, 0f);
            this.m_pTreeView = null;
            this.m_pTag = null;
            this.m_NodeType = NodeType.None;
            this.m_ChildNodes.Owner = this;
            this.m_Text = name;
            this.m_HasCheck = HasCheck;
            this.m_HasImage = HasIamge;
            if (this.m_Text.Length > 0)
            {
                this.m_HasText = true;
            }
            this.m_BmpUnCheck = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.uncheck.bmp"));
            this.m_BmpCheck = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.checked.bmp"));
            this.m_BmpUnCheck_nofc = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JYutai.ArcGIS.ControlsLK.Controls.TOCTreeview.unchecked_nofc.bmp"));
            this.m_BmpCheck_nofc = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.checked_nofc.bmp"));
            this.m_BmpCheck_novis = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.TOCTreeview.checked_novis.bmp"));
        }

        public void BeginEdit()
        {
            this.m_IsEditing = true;
        }

        private void CalBounds()
        {
            int x = 0;
            int y = 0;
            int num3 = 0;
            int height = 0;
            if (this.m_pTreeView != null)
            {
                x = this.m_pTreeView.AutoScrollPosition.X;
            }
            y = this.m_ExpandRect.Y;
            int right = this.m_ExpandRect.Right;
            right = (right > this.m_ChectRect.Right) ? right : this.m_ChectRect.Right;
            right = (right > this.m_ImageRect.Right) ? right : this.m_ImageRect.Right;
            right = (right > this.m_TextRect.Right) ? right : this.m_TextRect.Right;
            if (this.m_pTreeView != null)
            {
                right = (right > this.m_pTreeView.ClientRectangle.Right) ? right : this.m_pTreeView.ClientRectangle.Right;
            }
            height = this.m_ExpandRect.Height;
            height = (height > this.m_ChectRect.Height) ? height : this.m_ChectRect.Height;
            height = (height > this.m_ImageRect.Height) ? height : this.m_ImageRect.Height;
            height = (height > this.m_TextRect.Height) ? height : this.m_TextRect.Height;
            if (this.IsExpanded && (this.m_ChildNodes.Count > 0))
            {
                for (int i = 0; i < this.m_ChildNodes.Count; i++)
                {
                    Rectangle bounds = (this.m_ChildNodes[i] as TOCTreeNode).Bounds;
                    right = (right > bounds.Right) ? right : bounds.Right;
                }
                if (this.m_ChildNodes.Count > 0)
                {
                    height = (height + (this.m_ChildNodes[this.m_ChildNodes.Count - 1] as TOCTreeNode).Bounds.Bottom) - (this.m_ChildNodes[0] as TOCTreeNode).Bounds.Top;
                }
            }
            num3 = right - x;
            this.m_Bounds.X = x;
            this.m_Bounds.Y = y;
            this.m_Bounds.Width = num3;
            this.m_Bounds.Height = height;
        }

        public void CalculateBounds(Graphics pGraphics, ref int x, ref int y, int Indent)
        {
            int num = x;
            int num2 = y;
            this.m_ExpandRect.X = x;
            this.m_ExpandRect.Y = y;
            this.m_ExpandRect.Width = 12;
            x += 12;
            this.m_ExpandRect.Height = this.m_nItemHeight;
            int nItemHeight = this.m_nItemHeight;
            if (this.m_HasCheck)
            {
                x += this.m_Space;
                this.m_ChectRect.X = x;
                this.m_ChectRect.Y = y;
                this.m_ChectRect.Width = 12;
                x += 12;
                this.m_ChectRect.Height = this.m_nItemHeight;
            }
            if (this.m_pTag is ILegendClass)
            {
                x += this.m_Space;
                ILegendClass pTag = this.m_pTag as ILegendClass;
                ISymbol symbol = pTag.Symbol;
                if (symbol is IMarkerSymbol)
                {
                    IMarkerSymbol symbol2 = (IMarkerSymbol) symbol;
                    double num5 = symbol2.Size + 10.0;
                    double num4 = num5;
                    if (((int) num5) < this.m_nItemHeight)
                    {
                        num5 = this.m_nItemHeight;
                    }
                    nItemHeight = (int) num5;
                    if (num4 < 18.0)
                    {
                        num4 = 18.0;
                    }
                    this.m_ImageRect.X = x;
                    this.m_ImageRect.Y = y;
                    this.m_ImageRect.Width = (int) num4;
                    this.m_ImageRect.Height = (int) num5;
                    x += ((int) num4) + 10;
                }
                else
                {
                    this.m_ImageRect.X = x;
                    this.m_ImageRect.Y = y;
                    this.m_ImageRect.Width = 18;
                    this.m_ImageRect.Height = this.m_nItemHeight;
                    x += 18;
                }
            }
            else if (this.m_HasImage)
            {
                x += this.m_Space;
                this.m_ImageRect.X = x;
                this.m_ImageRect.Y = y;
                this.m_ImageRect.Width = 12;
                x += 12;
                this.m_ImageRect.Height = this.m_nItemHeight;
            }
            if (this.m_HasText)
            {
                x += this.m_Space;
                SizeF ef = pGraphics.MeasureString(this.m_Text, this.m_NodeFont);
                this.m_TextRect.X = x;
                this.m_TextRect.Y = y;
                this.m_TextRect.Width = ((int) ef.Width) + 5;
                this.m_TextRect.Height = this.m_nItemHeight;
                x = (x + ((int) ef.Width)) + 5;
            }
            if (nItemHeight > this.m_nItemHeight)
            {
                this.m_TextRect.Height = nItemHeight;
                this.m_ExpandRect.Height = nItemHeight;
                this.m_ChectRect.Height = nItemHeight;
                this.m_ImageRect.Height = nItemHeight;
            }
            y += nItemHeight;
            if (this.m_IsExpanded)
            {
                int num6;
                x = num + Indent;
                for (num6 = 0; num6 < this.m_ChildNodes.Count; num6++)
                {
                    (this.m_ChildNodes[num6] as TOCTreeNode).CalculateBounds(pGraphics, ref x, ref y, Indent);
                }
                if (this.m_ChildNodes.Count > 2)
                {
                    int left = 0;
                    left = (this.m_ChildNodes[0] as TOCTreeNode).TextRect.Left;
                    int width = (this.m_ChildNodes[0] as TOCTreeNode).ImageRect.Width;
                    for (num6 = 1; num6 < this.m_ChildNodes.Count; num6++)
                    {
                        left = (left > (this.m_ChildNodes[num6] as TOCTreeNode).TextRect.Left) ? left : (this.m_ChildNodes[num6] as TOCTreeNode).TextRect.Left;
                        width = (width > (this.m_ChildNodes[num6] as TOCTreeNode).ImageRect.Width) ? width : (this.m_ChildNodes[num6] as TOCTreeNode).ImageRect.Width;
                    }
                    for (num6 = 0; num6 < this.m_ChildNodes.Count; num6++)
                    {
                        Rectangle textRect = (this.m_ChildNodes[num6] as TOCTreeNode).TextRect;
                        textRect.X = left;
                        (this.m_ChildNodes[num6] as TOCTreeNode).TextRect = textRect;
                        textRect = (this.m_ChildNodes[num6] as TOCTreeNode).ImageRect;
                        textRect.Width = width;
                        (this.m_ChildNodes[num6] as TOCTreeNode).ImageRect = textRect;
                    }
                }
            }
            x = num;
        }

        public void Collapse()
        {
            this.m_IsExpanded = false;
        }

        public void Draw(Graphics pGraphics)
        {
            Point point = new Point(0, 0);
            if ((this.m_ExpandRect.Bottom > 0) && (this.m_ExpandRect.Top < this.m_pTreeView.Height))
            {
                if (this.m_ChildNodes.Count > 0)
                {
                    point.X = (this.m_ExpandRect.Left + this.m_ExpandRect.Right) / 2;
                    point.Y = (this.m_ExpandRect.Top + this.m_ExpandRect.Bottom) / 2;
                    pGraphics.DrawLine(this.m_Pen, point.X - 4, point.Y, point.X + 4, point.Y);
                    if (!this.m_IsExpanded)
                    {
                        pGraphics.DrawLine(this.m_Pen, point.X, point.Y - 4, point.X, point.Y + 4);
                    }
                    pGraphics.DrawLine(this.m_Pen, (int) (point.X - 4), (int) (point.Y - 4), (int) (point.X + 4), (int) (point.Y - 4));
                    pGraphics.DrawLine(this.m_Pen, (int) (point.X + 4), (int) (point.Y - 4), (int) (point.X + 4), (int) (point.Y + 4));
                    pGraphics.DrawLine(this.m_Pen, (int) (point.X + 4), (int) (point.Y + 4), (int) (point.X - 4), (int) (point.Y + 4));
                    pGraphics.DrawLine(this.m_Pen, (int) (point.X - 4), (int) (point.Y + 4), (int) (point.X - 4), (int) (point.Y - 4));
                }
                if (this.m_HasCheck)
                {
                    Bitmap image = null;
                    if (this.m_Checked)
                    {
                        ILayer tag;
                        TOCTreeNode node;
                        IBasicMap map;
                        double mapScale;
                        if (this.Tag is IFeatureLayer)
                        {
                            if ((this.Tag as IFeatureLayer).FeatureClass == null)
                            {
                                image = this.m_BmpCheck_nofc;
                            }
                            else
                            {
                                tag = this.Tag as ILayer;
                                if ((tag.MinimumScale == 0.0) && (tag.MaximumScale == 0.0))
                                {
                                    image = this.m_BmpCheck;
                                }
                                else
                                {
                                    node = this.FindMapNode(this);
                                    if (node != null)
                                    {
                                        if (node.Tag is IBasicMap)
                                        {
                                            map = node.Tag as IBasicMap;
                                        }
                                        else
                                        {
                                            map = (node.Tag as IMapFrame).Map as IBasicMap;
                                        }
                                        try
                                        {
                                            mapScale = 0.0;
                                            if (map is IMap)
                                            {
                                                mapScale = (map as IMap).MapScale;
                                            }
                                            if ((mapScale >= tag.MaximumScale) && (mapScale <= tag.MinimumScale))
                                            {
                                                image = this.m_BmpCheck;
                                            }
                                            else
                                            {
                                                image = this.m_BmpCheck_novis;
                                            }
                                        }
                                        catch
                                        {
                                            image = this.m_BmpCheck;
                                        }
                                    }
                                    else
                                    {
                                        image = this.m_BmpCheck;
                                    }
                                }
                            }
                        }
                        else if (this.Tag is ITinLayer)
                        {
                            if ((this.Tag as ITinLayer).Dataset == null)
                            {
                                image = this.m_BmpCheck_nofc;
                            }
                            else
                            {
                                tag = this.Tag as ILayer;
                                if ((tag.MinimumScale == 0.0) && (tag.MaximumScale == 0.0))
                                {
                                    image = this.m_BmpCheck;
                                }
                                else
                                {
                                    node = this.FindMapNode(this);
                                    if (node != null)
                                    {
                                        if (node.Tag is IBasicMap)
                                        {
                                            map = node.Tag as IBasicMap;
                                        }
                                        else
                                        {
                                            map = (node.Tag as IMapFrame).Map as IBasicMap;
                                        }
                                        try
                                        {
                                            mapScale = 0.0;
                                            if (map is IMap)
                                            {
                                                mapScale = (map as IMap).MapScale;
                                            }
                                            if ((mapScale >= tag.MaximumScale) && (mapScale <= tag.MinimumScale))
                                            {
                                                image = this.m_BmpCheck;
                                            }
                                            else
                                            {
                                                image = this.m_BmpCheck_novis;
                                            }
                                        }
                                        catch
                                        {
                                            image = this.m_BmpCheck;
                                        }
                                    }
                                    else
                                    {
                                        image = this.m_BmpCheck;
                                    }
                                }
                            }
                        }
                        else
                        {
                            image = this.m_BmpCheck;
                        }
                    }
                    else if (this.Tag is IFeatureLayer)
                    {
                        if ((this.Tag as IFeatureLayer).FeatureClass == null)
                        {
                            image = this.m_BmpUnCheck_nofc;
                        }
                        else
                        {
                            image = this.m_BmpUnCheck;
                        }
                    }
                    else
                    {
                        image = this.m_BmpUnCheck;
                    }
                    point.X = (this.m_ChectRect.Left + this.m_ChectRect.Right) / 2;
                    point.Y = (this.m_ChectRect.Top + this.m_ChectRect.Bottom) / 2;
                    pGraphics.DrawImageUnscaled(image, point.X - 4, point.Y - 7);
                }
                if (this.m_pImage != null)
                {
                    point.X = (this.m_ImageRect.Left + this.m_ImageRect.Right) / 2;
                    point.Y = (this.m_ImageRect.Top + this.m_ImageRect.Bottom) / 2;
                    pGraphics.DrawImageUnscaled(this.m_pImage, point.X - 8, point.Y - 8);
                }
                else if (this.m_pTag != null)
                {
                    ISymbol pTag;
                    IntPtr hdc;
                    IStyleDraw draw;
                    Rectangle rectangle;
                    if (this.m_pTag is ILegendClass)
                    {
                        pTag = (this.m_pTag as ILegendClass).Symbol;
                        if (pTag != null)
                        {
                            hdc = pGraphics.GetHdc();
                            draw = StyleDrawFactory.CreateStyleDraw(pTag);
                            if (draw != null)
                            {
                                rectangle = new Rectangle(this.m_ImageRect.Location, this.m_ImageRect.Size);
                                if (pTag is IMarkerSymbol)
                                {
                                    rectangle.Inflate(-2, -2);
                                }
                                else
                                {
                                    rectangle.Inflate(-6, -4);
                                }
                                draw.Draw(hdc.ToInt32(), this.m_ImageRect, 72.0, 1.0);
                            }
                            pGraphics.ReleaseHdc(hdc);
                        }
                    }
                    else if (this.m_pTag is ISymbol)
                    {
                        pTag = this.m_pTag as ISymbol;
                        if (pTag != null)
                        {
                            hdc = pGraphics.GetHdc();
                            draw = StyleDrawFactory.CreateStyleDraw(pTag);
                            if (draw != null)
                            {
                                rectangle = new Rectangle(this.m_ImageRect.Location, this.m_ImageRect.Size);
                                if (pTag is IMarkerSymbol)
                                {
                                    rectangle.Inflate(-2, -2);
                                }
                                else
                                {
                                    rectangle.Inflate(-6, -4);
                                }
                                draw.Draw(hdc.ToInt32(), this.m_ImageRect, 72.0, 1.0);
                            }
                            pGraphics.ReleaseHdc(hdc);
                        }
                    }
                }
                if (this.m_HasText)
                {
                    SolidBrush brush;
                    SolidBrush brush2;
                    if (this.m_IsSelected)
                    {
                        brush2 = new SolidBrush(Color.FromArgb(190, 213, 255));
                        brush = new SolidBrush(Color.White);
                    }
                    else
                    {
                        brush2 = new SolidBrush(Color.White);
                        brush = new SolidBrush(Color.Black);
                    }
                    pGraphics.FillRectangle(brush2, this.m_TextRect);
                    StringFormat format = new StringFormat {
                        LineAlignment = StringAlignment.Center
                    };
                    try
                    {
                        pGraphics.DrawString(this.m_Text, this.m_NodeFont, brush, this.m_TextRect, format);
                    }
                    catch
                    {
                    }
                }
            }
            if (this.m_IsExpanded && (this.m_ExpandRect.Top < this.m_pTreeView.Height))
            {
                for (int i = 0; i < this.m_ChildNodes.Count; i++)
                {
                    (this.m_ChildNodes[i] as TOCTreeNode).Draw(pGraphics);
                }
            }
        }

        public void EndEdit(bool cancel)
        {
            this.m_IsEditing = false;
        }

        public void EnsureVisible()
        {
        }

        public void Expand()
        {
            this.m_IsExpanded = true;
        }

        public void ExpandAll()
        {
            this.m_IsExpanded = true;
            for (int i = 0; i < this.m_ChildNodes.Count; i++)
            {
                (this.m_ChildNodes[i] as TOCTreeNode).ExpandAll();
            }
        }

        private TOCTreeNode FindMapNode(TOCTreeNode pChildNode)
        {
            if (pChildNode.Parent != null)
            {
                if ((pChildNode.Parent.GetNodeType() == NodeType.Map) || (pChildNode.Parent.GetNodeType() == NodeType.MapFrame))
                {
                    return pChildNode.Parent;
                }
                return this.FindMapNode(pChildNode.Parent);
            }
            return null;
        }

        public int GetNodeCount(bool includeSubTrees)
        {
            int count = this.m_ChildNodes.Count;
            if (includeSubTrees)
            {
                for (int i = 0; i < this.m_ChildNodes.Count; i++)
                {
                    count += (this.m_ChildNodes[i] as TOCTreeNode).GetNodeCount(includeSubTrees);
                }
            }
            return count;
        }

        public NodeType GetNodeType()
        {
            return this.m_NodeType;
        }

        public HitType HitTest(int x, int y)
        {
            if (this.ExpandRect.Contains(x, y))
            {
                return HitType.Expand;
            }
            if (this.ChectRect.Contains(x, y))
            {
                return HitType.Check;
            }
            if (this.ImageRect.Contains(x, y))
            {
                return HitType.Image;
            }
            if (this.TextRect.Contains(x, y))
            {
                return HitType.Text;
            }
            return HitType.None;
        }

        public void Remove()
        {
        }

        internal void SetParent(TOCTreeNode node)
        {
            this.m_pParentNode = node;
        }

        internal void SetTreeView(TocTreeViewBase pTreeView)
        {
            this.m_pTreeView = pTreeView;
            for (int i = 0; i < this.m_ChildNodes.Count; i++)
            {
                (this.m_ChildNodes[i] as TOCTreeNode).SetTreeView(pTreeView);
            }
        }

        public override string ToString()
        {
            return this.Text;
        }

        public Color BackColor
        {
            get
            {
                return this.m_BackColor;
            }
            set
            {
                this.m_BackColor = value;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                int x = 0;
                int y = 0;
                int num3 = 0;
                int height = 0;
                if (this.m_pTreeView != null)
                {
                    x = this.m_pTreeView.AutoScrollPosition.X;
                }
                y = this.m_ExpandRect.Y;
                int right = this.m_ExpandRect.Right;
                right = (right > this.m_ChectRect.Right) ? right : this.m_ChectRect.Right;
                right = (right > this.m_ImageRect.Right) ? right : this.m_ImageRect.Right;
                right = (right > this.m_TextRect.Right) ? right : this.m_TextRect.Right;
                if (this.m_pTreeView != null)
                {
                    right = (right > this.m_pTreeView.ClientRectangle.Right) ? right : this.m_pTreeView.ClientRectangle.Right;
                }
                height = this.m_ExpandRect.Height;
                height = (height > this.m_ChectRect.Height) ? height : this.m_ChectRect.Height;
                height = (height > this.m_ImageRect.Height) ? height : this.m_ImageRect.Height;
                height = (height > this.m_TextRect.Height) ? height : this.m_TextRect.Height;
                if (this.IsExpanded && (this.m_ChildNodes.Count > 0))
                {
                    for (int i = 0; i < this.m_ChildNodes.Count; i++)
                    {
                        Rectangle bounds = (this.m_ChildNodes[i] as TOCTreeNode).Bounds;
                        right = (right > bounds.Right) ? right : bounds.Right;
                    }
                    if (this.m_ChildNodes.Count > 0)
                    {
                        height = (height + (this.m_ChildNodes[this.m_ChildNodes.Count - 1] as TOCTreeNode).Bounds.Bottom) - (this.m_ChildNodes[0] as TOCTreeNode).Bounds.Top;
                    }
                }
                num3 = right - x;
                this.m_Bounds.X = x;
                this.m_Bounds.Y = y;
                this.m_Bounds.Width = num3;
                this.m_Bounds.Height = height;
                return this.m_Bounds;
            }
        }

        public bool Checked
        {
            get
            {
                return this.m_Checked;
            }
            set
            {
                this.m_Checked = value;
            }
        }

        public Rectangle ChectRect
        {
            get
            {
                return this.m_ChectRect;
            }
            set
            {
                this.m_ChectRect = value;
            }
        }

        public Rectangle ExpandRect
        {
            get
            {
                return this.m_ExpandRect;
            }
            set
            {
                this.m_ExpandRect = value;
            }
        }

        public TOCTreeNode FirstNode
        {
            get
            {
                if (this.m_ChildNodes.Count == 0)
                {
                    return null;
                }
                return (this.m_ChildNodes[0] as TOCTreeNode);
            }
        }

        public Color ForeColor
        {
            get
            {
                return this.m_ForeColor;
            }
            set
            {
                this.m_ForeColor = value;
            }
        }

        public bool HasCheck
        {
            set
            {
                this.m_HasCheck = value;
            }
        }

        public bool HasImage
        {
            set
            {
                this.m_HasImage = value;
            }
        }

        public bool HasText
        {
            set
            {
                this.m_HasText = value;
            }
        }

        public System.Drawing.Image Image
        {
            set
            {
                this.m_pImage = value;
            }
        }

        public Rectangle ImageRect
        {
            get
            {
                return this.m_ImageRect;
            }
            set
            {
                this.m_ImageRect = value;
            }
        }

        public bool IsEditing
        {
            get
            {
                return this.m_IsEditing;
            }
        }

        public bool IsExpanded
        {
            get
            {
                return this.m_IsExpanded;
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.m_IsSelected;
            }
            set
            {
                this.m_IsSelected = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.m_IsVisible;
            }
        }

        public TOCTreeNode LastNode
        {
            get
            {
                if (this.m_ChildNodes.Count == 0)
                {
                    return null;
                }
                return (this.m_ChildNodes[this.m_ChildNodes.Count - 1] as TOCTreeNode);
            }
        }

        public TOCTreeNode NextNode
        {
            get
            {
                int index = -1;
                TOCTreeNodeCollection nodes = null;
                if (this.m_pParentNode != null)
                {
                    nodes = this.m_pParentNode.Nodes;
                }
                else if (this.m_pTreeView != null)
                {
                    nodes = this.m_pTreeView.Nodes;
                }
                if (nodes == null)
                {
                    return null;
                }
                index = nodes.IndexOf(this);
                if (index == -1)
                {
                    return null;
                }
                index++;
                if (index == nodes.Count)
                {
                    return null;
                }
                return (nodes[index] as TOCTreeNode);
            }
        }

        public TOCTreeNode NextVisibleNode
        {
            get
            {
                return this.m_pNextVisibleNode;
            }
        }

        public Font NodeFont
        {
            get
            {
                return this.m_NodeFont;
            }
            set
            {
                this.m_NodeFont = value;
            }
        }

        public Rectangle NodeRect
        {
            get
            {
                int x = this.m_ExpandRect.X;
                int y = this.m_ExpandRect.Y;
                int right = this.m_ExpandRect.Right;
                right = (right > this.m_ChectRect.Right) ? right : this.m_ChectRect.Right;
                right = (right > this.m_ImageRect.Right) ? right : this.m_ImageRect.Right;
                right = (right > this.m_TextRect.Right) ? right : this.m_TextRect.Right;
                if (this.m_pTreeView != null)
                {
                    x = this.m_pTreeView.AutoScrollPosition.X;
                }
                if (this.m_pTreeView != null)
                {
                    right = (right > this.m_pTreeView.ClientRectangle.Right) ? right : this.m_pTreeView.ClientRectangle.Right;
                }
                int width = right - x;
                int height = this.m_ExpandRect.Height;
                height = (height > this.m_ChectRect.Height) ? height : this.m_ChectRect.Height;
                height = (height > this.m_ImageRect.Height) ? height : this.m_ImageRect.Height;
                return new Rectangle(x, y, width, (height > this.m_TextRect.Height) ? height : this.m_TextRect.Height);
            }
        }

        public TOCTreeNodeCollection Nodes
        {
            get
            {
                return this.m_ChildNodes;
            }
        }

        public TOCTreeNode Parent
        {
            get
            {
                return this.m_pParentNode;
            }
        }

        public TOCTreeNode PrevNode
        {
            get
            {
                int index = -1;
                TOCTreeNodeCollection nodes = null;
                if (this.m_pParentNode != null)
                {
                    nodes = this.m_pParentNode.Nodes;
                }
                else if (this.m_pTreeView != null)
                {
                    nodes = this.m_pTreeView.Nodes;
                }
                if (nodes == null)
                {
                    return null;
                }
                index = nodes.IndexOf(this);
                switch (index)
                {
                    case -1:
                        return null;

                    case 0:
                        return null;
                }
                return (nodes[index - 1] as TOCTreeNode);
            }
        }

        public TOCTreeNode PrevVisibleNode
        {
            get
            {
                return this.m_pPrevVisibleNode;
            }
        }

        public object Tag
        {
            get
            {
                return this.m_pTag;
            }
            set
            {
                this.m_pTag = value;
                if (this.m_pTag is IMapFrame)
                {
                    this.m_NodeType = NodeType.MapFrame;
                }
                else if (this.m_pTag is IBasicMap)
                {
                    this.m_NodeType = NodeType.Map;
                }
                else if (this.m_pTag is ILegendClass)
                {
                    this.m_NodeType = NodeType.LegendClass;
                }
                else if (this.m_pTag is ISymbol)
                {
                    this.m_NodeType = NodeType.Symbol;
                }
                else if (this.m_pTag is IWorkspace)
                {
                    this.m_NodeType = NodeType.Workspace;
                }
                else if (this.m_pTag is IGroupLayer)
                {
                    this.m_NodeType = NodeType.GroupLayer;
                }
                else if (this.m_pTag is IAnnotationSublayer)
                {
                    this.m_NodeType = NodeType.AnnotationSublayer;
                }
                else if (this.m_pTag is ILayer)
                {
                    this.m_NodeType = NodeType.Layer;
                }
                else if (this.m_pTag is ITable)
                {
                    this.m_NodeType = NodeType.Table;
                }
                else if (this.m_pTag is IFeatureDataset)
                {
                    this.m_NodeType = NodeType.FeatureDataset;
                }
            }
        }

        public string Text
        {
            get
            {
                return this.m_Text;
            }
            set
            {
                this.m_Text = value;
                if (this.m_Text.Length > 0)
                {
                    this.m_HasText = true;
                }
            }
        }

        public Rectangle TextRect
        {
            get
            {
                return this.m_TextRect;
            }
            set
            {
                this.m_TextRect = value;
            }
        }

        public NodeType TOCNodeType
        {
            get
            {
                return this.m_NodeType;
            }
            set
            {
                this.m_NodeType = value;
            }
        }

        public TocTreeViewBase TreeView
        {
            get
            {
                return this.m_pTreeView;
            }
        }
    }
}

