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
    public class StudentPro5ViewModel: BaseViewModel
    {
        private string _name;
        public string name { get => _name; set { _name = value; OnPropertyChanged(); } }

        public ICommand navBack { get; }
        public ICommand navEdit { get; }

        public StudentPro5ViewModel(NavigationStore navigationStore)
        {
            //navigate
            navBack = new NavigationCommand<ClassListViewModel>(navigationStore, () => new ClassListViewModel(navigationStore));
            navEdit = new NavigationCommand<EditStudentPro5ViewModel>(navigationStore, () => new EditStudentPro5ViewModel(navigationStore));

            //value
            ClassListViewModel.Student studentSelected = ClassListViewModel.CurrentSelected;
            name = studentSelected.Name;
        }
    }
}
