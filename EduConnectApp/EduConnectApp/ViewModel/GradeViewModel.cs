using EduConnectApp.Commands;
using EduConnectApp.Model;
using EduConnectApp.Store;
using EduConnectApp.ViewUCs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using static EduConnectApp.ViewModel.EditScoreViewModel;

namespace EduConnectApp.ViewModel
{
    public class GradeViewModel : BaseViewModel
    {
        public struct AvailableClass
        {
            public int ClassID { get; set; }
            public string Grade { get; set; }
            public string Class { get; set; }
            public int NumofAttendants { get; set; }
            public string Teacher { get; set; }
        }
        public static AvailableClass CurrentSelected { get; set; }


        public ICommand navScoreTable { get; }
        public ICommand navCchangeColorlassListUC { get; }
        public ICommand dtGrid_ScrollChanged { get; }
        public ICommand getDetail { get; }
        public ICommand navInputscore { get; }
        public ICommand Detail { get; }


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
        private string _visLine1;
        public string visLine1 { get => _visLine1; set { _visLine1 = value; OnPropertyChanged(); } }
        private string _visLine2;
        public string visLine2 { get => _visLine2; set { _visLine2 = value; OnPropertyChanged(); } }
        private string _semester;
        public string semester { get => _semester; set { _semester = value; OnPropertyChanged(); } }

        public GradeViewModel(NavigationStore navigationStore)
        {
            semester = "Học kì " + Const.Semester;
            if (semester == "Học kì 1")
            {
                visLine1= "Visible";
                visLine2="Collapsed";
            }
            else
            {
                visLine1= "Collapsed";
                visLine2="Visible";
            }

            schoolYear = "NIÊN KHÓA " + Const.SchoolYear;
            if (!Const.IsAdmin)
            {

                AvailableClass availableClass = new AvailableClass();

                navScoreTable = new NavigationCommand<SemesterScoreViewModel>(navigationStore, () => new SemesterScoreViewModel(navigationStore));
                navInputscore = new NavigationCommand<InputScoreViewModel>(navigationStore, () => new InputScoreViewModel(navigationStore));

                Detail = new RelayCommand<DataGrid>((p) => { return p.SelectedItem == null ? false : true; }, (p) => _Detail(p));
                getDetail = new RelayCommand<GradeUC>((p) => { return p.dtg_Input1.SelectedItem == null|| p.dtg_Input2.SelectedItem==null ? false : true; }, (p) => _GetDetail(p));

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
                group = "Tổ " + temp2.TENTO;

                //Lop chu nhiem
                foreach (LOP lop in ClassList)
                {
                    if (lop.GVCN == Const.KeyID && lop.DELETED ==false)
                    {
                        availableClass.ClassID = lop.MALOP;
                        availableClass.Grade = " " + lop.TENLOP.Substring(0, 2);
                        availableClass.Class = lop.TENLOP;
                        availableClass.NumofAttendants=(int)lop.SISO;
                        foreach (GIAOVIEN gv in Teacher)
                        {
                            if (gv.MAGV == lop.GVCN&& gv.DELETED ==false) availableClass.Teacher = gv.HOTEN;
                        }
                    }
                }
                HomeroomList.Add(availableClass);

                //Lop Giang day
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
                    }

                    //    HomeroomList.Add(new AvailableClass { Grade = 10, Class = "10A1", NumofAttendants = 45, Teacher="Nguyễn Văn A" });

                    //dtGrid_ScrollChanged = new RelayCommand<GradeUC>((p) => { return true; }, (p) => _Scroll(p));


                    //ClassList.Add(new AvailableClass { Grade = 10, Class = "10A2", NumofAttendants = 45, Teacher = "Nguyễn Văn B" });
                    //ClassList.Add(new AvailableClass { Grade = 11, Class = "11A2", NumofAttendants = 45, Teacher = "Nguyễn Văn C" });
                    //ClassList.Add(new AvailableClass { Grade = 11, Class = "11A3", NumofAttendants = 45, Teacher = "Nguyễn Văn D" });
                    //ClassList.Add(new AvailableClass { Grade = 12, Class = "12A2", NumofAttendants = 45, Teacher = "Nguyễn Văn E" });

                }
            }

            void _Detail(DataGrid p)
            {
                CurrentSelected = (AvailableClass)p.SelectedItem;
            }
            void _Scroll(GradeUC p)
            {
                //p.dtg_Input2.ScrollIntoView();
            }
            void _GetDetail(GradeUC p)
            {
                //p.dtg_HomeRoomList.SelectedIndex = p.dtg_Input1.SelectedIndex;
                //CurrentSelected =(AvailableClass)p.dtg_HomeRoomList.SelectedItem;

                if (p.dtg_Input1.SelectedIndex==-1)
                {
                    p.dtg_Class.SelectedIndex = p.dtg_Input2.SelectedIndex;
                    _Detail(p.dtg_Class);
                }
                else
                {
                    p.dtg_HomeRoomList = p.dtg_Input1;
                    _Detail(p.dtg_HomeRoomList);
                }

            }
        }

    }

}
