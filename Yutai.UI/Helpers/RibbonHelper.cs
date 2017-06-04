using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.UI.Helpers
{
   public  class RibbonHelper
    {
        public static int CalculateSubTypeFromName(string menuName)
        {
            int lastIndex = menuName.LastIndexOf('_');
            if (lastIndex < 0) return 0;
            string numStr = menuName.Substring(lastIndex + 1);
            return Convert.ToInt32(numStr);
        }
    }
}
