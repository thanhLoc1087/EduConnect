using EduConnectApp.Store;
using EduConnectApp.ViewUCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EduConnectApp.ViewModel
{
    public class SemesterScoreViewModel :BaseViewModel
    {
        public ICommand changeDtg { get; }

        public SemesterScoreViewModel(NavigationStore navigationStore) {
            changeDtg = new RelayCommand<SemesterScore>((p) => { return true; }, (p) => _UpdateDtg(p));

        }

        void _UpdateDtg (SemesterScore p)
        {
            if (p.cbb_Semester.SelectedIndex==0)
            {
                p.dtg_Semester.Visibility=Visibility.Hidden;
                p.dtg_Year.Visibility=Visibility.Visible;
            }
            else
            {
                p.dtg_Semester.Visibility=Visibility.Visible;
                p.dtg_Year.Visibility=Visibility.Hidden;
            }
        }
    }
}
