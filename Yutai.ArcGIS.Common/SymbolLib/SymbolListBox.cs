using System.ComponentModel;
using System.Drawing;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.SymbolLib
{
	public class SymbolListBox : System.Windows.Forms.ListBox
	{
		private int int_0 = 0;

		private Container container_0 = null;

		private LayerVisibleChangedHandler layerVisibleChangedHandler_0;

		private LayerColorLockedChangedHandler layerColorLockedChangedHandler_0;

		public event LayerVisibleChangedHandler LayerVisibleChanged
		{
			add
			{
				LayerVisibleChangedHandler layerVisibleChangedHandler = this.layerVisibleChangedHandler_0;
				LayerVisibleChangedHandler layerVisibleChangedHandler2;
				do
				{
					layerVisibleChangedHandler2 = layerVisibleChangedHandler;
					LayerVisibleChangedHandler value2 = (LayerVisibleChangedHandler)System.Delegate.Combine(layerVisibleChangedHandler2, value);
					layerVisibleChangedHandler = System.Threading.Interlocked.CompareExchange<LayerVisibleChangedHandler>(ref this.layerVisibleChangedHandler_0, value2, layerVisibleChangedHandler2);
				}
				while (layerVisibleChangedHandler != layerVisibleChangedHandler2);
			}
			remove
			{
				LayerVisibleChangedHandler layerVisibleChangedHandler = this.layerVisibleChangedHandler_0;
				LayerVisibleChangedHandler layerVisibleChangedHandler2;
				do
				{
					layerVisibleChangedHandler2 = layerVisibleChangedHandler;
					LayerVisibleChangedHandler value2 = (LayerVisibleChangedHandler)System.Delegate.Remove(layerVisibleChangedHandler2, value);
					layerVisibleChangedHandler = System.Threading.Interlocked.CompareExchange<LayerVisibleChangedHandler>(ref this.layerVisibleChangedHandler_0, value2, layerVisibleChangedHandler2);
				}
				while (layerVisibleChangedHandler != layerVisibleChangedHandler2);
			}
		}

		public event LayerColorLockedChangedHandler LayerColorLockedChanged
		{
			add
			{
				LayerColorLockedChangedHandler layerColorLockedChangedHandler = this.layerColorLockedChangedHandler_0;
				LayerColorLockedChangedHandler layerColorLockedChangedHandler2;
				do
				{
					layerColorLockedChangedHandler2 = layerColorLockedChangedHandler;
					LayerColorLockedChangedHandler value2 = (LayerColorLockedChangedHandler)System.Delegate.Combine(layerColorLockedChangedHandler2, value);
					layerColorLockedChangedHandler = System.Threading.Interlocked.CompareExchange<LayerColorLockedChangedHandler>(ref this.layerColorLockedChangedHandler_0, value2, layerColorLockedChangedHandler2);
				}
				while (layerColorLockedChangedHandler != layerColorLockedChangedHandler2);
			}
			remove
			{
				LayerColorLockedChangedHandler layerColorLockedChangedHandler = this.layerColorLockedChangedHandler_0;
				LayerColorLockedChangedHandler layerColorLockedChangedHandler2;
				do
				{
					layerColorLockedChangedHandler2 = layerColorLockedChangedHandler;
					LayerColorLockedChangedHandler value2 = (LayerColorLockedChangedHandler)System.Delegate.Remove(layerColorLockedChangedHandler2, value);
					layerColorLockedChangedHandler = System.Threading.Interlocked.CompareExchange<LayerColorLockedChangedHandler>(ref this.layerColorLockedChangedHandler_0, value2, layerColorLockedChangedHandler2);
				}
				while (layerColorLockedChangedHandler != layerColorLockedChangedHandler2);
			}
		}

		public SymbolListBox()
		{
			this.method_0();
		}

		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_0);
		}

		private void method_0()
		{
			this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			base.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SymbolListBox_MouseDown);
			base.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.SymbolListBox_MeasureItem);
			base.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.SymbolListBox_DrawItem);
		}

		private void SymbolListBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			if (base.Items.Count != 0)
			{
				SymbolListItem symbolListItem = (SymbolListItem)base.Items[e.Index];
				e.DrawBackground();
				Bitmap image;
				if (symbolListItem.m_bVisible)
				{
					image = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Symbol.SymbolControl.Check.bmp"));
				}
				else
				{
					image = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Symbol.SymbolControl.UnCheck.bmp"));
				}
				Bitmap image2;
				if (symbolListItem.m_bLockColor)
				{
					image2 = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Symbol.SymbolControl.Lock.bmp"));
				}
				else
				{
					image2 = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Symbol.SymbolControl.UnLock.bmp"));
				}
				Rectangle rect = new Rectangle(2, e.Bounds.Y + (e.Bounds.Height - 16) / 2, 16, 16);
				Rectangle rect2 = new Rectangle(e.Bounds.Right - 21, e.Bounds.Y + (e.Bounds.Height - 16) / 2, 16, 16);
				Rectangle rectangle = new Rectangle(rect.Right + 2, e.Bounds.Y + 1, e.Bounds.Width - 46, e.Bounds.Height - 2);
				e.Graphics.DrawImageUnscaled(image, rect);
				e.Graphics.DrawImageUnscaled(image2, rect2);
				e.Graphics.FillRectangle(Brushes.White, rectangle);
				Graphics graphics = e.Graphics;
				System.IntPtr hdc = graphics.GetHdc();
				this.DrawSymbol(hdc.ToInt32(), rectangle, symbolListItem.m_pSymbol);
				graphics.ReleaseHdc(hdc);
			}
		}

		private void SymbolListBox_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
		{
			switch (this.int_0)
			{
			case 0:
				e.ItemHeight = 40;
				break;
			case 1:
				e.ItemHeight = 30;
				break;
			case 2:
				e.ItemHeight = 50;
				break;
			default:
				e.ItemHeight = 40;
				break;
			}
		}

		private void SymbolListBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Rectangle itemRectangle = base.GetItemRectangle(this.SelectedIndex);
			Rectangle rc = new Rectangle(2, itemRectangle.Y + (itemRectangle.Height - 16) / 2, 16, 16);
			Rectangle rc2 = new Rectangle(itemRectangle.Right - 21, itemRectangle.Y + (itemRectangle.Height - 16) / 2, 16, 16);
			if (rc.Contains(e.X, e.Y))
			{
				((SymbolListItem)base.Items[this.SelectedIndex]).m_bVisible = !((SymbolListItem)base.Items[this.SelectedIndex]).m_bVisible;
				base.Invalidate(rc);
				if (this.layerVisibleChangedHandler_0 != null)
				{
					this.layerVisibleChangedHandler_0(sender, e);
				}
			}
			else if (rc2.Contains(e.X, e.Y))
			{
				((SymbolListItem)base.Items[this.SelectedIndex]).m_bLockColor = !((SymbolListItem)base.Items[this.SelectedIndex]).m_bLockColor;
				base.Invalidate(rc2);
				if (this.layerColorLockedChangedHandler_0 != null)
				{
					this.layerColorLockedChangedHandler_0(sender, e);
				}
			}
		}

		internal void SetSymbolType(int int_1)
		{
			this.int_0 = int_1;
		}

		protected void DrawSymbol(int int_1, Rectangle rectangle_0, object object_0)
		{
			IDisplayTransformation displayTransformation = new DisplayTransformation() as IDisplayTransformation;
			IEnvelope envelope = new Envelope() as ESRI.ArcGIS.Geometry.IEnvelope;
			envelope.PutCoords((double)rectangle_0.Left, (double)rectangle_0.Top, (double)rectangle_0.Right, (double)rectangle_0.Bottom);
			tagRECT tagRECT;
			tagRECT.left = rectangle_0.Left;
			tagRECT.right = rectangle_0.Right;
			tagRECT.bottom = rectangle_0.Bottom;
			tagRECT.top = rectangle_0.Top;
			displayTransformation.set_DeviceFrame (ref tagRECT);
			displayTransformation.Bounds = envelope;
			ISymbol symbol = object_0 as ISymbol;
			if (symbol != null)
			{
				if (object_0 is IMarkerSymbol)
				{
					IStyleGalleryClass styleGalleryClass = new MarkerSymbolStyleGalleryClass();
					tagRECT tagRECT2 = default(tagRECT);
					tagRECT2.left = rectangle_0.Left;
					tagRECT2.right = rectangle_0.Right;
					tagRECT2.top = rectangle_0.Top;
					tagRECT2.bottom = rectangle_0.Bottom;
					styleGalleryClass.Preview(object_0, int_1, ref tagRECT2);
				}
				else if (object_0 is ILineSymbol)
				{
					IStyleGalleryClass styleGalleryClass = new LineSymbolStyleGalleryClass();
					tagRECT tagRECT2 = default(tagRECT);
					tagRECT2.left = rectangle_0.Left;
					tagRECT2.right = rectangle_0.Right;
					tagRECT2.top = rectangle_0.Top;
					tagRECT2.bottom = rectangle_0.Bottom;
					styleGalleryClass.Preview(object_0, int_1, ref tagRECT2);
				}
				else if (object_0 is IFillSymbol)
				{
					IStyleGalleryClass styleGalleryClass = new FillSymbolStyleGalleryClass();
					tagRECT tagRECT2 = default(tagRECT);
					tagRECT2.left = rectangle_0.Left;
					tagRECT2.right = rectangle_0.Right;
					tagRECT2.top = rectangle_0.Top;
					tagRECT2.bottom = rectangle_0.Bottom;
					styleGalleryClass.Preview(object_0, int_1, ref tagRECT2);
				}
			}
		}

		private void method_1(IMarkerSymbol imarkerSymbol_0, Rectangle rectangle_0)
		{
			if (!(imarkerSymbol_0 is IPictureMarkerSymbol) || ((IPictureMarkerSymbol)imarkerSymbol_0).Picture != null)
			{
				if (imarkerSymbol_0 is IMarker3DSymbol)
				{
					try
					{
						if ((imarkerSymbol_0 as IMarker3DSymbol).MaterialCount == 0)
						{
							return;
						}
					}
					catch
					{
						return;
					}
				}
				IPoint point = new ESRI.ArcGIS.Geometry.Point();
				point.X = (double)((rectangle_0.Left + rectangle_0.Right) / 2);
				point.Y = (double)((rectangle_0.Bottom + rectangle_0.Top) / 2);
				((ISymbol)imarkerSymbol_0).Draw(point);
			}
		}

		private void method_2(ILineSymbol ilineSymbol_0, Rectangle rectangle_0)
		{
			if (ilineSymbol_0 is IPictureLineSymbol)
			{
				if (((IPictureLineSymbol)ilineSymbol_0).Picture == null)
				{
					return;
				}
			}
			else if (ilineSymbol_0 is IMarkerLineSymbol || ilineSymbol_0 is IHashLineSymbol)
			{
				ITemplate template = ((ILineProperties)ilineSymbol_0).Template;
				if (template != null)
				{
					bool flag = false;
					int i = 0;
					while (i < template.PatternElementCount)
					{
						double num;
						double num2;
						template.GetPatternElement(i, out num, out num2);
						if (num + num2 <= 0.0)
						{
							i++;
						}
						else
						{
							flag = true;
							if (flag)
							{
                                object value = System.Reflection.Missing.Value;
                                IPointCollection pointCollection = new Polyline();
                                IPoint point = new ESRI.ArcGIS.Geometry.Point();
                                point.PutCoords((double)(rectangle_0.Left + 3), (double)((rectangle_0.Bottom + rectangle_0.Top) / 2));
                                pointCollection.AddPoint(point, ref value, ref value);
                                point.PutCoords((double)(rectangle_0.Right - 3), (double)((rectangle_0.Bottom + rectangle_0.Top) / 2));
                                pointCollection.AddPoint(point, ref value, ref value);
                                ((ISymbol)ilineSymbol_0).Draw((IGeometry)pointCollection);
							    break;
							}
							return;
						}
					}
					
				}
			}
			
			
		}

		private void method_3(IFillSymbol ifillSymbol_0, Rectangle rectangle_0)
		{
			if (!(ifillSymbol_0 is IPictureFillSymbol) || ((IPictureFillSymbol)ifillSymbol_0).Picture != null)
			{
				object value = System.Reflection.Missing.Value;
				IPoint point = new ESRI.ArcGIS.Geometry.Point();
				IPointCollection pointCollection = new Polygon();
				point.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Top + 3));
				pointCollection.AddPoint(point, ref value, ref value);
				point.PutCoords((double)(rectangle_0.Right - 3), (double)(rectangle_0.Top + 3));
				pointCollection.AddPoint(point, ref value, ref value);
				point.PutCoords((double)(rectangle_0.Right - 3), (double)(rectangle_0.Bottom - 3));
				pointCollection.AddPoint(point, ref value, ref value);
				point.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Bottom - 3));
				pointCollection.AddPoint(point, ref value, ref value);
				point.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Top + 3));
				pointCollection.AddPoint(point, ref value, ref value);
				((ISymbol)ifillSymbol_0).Draw((IGeometry)pointCollection);
			}
		}
	}
}
