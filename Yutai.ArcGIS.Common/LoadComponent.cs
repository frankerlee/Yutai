using System;
using System.Reflection;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common
{
    public class LoadComponent
    {
        private Assembly assembly_0;
        private object object_0;

        public object LoadClass(string string_0)
        {
            if (this.assembly_0 == null)
            {
                return null;
            }
            try
            {
                this.object_0 = this.assembly_0.CreateInstance(string_0);
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(null, exception, "");
                return null;
            }
            return this.object_0;
        }

        public bool LoadComponentLibrary(string string_0)
        {
            try
            {
                this.assembly_0 = Assembly.LoadFrom(string_0);
                if (this.assembly_0 == null)
                {
                    MessageBox.Show("Can't Load library");
                    return false;
                }
            }
            catch (SystemException exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
            return true;
        }
    }
}