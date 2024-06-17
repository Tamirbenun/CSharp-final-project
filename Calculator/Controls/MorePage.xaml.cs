using System;
using System.Collections.Generic;
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

namespace Calculator.Controls;

public partial class MorePage : UserControl
{

    public MorePage()
    {
        InitializeComponent();
    }

    private void Button_AreaS(object sender, RoutedEventArgs e)
    {
        try
        {
            string length = AreaSinputLen.Text.ToString();
            string width = AreaSinputWid.Text.ToString();

            double result = double.Parse(length) * double.Parse(width);

            if (result >= 0)
            {
                AreaSResult.Text = result.ToString();
            } else
            {
                AreaSResult.Text = "Error";
            }
        } catch
        {
            AreaSResult.Text = "Error";
        }
    }

    private void Button_AreaC(object sender, RoutedEventArgs e)
    {
        try
        {
            string radius = AreaCinputRad.Text.ToString();

            double result = double.Parse(radius) * double.Parse(radius) * Math.PI;

            if (result >= 0)
            {
                AreaCResult.Text = result.ToString();
            } else
            {
                AreaCResult.Text = "Error";
            }
        } catch
        {
            AreaCResult.Text = "Error";
        }
    }

    private void Button_Areat(object sender, RoutedEventArgs e)
    {
        try
        {
            string baseT = AreaTinputBas.Text.ToString();
            string heigth = AreaTinputHei.Text.ToString();

            double result = double.Parse(baseT) * double.Parse(heigth) / 2;

            if (result >= 0)
            {
                AreaTResult.Text = result.ToString();
            } else
            {
                AreaTResult.Text = "Error";
            }
        } catch
        {
            AreaTResult.Text = "Error";
        }
    }

    private void Button_CircumS(object sender, RoutedEventArgs e)
    {
        try
        {
            string edge1 = CircumSEdge1.Text.ToString();
            string edge2 = CircumSEdge2.Text.ToString();
            string edge3 = CircumSEdge3.Text.ToString();
            string edge4 = CircumSEdge4.Text.ToString();

            double result = double.Parse(edge1) + double.Parse(edge2) + double.Parse(edge3) + double.Parse(edge4);

            if (result >= 0)
            {
                CircumSResult.Text = result.ToString();
            } else
            {
                CircumSResult.Text = "Error";
            }
        } catch
        {
            CircumSResult.Text = "Error";
        }
    }

    private void Button_CircumC(object sender, RoutedEventArgs e)
    {
        try
        {
            string radius = CircumCRad.Text.ToString();

            double result = 2 * Math.PI * double.Parse(radius);

            if (result >= 0)
            {
                CircumCResult.Text = result.ToString();
            } else
            {
                CircumCResult.Text = "Error";
            }
        } catch
        {
            CircumCResult.Text = "Error";
        }
    }

    private void Button_CircumT(object sender, RoutedEventArgs e)
    {
        try
        {
            string baseT = CircumTBas.Text.ToString();
            string side1 = CircumTSide1.Text.ToString();
            string side2 = CircumTSide2.Text.ToString();

            double result = double.Parse(baseT) + double.Parse(side1) + double.Parse(side2);

            if (result >= 0)
            {
                CircumTResult.Text = result.ToString();
            } else
            {
                CircumTResult.Text = "Error";
            }
        } catch
        {
            CircumTResult.Text = "Error";
        }
    }
}
