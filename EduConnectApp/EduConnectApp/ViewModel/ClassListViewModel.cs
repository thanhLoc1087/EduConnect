using EduConnectApp.Commands;
using EduConnectApp.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using EduConnectApp.Model;
using EduConnectApp.ViewUCs;
using System.Windows;

namespace EduConnectApp.ViewModel
{

    public class ClassListViewModel :BaseViewModel
    {
        public struct Student
        {
            public int ID { get; set; }
            public int number { get; set; }
            public string Name { get; set; }
            public string DOB { get; set; }
            public string Gender { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }

        }
        public static Student CurrentSelected { get; set; }
        public ICommand navDetail { get; }
        public ICommand Detail { get; }
        public ICommand navMouse { get; }
        public ICommand mouseEnter { get; }
        public ICommand navEditStPro5 { get; }
        public ICommand navClassList { get; }
        public ICommand getDetail { get; }
        public ICommand DeleteCommand { get; }

        private string _schoolYear;
        public string schoolYear { get => _schoolYear; set { _schoolYear = value; OnPropertyChanged(); } }
        private string _className;
        public string className { get => _className; set { _className = value; OnPropertyChanged(); } }

        private string _AmountSt;
        public string AmountSt { get => _AmountSt; set { _AmountSt = value; OnPropertyChanged(); } }

        private string _teacherName;
        public string teacherName { get => _teacherName; set { _teacherName = value; OnPropertyChanged(); } }


        private List<Student> _StudentList = new List<Student>();
        public List<Student> StudentList { get => _StudentList; set { _StudentList = value; OnPropertyChanged(); } }

