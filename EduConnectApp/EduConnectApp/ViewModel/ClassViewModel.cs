using EduConnectApp.Commands;
using EduConnectApp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Input;
using EduConnectApp.ViewUCs;
using System.Windows.Media;

namespace EduConnectApp.ViewModel
{
    public class ClassViewModel : BaseViewModel
    {
        public struct AvailableClass
        {
            public int Grade { get; set; }
            public string Class { get; set; }
            public int  NumofAttendants { get; set; }
            public string Teacher { get; set; }
        }

        public ICommand navClassListUC { get; }
        public ICommand navCchangeColorlassListUC { get; }

        private List<AvailableClass> _HomeroomList = new List<AvailableClass>();
        public List<AvailableClass> HomeroomList { get => _HomeroomList; set { _HomeroomList = value; OnPropertyChanged(); } }

        private List<AvailableClass> _ClassList = new List<AvailableClass>();
        public List<AvailableClass> ClassList { get => _ClassList; set { _ClassList = value; OnPropertyChanged(); } }

        public ClassViewModel(NavigationStore navigationStore)
        {
            navClassListUC = new NavigationCommand<ClassListViewModel>(navigationStore, () => new ClassListViewModel(navigationStore));


            HomeroomList.Add(new AvailableClass { Grade = 10, Class = "10A1", NumofAttendants = 45, Teacher="Nguyễn Văn A" });


            ClassList.Add(new AvailableClass { Grade = 10, Class = "10A2", NumofAttendants = 45, Teacher = "Nguyễn Văn B" });
            ClassList.Add(new AvailableClass { Grade = 11, Class = "11A2", NumofAttendants = 45, Teacher = "Nguyễn Văn C" });
            ClassList.Add(new AvailableClass { Grade = 11, Class = "11A3", NumofAttendants = 45, Teacher = "Nguyễn Văn D" });
            ClassList.Add(new AvailableClass { Grade = 12, Class = "12A2", NumofAttendants = 45, Teacher = "Nguyễn Văn E" });

        }
    }
}
