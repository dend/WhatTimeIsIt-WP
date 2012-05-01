using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Globalization;
using System.Diagnostics;

namespace BerlinTime
{
    public partial class MainPage : PhoneApplicationPage
    {
        public const string UrlTemplate = "http://176.9.156.38/big_{0}-{1}.mp4?start={2}";
        TimeUnit CurrentUnit;

        public MainPage()
        {
            InitializeComponent();
            mediaPlayer.MediaEnded += new RoutedEventHandler(mediaPlayer_MediaEnded);
        }

        void mediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            PlayVideo();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            PlayVideo();
            base.OnNavigatedTo(e);
        }

        private void PlayVideo()
        {
            CurrentUnit = GenerateCurrentTime();

            mediaPlayer.Source = new Uri(string.Format(UrlTemplate, CurrentUnit.Hours, CurrentUnit.Minutes, CurrentUnit.SecondsIn));
            mediaPlayer.Play();
        }

        private TimeUnit GenerateCurrentTime()
        {
            TimeUnit unit = new TimeUnit();

            TimeSpan current = DateTime.Now.TimeOfDay;
            Debug.WriteLine(current.ToString());

            int minutes = current.Minutes;
            int seconds = 0;
            int modulusResult = minutes % 5;

            minutes = minutes - modulusResult - 5;

            seconds = (modulusResult + 5) * 60 + current.Seconds;
            Debug.WriteLine(seconds);

            unit.Hours = current.Hours.ToString("D2");
            unit.Minutes = minutes.ToString("D2");
            unit.SecondsIn = seconds.ToString();

            return unit;
        }
    }

    public class TimeUnit
    {
        public string Hours { get; set; }
        public string Minutes { get; set; }
        public string SecondsIn { get; set; }
    }
}