using System.Runtime.InteropServices;

namespace Yutai.ArcGIS.Common.SymbolEx
{
    [ComVisible(true)]
    [Guid("4C584D74-9847-422c-B713-5FBB584AB91F")]
    public interface IMyText
    {
        string DenominatorText { get; set; }

        string NumeratorText { get; set; }
    }
}