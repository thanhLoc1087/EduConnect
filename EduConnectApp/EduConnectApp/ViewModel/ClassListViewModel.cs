using EduConnectApp.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EduConnectApp.ViewModel
{
    public class ClassListViewModel :BaseViewModel
    {
        public struct Student
        {
            public int number { get; set; }
            public string Name { get; set; }
            public string DOB { get; set; }
            public string Gender { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }

        }

        private List<Student> _StudentList = new List<Student>();
        public List<Student> StudentList { get => _StudentList; set { _StudentList = value; OnPropertyChanged(); } }


        public ClassListViewModel(NavigationStore navigationStore) {

            StudentList.Add(new Student { number = 1, Name = "Nguyễn Bình An", DOB = "5/10/2007", Gender = "Nữ", Phone = "415-954-1475", Address="TP HCM" });
            StudentList.Add(new Student { number = 2, Name = "Đỗ Duy Dũng", DOB = "5/10/2007", Gender = "Nữ", Phone = "254-451-7893", Address="Đồng Nai" });
            StudentList.Add(new Student { number = 3, Name = "Dennis Castillo", DOB = "5/10/2007", Gender = "Nữ", Phone = "125-520-0141", Address="TP HCM" });
            StudentList.Add(new Student { number = 4, Name = "Gabriel Cox", DOB = "17/7/2007", Gender = "Nam", Phone = "808-635-1221", Address="TP HCM" });
            StudentList.Add(new Student { number = 5, Name = "Lena Jones", DOB = "17/7/2007", Gender = "Nam", Phone = "320-658-9174" , Address = "Tiền Giang" });
            StudentList.Add(new Student { number = 6, Name = "Benjamin Caliword", DOB = "17/7/2007", Gender = "Nữ", Phone = "114-203-6258", Address="TP HCM" });
            StudentList.Add(new Student { number = 7, Name = "Sophia Muris", DOB = "19/5/2005", Gender = "Nữ", Phone = "852-233-6854", Address="TP HCM" });
            StudentList.Add(new Student { number = 8, Name = "Ali Pormand", DOB = "19/5/2005", Gender = "Nữ", Phone = "968-378-4849", Address="TP HCM" });
            StudentList.Add(new Student { number = 9, Name = "Frank Underwood", DOB = "19/5/2005", Gender = "Nữ", Phone = "301-584-6966" , Address = "TP HCM" });
            StudentList.Add(new Student { number = 10, Name = "Saeed Dasman", DOB = "19/5/2005", Gender = "Nữ", Phone = "817-320-5052", Address="TP HCM" });
        }

       
    }
}
