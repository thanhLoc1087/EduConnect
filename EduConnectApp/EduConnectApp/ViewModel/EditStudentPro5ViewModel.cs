using EduConnectApp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnectApp.ViewModel
{
    public class EditStudentPro5ViewModel: BaseViewModel
    {
        private string _name;
        public string name { get => _name; set { _name = value; OnPropertyChanged(); } }

        public EditStudentPro5ViewModel(NavigationStore navigationStore)
        {
            ClassListViewModel.Student studentSelected = ClassListViewModel.CurrentSelected;
            name = studentSelected.Name;
        }
    }
}
