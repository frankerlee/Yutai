using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Check.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Check.Views
{
    public class CheckResultPresenter : CommandDispatcher<ICheckResultView, CheckResultCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;
        public CheckResultPresenter(IAppContext context, ICheckResultView view) : base(view)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;

            View.Initialize(context);
        }

        public override void RunCommand(CheckResultCommand command)
        {
            return;
        }

        public Control GetInternalObject()
        {
            return View as Control;
        }
    }
}
