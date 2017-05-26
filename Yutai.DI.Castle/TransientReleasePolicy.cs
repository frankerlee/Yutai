using System;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Releasers;

namespace Yutai.DI.Castle
{
    [Serializable]
    internal class TransientReleasePolicy : LifecycledComponentsReleasePolicy
    {
        public TransientReleasePolicy(IKernel kernel)
            : base(kernel)
        {

        }

        public override void Track(object instance, Burden burden)
        {
            ComponentModel model = burden.Model;

            // to modify the way Castle handles the Transient object uncomment the following lines
            if (model.LifestyleType == LifestyleType.Transient)
                return;

            base.Track(instance, burden);
        }
    }
}