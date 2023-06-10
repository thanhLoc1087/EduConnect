using EduConnectApp.Store;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace EduConnectApp.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private string _hour;
        public string hour { get { return _hour; } set { _hour = value; OnPropertyChanged(); } }
        private string _min;
        public string min { get { return _min; } set { _min = value; OnPropertyChanged(); } }
        private string _sec;
        public string sec { get { return _sec; } set { _sec = value; OnPropertyChanged(); } }
        private string _time;
        public string mer { get { return _time; } set { _time = value; OnPropertyChanged(); } }
        public HomeViewModel(NavigationStore navigationStore)
        {
            InitializeTimer();
        }
        public void InitializeTimer()
        {

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {

            hour = DateTime.Now.ToString("HH");
            min = DateTime.Now.ToString("mm");
            sec = DateTime.Now.ToString("ss");
            mer = DateTime.Now.ToString("tt");
            if (hour.Length == 1) hour = "0"+hour;
            if (min.Length == 1) min = "0"+min;
            if (sec.Length == 1) sec = "0"+sec;

        }
    }
}
