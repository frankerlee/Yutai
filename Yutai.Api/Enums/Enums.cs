using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Api.Enums
{
    public enum CoordinatesDisplay
    {
        None = 0,
        Auto = 1,
        Degrees = 2,
        MapUnits = 3,
    }
    public enum AutoToggle
    {
        Auto = 0,
        True = 1,
        False = 2,
    }

    public enum AngleFormat
    {
        Degrees = 0,
        Minutes = 1,
        Seconds = 2,
        Radians = 3,
    }

    public enum LengthUnits
    {
        DecimalDegrees = 0,
        Milimeters = 1,
        Centimeters = 2,
        Inches = 3,
        Feet = 4,
        Yards = 5,
        Meters = 6,
        Miles = 7,
        Kilometers = 8,
    }

    public enum AreaDisplay
    {
        Metric = 0,
        Hectars = 1,
        American = 2,
    }

    public enum LengthDisplay
    {
        Metric = 0,
        American = 1,
    }
    public enum AreaUnits
    {
        SquareMeters = 0,
        Hectares = 1,
        SquareKilometers = 2,
        SquareFeet = 3,
        SquareYards = 4,
        Acres = 5,
        SquareMiles = 6,
    }

    public enum ScalebarUnits
    {
        Metric = 0,
        American = 1,
        GoogleStyle = 2,
    }

    public enum IdentifierMode
    {
        TopLayer=0,
        SelectableLayer = 1,
        VisibleLayer=2,
        AllLayer=3,
        CurrentLayer=4
    }

}
