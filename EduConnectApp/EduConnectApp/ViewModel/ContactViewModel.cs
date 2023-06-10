using EduConnectApp.Store;
using EduConnectApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Data.Entity;
using System.Collections.ObjectModel;
using EduConnectApp.UserControlCustom;
using EduConnectApp.UCViewModel;

namespace EduConnectApp.ViewModel
{
    public class ContactViewModel : BaseViewModel
    {

        public struct Teacher
        {
            public int Number { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
            public string Department { get; set; }
            public string PhoneNum { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }

        }
        public ICommand navClassListUC { get; }
        public ICommand navClassList { get; }
        public ICommand navMouse { get; }


        private List<Teacher> _BoardofDirector = new List<Teacher>();
        public List<Teacher> BoardofDirector { get => _BoardofDirector; set { _BoardofDirector = value; OnPropertyChanged(); } }

        private List<Teacher> _BoardofDirector1 = new List<Teacher>();
        public List<Teacher> BoardofDirector1 { get => _BoardofDirector1; set { _BoardofDirector1 = value; OnPropertyChanged(); } }

        private List<Teacher> _BoardofDirector2 = new List<Teacher>();
        public List<Teacher> BoardofDirector2 { get => _BoardofDirector2; set { _BoardofDirector2 = value; OnPropertyChanged(); } }

        private List<Teacher> _BoardofDirector3 = new List<Teacher>();
        public List<Teacher> BoardofDirector3 { get => _BoardofDirector3; set { _BoardofDirector3 = value; OnPropertyChanged(); } }

        private List<Teacher> _MyTeacherList = new List<Teacher>();
        public List<Teacher> MyTeacherList { get => _MyTeacherList; set { _MyTeacherList = value; OnPropertyChanged(); } }

        private List<Teacher> _MyTeacherList1 = new List<Teacher>();
        public List<Teacher> MyTeacherList1 { get => _MyTeacherList1; set { _MyTeacherList1 = value; OnPropertyChanged(); } }

        private List<Teacher> _MyTeacherList2 = new List<Teacher>();
        public List<Teacher> MyTeacherList2 { get => _MyTeacherList2; set { _MyTeacherList2 = value; OnPropertyChanged(); } }

        private List<Teacher> _MyTeacherList3 = new List<Teacher>();
        public List<Teacher> MyTeacherList3 { get => _MyTeacherList3; set { _MyTeacherList3 = value; OnPropertyChanged(); } }

        private ObservableCollection<GIAOVIEN> _TeacherList;
        public ObservableCollection<GIAOVIEN> TeacherList { get => _TeacherList; set { _TeacherList = value; OnPropertyChanged(); } }

        private ObservableCollection<TO1> _DepartmentList;
        public ObservableCollection<TO1> DepartmentList { get => _DepartmentList; set { _DepartmentList = value; OnPropertyChanged(); } }

        public MemberCardViewModel.Teacher CurrentTeacher { get; set; }

        public ContactViewModel(NavigationStore navigationStore)
        {
            TeacherList = new ObservableCollection<GIAOVIEN>(DataProvider.Ins.DB.GIAOVIENs.Where(x => x.DELETED == false));
            DepartmentList = new ObservableCollection<TO1>(DataProvider.Ins.DB.TO1.Where(x => x.DELETED == false));

            int num1 = 0;
            int num2 = 0;

            foreach (GIAOVIEN gv in TeacherList)
            {
                Teacher teacher = new Teacher();
                teacher.Name = gv.HOTEN;
                teacher.Address = gv.DIACHI;
                teacher.PhoneNum = gv.SDT;
                teacher.Email = gv.EMAIL;
                foreach(TO1 to in DepartmentList.Where(x => x.MATO == gv.MATO))
                {
                    teacher.Department = to.TENTO;
                    if (to.TOTRUONG == gv.MAGV)
                    {
                        num1++;
                        teacher.Number = num1;
                        teacher.Role = "Tổ trưởng";
                        BoardofDirector.Add(teacher);
                    }
                    else
                    {
                        num2++;
                        teacher.Number = num2;
                        teacher.Role = "Giáo viên";
                        MyTeacherList.Add(teacher);
                    }
                }
            }

            foreach(Teacher teacher in BoardofDirector) {
                if(teacher.Number % 3 == 1)
                    BoardofDirector1.Add(teacher);
                else if(teacher.Number %3 ==2)
                    BoardofDirector2.Add(teacher);
                else
                    BoardofDirector3.Add(teacher);
            }

            foreach (Teacher teacher in MyTeacherList)
            {
                if (teacher.Number % 3 == 1)
                    MyTeacherList1.Add(teacher);
                else if(teacher.Number %3 ==2)
                    MyTeacherList2.Add(teacher);
                else
                    MyTeacherList3.Add(teacher);
            }

        }
    }
}
