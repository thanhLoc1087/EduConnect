using EduConnectApp.Commands;
using EduConnectApp.Store;
using EduConnectApp.ViewUCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static EduConnectApp.ViewModel.ClassViewModel;

namespace EduConnectApp.ViewModel
{
    public class InputScoreViewModel : BaseViewModel
    {
        public struct Student
        {
            public int number { get; set; }
            public string name { get; set; }
        }

        public ICommand navBack { get; }
        public ICommand InputCommand { get; }


        private List<Student> _StudentList = new List<Student>();
        public List<Student> StudentList { get => _StudentList; set { _StudentList = value; OnPropertyChanged(); } }
        public InputScoreViewModel(NavigationStore navigationStore)
        {
            //StudentList.Add(new Student { number = 1, name = "Nguyễn Văn A" });
            //StudentList.Add(new Student { number = 2, name = "Nguyễn Văn B" });
            //StudentList.Add(new Student { number = 3, name = "Nguyễn Văn C" });

            //navigation
            navBack = new NavigationCommand<GradeViewModel>(navigationStore, () => new GradeViewModel(navigationStore));

        }
    }
}