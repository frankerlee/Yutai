using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Shared
{
    /// <summary>
    /// Specifies that R# cleanup should not reorder class members.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class CustomLayoutAttribute: Attribute
    {
    }
}