        private ObservableCollection<HOCTAP> _Learning;
        public ObservableCollection<HOCTAP> Learning { get => _Learning; set { _Learning = value; OnPropertyChanged(); } }
        private string _filterText;
        public string filterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged("filterText");
                OnPropertyChanged("MyFilterList");
            }
        }
        private string _searchText;
        public string searchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("searchText");
                OnPropertyChanged("MyFilterList");
            }
        }
        public IEnumerable<Student> MyFilterList
        {
            get
            {
                if (searchText == null && filterText == null)
                        return StudentList;
                if (searchText == null && filterText != null)
                    return StudentList.Where(x=> x.Gender == filterText);
                if (searchText != null && filterText == null)
                    return StudentList.Where(x => (x.Name.ToUpper().Contains(searchText.ToUpper())));
                return StudentList.Where(x => (x.Name.ToUpper().Contains(searchText.ToUpper())) && x.Gender == filterText);
            }
        }

        public ClassListViewModel(NavigationStore navigationStore) {
            ClassViewModel.AvailableClass classSelected = ClassViewModel.CurrentSelected;

            

            //navigate
            navClassList = new NavigationCommand<ClassListViewModel>(navigationStore, () => new ClassListViewModel(navigationStore));
            navDetail = new NavigationCommand<StudentPro5ViewModel>(navigationStore, () => new StudentPro5ViewModel(navigationStore));
            navEditStPro5 = new NavigationCommand<EditStudentPro5ViewModel>(navigationStore, () => new EditStudentPro5ViewModel(navigationStore));
            //navMouse = new RelayCommand<DataGrid>((p) => { return true; }, (p) => _UpdateSpn(p));
           getDetail = new RelayCommand<ClassListUC>((p) => { return p.dtg_Edit.SelectedItem == null ? false : true; }, (p) => _GetDetail(p));
           Detail = new RelayCommand<DataGrid>((p) => { return p.SelectedItem == null ? false : true; }, (p) => _Detail(p));

            Learning = new ObservableCollection<HOCTAP>(DataProvider.Ins.DB.HOCTAPs.Where(x => x.DELETED == false));

            //student List
            Student st = new Student();
            st.number= 1;
            DateTime dateTime;
            foreach (HOCTAP ht in Learning )
            {
                if (ht.MALOP == classSelected.ClassID)
                {
                    var temp = DataProvider.Ins.DB.HOCSINHs.Where(x => x.MAHS == ht.MAHS && x.DELETED == false).FirstOrDefault();
                    st.ID = temp.MAHS;
                    st.Name = temp.HOTEN;
                    dateTime = (DateTime)temp.NTNS;
                    st.DOB = dateTime.ToString("dd/MM/yyyy");
                    st.Gender = temp.GIOITINH==true ? "Nữ" : "Nam";
                    st.Phone = temp.SDT;
                    st.Address=temp.DIACHI;
                    StudentList.Add(st);
                    st.number++;
                }

            }

            //StudentList.Add(new Student { number = 1, Name = "Nguyễn Bình An", DOB = "5/10/2007", Gender = "Nữ", Phone = "415-954-1475", Address="TP HCM" });
            //StudentList.Add(new Student { number = 2, Name = "Đỗ Duy Dũng", DOB = "5/10/2007", Gender = "Nữ", Phone = "254-451-7893", Address="Đồng Nai" });
            //StudentList.Add(new Student { number = 3, Name = "Dennis Castillo", DOB = "5/10/2007", Gender = "Nữ", Phone = "125-520-0141", Address="TP HCM" });
            //StudentList.Add(new Student { number = 4, Name = "Gabriel Cox", DOB = "17/7/2007", Gender = "Nam", Phone = "808-635-1221", Address="TP HCM" });
            //StudentList.Add(new Student { number = 5, Name = "Lena Jones", DOB = "17/7/2007", Gender = "Nam", Phone = "320-658-9174" , Address = "Tiền Giang" });
            //StudentList.Add(new Student { number = 6, Name = "Benjamin Caliword", DOB = "17/7/2007", Gender = "Nữ", Phone = "114-203-6258", Address="TP HCM" });
            //StudentList.Add(new Student { number = 7, Name = "Sophia Muris", DOB = "19/5/2005", Gender = "Nữ", Phone = "852-233-6854", Address="TP HCM" });
            //StudentList.Add(new Student { number = 8, Name = "Ali Pormand", DOB = "19/5/2005", Gender = "Nữ", Phone = "968-378-4849", Address="TP HCM" });
            //StudentList.Add(new Student { number = 9, Name = "Frank Underwood", DOB = "19/5/2005", Gender = "Nữ", Phone = "301-584-6966" , Address = "TP HCM" });
            //StudentList.Add(new Student { number = 10, Name = "Saeed Dasman", DOB = "19/5/2005", Gender = "Nữ", Phone = "817-320-5052", Address="TP HCM" });

            //infoClass
            schoolYear = Const.SchoolYear;
            var temp2 = DataProvider.Ins.DB.LOPs.Where(x => x.MALOP == classSelected.ClassID && x.DELETED == false).FirstOrDefault();
            var temp3 = DataProvider.Ins.DB.GIAOVIENs.Where(x => x.MAGV == temp2.GVCN && x.DELETED == false).FirstOrDefault();
            teacherName = temp3.HOTEN;
            className = temp2.TENLOP;
            AmountSt = StudentList.Count().ToString() + " học sinh";

            DeleteCommand = new RelayCommand<ClassListUC>((p) => { return p.dtg_Delete.SelectedIndex != -1; }, (p) =>
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa học sinh này khỏi danh sách?", "Xác nhận!", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;

                Student std = MyFilterList.ElementAt(p.dtg_Delete.SelectedIndex);
                var temp1 = DataProvider.Ins.DB.HOCSINHs.Where(x => x.MAHS == std.ID && x.DELETED == false).FirstOrDefault();
                temp1.DELETED = true;
                var tempHT = DataProvider.Ins.DB.HOCTAPs.Where(x => x.MAHS == std.ID && x.MALOP == classSelected.ClassID && x.DELETED == false).FirstOrDefault();
                tempHT.DELETED = true;
                DataProvider.Ins.DB.SaveChanges();
            });
        }

       void _Detail (DataGrid p)
        {
            CurrentSelected =(Student)p.SelectedItem;
        }
       void _GetDetail (ClassListUC p)
        {
            p.dtg_Student.SelectedIndex = p.dtg_Edit.SelectedIndex;
            CurrentSelected =(Student)p.dtg_Student.SelectedItem;

        }
        private void ListViewScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
            }
    }
}
