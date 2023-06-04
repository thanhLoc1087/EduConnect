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
        public struct AvailableClass
        {
            public int Grade { get; set; }
            public string Class { get; set; }
            public int NumofAttendants { get; set; }
            public string Teacher { get; set; }
        }

        public ICommand navScoreDetail { get; }
        public ICommand navCchangeColorlassListUC { get; }

        private List<AvailableClass> _HomeroomList = new List<AvailableClass>();
        public List<AvailableClass> HomeroomList { get => _HomeroomList; set { _HomeroomList = value; OnPropertyChanged(); } }

        private List<AvailableClass> _ClassList = new List<AvailableClass>();
        public List<AvailableClass> ClassList { get => _ClassList; set { _ClassList = value; OnPropertyChanged(); } }

        public GradeViewModel(NavigationStore navigationStore)
        {
            navScoreDetail = new NavigationCommand<ScoreDetailViewModel>(navigationStore, () => new ScoreDetailViewModel(navigationStore));


            HomeroomList.Add(new AvailableClass { Grade = 10, Class = "10A1", NumofAttendants = 45, Teacher="Nguyễn Văn A" });


            ClassList.Add(new AvailableClass { Grade = 10, Class = "10A2", NumofAttendants = 45, Teacher = "Nguyễn Văn B" });
            ClassList.Add(new AvailableClass { Grade = 11, Class = "11A2", NumofAttendants = 45, Teacher = "Nguyễn Văn C" });
            ClassList.Add(new AvailableClass { Grade = 11, Class = "11A3", NumofAttendants = 45, Teacher = "Nguyễn Văn D" });
            ClassList.Add(new AvailableClass { Grade = 12, Class = "12A2", NumofAttendants = 45, Teacher = "Nguyễn Văn E" });

        }
    }
}
