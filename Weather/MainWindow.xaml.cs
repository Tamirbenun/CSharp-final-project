using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Globalization;

namespace Weather;

public partial class MainWindow : Window
{
    private HttpClient client = new HttpClient();

    private DispatcherTimer timer;
    private DateTime TimeUniversalUTC = DateTime.UtcNow;
    private double CurrentUTCSecond;

    private string Unit = "";
    private string Search = "Afula";
    private string CityDisplay = "Afula";
    private string latitude = "32.60907";
    private string longitude = "35.2892";

    public MainWindow()
    {
        InitializeComponent();
        DisplayDataAsync();
    }

    private async void DisplayDataAsync()
    {
        //Default Location On Start
        LoactionCityText.Text = CityDisplay;

        //Call API
        WeatherResponse weatherResponse = await GetWeatherAsync();

        //Display Weather Image Now
        int isDayNow1 = weatherResponse.WeatherNow.isDayNow;
        double isRainNow = weatherResponse.WeatherNow.RainNow;
        int isCloudyNow = weatherResponse.WeatherNow.CloudCoverNow;
        WeatherDisplay(isDayNow1, isRainNow, isCloudyNow);

        //Display Temperture Now
        double TempNowRounded = Math.Round(weatherResponse.WeatherNow.TempNow);
        TempNowRun.Text = TempNowRounded.ToString();

        //Display Day and Clock now
        DateTime dateNow = weatherResponse.WeatherNow.TimeNow;
        DayOfWeek dayNow = dateNow.DayOfWeek;
        DayNowRun.Text = dayNow.ToString();
        Clock();

        //Display LAst Update
        DateTime TimeLastUpdate = weatherResponse.WeatherNow.TimeNow;
        LastUpdateDisplay(TimeLastUpdate);

        //Next 7 Days Temperture
        CultureInfo dayInEn = new CultureInfo("en-US");
        DateTime day1 = weatherResponse.DailyWeather.Day[0];
        Day1.Text = day1.ToString("dddd", dayInEn);
        Day1TempMax.Text = Math.Round(weatherResponse.DailyWeather.TempDayMax[0]).ToString();
        Day1TempMin.Text = Math.Round(weatherResponse.DailyWeather.TempMinDay[0]).ToString();
        double Day1Rain = weatherResponse.DailyWeather.RainSum[0];
        int Day1Cloud = weatherResponse.HourlyWeather.Cloud.Take(24).Sum();
        int Day1CloudAverage = Day1Cloud / 24;
        WeatherWeekDisplay(Day1Rain, Day1CloudAverage, 1);

        DateTime day2 = weatherResponse.DailyWeather.Day[1];
        Day2.Text = day2.ToString("dddd", dayInEn);
        Day2TempMax.Text = Math.Round(weatherResponse.DailyWeather.TempDayMax[1]).ToString();
        Day2TempMin.Text = Math.Round(weatherResponse.DailyWeather.TempMinDay[1]).ToString();
        double Day2Rain = weatherResponse.DailyWeather.RainSum[0];
        int Day2Cloud = weatherResponse.HourlyWeather.Cloud.Skip(24).Take(24).Sum();
        int Day2CloudAverage = Day2Cloud / 24;
        WeatherWeekDisplay(Day1Rain, Day1CloudAverage, 2);

        DateTime day3 = weatherResponse.DailyWeather.Day[2];
        Day3.Text = day3.ToString("dddd", dayInEn);
        Day3TempMax.Text = Math.Round(weatherResponse.DailyWeather.TempDayMax[2]).ToString();
        Day3TempMin.Text = Math.Round(weatherResponse.DailyWeather.TempMinDay[2]).ToString();
        double Day3Rain = weatherResponse.DailyWeather.RainSum[0];
        int Day3Cloud = weatherResponse.HourlyWeather.Cloud.Skip(48).Take(24).Sum();
        int Day3CloudAverage = Day3Cloud / 24;
        WeatherWeekDisplay(Day1Rain, Day1CloudAverage, 3);

        DateTime day4 = weatherResponse.DailyWeather.Day[3];
        Day4.Text = day4.ToString("dddd", dayInEn);
        Day4TempMax.Text = Math.Round(weatherResponse.DailyWeather.TempDayMax[3]).ToString();
        Day4TempMin.Text = Math.Round(weatherResponse.DailyWeather.TempMinDay[3]).ToString();
        double Day4Rain = weatherResponse.DailyWeather.RainSum[0];
        int Day4Cloud = weatherResponse.HourlyWeather.Cloud.Skip(72).Take(24).Sum();
        int Day4CloudAverage = Day4Cloud / 24;
        WeatherWeekDisplay(Day1Rain, Day1CloudAverage, 4);

        DateTime day5 = weatherResponse.DailyWeather.Day[4];
        Day5.Text = day5.ToString("dddd", dayInEn);
        Day5TempMax.Text = Math.Round(weatherResponse.DailyWeather.TempDayMax[4]).ToString();
        Day5TempMin.Text = Math.Round(weatherResponse.DailyWeather.TempMinDay[4]).ToString();
        double Day5Rain = weatherResponse.DailyWeather.RainSum[0];
        int Day5Cloud = weatherResponse.HourlyWeather.Cloud.Skip(96).Take(24).Sum();
        int Day5CloudAverage = Day5Cloud / 24;
        WeatherWeekDisplay(Day1Rain, Day1CloudAverage, 5);

        DateTime day6 = weatherResponse.DailyWeather.Day[5];
        Day6.Text = day6.ToString("dddd", dayInEn);
        Day6TempMax.Text = Math.Round(weatherResponse.DailyWeather.TempDayMax[5]).ToString();
        Day6TempMin.Text = Math.Round(weatherResponse.DailyWeather.TempMinDay[5]).ToString();
        double Day6Rain = weatherResponse.DailyWeather.RainSum[0];
        int Day6Cloud = weatherResponse.HourlyWeather.Cloud.Skip(120).Take(24).Sum();
        int Day6CloudAverage = Day6Cloud / 24;
        WeatherWeekDisplay(Day1Rain, Day1CloudAverage, 6);

        DateTime day7 = weatherResponse.DailyWeather.Day[6];
        Day7.Text = day7.ToString("dddd", dayInEn);
        Day7TempMax.Text = Math.Round(weatherResponse.DailyWeather.TempDayMax[6]).ToString();
        Day7TempMin.Text = Math.Round(weatherResponse.DailyWeather.TempMinDay[6]).ToString();
        double Day7Rain = weatherResponse.DailyWeather.RainSum[0];
        int Day7Cloud = weatherResponse.HourlyWeather.Cloud.Skip(144).Take(24).Sum();
        int Day7CloudAverage = Day7Cloud / 24;
        WeatherWeekDisplay(Day1Rain, Day1CloudAverage, 7);

        //UV Index Box
        int CurrentTimeUV = Convert.ToInt32(TimeLastUpdate.Hour.ToString());
        UV_IndexLevel(weatherResponse.HourlyWeather.UVindex[CurrentTimeUV]);
        UV_IndexText.Text = weatherResponse.HourlyWeather.UVindex[CurrentTimeUV].ToString();
        UV_IndexImage(weatherResponse.HourlyWeather.UVindex[CurrentTimeUV]);

        //Wind Box
        CompassSpeed.Text = weatherResponse.WeatherNow.WindSpeedNow.ToString();
        RotateTransform rotateTransform = ((RotateTransform)((TransformGroup)CompassArrow.RenderTransform).Children.First(tr => tr is RotateTransform));
        rotateTransform.Angle = weatherResponse.WeatherNow.WindDirectionNow;
        WindDirectionNumber.Text = weatherResponse.WeatherNow.WindDirectionNow.ToString();
        WindDirectionLetter.Text = WindDirectionToLetter(weatherResponse.WeatherNow.WindDirectionNow);

        //sunriseTime Box
        DateTime sunriseTime = weatherResponse.DailyWeather.SunriseDay[0];
        SunriseTimeDisplay(sunriseTime);
        DateTime sunsetTime = weatherResponse.DailyWeather.SunsetDay[0];
        SunsetTimeDisplay(sunsetTime);

        //Humidity Box
        int CurrentTimeHumidity = Convert.ToInt32(TimeLastUpdate.Hour.ToString());
        HumidityImage(weatherResponse.HourlyWeather.Humidity[CurrentTimeHumidity]);
        HumidityText.Text = weatherResponse.HourlyWeather.Humidity[CurrentTimeHumidity].ToString();

        //Pressure Box
        int pressure = Convert.ToInt32(weatherResponse.WeatherNow.Pressure);
        PressureText.Text = pressure.ToString();
        PressureImageDisplay(pressure);

        //Visibility Box
        int CurrentTimeVisibility = Convert.ToInt32(TimeLastUpdate.Hour.ToString());
        double MeterToKilometer = weatherResponse.HourlyWeather.Visibility[CurrentTimeVisibility] / 1000;
        VisibilityNumber.Text = MeterToKilometer.ToString();
        VisibilityText.Text = VisibilityLevel(weatherResponse.HourlyWeather.Visibility[CurrentTimeVisibility]);

        //Call to API evry 15 min to update Data
        CurrentUTCSecond = weatherResponse.UTC;
        CallAgain(CurrentUTCSecond);
    }

