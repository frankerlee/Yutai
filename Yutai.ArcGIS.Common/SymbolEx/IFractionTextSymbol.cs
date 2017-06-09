using System.Runtime.InteropServices;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.SymbolEx
{
	[ComVisible(true)]
	[Guid("E67CB2FD-32E3-4120-B0A6-10B8BAAC668A")]
	public interface IFractionTextSymbol
	{
		string DenominatorText
		{
			get;
			set;
		}

		ITextSymbol DenominatorTextSymbol
		{
			get;
			set;
		}

		ILineSymbol LineSymbol
		{
			get;
			set;
		}

		string NumeratorText
		{
			get;
			set;
		}

		ITextSymbol NumeratorTextSymbol
		{
			get;
			set;
		}
	}
}