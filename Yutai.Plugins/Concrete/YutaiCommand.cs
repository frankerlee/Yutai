using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Concrete
{
    
    
    //YutaiCommand将替换掉MenuCommand,以便实现命令的调用
    public abstract class YutaiCommand : ICommand, Yutai.Plugins.Interfaces.IRibbonItem
    {
        protected bool m_enabled = true;
        private IntPtr m_hBitmap;
        protected System.Drawing.Bitmap m_bitmap;
        protected string m_caption;
        protected string m_category;
        protected string m_helpFile;
        protected int m_helpID;
        protected string m_message;
        protected string m_name;
        protected string m_toolTip;
        protected bool m_checked;
        protected IAppContext _context;
        protected string _key;
        protected RibbonItemType _itemType;
        private TextImageRelationYT _textImageRelationYt;
        private DisplayStyleYT _displayStyleYt;
        private ToolStripItemImageScalingYT _toolStripItemImageScalingYt;
        private ToolStripLayoutStyleYT _toolStripLayoutStyleYt;
        private int _panelRowCount;
        private string _parentName;
        private int _position=-1;
        private bool _isGroup;

        public  Keys ShortcutKeys { get; set; }
        public PluginIdentity PluginIdentity { get; internal set; }

        public TextImageRelationYT TextImageRelationYT
        {
            get { return _textImageRelationYt; }
            set { _textImageRelationYt = value; }
        }

        public DisplayStyleYT DisplayStyleYT
        {
            get { return _displayStyleYt; }
            set { _displayStyleYt = value; }
        }

        public ToolStripItemImageScalingYT ToolStripItemImageScalingYT
        {
            get { return _toolStripItemImageScalingYt; }
            set { _toolStripItemImageScalingYt = value; }
        }

        public ToolStripLayoutStyleYT ToolStripLayoutStyleYT
        {
            get { return _toolStripLayoutStyleYt; }
            set { _toolStripLayoutStyleYt = value; }
        }

        public int PanelRowCount
        {
            get { return _panelRowCount; }
            set { _panelRowCount = value; }
        }

        public bool IsGroup
        {
            get { return _isGroup; }
            set { _isGroup = value; }
        }


        public virtual string Message
        {
            get
            {
                return this.m_message;
            }
        }

        public virtual Bitmap Image
        {
            get { return m_bitmap; }
        }

        public string Key
        {
            get { return _key; }
        }

        public virtual string Caption
        {
            get
            {
                return this.m_caption;
            }
        }

        public virtual string Tooltip
        {
            get
            {
                return this.m_toolTip;
            }
        }

        public virtual int HelpContextID
        {
            get
            {
                return this.m_helpID;
            }
        }

        public  int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public  string ParentName
        {
            get { return _parentName; }
            set { _parentName = value; }
        }

        public virtual string Name
        {
            get
            {
                return this.m_name;
            }
        }



        public virtual bool Checked
        {
            get
            {
                return this.m_checked;
            }
        }

        public virtual bool Enabled
        {
            get
            {
                return this.m_enabled;
            }
        }

        public virtual string HelpFile
        {
            get
            {
                return this.m_helpFile;
            }
        }

        public virtual string Category
        {
            get
            {
                if (this.m_category == null || this.m_category == "")
                    return "Misc.";
                return this.m_category;
            }
        }

        public virtual RibbonItemType ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }


        protected YutaiCommand()
        {
            _itemType = RibbonItemType.Tool;
            _textImageRelationYt= TextImageRelationYT.ImageAboveText;
            _displayStyleYt= DisplayStyleYT.ImageAndText;
            _toolStripItemImageScalingYt= ToolStripItemImageScalingYT.None;
            ToolStripLayoutStyleYT= ToolStripLayoutStyleYT.Flow;
            _panelRowCount = 1;
        }

        public void InitType()
        {
            int dotNum = m_name.Count(c => c == '.');
            if (dotNum < 1)
            {
                _itemType = RibbonItemType.TabItem;
            }
            else if (dotNum == 1)
            {
                _itemType = RibbonItemType.ToolStrip;
            }
            else
            {
                _itemType = RibbonItemType.NormalItem;
            }
        }

       protected YutaiCommand(string category, string key, string name, string caption, string tooltip, string message)
       {
          
            this.m_name = name;
            this.m_message = message;
            this.m_caption = caption;
            this.m_category = category;
            this.m_toolTip = tooltip;
            this._key = key;
            _itemType = RibbonItemType.Tool;
            _textImageRelationYt = TextImageRelationYT.ImageAboveText;
            _displayStyleYt = DisplayStyleYT.ImageAndText;
            _toolStripItemImageScalingYt = ToolStripItemImageScalingYT.None;
            ToolStripLayoutStyleYT = ToolStripLayoutStyleYT.Flow;
            _panelRowCount = 1;
        }

        protected  YutaiCommand(RibbonItemType itemType,string category, string key, string name, string caption, string tooltip, string message)
        {
            this.m_name = name;
            this.m_message = message;
            this.m_caption = caption;
            this.m_category = category;
            this.m_toolTip = tooltip;
            this._key = key;
            
            _textImageRelationYt = TextImageRelationYT.ImageAboveText;
            _displayStyleYt = DisplayStyleYT.ImageAndText;
            _toolStripItemImageScalingYt = ToolStripItemImageScalingYT.None;
            ToolStripLayoutStyleYT = ToolStripLayoutStyleYT.Flow;
            _panelRowCount = 1;
            this._itemType = itemType;
        }


        protected YutaiCommand(RibbonItemType itemType, System.Drawing.Bitmap bitmap, string caption, string category, int helpContextId, string helpFile, string message, string name, string toolTip)
        {
            this._itemType = itemType;
            this.m_bitmap = bitmap;
            this.m_name = name;
            this.m_message = message;
            this.m_caption = caption;
            this.m_category = category;
            this.m_toolTip = toolTip;
            this.m_helpID = helpContextId;
            this.m_helpFile = helpFile;
            _textImageRelationYt = TextImageRelationYT.ImageAboveText;
            _displayStyleYt = DisplayStyleYT.ImageAndText;
            _toolStripItemImageScalingYt = ToolStripItemImageScalingYT.None;
            ToolStripLayoutStyleYT = ToolStripLayoutStyleYT.Flow;
            _panelRowCount = 1;
            this._itemType = itemType;
        }

        protected YutaiCommand( System.Drawing.Bitmap bitmap, string caption, string category, int helpContextId, string helpFile, string message, string name, string toolTip)
        {
           
            this.m_bitmap = bitmap;
            this.m_name = name;
            this.m_message = message;
            this.m_caption = caption;
            this.m_category = category;
            this.m_toolTip = toolTip;
            this.m_helpID = helpContextId;
            this.m_helpFile = helpFile;
            _textImageRelationYt = TextImageRelationYT.ImageAboveText;
            _displayStyleYt = DisplayStyleYT.ImageAndText;
            _toolStripItemImageScalingYt = ToolStripItemImageScalingYT.None;
            ToolStripLayoutStyleYT = ToolStripLayoutStyleYT.Flow;
            _panelRowCount = 1;
            this._itemType = RibbonItemType.Tool;
        }

        
            
        /// <summary>
        /// Destructor. Cleans up GDI resources used by the BaseCommand.
        /// </summary>
        ~YutaiCommand()
        {
            if (this.m_hBitmap.ToInt32() == 0)
                return;
            YutaiCommand.DeleteObject(this.m_hBitmap);
        }

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        public virtual void OnClick()
        {
            GC.Collect();
        }

        public abstract void OnClick(object sender, EventArgs args);


        public abstract void OnCreate(object hook);

        int ICommand.Bitmap
        {
            get { return 0; }
        }
    }
}