    //Call API Evry Key Down in TextBox and display results
    private async void TextBoxChange(object sender, RoutedEventArgs e)
    {
        ListBoxResults.ItemsSource = "";
        Search = TextBoxSearch.Text.ToLower();
        LocationResponse locationResponse = await GetlocationDataAsync();

        if (TextBoxSearch.Text == "")
        {
            ListBoxResults.ItemsSource = "";
            TextBoxSearch.Background = null;
        } else if (locationResponse.Results == null && TextBoxSearch.Text != "")
        {
            ListBoxResults.ItemsSource = "";
            TextBoxSearch.Background = Brushes.White;

        } else if (locationResponse.Results != null)
        {
            ListBoxResults.ItemsSource = locationResponse.Results;
            TextBoxSearch.Background = Brushes.White;
        }
    }

    //Take the selection result and call to the main Function with new lat & lon
    private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = ListBoxResults.SelectedItem;

        if (selectedItem != null)
        {
            var city = (selectedItem as Results).City;
            var latitude1 = (selectedItem as Results).Latitude;
            var longitude2 = (selectedItem as Results).Longitude;

            latitude = latitude1.ToString();
            longitude = longitude2.ToString();
            CityDisplay = city;
            DisplayDataAsync();
            TextBoxSearch.Text = "";
        }
    }

    //Display Weather Image Now
    private void WeatherDisplay(int isDay, double rain, int cloud)
    {
        if (rain == 0 && cloud <= 30 )
        {
            if (isDay == 1)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Sunny.png", UriKind.RelativeOrAbsolute));
                MainWeatherImage.Source = bitmapImage;
            } else if (isDay == 0)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Night-01.png", UriKind.RelativeOrAbsolute));
                MainWeatherImage.Source = bitmapImage;
            }
        } else if (rain == 0 && cloud > 30 && cloud < 90)
        {
            if (isDay == 1)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/SunCloudy.png", UriKind.RelativeOrAbsolute));
                MainWeatherImage.Source = bitmapImage;
            }
            else if (isDay == 0)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Night-02.png", UriKind.RelativeOrAbsolute));
                MainWeatherImage.Source = bitmapImage;
            }
        } else if (rain == 0 && cloud > 90)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Cloud-01.png", UriKind.RelativeOrAbsolute));
            MainWeatherImage.Source = bitmapImage;
        } else if (rain > 0)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Rain.png", UriKind.RelativeOrAbsolute));
            MainWeatherImage.Source = bitmapImage;
        }
    }

    private void Clock()
    {
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += UpdateTimer_Tick;
        timer.Start();
    }

    private void UpdateTimer_Tick(object sender, EventArgs e)
    {
        DateTime utcTime = DateTime.UtcNow;
        double currentUTCHours = CurrentUTCSecond / 3600;
        DateTime CurrentTime = utcTime.AddHours(currentUTCHours);
        TimeNowRun.Text = CurrentTime.ToString("HH:mm");
    }

    private void LastUpdateDisplay(DateTime lastUpdate)
    {
        if (lastUpdate.Hour < 10 && lastUpdate.Minute < 10)
        {
            LastUpdateTextBlock.Text = "0" + lastUpdate.Hour.ToString() + ":" + "0" + lastUpdate.Minute.ToString();
        } else if (lastUpdate.Hour >= 10 && lastUpdate.Minute < 10)
        {
            LastUpdateTextBlock.Text = lastUpdate.Hour.ToString() + ":" + "0" + lastUpdate.Minute.ToString();
        } else if (lastUpdate.Hour >= 10 && lastUpdate.Minute >= 10)
        {
            LastUpdateTextBlock.Text = lastUpdate.Hour.ToString() + ":" + lastUpdate.Minute.ToString();
        }
    }

    private void WeatherWeekDisplay(double rain, int cloud, int day)
    {
        if (rain == 0 && cloud <= 30)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Sunny.png", UriKind.RelativeOrAbsolute));
            if (day == 1)
            {
                Day1WeatherImage.Source = bitmapImage;
            } else if (day == 2)
            {
                Day2WeatherImage.Source = bitmapImage;
            } else if (day == 3)
            {
                Day3WeatherImage.Source = bitmapImage;
            } else if (day == 4)
            {
                Day4WeatherImage.Source = bitmapImage;
            } else if (day == 5)
            {
                Day5WeatherImage.Source = bitmapImage;
            } else if (day == 6)
            {
                Day6WeatherImage.Source = bitmapImage;
            } else if (day == 7)
            {
                Day7WeatherImage.Source = bitmapImage;
            }
        } else if (rain == 0 && cloud > 30 && cloud < 90)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/SunCloudy.png", UriKind.RelativeOrAbsolute));
            if (day == 1)
            {
                Day1WeatherImage.Source = bitmapImage;
            } else if (day == 2)
            {
                Day2WeatherImage.Source = bitmapImage;
            } else if (day == 3)
            {
                Day3WeatherImage.Source = bitmapImage;
            } else if (day == 4)
            {
                Day4WeatherImage.Source = bitmapImage;
            } else if (day == 5)
            {
                Day5WeatherImage.Source = bitmapImage;
            } else if (day == 6)
            {
                Day6WeatherImage.Source = bitmapImage;
            } else if (day == 7)
            {
                Day7WeatherImage.Source = bitmapImage;
            }
        } else if (rain == 0 && cloud > 90)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Cloud-01.png", UriKind.RelativeOrAbsolute));
            if (day == 1)
            {
                Day1WeatherImage.Source = bitmapImage;
            } else if (day == 2)
            {
                Day2WeatherImage.Source = bitmapImage;
            } else if (day == 3)
            {
                Day3WeatherImage.Source = bitmapImage;
            } else if (day == 4)
            {
                Day4WeatherImage.Source = bitmapImage;
            } else if (day == 5)
            {
                Day5WeatherImage.Source = bitmapImage;
            } else if (day == 6)
            {
                Day6WeatherImage.Source = bitmapImage;
            } else if (day == 7)
            {
                Day7WeatherImage.Source = bitmapImage;
            }
        } else if (rain > 0)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Rain.png", UriKind.RelativeOrAbsolute));
            if (day == 1)
            {
                Day1WeatherImage.Source = bitmapImage;
            } else if (day == 2)
            {
                Day2WeatherImage.Source = bitmapImage;
            } else if (day == 3)
            {
                Day3WeatherImage.Source = bitmapImage;
            } else if (day == 4)
            {
                Day4WeatherImage.Source = bitmapImage;
            } else if (day == 5)
            {
                Day5WeatherImage.Source = bitmapImage;
            } else if (day == 6)
            {
                Day6WeatherImage.Source = bitmapImage;
            } else if (day == 7)
            {
                Day7WeatherImage.Source = bitmapImage;
            }
        }
    }

    private void UV_IndexLevel(double UV)
    {
        if (UV <= 2)
        {
            UVIndexLevel.Text = "Low";
        } else if (UV >= 3 && UV < 6) 
        {
            UVIndexLevel.Text = "Moderate";
        } else if (UV >= 6 && UV < 8)
        {
            UVIndexLevel.Text = "High";
        } else if (UV >= 8 && UV < 11)
        {
            UVIndexLevel.Text = "Very High";
        } else if (UV >= 11)
        {
            UVIndexLevel.Text = "Extreme";
        }
    }

    private void UV_IndexImage(double UV)
    {
        if (UV < 1)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-00.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        } else if (UV >= 1 && UV < 2)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-01.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        } else if (UV >= 2 && UV < 3)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-02.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        } else if (UV >= 3 && UV < 4)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-03.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        } else if (UV >= 4 && UV < 5)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-04.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        } else if (UV >= 5 && UV < 6)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-05.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        } else if (UV >= 6 && UV < 7)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-06.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        } else if (UV >= 7 && UV < 8)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-07.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        } else if (UV >= 8 && UV < 9)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-08.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        } else if (UV >= 9 && UV < 10)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-09.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        } else if (UV >= 10 && UV < 11)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-10.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        } else if (UV >= 11 && UV < 12)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-11.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        } else if (UV >= 12)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/UV-12+.png", UriKind.RelativeOrAbsolute));
            UVImageSource.Source = bitmapImage;
        }
    }

    private string WindDirectionToLetter(int direction)
    {
        if (direction > 337 || direction < 23)
        {
            return "N";
        } else if (direction > 22 && direction < 68)
        {
            return "NE";
        } else if (direction > 67 && direction < 113)
        {
            return "E";
        } else if (direction > 112 && direction < 158)
        {
            return "SE";
        } else if (direction > 157 && direction < 203)
        {
            return "S";
        } else if (direction > 202 && direction < 248)
        {
            return "SW";
        } else if (direction > 247 && direction < 293)
        {
            return "W";
        } else if (direction > 292 && direction < 338)
        {
            return "NW";
        }
        return "Error";

    }

    private void SunriseTimeDisplay(DateTime time)
    {
        if (time.Hour < 10)
        {
            if (time.Minute < 10)
            {
                SunriseText.Text = "0" + time.Hour.ToString() + ":" + "0" + time.Minute.ToString();
            } else
            {
                SunriseText.Text = "0" + time.Hour.ToString() + ":" + time.Minute.ToString();
            }

        } else if (time.Minute < 10)
        {
            SunriseText.Text = time.Hour.ToString() + ":" + time.Minute.ToString();
        } else
        {
            SunriseText.Text = time.Hour.ToString() + ":" + time.Minute.ToString();
        }
    }

    private void SunsetTimeDisplay(DateTime time)
    {
        if (time.Hour < 10)
        {
            if (time.Minute < 10)
            {
                SunsetText.Text = "0" + time.Hour.ToString() + ":" + "0" + time.Minute.ToString();
            } else
            {
                SunsetText.Text = "0" + time.Hour.ToString() + ":" + time.Minute.ToString();
            }
        } else if (time.Minute < 10)
        {
            SunsetText.Text = time.Hour.ToString() + ":" + "0" + time.Minute.ToString();
        } else
        {
            SunsetText.Text = time.Hour.ToString() + ":" + time.Minute.ToString();
        }
    }

    private void HumidityImage(double Humidity)
    {
        if (Humidity < 10)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Humidity-00.png", UriKind.RelativeOrAbsolute));
            HumiditySource.Source = bitmapImage;
        } else if (Humidity >= 10 && Humidity < 20)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Humidity-01.png", UriKind.RelativeOrAbsolute));
            HumiditySource.Source = bitmapImage;
        } else if (Humidity >= 20 && Humidity < 30)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Humidity-02.png", UriKind.RelativeOrAbsolute));
            HumiditySource.Source = bitmapImage;
        } else if (Humidity >= 30 && Humidity < 40)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Humidity-03.png", UriKind.RelativeOrAbsolute));
            HumiditySource.Source = bitmapImage;
        } else if (Humidity >= 40 && Humidity < 50)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Humidity-04.png", UriKind.RelativeOrAbsolute));
            HumiditySource.Source = bitmapImage;
        } else if (Humidity >= 50 && Humidity < 60)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Humidity-05.png", UriKind.RelativeOrAbsolute));
            HumiditySource.Source = bitmapImage;
        } else if (Humidity >= 60 && Humidity < 70)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Humidity-06.png", UriKind.RelativeOrAbsolute));
            HumiditySource.Source = bitmapImage;
        } else if (Humidity >= 70 && Humidity < 80)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Humidity-07.png", UriKind.RelativeOrAbsolute));
            HumiditySource.Source = bitmapImage;
        } else if (Humidity >= 80 && Humidity < 90)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Humidity-08.png", UriKind.RelativeOrAbsolute));
            HumiditySource.Source = bitmapImage;
        } else if (Humidity >= 90 && Humidity < 100)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Humidity-09.png", UriKind.RelativeOrAbsolute));
            HumiditySource.Source = bitmapImage;
        } else if (Humidity >=100)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Humidity-10.png", UriKind.RelativeOrAbsolute));
            HumiditySource.Source = bitmapImage;
        }
    }

    private void PressureImageDisplay(int pressure)
    {
        if (pressure < 800)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pressure-00.png", UriKind.RelativeOrAbsolute));
            PressureSource.Source = bitmapImage;
        } else if (pressure >= 800 && pressure < 840)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pressure-01.png", UriKind.RelativeOrAbsolute));
            PressureSource.Source = bitmapImage;
        } else if (pressure >= 840 && pressure < 880)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pressure-02.png", UriKind.RelativeOrAbsolute));
            PressureSource.Source = bitmapImage;
        } else if (pressure >= 880 && pressure < 920)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pressure-03.png", UriKind.RelativeOrAbsolute));
            PressureSource.Source = bitmapImage;
        } else if (pressure >= 920 && pressure < 960)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pressure-04.png", UriKind.RelativeOrAbsolute));
            PressureSource.Source = bitmapImage;
        } else if (pressure >= 960 && pressure < 1000)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pressure-05.png", UriKind.RelativeOrAbsolute));
            PressureSource.Source = bitmapImage;
        } else if (pressure >= 1000 && pressure < 1040)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pressure-06.png", UriKind.RelativeOrAbsolute));
            PressureSource.Source = bitmapImage;
        } else if (pressure >= 1040 && pressure < 1080)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pressure-07.png", UriKind.RelativeOrAbsolute));
            PressureSource.Source = bitmapImage;
        } else if (pressure >= 1080 && pressure < 1120)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pressure-08.png", UriKind.RelativeOrAbsolute));
            PressureSource.Source = bitmapImage;
        } else if (pressure >= 1120 && pressure < 1160)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pressure-09.png", UriKind.RelativeOrAbsolute));
            PressureSource.Source = bitmapImage;
        } else if (pressure >= 1160 && pressure < 1200)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pressure-10.png", UriKind.RelativeOrAbsolute));
            PressureSource.Source = bitmapImage;
        } else if (pressure >= 1200)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Resources/Pressure-11.png", UriKind.RelativeOrAbsolute));
            PressureSource.Source = bitmapImage;
        }
    }

    private string VisibilityLevel(double Visibility)
    {
        if (Visibility <= 50)
        {
            return "Dense Fog";
        } else if (Visibility > 50 && Visibility <= 200)
        {
            return "Thick Fog";
        } else if (Visibility > 200 && Visibility <= 500)
        {
            return "Moderate Fog";
        } else if (Visibility > 500 && Visibility <= 1000)
        {
            return "Light Fog";
        } else if (Visibility > 1000 && Visibility <= 2000)
        {
            return "Very Light Fog";
        } else if (Visibility > 2000 && Visibility <= 2800)
        {
            return "Light Mist";
        } else if (Visibility > 2800 && Visibility <= 10000)
        {
            return "Very Light Mist";
        } else if (Visibility > 10000 && Visibility <= 20000)
        {
            return "Clear Air";
        } else if (Visibility > 20000)
        {
            return "Very Clear Air";
        }
        return "Error";
    }

    //Call API Evry 15 Min
    private void CallAgain(double UTC)
    {
        double UTC_To_Hours = UTC / 3600;
        DateTime CurrentTimeByUTC = TimeUniversalUTC.AddHours(UTC_To_Hours);
        double minute = CurrentTimeByUTC.Minute;

        if (minute < 15)
        {
            double minToCallAgain = 15 - minute;
            double secToCallAgain = minToCallAgain * 60;

            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(secToCallAgain)
            };
            timer.Tick += (sender, e) => {
                timer.Stop();
                DisplayDataAsync();
            };
            timer.Start();

        } else if (minute > 15 && minute < 30)
        {
            double minToCallAgain = 30 - minute;
            double secToCallAgain = minToCallAgain * 60;

            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(secToCallAgain)
            };
            timer.Tick += (sender, e) => {
                timer.Stop();
                DisplayDataAsync();
            };
            timer.Start();

        } else if (minute > 30 && minute < 45)
        {
            double minToCallAgain = 45 - minute;
            double secToCallAgain = minToCallAgain * 60;

            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(secToCallAgain)
            };
            timer.Tick += (sender, e) => {
                timer.Stop();
                DisplayDataAsync();
            };
            timer.Start();

        } else if (minute > 45)
        {
            double minToCallAgain = 60 - minute;
            double secToCallAgain = minToCallAgain * 60;

            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(secToCallAgain)
            };
            timer.Tick += (sender, e) => {
                timer.Stop();
                DisplayDataAsync();
            };
            timer.Start();

        } else if (minute == 15 || minute == 30 || minute == 45)
        {
            double minToSec = minute * 60;
            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(minToSec)
            };
            timer.Tick += (sender, e) => {
                timer.Stop();
                DisplayDataAsync();
            };
            timer.Start();
        }
    }

    private async Task<LocationResponse> GetlocationDataAsync()
    {
        HttpResponseMessage response = await client.GetAsync($"https://geocoding-api.open-meteo.com/v1/search?name={Search}&count=1&language=en&format=json");
        response.EnsureSuccessStatusCode();
        LocationResponse locationdata = await response.Content.ReadFromJsonAsync<LocationResponse>();

        return locationdata;
    }

    private async Task<WeatherResponse> GetWeatherAsync()
    {
        HttpResponseMessage response = await client.GetAsync($"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,is_day,rain,cloud_cover,wind_speed_10m,wind_direction_10m,surface_pressure&hourly=temperature_2m,relative_humidity_2m,rain,snow_depth,cloud_cover,visibility,uv_index,is_day&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,uv_index_max,rain_sum,wind_speed_10m_max{Unit}&timezone=auto");
        response.EnsureSuccessStatusCode();
        WeatherResponse Weatherdata = await response.Content.ReadFromJsonAsync<WeatherResponse>();
        
        return Weatherdata;
    }

    private void Button_Unit_F(object sender, RoutedEventArgs e)
    {
        Unit = "&temperature_unit=fahrenheit";
        DisplayDataAsync();

        ButtonC.Background = Brushes.White;
        ButtonCText.Foreground = Brushes.Black;
        ButtonF.Background = Brushes.Black;
        ButtonFText.Foreground = Brushes.White;
        TempUnitText.Text = "F";
    }

    private void Button_Unit_C(object sender, RoutedEventArgs e)
    {
        Unit = "";
        DisplayDataAsync();

        ButtonC.Background = Brushes.Black;
        ButtonCText.Foreground = Brushes.White;
        ButtonF.Background = Brushes.White;
        ButtonFText.Foreground = Brushes.Black;
        TempUnitText.Text = "C";
    }
}
