using System;
using System.Drawing;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace Yutai.ArcGIS.Controls.ApplicationStyle
{
    public class PaintStyleMenuItem
    {
        private BarSubItem iPaintStyle;
        private BarButtonItem ipsDefault;
        private BarButtonItem ipsO2K;
        private BarButtonItem ipsO3;
        private BarButtonItem ipsO7;
        private BarButtonItem ipsOXP;
        private BarButtonItem ipsWXP;
        private BarManager m_pBarManager = null;
        private RibbonControl m_ribbonctrl = null;
        private string skinMask = "肤色: ";

        private void Init()
        {
            BarManagerCategory category = null;
            if (this.m_pBarManager != null)
            {
                category = this.m_pBarManager.Categories["界面风格"];
            }
            else if (this.m_ribbonctrl != null)
            {
                category = this.m_ribbonctrl.Categories["界面风格"];
            }
            if (category == null)
            {
                category = new BarManagerCategory("界面风格", Guid.NewGuid());
                if (this.m_pBarManager != null)
                {
                    this.m_pBarManager.Categories.Add(category);
                }
                else if (this.m_ribbonctrl != null)
                {
                    this.m_ribbonctrl.Categories.Add(category);
                }
            }
            this.iPaintStyle = new BarSubItem();
            this.ipsDefault = new BarButtonItem();
            this.ipsWXP = new BarButtonItem();
            this.ipsOXP = new BarButtonItem();
            this.ipsO2K = new BarButtonItem();
            this.ipsO3 = new BarButtonItem();
            this.ipsO7 = new BarButtonItem();
            this.iPaintStyle.Caption = "界面风格";
            if (this.m_pBarManager != null)
            {
                this.iPaintStyle.Id = this.m_pBarManager.GetNewItemId();
            }
            this.iPaintStyle.Name = "iPaintStyle";
            this.iPaintStyle.Category = category;
            this.iPaintStyle.UseOwnFont = true;
            this.ipsDefault.Caption = "默认风格";
            if (this.m_pBarManager != null)
            {
                this.ipsDefault.Id = this.m_pBarManager.GetNewItemId();
            }
            this.ipsDefault.Description = "Default";
            this.ipsDefault.Name = "ipsDefault";
            this.ipsDefault.Category = category;
            this.ipsDefault.ItemClick += new ItemClickEventHandler(this.ips_ItemClick);
            this.ipsWXP.Caption = "Windows XP";
            this.ipsWXP.Glyph = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.PaintStyle.WindowsXP.bmp"));
            this.ipsWXP.Description = "WindowsXP";
            if (this.m_pBarManager != null)
            {
                this.ipsWXP.Id = this.m_pBarManager.GetNewItemId();
            }
            this.ipsWXP.Name = "ipsWXP";
            this.ipsWXP.Category = category;
            this.ipsWXP.ItemClick += new ItemClickEventHandler(this.ips_ItemClick);
            this.ipsOXP.Caption = "Office XP";
            this.ipsOXP.Glyph = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.PaintStyle.OfficeXP.bmp"));
            this.ipsOXP.Description = "OfficeXP";
            this.ipsOXP.Name = "ipsOXP";
            if (this.m_pBarManager != null)
            {
                this.ipsOXP.Id = this.m_pBarManager.GetNewItemId();
            }
            this.ipsOXP.Category = category;
            this.ipsOXP.ItemClick += new ItemClickEventHandler(this.ips_ItemClick);
            this.ipsO2K.Caption = "Office 2000";
            this.ipsO2K.Glyph = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.PaintStyle.Office2000.bmp"));
            this.ipsO2K.Description = "Office2000";
            this.ipsO2K.Name = "ipsO2K";
            this.ipsO2K.Category = category;
            if (this.m_pBarManager != null)
            {
                this.ipsO2K.Id = this.m_pBarManager.GetNewItemId();
            }
            this.ipsO2K.ItemClick += new ItemClickEventHandler(this.ips_ItemClick);
            this.ipsO3.Caption = "Office 2003";
            this.ipsO3.Glyph = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.PaintStyle.Office2003.bmp"));
            this.ipsO3.Description = "Office2003";
            this.ipsO3.Name = "ipsO3";
            if (this.m_pBarManager != null)
            {
                this.ipsO3.Id = this.m_pBarManager.GetNewItemId();
            }
            this.ipsO3.Category = category;
            this.ipsO3.ItemClick += new ItemClickEventHandler(this.ips_ItemClick);
            this.ipsO7.Caption = "Office 2007";
            this.ipsO7.Description = "Office2007";
            this.ipsO7.Name = "ipsO7";
            if (this.m_pBarManager != null)
            {
                this.ipsO7.Id = this.m_pBarManager.GetNewItemId();
            }
            this.ipsO7.Category = category;
            this.ipsO7.ItemClick += new ItemClickEventHandler(this.ips_ItemClick);
            if (this.m_pBarManager != null)
            {
                this.m_pBarManager.Items.Add(this.iPaintStyle);
                if (this.m_pBarManager.MainMenu != null)
                {
                    this.m_pBarManager.MainMenu.AddItem(this.iPaintStyle);
                }
                else if (this.m_pBarManager.Bars.Count > 0)
                {
                    this.m_pBarManager.Bars[0].AddItem(this.iPaintStyle);
                }
            }
            else if (this.m_ribbonctrl != null)
            {
                this.m_ribbonctrl.Items.Add(this.iPaintStyle);
                this.m_ribbonctrl.Toolbar.ItemLinks.Add(this.iPaintStyle);
            }
            this.iPaintStyle.AddItem(this.ipsDefault);
            this.iPaintStyle.AddItem(this.ipsWXP);
            this.iPaintStyle.AddItem(this.ipsOXP);
            this.iPaintStyle.AddItem(this.ipsO2K);
            this.iPaintStyle.AddItem(this.ipsO3);
        }

        private void InitPaintStyle(BarItem item)
        {
            if (item != null)
            {
                this.iPaintStyle.ImageIndex = item.ImageIndex;
                this.iPaintStyle.Caption = item.Caption;
                this.iPaintStyle.Hint = item.Description;
            }
        }

        private void InitSkins()
        {
            BarButtonItem item;
            if (this.m_pBarManager != null)
            {
                this.m_pBarManager.ForceInitialize();
                if (this.m_pBarManager.GetController().PaintStyleName == "Skin")
                {
                    this.iPaintStyle.Caption = this.skinMask + UserLookAndFeel.Default.ActiveSkinName;
                    this.iPaintStyle.Hint = this.iPaintStyle.Caption;
                }
                foreach (SkinContainer container in SkinManager.Default.Skins)
                {
                    item = new BarButtonItem(this.m_pBarManager, this.skinMask + container.SkinName) {
                        Name = "bi" + container.SkinName,
                        Id = this.m_pBarManager.GetNewItemId()
                    };
                    this.iPaintStyle.AddItem(item);
                    item.ItemClick += new ItemClickEventHandler(this.OnSkinClick);
                }
            }
            else if (this.m_ribbonctrl != null)
            {
                this.m_ribbonctrl.ForceInitialize();
                if (this.m_ribbonctrl.GetController().PaintStyleName == "Skin")
                {
                    this.iPaintStyle.Caption = this.skinMask + UserLookAndFeel.Default.ActiveSkinName;
                    this.iPaintStyle.Hint = this.iPaintStyle.Caption;
                }
                foreach (SkinContainer container in SkinManager.Default.Skins)
                {
                    item = new BarButtonItem {
                        Caption = this.skinMask + container.SkinName,
                        Name = "bi" + container.SkinName
                    };
                    this.iPaintStyle.AddItem(item);
                    item.ItemClick += new ItemClickEventHandler(this.OnSkinClick);
                }
            }
        }

        private void ips_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.m_pBarManager != null)
            {
                this.m_pBarManager.GetController().PaintStyleName = e.Item.Description;
            }
            else if (this.m_ribbonctrl != null)
            {
                this.m_ribbonctrl.GetController().PaintStyleName = e.Item.Description;
            }
            this.InitPaintStyle(e.Item);
            if (this.m_pBarManager != null)
            {
                this.m_pBarManager.GetController().ResetStyleDefaults();
            }
            else if (this.m_ribbonctrl != null)
            {
                this.m_ribbonctrl.GetController().ResetStyleDefaults();
            }
            UserLookAndFeel.Default.SetDefaultStyle();
        }

        private void OnSkinClick(object sender, ItemClickEventArgs e)
        {
            string skinName = e.Item.Caption.Replace(this.skinMask, "");
            UserLookAndFeel.Default.SetSkinStyle(skinName);
            if (this.m_pBarManager != null)
            {
                this.m_pBarManager.GetController().PaintStyleName = "Skin";
            }
            else if (this.m_ribbonctrl != null)
            {
                this.m_ribbonctrl.GetController().PaintStyleName = "Skin";
            }
            this.iPaintStyle.Caption = e.Item.Caption;
            this.iPaintStyle.Hint = this.iPaintStyle.Caption;
            this.iPaintStyle.ImageIndex = -1;
        }

        public static void SetDefaultStyle()
        {
            OfficeSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("Office 2007 Blue");
        }

        public BarManager BarManager
        {
            set
            {
                this.m_pBarManager = value;
                OfficeSkins.Register();
                this.Init();
                this.InitSkins();
            }
        }

        public RibbonControl RibbonControl
        {
            set
            {
                this.m_ribbonctrl = value;
                OfficeSkins.Register();
                this.Init();
                this.InitSkins();
            }
        }
    }
}

