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

namespace EduConnectApp.ViewModel
{
    public class ClassViewModel : BaseViewModel
    {
        public struct AvailableClass
        {
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

        public ClassViewModel(NavigationStore navigationStore)
        {
            AvailableClass availableClass = new AvailableClass();
            LoginWindow loginWindow = new LoginWindow();

            var loginVM = loginWindow.DataContext as LoginViewModel;

            Detail = new RelayCommand<DataGrid>((p) => { return true; }, (p) => _Detail(p));

            navClassListUC = new NavigationCommand<ClassListViewModel>(navigationStore, () => new ClassListViewModel(navigationStore));

            ClassList = new ObservableCollection<LOP>(DataProvider.Ins.DB.LOPs.Where(x => x.DELETED == false));
            Teacher = new ObservableCollection<GIAOVIEN>(DataProvider.Ins.DB.GIAOVIENs.Where(x => x.DELETED == false));
            Teaching = new ObservableCollection<GIANGDAY>(DataProvider.Ins.DB.GIANGDAYs.Where(x => x.DELETED == false));

            //Lop chu nhiem
            foreach (LOP lop in ClassList)
            {
                if (lop.GVCN == loginVM.ID)
                {
                    availableClass.Grade = lop.TENLOP.Substring(0, 1);
                    availableClass.Class = lop.TENLOP;
                    availableClass.NumofAttendants=(int)lop.SISO;
                    foreach (GIAOVIEN gv in Teacher)
                    {
                        if (gv.MAGV == lop.GVCN) availableClass.Teacher = gv.HOTEN;
                    }
                }
            }
            HomeroomList.Add(availableClass);

            //Lop Giang day

            foreach (GIANGDAY gd in Teaching)
            {
                if (gd.MAGV == loginVM.ID)
                {
                    foreach (LOP lop in ClassList)
                    {
                        if (lop.MALOP== gd.MALOP)
                        {
                            availableClass.Grade = lop.TENLOP.Substring(0, 1);
                            availableClass.Class = lop.TENLOP;
                            availableClass.NumofAttendants=(int)lop.SISO;
                            foreach (GIAOVIEN gv in Teacher)
                            {
                                if (gv.MAGV == lop.GVCN) availableClass.Teacher = gv.HOTEN;
                            }
                            TeachingList.Add(availableClass);
                        }
                    }
                }
            }
        }

        void _Detail (DataGrid p )
        {
        }
    }
}
