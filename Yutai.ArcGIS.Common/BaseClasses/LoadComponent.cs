using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public class LoadComponent
    {
        private Assembly assembly_0;

        private object object_0;

        public LoadComponent()
        {
        }

        public object LoadClass(string string_0)
        {
            object object0;
            if (this.assembly_0 != null)
            {
                try
                {
                    this.object_0 = this.assembly_0.CreateInstance(string_0);
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                    object0 = null;
                    return object0;
                }
                object0 = this.object_0;
            }
            else
            {
                object0 = null;
            }
            return object0;
        }

        public bool LoadComponentLibrary(string string_0)
        {
            bool flag;
            try
            {
                this.assembly_0 = Assembly.LoadFrom(string_0);
                if (this.assembly_0 == null)
                {
                    MessageBox.Show("Can't Load library");
                    flag = false;
                    return flag;
                }
            }
            catch (SystemException systemException)
            {
                MessageBox.Show(systemException.Message);
                flag = false;
                return flag;
            }
            flag = true;
            return flag;
        }
    }
}