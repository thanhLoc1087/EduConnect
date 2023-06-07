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
    public class StatisticViewModel : BaseViewModel
    {
        public struct Student
        {
            public int Grade { get; set; }
            public string Class { get; set; }
            public string Name { get; set; }
            public string Arrange { get; set; }
            public double Score { get; set; }
        }


        private List<Student> _StudentList = new List<Student>();
        public List<Student> StudentList { get => _StudentList; set { _StudentList = value; OnPropertyChanged(); } }

        public StatisticViewModel(NavigationStore navigationStore)
        {

            StudentList.Add(new Student { Grade = 10, Class = "10A2", Name = "Nguyễn Văn A", Arrange= "Giỏi", Score= 8.6});
            StudentList.Add(new Student { Grade = 10, Class = "10A2", Name = "Nguyễn Văn B", Arrange = "Giỏi", Score = 8.6 });
            StudentList.Add(new Student { Grade = 10, Class = "10A2", Name = "Nguyễn Văn C", Arrange = "Giỏi", Score = 8.6 });
            StudentList.Add(new Student { Grade = 10, Class = "10A2", Name = "Nguyễn Văn D", Arrange = "Giỏi", Score = 8.6 });
            StudentList.Add(new Student { Grade = 10, Class = "10A2", Name = "Nguyễn Văn E", Arrange = "Giỏi", Score = 8.6 });


        }
    }
}

