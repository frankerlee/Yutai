using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IPipelineType
    {
        string Code { get; set; }
        string Name { get; set; }
        string AutoNames { get; set; }
    }
}