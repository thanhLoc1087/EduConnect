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
using EduConnectApp.Model;
using System.Collections.ObjectModel;
using EduConnectApp.UserControlCustom;
using System.Windows;

namespace EduConnectApp.ViewModel
{
    public class ClassViewModel : BaseViewModel
    {
        public struct AvailableClass
        {
            public int ClassID { get; set; }
            public string Grade { get; set; }
            public string Class { get; set; }
            public int NumofAttendants { get; set; }
            public string Teacher { get; set; }
        }
        public static AvailableClass ClassSelected { get; set; }

        public ICommand navClassListUC { get; }
        public ICommand Detail { get; }
        public ICommand navCchangeColorlassListUC { get; }

        private List<AvailableClass> _HomeroomList = new List<AvailableClass>();
        public List<AvailableClass> HomeroomList { get => _HomeroomList; set { _HomeroomList = value; OnPropertyChanged(); } }
        private List<AvailableClass> _TeachingList = new List<AvailableClass>();
        public List<AvailableClass> TeachingList { get => _TeachingList; set { _TeachingList = value; OnPropertyChanged(); } }

        private ObservableCollection<LOP> _ClassList;
        public ObservableCollection<LOP> ClassList { get => _ClassList; set { _ClassList = value; OnPropertyChanged(); } }
        private ObservableCollection<GIAOVIEN> _Teacher;
        public ObservableCollection<GIAOVIEN> Teacher { get => _Teacher; set { _Teacher = value; OnPropertyChanged(); } }
        private ObservableCollection<GIANGDAY> _Teaching;
        public ObservableCollection<GIANGDAY> Teaching { get => _Teaching; set { _Teaching = value; OnPropertyChanged(); } }

        private string _schoolYear;
        public string schoolYear { get => _schoolYear; set { _schoolYear = value; OnPropertyChanged(); } }
        private string _email;
        public string email { get => _email; set { _email = value; OnPropertyChanged(); } }
        private string _address;
        public string address { get => _address; set { _address = value; OnPropertyChanged(); } }
        private string _school;
        public string school { get => _school; set { _school = value; OnPropertyChanged(); } }
        private string _group;
        public string group { get => _group; set { _group = value; OnPropertyChanged(); } }
        private string _subject1;
        public string subject1 { get => _subject1; set { _subject1 = value; OnPropertyChanged(); } }
        private string _subject2;
        public string subject2 { get => _subject2; set { _subject2 = value; OnPropertyChanged(); } }
        private string _subject3;
        public string subject3 { get => _subject3; set { _subject3 = value; OnPropertyChanged(); } }
        private string _vis1;
        public string vis1 { get => _vis1; set { _vis1 = value; OnPropertyChanged(); } }
        private string _vis2;
        public string vis2 { get => _vis2; set { _vis2 = value; OnPropertyChanged(); } }
        private string _vis3;
        public string vis3 { get => _vis3; set { _vis3 = value; OnPropertyChanged(); } }

        public static AvailableClass CurrentSelected { get; set; }



        public ClassViewModel(NavigationStore navigationStore)
        {
            schoolYear = "NIÊN KHÓA " + Const.SchoolYear;
            if (!Const.IsAdmin)
            {

                AvailableClass availableClass = new AvailableClass();
                //LoginWindow loginWindow = new LoginWindow();

                //var loginVM = loginWindow.DataContext as LoginViewModel;

                Detail = new RelayCommand<DataGrid>((p) => { return p.SelectedItem == null ? false : true; }, (p) => _Detail(p));

                //navigate
                navClassListUC = new NavigationCommand<ClassListViewModel>(navigationStore, () => new ClassListViewModel(navigationStore));

                //ListDTB
                ClassList = new ObservableCollection<LOP>(DataProvider.Ins.DB.LOPs.Where(x => x.DELETED == false));
                Teacher = new ObservableCollection<GIAOVIEN>(DataProvider.Ins.DB.GIAOVIENs.Where(x => x.DELETED == false));
                Teaching = new ObservableCollection<GIANGDAY>(DataProvider.Ins.DB.GIANGDAYs.Where(x => x.DELETED == false));

                //Infor
                var temp = DataProvider.Ins.DB.GIAOVIENs.Where(x => x.MAGV == Const.KeyID && x.DELETED == false).FirstOrDefault();
                email =  temp.EMAIL;
                address = temp.DIACHI;
                school = Const.School;
                var temp2 = DataProvider.Ins.DB.TO1.Where(x => x.MATO == temp.MATO && x.DELETED == false).FirstOrDefault();
                group = temp2.TENTO;

                //Lop chu nhiem
                foreach (LOP lop in ClassList)
                {
                    if (lop.GVCN == Const.KeyID && lop.DELETED ==false)
                    {
                        availableClass.ClassID = lop.MALOP;
                        availableClass.Grade = " " + lop.TENLOP.Substring(0, 2);
                        availableClass.Class = lop.TENLOP;
                        availableClass.NumofAttendants=(int)lop.SISO;
                        foreach (GIAOVIEN gv in Teacher  )
                        {
                            if (gv.MAGV == lop.GVCN&& gv.DELETED ==false) availableClass.Teacher = gv.HOTEN;
                        }
                    }
                }
                HomeroomList.Add(availableClass);

                //Lop Giang day
                vis1 = "Collapsed";
                vis2 = "Collapsed";
                vis3 = "Collapsed";
                int flag = 0;
                ClassUC classuc = new ClassUC();

                foreach (GIANGDAY gd in Teaching)
                {
                    if (gd.MAGV == Const.KeyID)
                    {
                        foreach (LOP lop in ClassList)
                        {
                            if (lop.MALOP== gd.MALOP)
                            {
                                availableClass.ClassID = lop.MALOP;
                                availableClass.Grade = lop.TENLOP.Substring(0, 2);
                                availableClass.Class = lop.TENLOP;
                                availableClass.NumofAttendants=(int)lop.SISO;
                                foreach (GIAOVIEN gv in Teacher)
                                {
                                    if (gv.MAGV == lop.GVCN && gv.DELETED ==false) availableClass.Teacher = gv.HOTEN;
                                }
                                TeachingList.Add(availableClass);
                            }
                        }

                        var temp3 = DataProvider.Ins.DB.MONHOCs.Where(x => x.MAMH == gd.MAMH && x.DELETED == false).FirstOrDefault();
                        if (flag== 0) { subject1 = temp3.TENMH; flag=1; vis1 = "Visible";  }
                        if (flag== 1 && subject1 != temp3.TENMH) { subject2= temp3.TENMH;flag =2; vis2 = "Visible"; }
                        if (flag== 2 && subject1 != temp3.TENMH && subject2 != temp3.TENMH) { subject3= temp3.TENMH; vis1 = "Visible"; }
                    }
                }
            }
        }

        void _Detail (DataGrid p )
        {
            CurrentSelected =(AvailableClass)p.SelectedItem;

        }
    }
}
