using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    internal class TreeCreatePopMenuItem
    {
        internal List<BarItem> baritems = null;
        internal List<bool> isgroups = null;
        private Hashtable m_assemblys = new Hashtable();
        private BarManager m_barManager1 = null;
        private Hashtable m_LoadComponents = new Hashtable();
        private IApplication m_pApp = null;
        private IMapControl2 m_pInMapCtrl = null;
        protected IPageLayoutControl2 m_pPageLayoutCtrl = null;
        private TOCTreeViewEx m_pTOCTreeView = null;

        private Assembly GetAssembly(string assname)
        {
            Assembly assembly = this.m_assemblys[assname] as Assembly;
            if (assembly == null)
            {
                assembly = Assembly.Load(assname);
                this.m_assemblys[assname] = assembly;
            }
            return assembly;
        }

        private string[] GetItemAttribute(XmlNode pNode)
        {
            string[] strArray = new string[7];
            for (int i = 0; i < pNode.Attributes.Count; i++)
            {
                XmlAttribute attribute = pNode.Attributes[i];
                switch (attribute.Name.ToLower())
                {
                    case "name":
                        strArray[0] = attribute.Value;
                        break;

                    case "caption":
                        strArray[1] = attribute.Value;
                        break;

                    case "path":
                        strArray[2] = attribute.Value;
                        break;

                    case "classname":
                        strArray[3] = attribute.Value;
                        break;

                    case "subtype":
                        strArray[4] = attribute.Value;
                        break;

                    case "begingroup":
                        strArray[5] = attribute.Value;
                        break;

                    case "bitmap":
                        strArray[6] = attribute.Value;
                        break;
                }
            }
            return strArray;
        }

        private BaseClass.LoadComponent GetLoadComponent(string name)
        {
            BaseClass.LoadComponent component = this.m_assemblys[name] as BaseClass.LoadComponent;
            if (component == null)
            {
                component = new BaseClass.LoadComponent();
                component.LoadComponentLibrary(name);
                this.m_LoadComponents[name] = component;
            }
            return component;
        }

        private void pBarItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Tag is ICommand)
            {
                (e.Item.Tag as ICommand).OnClick();
            }
        }

        private void ReadBarItems(object items, XmlNode pNode)
        {
            string[] itemAttribute = this.GetItemAttribute(pNode);
            Bitmap bitmap = null;
            if (((itemAttribute[6] != null) && (itemAttribute[6].Length > 3)) && (itemAttribute[6][1] != ':'))
            {
                itemAttribute[6] = System.Windows.Forms.Application.StartupPath + @"\" + itemAttribute[6];
                try
                {
                    if (File.Exists(itemAttribute[6]))
                    {
                        bitmap = new Bitmap(itemAttribute[6]);
                    }
                }
                catch
                {
                }
            }
            ICommand command = null;
            if ((itemAttribute[2] != null) && (itemAttribute[3] != null))
            {
                try
                {
                    if (itemAttribute[2][1] != ':')
                    {
                        itemAttribute[2] = System.Windows.Forms.Application.StartupPath + @"\" + itemAttribute[2];
                    }
                    if (File.Exists(itemAttribute[2]))
                    {
                        command = this.GetLoadComponent(itemAttribute[2]).LoadClass(itemAttribute[3]) as ICommand;
                        if (command != null)
                        {
                            command.OnCreate(this.m_pApp);
                            if (command is ITOCNodePopmenuItem)
                            {
                                (command as ITOCNodePopmenuItem).InMapCtrl = this.m_pInMapCtrl;
                                (command as ITOCNodePopmenuItem).PageLayoutControl = this.m_pPageLayoutCtrl;
                                (command as ITOCNodePopmenuItem).TreeView = this.m_pTOCTreeView;
                            }
                            if (itemAttribute[4] != null)
                            {
                                try
                                {
                                    int subType = int.Parse(itemAttribute[4]);
                                    (command as ICommandSubType).SetSubType(subType);
                                }
                                catch
                                {
                                    CErrorLog.writeErrorLog("null", null, string.Format("创建{0}中的类{1}的子命令设置失败", itemAttribute[4], itemAttribute[5]));
                                }
                            }
                        }
                        else
                        {
                            CErrorLog.writeErrorLog("null", null, string.Format("创建{0}中的类{1}的失败，无法创建该工具", itemAttribute[4], itemAttribute[5]));
                        }
                    }
                    else
                    {
                        CErrorLog.writeErrorLog("null", null, string.Format("文件{0}不存在！", itemAttribute[4]));
                    }
                }
                catch (Exception exception)
                {
                    CErrorLog.writeErrorLog("null", exception, string.Format("创建{0}中的类{1}的失败", itemAttribute[4], itemAttribute[5]));
                }
            }
            if ((itemAttribute[0] != null) || (command != null))
            {
                if ((command != null) && (command.Name != null))
                {
                    itemAttribute[0] = command.Name;
                }
                BarItem item = null;
                if (itemAttribute[0].Length > 0)
                {
                    item = this.m_barManager1.Items[itemAttribute[0]];
                }
                if (item == null)
                {
                    if (pNode.ChildNodes.Count > 0)
                    {
                        item = new BarSubItem();
                        this.m_barManager1.Items.Add(item);
                    }
                    else
                    {
                        item = new BarButtonItem();
                        item.ItemClick += new ItemClickEventHandler(this.pBarItem_ItemClick);
                        this.m_barManager1.Items.Add(item);
                    }
                    if (items == null)
                    {
                        this.baritems.Add(item);
                        if (itemAttribute[5] != null)
                        {
                            this.isgroups.Add(bool.Parse(itemAttribute[5]));
                        }
                        else
                        {
                            this.isgroups.Add(false);
                        }
                    }
                    item.Name = itemAttribute[0];
                    item.Tag = command;
                    if (itemAttribute[1] != null)
                    {
                        item.Caption = itemAttribute[1];
                    }
                    if (command != null)
                    {
                        if (command.Tooltip != null)
                        {
                            item.Hint = command.Tooltip;
                        }
                        else if (command.Caption != null)
                        {
                            item.Hint = command.Caption;
                        }
                        if (bitmap != null)
                        {
                            item.Glyph = bitmap;
                        }
                        else if (command.Bitmap != 0)
                        {
                            try
                            {
                                IntPtr hbitmap = new IntPtr(command.Bitmap);
                                item.Glyph = Image.FromHbitmap(hbitmap);
                            }
                            catch
                            {
                            }
                        }
                        if (itemAttribute[1] == null)
                        {
                            item.Caption = command.Caption;
                        }
                    }
                }
                BarItemLink link = null;
                if (items is BarSubItem)
                {
                    link = (items as BarSubItem).AddItem(item);
                    if ((link != null) && (itemAttribute[5] != null))
                    {
                        link.BeginGroup = bool.Parse(itemAttribute[5]);
                    }
                }
                for (int i = 0; i < pNode.ChildNodes.Count; i++)
                {
                    this.ReadBarItems(item, pNode.ChildNodes[i]);
                }
            }
        }

        internal void StartCreateBar(string XMLConfig, TOCTreeViewEx pTOCTreeView, IApplication pApp, IMapControl2 pInMapCtrl, IPageLayoutControl2 pPageLayoutCtrl, BarManager barManager1, List<BarItem> baritems, List<bool> isgroups)
        {
            try
            {
                this.m_pTOCTreeView = pTOCTreeView;
                this.m_pApp = pApp;
                this.m_pInMapCtrl = pInMapCtrl;
                this.m_pPageLayoutCtrl = pPageLayoutCtrl;
                this.m_barManager1 = barManager1;
                this.baritems = baritems;
                this.isgroups = isgroups;
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(XMLConfig);
                    XmlElement documentElement = document.DocumentElement;
                    for (int i = 0; i < documentElement.ChildNodes.Count; i++)
                    {
                        XmlNode pNode = documentElement.ChildNodes[i];
                        this.ReadBarItems(null, pNode);
                    }
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        internal IApplication Application
        {
            set
            {
                this.m_pApp = value;
            }
        }
    }
}

