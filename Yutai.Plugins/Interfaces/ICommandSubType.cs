using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.Interfaces
{
    public interface ICommandSubType
    {
        void SetSubType(int SubType);
        int GetCount();

        int SubType { get; set; }
    }
}