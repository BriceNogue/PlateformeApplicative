using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop.Components
{
    /// <summary>
    /// Logique d'interaction pour RadialProgressBar.xaml
    /// </summary>
    public partial class RadialProgressBar : UserControl
    {
        public RadialProgressBar()
        {
            InitializeComponent();
        }

        public Brush IndicatorBrush
        {
            get { return (Brush)GetValue(IndicatorBrushProperty); }
            set { SetValue(IndicatorBrushProperty, value); }
        }

        // Pour activer les animations, le style, le binding, etc...
        public static readonly DependencyProperty IndicatorBrushProperty = 
            DependencyProperty.Register("IndicatorBrush", typeof(Brush), typeof(RadialProgressBar));

        
        public Brush BackgroundBrush
        {
            get { return (Brush)GetValue(BackgroundBrushProperty); }
            set { SetValue(BackgroundBrushProperty, value); }
        }

        // Pour activer les animations, le style, le binding, etc...
        public static readonly DependencyProperty BackgroundBrushProperty = 
            DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(RadialProgressBar));

        
        public Brush ProgressBorderBrush
        {
            get { return (Brush)GetValue(ProgressBorderBrushProperty); }
            set { SetValue(ProgressBorderBrushProperty, value); }
        }

        // Pour activer les animations, le style, le binding, etc...
        public static readonly DependencyProperty ProgressBorderBrushProperty =
            DependencyProperty.Register("ProgressBorderBrush", typeof(Brush), typeof(RadialProgressBar));

        public int ArcThickness
        {
            get { return (int)GetValue(ArcThicknessProperty); }
            set { SetValue(ArcThicknessProperty, value); }
        }

        // Pour activer les animations, le style, le binding, etc...
        public static readonly DependencyProperty ArcThicknessProperty =
            DependencyProperty.Register("ArcThickness", typeof(int), typeof(RadialProgressBar));


        public int PercentFontSize
        {
            get { return (int)GetValue(PercentFontSizeProperty); }
            set { SetValue(PercentFontSizeProperty, value); }
        }

        // Pour activer les animations, le style, le binding, etc...
        public static readonly DependencyProperty PercentFontSizeProperty =
            DependencyProperty.Register("PercentFontSize", typeof(int), typeof(RadialProgressBar));


        public string PercentText
        {
            get { return (string)GetValue(PercentTextProperty); }
            set { SetValue(PercentTextProperty, value); }
        }

        // Pour activer les animations, le style, le binding, etc...
        public static readonly DependencyProperty PercentTextProperty =
            DependencyProperty.Register("PercentTextProperty", typeof(string), typeof(RadialProgressBar));


        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Pour activer les animations, le style, le binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(RadialProgressBar));
    }

    [ValueConversion(typeof(int), typeof(double))]
    public class ValueToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)(((int)value * 0.01) * 360);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)((double)value / 360) * 100;
        }
    }
}
