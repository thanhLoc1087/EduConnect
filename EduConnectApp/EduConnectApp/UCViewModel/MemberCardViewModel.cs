using EduConnectApp.UserControlCustom;
using EduConnectApp.ViewModel;
using EduConnectApp.ViewUCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EduConnectApp.UCViewModel
{
    public class MemberCardViewModel: BaseViewModel
    {
        public struct Teacher
        {
            public string Name { get; set; }
            public string Role { get; set; }
            public string Department { get; set; }
            public string PhoneNum { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string Ava { get; set; }
        }

        public ICommand navClassListUC { get; }
        public ICommand navClassList { get; }
        public ICommand navMouse { get; }


        //public string MemberCardType { get; set; }

        //private List<Student> _StudentList = new List<Student>();
        //public List<Student> StudentList { get => _StudentList; set { _StudentList = value; OnPropertyChanged(); } }

        public MemberCardViewModel()
        {

        }
    }
}
