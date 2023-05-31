using EduConnectApp.Commands;
using EduConnectApp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EduConnectApp.ViewModel
{
    public class GradeViewModel :BaseViewModel
    {
        public ICommand navSemesterScore { get; }

        public GradeViewModel(NavigationStore navigationStore)
        {
            navSemesterScore = new NavigationCommand<SemesterScoreViewModel>(navigationStore, () => new SemesterScoreViewModel(navigationStore));

        }
    }
}
