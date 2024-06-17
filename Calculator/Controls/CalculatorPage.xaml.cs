using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using AngouriMath;
using AngouriMath.Extensions;
using HonkSharp.Fluency;

namespace Calculator.Controls;

public partial class CalculatorPage : UserControl
{
    Entity equalsResult;

    public string add = "+";
    public string sub = "-";
    public string mul = "x";
    public string div = Convert.ToChar(247).ToString();
    public string root = "√";
    public string exp = "^";
    public string pi = "π";
    public string cos = "cos(";
    public string sin = "sin(";
    public string tan = "tan(";
    public string par_l = "(";
    public string par_r = ")";
    public string deg = "°";

    public string result = "0";
    public string calculation = "";
    public string newResult = "";
    public string currentNumber = "0";

    public CalculatorPage()
    {
        InitializeComponent();
        ResultsTextBlock.Text = result;
    }

    private void Button_AC(object sender, RoutedEventArgs e)
    {
        result = "0";
        currentNumber = "0";
        CalculationTextBlock.Text = "";
        ResultsTextBlock.Text = result;
    }

    private void Button_Delete(object sender, RoutedEventArgs e)
    {
        if (result == "Error")
        {
            result = "0";
            currentNumber = "0";
            ResultsTextBlock.Text = result;
        } else if (result == "0")
        {
            CalculationTextBlock.Text = "";
        } else
        {
            newResult = result.Remove(result.Length - 1);

            if (newResult.Length == 0)
            {
                newResult += "0";
            }
            result = newResult;

            if (!result.Contains(add) && !result.Contains(sub) && !result.Contains(mul) && !result.Contains(div) && result.Contains(","))
            {
                newResult = result.Replace(",", "");
                float resultToFloat = float.Parse(newResult);
                int resultToInt = int.Parse(newResult);

                if (resultToInt == resultToFloat)
                {
                    result = $"{resultToFloat:n0}";
                }
                else
                {
                    result = $"{resultToFloat:n}";
                }
            }

            ResultsTextBlock.Text = result;

            if (currentNumber != "")
            {
                string newCurrentNumber = currentNumber.Remove(currentNumber.Length - 1);

                if (newCurrentNumber.Length == 0)
                {
                    newCurrentNumber += "0";
                }

                currentNumber = newCurrentNumber;
            }

            if (result == "-")
            {
                result = "0";
                currentNumber = "0";
                ResultsTextBlock.Text = result;
            }
        }
    }

    private void Button_Percent(object sender, RoutedEventArgs e)
    {
        if (result == "0" || result == "Error" || result.EndsWith(add) || result.EndsWith(sub) || result.EndsWith(mul) || result.EndsWith(div) || result.EndsWith("."))
        {
        } else 
        {
            if (currentNumber.EndsWith(".") || currentNumber == "0" || currentNumber == "")
            {
            } else
            {
                int LengthOfResult = result.Length;
                int LengthOfCurrentNumber = currentNumber.Length;
                int removeFrom = LengthOfResult - LengthOfCurrentNumber;

                newResult = result.Remove(removeFrom, LengthOfCurrentNumber);
                result = newResult;

                if (currentNumber.Contains(par_l))
                {
                    newResult = currentNumber.Replace(par_l, "");
                    if (newResult.Contains(par_r))
                    {
                        newResult = newResult.Replace(par_r, "");
                    }
                    currentNumber = newResult;
                }
                float percent = float.Parse(currentNumber);
                float toDecimal = percent / 100;
                currentNumber = toDecimal.ToString();

                if (currentNumber.Contains(sub) && (result.EndsWith(add) || result.EndsWith(sub) || result.EndsWith(mul) || result.EndsWith(div)))
                {
                    currentNumber = par_l + toDecimal.ToString() + par_r;
                    result += currentNumber;
                    ResultsTextBlock.Text = result;
                } else
                {
                    result += currentNumber;
                    ResultsTextBlock.Text = result;
                }
            }
        }
    }

