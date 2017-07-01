using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Yutai.ArcGIS.Framework.Docking
{
    public class FloatWindowCollection : ReadOnlyCollection<FloatWindow>
    {
        internal FloatWindowCollection() : base(new List<FloatWindow>())
        {
        }

        internal int Add(FloatWindow fw)
        {
            if (base.Items.Contains(fw))
            {
                return base.Items.IndexOf(fw);
            }
            base.Items.Add(fw);
            return (base.Count - 1);
        }

        internal void BringWindowToFront(FloatWindow fw)
        {
            base.Items.Remove(fw);
            base.Items.Add(fw);
        }

        internal void Dispose()
        {
            for (int i = base.Count - 1; i >= 0; i--)
            {
                base[i].Close();
            }
        }

        internal void Remove(FloatWindow fw)
        {
            base.Items.Remove(fw);
        }
    }
}