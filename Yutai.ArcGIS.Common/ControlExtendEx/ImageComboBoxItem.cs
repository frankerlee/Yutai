using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.Serialization;

namespace Yutai.ArcGIS.Common.ControlExtendEx
{
    [Serializable, TypeConverter(typeof(ImageComboItemConverter)), DesignTimeVisible(false), ToolboxItem(false)]
    public sealed class ImageComboBoxItem : ISerializable
    {
        private System.Drawing.Font font_0;
        private int int_0;
        private int int_1;
        private object object_0;
        private string string_0;
        private string string_1;

        public ImageComboBoxItem()
        {
            this.string_0 = "(none)";
            this.int_0 = -1;
            this.string_1 = string.Empty;
            this.font_0 = null;
            this.object_0 = null;
            this.int_1 = 0;
        }

        public ImageComboBoxItem(object object_1)
        {
            this.string_0 = "(none)";
            this.int_0 = -1;
            this.string_1 = string.Empty;
            this.font_0 = null;
            this.object_0 = null;
            this.int_1 = 0;
            this.Item = object_1;
            this.Text = object_1.ToString();
        }

        public ImageComboBoxItem(SerializationInfo serializationInfo_0, StreamingContext streamingContext_0)
        {
            this.string_0 = "(none)";
            this.int_0 = -1;
            this.string_1 = string.Empty;
            this.font_0 = null;
            this.object_0 = null;
            this.int_1 = 0;
            this.string_1 = (string) serializationInfo_0.GetValue("Text", typeof(string));
            this.int_0 = (int) serializationInfo_0.GetValue("ImageIndex", typeof(int));
            this.font_0 = (System.Drawing.Font) serializationInfo_0.GetValue("Font", typeof(System.Drawing.Font));
            this.Item = serializationInfo_0.GetValue("Item", typeof(object));
        }

        public ImageComboBoxItem(string string_2, int int_2)
        {
            this.string_0 = "(none)";
            this.int_0 = -1;
            this.string_1 = string.Empty;
            this.font_0 = null;
            this.object_0 = null;
            this.int_1 = 0;
            this.string_1 = string_2;
        }

        public ImageComboBoxItem(int int_2, string string_2, int int_3)
        {
            this.string_0 = "(none)";
            this.int_0 = -1;
            this.string_1 = string.Empty;
            this.font_0 = null;
            this.object_0 = null;
            this.int_1 = 0;
            this.ImageIndex = int_2;
            this.string_1 = string_2;
        }

        public ImageComboBoxItem(int int_2, string string_2, System.Drawing.Font font_1, int int_3)
        {
            this.string_0 = "(none)";
            this.int_0 = -1;
            this.string_1 = string.Empty;
            this.font_0 = null;
            this.object_0 = null;
            this.int_1 = 0;
            this.ImageIndex = int_2;
            this.string_1 = string_2;
            this.font_0 = font_1;
        }

        public object Clone()
        {
            if (this.Item != null)
            {
                return new ImageComboBoxItem(this.object_0);
            }
            return new ImageComboBoxItem(this.ImageIndex, this.string_1, this.font_0, this.int_1);
        }

        public void GetObjectData(SerializationInfo serializationInfo_0, StreamingContext streamingContext_0)
        {
            serializationInfo_0.AddValue("Text", this.string_1);
            serializationInfo_0.AddValue("ImageIndex", this.int_0);
            serializationInfo_0.AddValue("Font", this.font_0);
            serializationInfo_0.AddValue("Item", this.object_0);
        }

        public override string ToString()
        {
            if ((this.Text.Length <= 0) && (this.Item != null))
            {
                return this.Item.ToString();
            }
            return this.Text;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public System.Drawing.Font Font
        {
            get { return this.font_0; }
            set { this.font_0 = value; }
        }

        [Editor(typeof(DropDownImages), typeof(UITypeEditor)),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Image
        {
            get { return this.string_0; }
            set
            {
                if (value != null)
                {
                    this.string_0 = value.ToString();
                    if (this.string_0.Equals("(none)"))
                    {
                        this.ImageIndex = -1;
                    }
                    else
                    {
                        this.ImageIndex = Convert.ToInt32(this.string_0.ToString());
                    }
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Always)]
        public int ImageIndex
        {
            get { return this.int_0; }
            set { this.int_0 = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int IndentLevel
        {
            get { return this.int_1; }
            set
            {
                this.int_1 = value;
                if (this.int_1 < 0)
                {
                    throw new Exception("Please enter a value greater than 0 and less than 5 for indentation." +
                                        Environment.NewLine + "Supported indentation levels are from 0-5");
                }
                if (this.int_1 > 10)
                {
                    throw new Exception("Please enter a value greater than 0 and less than 5 for indentation." +
                                        Environment.NewLine + "Supported indentation levels are from 0-5");
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object Item
        {
            get { return this.object_0; }
            set { this.object_0 = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Localizable(true)]
        public string Text
        {
            get { return this.string_1; }
            set { this.string_1 = value; }
        }
    }
}