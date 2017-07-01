using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;
using Yutai.UI.Controls;
using Yutai.Views;

namespace Yutai.Controls
{
    public class ConfigTreeView : TreeViewBase
    {
        private ConfigViewModel _model;

        public ConfigTreeView()
        {
            AfterExpand += ConfigTreeView_AfterExpand;
        }

        private void ConfigTreeView_AfterExpand(object sender, TreeViewAdvNodeEventArgs e)
        {
            foreach (TreeNodeAdv node in Nodes)
            {
                if (node != e.Node)
                {
                    node.Expanded = false;
                }
            }

            SelectedNode = e.Node;
        }

        public void Initialize(ConfigViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model");
            _model = model;

            IconSize = 24;
            ApplyStyle = false;

            // usual call from constructor won't work here since list of icons is generated dynamically
            CreateImageList();

            AddAllPages();
        }

        private void AddAllPages()
        {
            AddPages(Nodes, "");

            foreach (var page in _model.Pages.Where(p => p.ParentKey == ""))
            {
                var node = NodeForPage(page);
                AddPages(node.Nodes, page.Key);
            }
        }

        private void AddPages(TreeNodeAdvCollection nodes, string parentKey)
        {
            foreach (var page in _model.Pages)
            {
                if (page.ParentKey != parentKey) continue;

                var node = CreateNodeForPage(page);
                page.Tag = node;
                node.Expanded = false;
                nodes.Add(node);
            }
        }

        protected override IEnumerable<Bitmap> OnCreateImageList()
        {
            if (_model == null)
            {
                yield break;
            }

            var pages = _model.Pages.ToList();
            for (int i = 0; i < pages.Count(); i++)
            {
                var p = pages[i];
                p.ImageIndex = i;
                yield return p.Icon;
            }
        }

        public void SetSelectedPage(string pageKey)
        {
            TreeNodeAdv selectedNode = null;

            var page = _model.Pages.FirstOrDefault(p => p.Key == pageKey);
            if (page != null)
            {
                var node = NodeForPage(page);
                if (node != null)
                {
                    selectedNode = node;
                }
            }

            SelectedNode = selectedNode ?? Nodes[0];
        }


        public void RestoreSelectedNode(string lastPageKey)
        {
            TreeNodeAdv selectedNode = null;

            if (lastPageKey == null)
            {
                lastPageKey = string.Empty;
            }

            foreach (var page in _model.Pages)
            {
                if (page.Key.ContainsIgnoreCase(lastPageKey))
                {
                    var node = NodeForPage(page);
                    if (node != null)
                    {
                        selectedNode = node;
                        if (selectedNode.Parent != null)
                        {
                            selectedNode.Parent.Expand();
                        }

                        break;
                    }
                }
            }

            SelectedNode = selectedNode ?? Nodes[0];
        }

        private TreeNodeAdv NodeForPage(IConfigPage page)
        {
            return page.Tag as TreeNodeAdv;
        }

        private TreeNodeAdv CreateNodeForPage(IConfigPage page)
        {
            return new TreeNodeAdv(page.PageName)
            {
                Tag = page,
                LeftImageIndices = new[] {page.ImageIndex}
            };
        }
    }
}