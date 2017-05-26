using System;
using Yutai.Plugins.Events;
using Yutai.Services.Serialization;
using Yutai.Shared;

namespace Yutai.Services.Concrete
{
    public class ProjectLoaderBase : IProjectLoaderBase
    {
        protected void FireProgressChanged(int step, int total, string message)
        {
            double percent = step / (double)total * 100.0;

            DelegateHelper.FireEvent(this, ProgressChanged, new ProgressEventArgs(message, Convert.ToInt32(percent)));
        }

        public event EventHandler<ProgressEventArgs> ProgressChanged;
    }
}