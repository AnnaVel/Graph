using GraphCore.Utilities;
using GraphView.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GraphView.Converters
{
    public class StringToBrushConverter : IValueConverter
    {
        private Dictionary<string, Color> stringToColorMapping;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueAsString = value as string;

            if (valueAsString == null)
            {
                return null;
            }

            if (this.stringToColorMapping.ContainsKey(valueAsString.ToLower()))
            {
                Color color = this.stringToColorMapping[valueAsString.ToLower()];
                return new SolidColorBrush(color);
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public StringToBrushConverter()
        {
            this.stringToColorMapping = new Dictionary<string, Color>()
            {
                { "aliceblue",  Colors.AliceBlue },
                { "antiquewhite",  Colors.AntiqueWhite },
                { "aqua",  Colors.Aqua },
                { "aquamarine",  Colors.Aquamarine },
                { "azure", Colors.Azure },
                { "beige", Colors.Beige },
                { "bisque", Colors.Bisque },
                { "black", Colors.Black },
                { "blanchedalmond", Colors.BlanchedAlmond },
                { "blue", Colors.Blue },
                { "blueviolet", Colors.BlueViolet },
                { "brown", Colors.Brown },
                { "burlywood", Colors.BurlyWood },
                { "cadetblue", Colors.CadetBlue },
                { "chartreuse", Colors.Chartreuse },
                { "chocolate", Colors.Chocolate },
                { "coral", Colors.Coral },
                { "cornflowerblue", Colors.CornflowerBlue },
                { "cornsilk", Colors.Cornsilk },
                { "crimson", Colors.Crimson },
                { "cyan", Colors.Cyan },
                { "darkblue", Colors.DarkBlue },
                { "darkcyan", Colors.DarkCyan },
                { "darkgoldenrod", Colors.DarkGoldenrod },
                { "darkgray", Colors.DarkGray },
                { "darkgreen", Colors.DarkGreen },
                { "darkkhaki", Colors.DarkKhaki },
                { "darkmagenta", Colors.DarkMagenta },
                { "darkolivegreen", Colors.DarkOliveGreen },
                { "darkorange", Colors.DarkOrange },
                { "darkorchid", Colors.DarkOrchid },
                { "darkred", Colors.DarkRed },
                { "darksalmon", Colors.DarkSalmon },
                { "darkseagreen", Colors.DarkSeaGreen },
                { "darkslateblue", Colors.DarkSlateBlue },
                { "darkslategray", Colors.DarkSlateGray },
                { "darkturquoise", Colors.DarkTurquoise },
                { "darkviolet", Colors.DarkViolet },
                { "deeppink", Colors.DeepPink },
                { "deepskyblue", Colors.DeepSkyBlue },
                { "dimgray", Colors.DimGray },
                { "dodgerblue", Colors.DodgerBlue },
                { "firebrick", Colors.Firebrick },
                { "floralwhite", Colors.FloralWhite },
                { "forestgreen", Colors.ForestGreen },
                { "fuchsia", Colors.Fuchsia },
                { "gainsboro", Colors.Gainsboro },
                { "ghostwhite", Colors.GhostWhite },
                { "gold", Colors.Gold },
                { "goldenrod", Colors.Goldenrod },
                { "gray", Colors.Gray },
                { "green", Colors.Green },
                { "greenyellow", Colors.GreenYellow },
                { "honeydew", Colors.Honeydew },
                { "hotpink", Colors.HotPink },
                { "indianred", Colors.IndianRed },
                { "indigo", Colors.Indigo },
                { "ivory", Colors.Ivory },
                { "khaki", Colors.Khaki },
                { "lavender", Colors.Lavender },
                { "lavenderblush", Colors.LavenderBlush },
                { "lawngreen", Colors.LawnGreen },
                { "lemonchiffon", Colors.LemonChiffon },
                { "lightblue", Colors.LightBlue },
                { "lightcoral", Colors.LightCoral },
                { "lightcyan", Colors.LightCyan },
                { "lightgoldenrodYellow", Colors.LightGoldenrodYellow },
                { "lightgray", Colors.LightGray },
                { "lightgreen", Colors.LightGreen },
                { "lightpink", Colors.LightPink },
                { "lightsalmon", Colors.LightSalmon },
                { "lightseagreen", Colors.LightSeaGreen },
                { "lightskyblue", Colors.LightSkyBlue },
                { "lightslategray", Colors.LightSlateGray },
                { "lightsteelblue", Colors.LightSteelBlue },
                { "lightyellow", Colors.LightYellow },
                { "lime", Colors.Lime },
                { "limegreen", Colors.LimeGreen },
                { "linen", Colors.Linen },
                { "magenta", Colors.Magenta },
                { "maroon", Colors.Maroon },
                { "mediumaquamarine", Colors.MediumAquamarine },
                { "mediumblue", Colors.MediumBlue },
                { "mediumorchid", Colors.MediumOrchid },
                { "mediumpurple", Colors.MediumPurple },
                { "mediumseagreen", Colors.MediumSeaGreen },
                { "mediumslateblue", Colors.MediumSlateBlue },
                { "mediumspringgreen", Colors.MediumSpringGreen },
                { "mediumturquoise", Colors.MediumTurquoise },
                { "mediumvioletred", Colors.MediumVioletRed },
                { "midnightblue", Colors.MidnightBlue },
                { "mintcream", Colors.MintCream },
                { "mistyrose", Colors.MistyRose },
                { "moccasin", Colors.Moccasin },
                { "navajowhite", Colors.NavajoWhite },
                { "navy", Colors.Navy },
                { "oldlace", Colors.OldLace },
                { "olive", Colors.Olive },
                { "olivedrab", Colors.OliveDrab },
                { "orange", Colors.Orange },
                { "orangered", Colors.OrangeRed },
                { "orchid", Colors.Orchid },
                { "palegoldenrod", Colors.PaleGoldenrod },
                { "palegreen", Colors.PaleGreen },
                { "paleturquoise", Colors.PaleTurquoise },
                { "palevioletred", Colors.PaleVioletRed },
                { "papayawhip", Colors.PapayaWhip },
                { "peachpuff", Colors.PeachPuff },
                { "peru", Colors.Peru },
                { "pink", Colors.Pink },
                { "plum", Colors.Plum },
                { "powderblue", Colors.PowderBlue },
                { "purple", Colors.Purple },
                { "red", Colors.Red },
                { "rosybrown", Colors.RosyBrown },
                { "royalblue", Colors.RoyalBlue },
                { "saddleBrown", Colors.SaddleBrown },
                { "salmon", Colors.Salmon },
                { "sandybrown", Colors.SandyBrown },
                { "seagreen", Colors.SeaGreen },
                { "seashell", Colors.SeaShell },
                { "sienna", Colors.Sienna },
                { "silver", Colors.Silver },
                { "skyblue", Colors.SkyBlue },
                { "slateblue", Colors.SlateBlue },
                { "slategray", Colors.SlateGray },
                { "snow", Colors.Snow },
                { "springgreen", Colors.SpringGreen },
                { "steelblue", Colors.SteelBlue },
                { "tan", Colors.Tan },
                { "teal", Colors.Teal },
                { "thistle", Colors.Thistle },
                { "tomato", Colors.Tomato },
                { "transparent", Colors.Transparent },
                { "turquoise", Colors.Turquoise },
                { "violet", Colors.Violet },
                { "wheat", Colors.Wheat },
                { "white", Colors.White },
                { "whitesmoke", Colors.WhiteSmoke },
                { "yellow", Colors.Yellow },
                { "yellowGreen", Colors.YellowGreen },
            };
        }
    }
}