    private void Button_Division(object sender, RoutedEventArgs e)
    {
        if (result.EndsWith(div) || result.EndsWith(".") || result == "Error")
        {
        } else if (result.EndsWith(add) || result.EndsWith(sub) || result.EndsWith(mul))
        {
            newResult = result.Remove(result.Length - 1);
            newResult += div;
            result = newResult;
            ResultsTextBlock.Text = result;
        } else if (currentNumber.Contains(par_l) && !currentNumber.Contains(par_r))
        {
            result += par_r + div;
            currentNumber = "";
            ResultsTextBlock.Text = result;
        } else
        {
            result += div;
            currentNumber = "";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_Multiplication(object sender, RoutedEventArgs e)
    {
        if (result.EndsWith(mul) || result.EndsWith(".") || result == "Error") 
        {
        } else if (result.EndsWith(add) || result.EndsWith(sub) || result.EndsWith(div))
        {
            newResult = result.Remove(result.Length - 1);
            newResult += mul;
            result = newResult;
            ResultsTextBlock.Text = result;
        } else if (currentNumber.Contains(par_l) && !currentNumber.Contains(par_r))
        {
            result += par_r + mul;
            currentNumber = "";
            ResultsTextBlock.Text = result;
        } else
        {
            result += mul;
            currentNumber = "";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_Subtraction(object sender, RoutedEventArgs e)
    {
        if (result.EndsWith(sub) || result.EndsWith(".") || result == "Error")
        {
        } else if (result == "0")
        {
            newResult = result.Remove(result.Length - 1);
            newResult += sub;
            result = newResult;
            currentNumber = result;
            ResultsTextBlock.Text = result;
        } else if (result.EndsWith(add) || result.EndsWith(mul) || result.EndsWith(div))
        {
            result += par_l + sub;
            currentNumber += par_l + sub;
            ResultsTextBlock.Text = result;
        } else if (currentNumber.Contains(par_l) && !currentNumber.Contains(par_r))
        {
            result += par_r + sub;
            currentNumber = "";
            ResultsTextBlock.Text = result;
        } else
        {
            result += sub;
            currentNumber = "";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_Addition(object sender, RoutedEventArgs e)
    {
        if (result.EndsWith(add) || result.EndsWith(".") || result == "Error")
        {
        } else if (result.EndsWith(sub) || result.EndsWith(mul) || result.EndsWith(div))
        {
            newResult = result.Remove(result.Length - 1);
            newResult += add;
            result = newResult;
            ResultsTextBlock.Text = result;
        } else if (currentNumber.Contains(par_l) && !currentNumber.Contains(par_r))
        {
            result += par_r + add;
            currentNumber = "";
            ResultsTextBlock.Text = result;
        } else
        {
            result += add;
            currentNumber = "";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_0(object sender, RoutedEventArgs e)
    {
        if (result == "0" || currentNumber == "Error")
        {
        } else if (currentNumber.StartsWith(cos) || currentNumber.StartsWith(sin) || currentNumber.StartsWith(tan))
        {
            if (currentNumber.EndsWith(deg))
            {
                newResult = result.Remove(result.Length - 1);
                result = newResult;
                result += "0" + deg;
                currentNumber += "0" + deg;
                ResultsTextBlock.Text = result;
            } else
            {
                result += "0" + deg;
                currentNumber += "0" + deg;
                ResultsTextBlock.Text = result;
            }
        } else
        {
            result += "0";
            currentNumber += "0";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_1(object sender, RoutedEventArgs e)
    {
        if (result == "Error")
        {
        } else if (result == "0")
        {
            result = "1";
            currentNumber = "1";
            ResultsTextBlock.Text = result;
        } else if (currentNumber.StartsWith(cos) || currentNumber.StartsWith(sin) || currentNumber.StartsWith(tan))
        {
            if (currentNumber.EndsWith(deg))
            {
                newResult = result.Remove(result.Length - 1);
                result = newResult;
                result += "1" + deg;
                currentNumber += "1" + deg;
                ResultsTextBlock.Text = result;
            } else
            {
                result += "1" + deg;
                currentNumber += "1" + deg;
                ResultsTextBlock.Text = result;
            }
        } else
        {
            result += "1";
            currentNumber += "1";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_2(object sender, RoutedEventArgs e)
    {
        if (result == "Error")
        {
        } else if (result == "0")
        {
            result = "2";
            currentNumber = "2";
            ResultsTextBlock.Text = result;
        } else if (currentNumber.StartsWith(cos) || currentNumber.StartsWith(sin) || currentNumber.StartsWith(tan))
        {
            if (currentNumber.EndsWith(deg))
            {
                newResult = result.Remove(result.Length - 1);
                result = newResult;
                result += "2" + deg;
                currentNumber += "2" + deg;
                ResultsTextBlock.Text = result;
            } else
            {
                result += "2" + deg;
                currentNumber += "2" + deg;
                ResultsTextBlock.Text = result;
            }
        } else
        {
            result += "2";
            currentNumber += "2";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_3(object sender, RoutedEventArgs e)
    {
        if (result == "Error")
        {
        } else if (result == "0")
        {
            result = "3";
            currentNumber = "3";
            ResultsTextBlock.Text = result;
        } else if (currentNumber.StartsWith(cos) || currentNumber.StartsWith(sin) || currentNumber.StartsWith(tan))
        {
            if (currentNumber.EndsWith(deg))
            {
                newResult = result.Remove(result.Length - 1);
                result = newResult;
                result += "3" + deg;
                currentNumber += "3" + deg;
                ResultsTextBlock.Text = result;
            } else
            {
                result += "3" + deg;
                currentNumber += "3" + deg;
                ResultsTextBlock.Text = result;
            }
        } else
        {
            result += "3";
            currentNumber += "3";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_4(object sender, RoutedEventArgs e)
    {
        if (result == "Error")
        {
        } else if (result == "0")
        {
            result = "4";
            currentNumber = "4";
            ResultsTextBlock.Text = result;
        } else if (currentNumber.StartsWith(cos) || currentNumber.StartsWith(sin) || currentNumber.StartsWith(tan))
        {
            if (currentNumber.EndsWith(deg))
            {
                newResult = result.Remove(result.Length - 1);
                result = newResult;
                result += "4" + deg;
                currentNumber += "4" + deg;
                ResultsTextBlock.Text = result;
            } else
            {
                result += "4" + deg;
                currentNumber += "4" + deg;
                ResultsTextBlock.Text = result;
            }
        } else
        {
            result += "4";
            currentNumber += "4";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_5(object sender, RoutedEventArgs e)
    {
        if (result == "Error")
        {
        } else if (result == "0")
        {
            result = "5";
            currentNumber = "5";
            ResultsTextBlock.Text = result;
        } else if (currentNumber.StartsWith(cos) || currentNumber.StartsWith(sin) || currentNumber.StartsWith(tan))
        {
            if (currentNumber.EndsWith(deg))
            {
                newResult = result.Remove(result.Length - 1);
                result = newResult;
                result += "5" + deg;
                currentNumber += "5" + deg;
                ResultsTextBlock.Text = result;
            } else
            {
                result += "5" + deg;
                currentNumber += "5" + deg;
                ResultsTextBlock.Text = result;
            }
        } else
        {
            result += "5";
            currentNumber += "5";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_6(object sender, RoutedEventArgs e)
    {
        if (result == "Error")
        {
        } else if (result == "0")
        {
            result = "6";
            currentNumber = "6";
            ResultsTextBlock.Text = result;
        } else if (currentNumber.StartsWith(cos) || currentNumber.StartsWith(sin) || currentNumber.StartsWith(tan))
        {
            if (currentNumber.EndsWith(deg))
            {
                newResult = result.Remove(result.Length - 1);
                result = newResult;
                result += "6" + deg;
                currentNumber += "6" + deg;
                ResultsTextBlock.Text = result;
            } else
            {
                result += "6" + deg;
                currentNumber += "6" + deg;
                ResultsTextBlock.Text = result;
            }
        } else
        {
            result += "6";
            currentNumber += "6";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_7(object sender, RoutedEventArgs e)
    {
        if (result == "Error")
        {
        } else if (result == "0")
        {
            result = "7";
            currentNumber = "7";
            ResultsTextBlock.Text = result;
        } else if (currentNumber.StartsWith(cos) || currentNumber.StartsWith(sin) || currentNumber.StartsWith(tan))
        {
            if (currentNumber.EndsWith(deg))
            {
                newResult = result.Remove(result.Length - 1);
                result = newResult;
                result += "7" + deg;
                currentNumber += "7" + deg;
                ResultsTextBlock.Text = result;
            } else
            {
                result += "7" + deg;
                currentNumber += "7" + deg;
                ResultsTextBlock.Text = result;
            }
        } else
        {
            result += "7";
            currentNumber += "7";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_8(object sender, RoutedEventArgs e)
    {
        if (result == "Error")
        {
        } else if (result == "0")
        {
            result = "8";
            currentNumber = "8";
            ResultsTextBlock.Text = result;
        } else if (currentNumber.StartsWith(cos) || currentNumber.StartsWith(sin) || currentNumber.StartsWith(tan))
        {
            if (currentNumber.EndsWith(deg))
            {
                newResult = result.Remove(result.Length - 1);
                result = newResult;
                result += "8" + deg;
                currentNumber += "8" + deg;
                ResultsTextBlock.Text = result;
            } else
            {
                result += "8" + deg;
                currentNumber += "8" + deg;
                ResultsTextBlock.Text = result;
            }
        } else
        {
            result += "8";
            currentNumber += "8";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_9(object sender, RoutedEventArgs e)
    {
        if (result == "Error")
        {
        } else if (result == "0")
        {
            result = "9";
            currentNumber = "9";
            ResultsTextBlock.Text = result;
        } else if (currentNumber.StartsWith(cos) || currentNumber.StartsWith(sin) || currentNumber.StartsWith(tan))
        {
            if (currentNumber.EndsWith(deg))
            {
                newResult = result.Remove(result.Length - 1);
                result = newResult;
                result += "9" + deg;
                currentNumber += "9" + deg;
                ResultsTextBlock.Text = result;
            } else
            {
                result += "9" + deg;
                currentNumber += "9" + deg;
                ResultsTextBlock.Text = result;
            }
        } else
        {
            result += "9";
            currentNumber += "9";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_Dot(object sender, RoutedEventArgs e)
    {
        if (result.EndsWith("."))
        {
        } else
        {
            if (currentNumber.Contains("."))
            {
            } else if (currentNumber.StartsWith(cos) || currentNumber.StartsWith(sin) || currentNumber.StartsWith(tan))
            {
                if (currentNumber.EndsWith(deg))
                {
                    newResult = result.Remove(result.Length - 1);
                    result = newResult;
                    result += ".";
                    currentNumber += ".";
                    ResultsTextBlock.Text = result;
                } else
                {
                    result += "." + deg;
                    currentNumber += "." + deg;
                    ResultsTextBlock.Text = result;
                }
            } else
            {
                result += ".";
                currentNumber += ".";
                ResultsTextBlock.Text = result;
            }
        }
    }

    private void Button_Cosinus(object sender, RoutedEventArgs e)
    {
        if (result.EndsWith(".") || result == "Error")
        {
        } else if (result == "0")
        {
            result = cos;
            currentNumber = cos;
            ResultsTextBlock.Text = result;
        } else
        {
            if (result.EndsWith(add) || result.EndsWith(sub) || result.EndsWith(mul) || result.EndsWith(div))
            {
                result += cos;
                currentNumber += cos;
                ResultsTextBlock.Text = result;
            }
        }
    }

    private void Button_Sinus(object sender, RoutedEventArgs e)
    {
        if (result.EndsWith(".") || result == "Error")
        {
        } else if (result == "0")
        {
            result = sin;
            currentNumber = sin;
            ResultsTextBlock.Text = result;
        } else
        {
            if (result.EndsWith(add) || result.EndsWith(sub) || result.EndsWith(mul) || result.EndsWith(div))
            {
                result += sin;
                currentNumber += sin;
                ResultsTextBlock.Text = result;
            }
        }
    }

    private void Button_Tangens(object sender, RoutedEventArgs e)
    {
        if (result.EndsWith(".") || result == "Error")
        {
        } else if (result == "0")
        {
            result = tan;
            currentNumber = tan;
            ResultsTextBlock.Text = result;
        } else
        {
            if (result.EndsWith(add) || result.EndsWith(sub) || result.EndsWith(mul) || result.EndsWith(div))
            {
                result += tan;
                currentNumber += tan;
                ResultsTextBlock.Text = result;
            }
        }
    }

    private void Button_Pi(object sender, RoutedEventArgs e)
    {
        if (result == "." || result == "Error")
        {
        } else if (result == "0")
        {
            result = Math.PI.ToString();
            currentNumber = Math.PI.ToString();
            ResultsTextBlock.Text = result;
        } else if (result.EndsWith(add) || result.EndsWith(sub) || result.EndsWith(mul) || result.EndsWith(div))
        {
            result += Math.PI.ToString(); ;
            currentNumber = Math.PI.ToString();
            ResultsTextBlock.Text = result;
        } else
        {
            result += "+" + Math.PI.ToString(); ;
            currentNumber = Math.PI.ToString();
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_Root(object sender, RoutedEventArgs e)
    {
        if (result == "." || result == "Error")
        {
        } else if (result == "0")
        {
            result = "";
            result += root + par_l;
            currentNumber += root + par_l;
            ResultsTextBlock.Text = result;
        } else if (result.EndsWith(add) || result.EndsWith(sub) || result.EndsWith(mul) || result.EndsWith(div))
        {
            result += root + par_l;
            currentNumber += root + par_l;
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_Exponent(object sender, RoutedEventArgs e)
    {
        if (result.EndsWith(exp) || result == "." || result == "Error")
        {
        } else 
        {
            if (result.EndsWith(add) || result.EndsWith(sub) || result.EndsWith(mul) || result.EndsWith(div))
            {
                newResult = result.Remove(result.Length - 1);
                result = newResult;
            }
            result += "^";
            currentNumber += "^";
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_ParentheseLeft(object sender, RoutedEventArgs e)
    {
        if (result == "." || result == "Error")
        {
        } else if (result == "0")
        {
            result = "";
            result += par_l;
            currentNumber += par_l;
            ResultsTextBlock.Text = result;
        } else
        {
            result += par_l;
            currentNumber += par_l;
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_ParentheseRight(object sender, RoutedEventArgs e)
    {
        if (result == "." || result == "Error")
        {
        }
        else if (result == "0")
        {
            result = "";
            result += par_r;
            currentNumber += par_r;
            ResultsTextBlock.Text = result;
        } else
        {
            result += par_r;
            currentNumber += par_r;
            ResultsTextBlock.Text = result;
        }
    }

    private void Button_Equals(object sender, RoutedEventArgs e)
    {
        if (result.EndsWith(add) || result.EndsWith(sub) || result.EndsWith(mul) || result.EndsWith(div))
        {
            newResult = result.Remove(result.Length - 1);
            result = newResult;
        }

        if (currentNumber.Contains(par_l) && !currentNumber.EndsWith(par_r))
        {
            currentNumber += par_r;
            result += par_r;
        }

        if (result.EndsWith(exp))
        {
            newResult = result.Remove(result.Length - 1);
            result = newResult;
            ResultsTextBlock.Text = result;
        }

        CalculationTextBlock.Text = result + "=";

        if (result.Contains(exp) && (!result.EndsWith(add) || !result.EndsWith(sub) || !result.EndsWith(mul) || !result.EndsWith(div)))
        {
            newResult = "0+" + result;
            result = newResult;
        }

        if (result.Contains(root))
        {
            if (result.Contains(add) || result.Contains(sub) || result.Contains(mul) || result.Contains(div))
            {
                newResult = result.Replace(root, "sqrt");
                result = newResult;
            } else
            {
                newResult = "0+";
                newResult = result.Replace(root, "sqrt");
                result = "0+" + newResult;
            }
        }

        if (result.Contains(cos) || result.Contains(sin) || result.Contains(tan))
        {
            if (result.StartsWith(cos) || result.StartsWith(sin) || result.StartsWith(tan))
            {
                newResult = result.Replace(deg, "*" + Math.PI.ToString() + "/" + "180");
                result = "0+" + newResult;
            } else
            {
                newResult = result.Replace(deg, "*" + Math.PI.ToString() + "/" + "180");
                result = newResult;
            }
        }

        if (result == "0" || result == "Error" || result.EndsWith(add) || result.EndsWith(sub) || result.EndsWith(mul) || result.EndsWith(div))
        {
        } else if (result.Contains(add) || result.Contains(sub) || result.Contains(mul) || result.Contains(div))
        {
            calculation = result + "=";

            try
            {
                if (result.Contains(","))
                {
                    string removeCommas = result.Replace(",", "");
                    result = removeCommas;
                }

                if (result.EndsWith("."))
                {
                    newResult = result.Remove(result.Length - 1);
                }

                if (result.Contains(div))
                {
                    string replaceDiv = result.Replace(div, "/");
                    result = replaceDiv;
                }

                if (result.Contains(mul))
                {
                    string replaceMul = result.Replace(mul, "*");
                    result = replaceMul;
                }

                equalsResult = result.EvalNumerical();
                result = equalsResult.ToString();

                if (result.Contains("/"))
                {
                    string[] get2Numbers = result.Split("/");
                    string stringNum1 = get2Numbers[0];
                    string stringNum2 = get2Numbers[1];

                    float num1 = float.Parse(stringNum1);
                    float num2 = float.Parse(stringNum2);

                    float fractionToDecimal = num1 / num2;
                    result = fractionToDecimal.ToString();
                }

                if (result.Length > 12 && result.Contains("."))
                {
                    newResult = result.Remove(12, result.Length - 12);
                    result = newResult;
                }

                if (result.Length > 4 && !result.Contains("."))
                {
                    float resultToFloat = float.Parse(result);
                    int resultToInt = int.Parse(result);

                    if (resultToInt == resultToFloat)
                    {
                        result = $"{resultToFloat:n0}";
                    } else
                    {
                        result = $"{resultToFloat:n}";
                    }
                }

                currentNumber = result;

                if (currentNumber.Contains(","))
                {
                    string removeCommas = result.Replace(",", "");
                    currentNumber = removeCommas;
                }
                ResultsTextBlock.Text = result.ToString();

            } catch (Exception ex) {
                result = "Error";
                ResultsTextBlock.Text = result;
                CalculationTextBlock.Text = ex.Message;
            }
        }
    }
}
