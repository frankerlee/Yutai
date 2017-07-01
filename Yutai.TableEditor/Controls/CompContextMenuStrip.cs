using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Commands.ContextMenu;
using Yutai.Plugins.TableEditor.Editor;
using Yutai.Plugins.TableEditor.Menu;

namespace Yutai.Plugins.TableEditor.Controls
{
    public partial class CompContextMenuStrip : ContextMenuStrip
    {
        private readonly IAppContext _context;
        private ITableView _tableView;
        private List<YutaiCommand> _commands;
        public int _columnIndex = -1;

        public CompContextMenuStrip(IAppContext context, ITableView tableView)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            _tableView = tableView;
            InitializeComponent();
            InitMenu();
        }

        public ITableView TableView
        {
            get { return _tableView; }
        }

        public int ColumnIndex
        {
            get { return _columnIndex; }
            set { _columnIndex = value; }
        }

        private void InitMenu()
        {
            CreateCommands();
            foreach (YutaiCommand yutaiCommand in _commands)
            {
                AddCommand(yutaiCommand);
            }
        }

        private void AddCommand(YutaiCommand command)
        {
            if (command is YutaiMenuCommand)
            {
                ToolStripDropDownButton toolStripDropDownButton = new ToolStripDropDownButton();
                toolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
                toolStripDropDownButton.Name = command.Name;
                toolStripDropDownButton.Text = command.Caption;
                Items.Add(toolStripDropDownButton);
            }
            else if (command is YutaiSeparatorCommand)
            {
                if (string.IsNullOrWhiteSpace(command.Key))
                {
                    Items.Add(new ToolStripSeparator());
                }
                else
                {
                    ToolStripDropDownButton dropDown = Items[command.Key] as ToolStripDropDownButton;
                    dropDown.DropDownItems.Add(new ToolStripSeparator());
                }
            }
            else
            {
                string[] names = command.Name.Split('.');
                if (names.Length == 1)
                {
                    ToolStripMenuItem menu = new ToolStripMenuItem
                    {
                        Text = command.Caption,
                        Name = command.Name,
                        ToolTipText = command.Tooltip,
                        Image = command.Image,
                    };
                    menu.Click += command.OnClick;
                    Items.Add(menu);
                }
                else if (names.Length == 2)
                {
                    ToolStripDropDownButton dropDown = Items[names[0]] as ToolStripDropDownButton;

                    ToolStripMenuItem menu = new ToolStripMenuItem
                    {
                        Text = command.Caption,
                        Name = command.Name,
                        ToolTipText = command.Tooltip,
                        Image = command.Image,
                    };

                    menu.Click += command.OnClick;
                    dropDown.DropDownItems.Add(menu);
                }
            }
        }

        private void CreateCommands()
        {
            if (_commands == null)
            {
                _commands = new List<YutaiCommand>()
                {
                    new CmdSortAscending(_context, this),
                    new CmdSortDescending(_context, this),
                    new YutaiSeparatorCommand(),
                    new CmdHideField(_context, this),
                    new YutaiSeparatorCommand(),
                    new CmdCalculateField(_context, this),
                    new YutaiSeparatorCommand(),
                    new CmdRemoveField(_context, this),
                    new YutaiSeparatorCommand(),
                    new CmdStatistics(_context, this),
                    new CmdFieldProperties(_context, this),
                };
            }
        }

        public void Show(int columnIndex, int x, int y)
        {
            _columnIndex = columnIndex;
            base.Show(x, y);
        }
    }
}