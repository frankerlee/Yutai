using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System;
using System.IO;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdNewPicture : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "图像";
            this.m_message = "插入图像";
            this.m_toolTip = "插入图像";
            this.m_category = "制图";
            base.m_bitmap = Properties.Resources.icon_picture;
            base.m_name = "Printing_NewPicture";
            _key = "Printing_NewPicture";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdNewPicture(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter =
                "所有支持格式|*.bmp;*.jpg;*.gif;*.emf;*.tif;*.png|位图文件|*.bmp|JPEG|*.jpg|TIFF|*.tif|EMF|*.emf|PNG|*.png|GIF|*.gif";
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            string fileName = openFileDialog.FileName;
            string text = System.IO.Path.GetExtension(fileName).ToLower();
            string text2 = text;
            IElement element;
            if (text2 != null)
            {
                if (text2 == ".bmp")
                {
                    element = new BmpPictureElement();
                }
                else if (text2 == ".jpg")
                {
                    element = new JpgPictureElement();
                }
                else if (text2 == ".gif")
                {
                    element = new GifPictureElement();
                }
                else if (text2 == ".tif")
                {
                    element = new TifPictureElement();
                }
                else if (text2 == ".emf")
                {
                    element = new EmfPictureElement();
                }
                else
                {
                    element = new PngPictureElement();
                }
            }
            else
            {
                element = new PngPictureElement();
            }


            (element as IPictureElement).ImportPictureFromFile(fileName);
            (element as IPictureElement).MaintainAspectRatio = true;
            double num = 0.0;
            double num2 = 0.0;
            (element as IPictureElement2).QueryIntrinsicSize(ref num, ref num2);
            num *= 0.0353;
            num2 *= 0.0353;
            IGraphicsContainer arg_11B_0 = this._context.ActiveView as IGraphicsContainer;
            (element as IElementProperties2).AutoTransform = true;
            IEnvelope envelope = new Envelope() as IEnvelope;
            envelope.PutCoords(0.0, 0.0, num, num2);
            element.Geometry = envelope;
            INewElementOperation operation = new NewElementOperation
            {
                ActiveView = this._context.ActiveView,
                Element = element
            };
            this._context.OperationStack.Do(operation);
        }
    }
}