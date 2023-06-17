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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EduConnectApp.ViewModel
{
    public class SemesterScoreViewModel : BaseViewModel
    {
        public struct semesterScore
        {
            public int num { get; set; }
            public int stID { get; set; }
            public string name { get; set; }
            public string mieng { get; set; }
            public string min15_1 { get; set; }
            public string min15_2 { get; set; }
            public string min15_3 { get; set; }
            public string min45_1 { get; set; }
            public string min45_2 { get; set; }
            public string test { get; set; }
            public string avg { get; set; }
            public string avgSub { get; set; }
            public string rank { get; set; }
            public string conduct { get; set; }
        }

        public struct yearScore
        {
            public int num { get; set; }
            public int stID { get; set; }
            public string name { get; set; }
            public string ses1 { get; set; }
            public string ses2 { get; set; }
            public string year { get; set; }
            public string ses1_tk { get; set; }
            public string ses2_tk { get; set; }
            public string year_tk { get; set; }
            public string rank { get; set; }
            public string conduct { get; set; }
        }

        public struct selectedStudent
        {
            public int malop { get; set; }
            public int mahs { get; set; }
            public int hk { get; set; }
        }
        public static selectedStudent CurrentSelected { get; set; }

        public ICommand changeDtgYear { get; }
        public ICommand changeDtgSub { get; }
        public ICommand Detail { get; }
        public ICommand navScoreDetail { get; }
        public ICommand navBack { get; }

        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }
        private string _schoolYear;
        public string schoolYear { get => _schoolYear; set { _schoolYear = value; OnPropertyChanged(); } }
        private string _formTeacher;
        public string formTeacher { get => _formTeacher; set { _formTeacher = value; OnPropertyChanged(); } }
        private string _teachingTeacher;
        public string teachingTeacher { get => _teachingTeacher; set { _teachingTeacher = value; OnPropertyChanged(); } }
        private string _AmountSt;
        public string AmountSt { get => _AmountSt; set { _AmountSt = value; OnPropertyChanged(); } }
        private int _semester;
        public int semester { get => _semester; set { _semester = value; OnPropertyChanged(); } }
        private List<semesterScore> _semesterScoreList = new List<semesterScore>();
        public List<semesterScore> semesterScoreList { get => _semesterScoreList; set { _semesterScoreList = value; OnPropertyChanged(); } }
        private List<yearScore> _yearScoreList = new List<yearScore>();
        public List<yearScore> yearScoreList { get => _yearScoreList; set { _yearScoreList = value; OnPropertyChanged(); } }

        private ObservableCollection<THI> _TestList;
        public ObservableCollection<THI> TestList { get => _TestList; set { _TestList = value; OnPropertyChanged(); } }
        private ObservableCollection<HOCTAP> _LearningList;
        public ObservableCollection<HOCTAP> LearningList { get => _LearningList; set { _LearningList = value; OnPropertyChanged(); } }
        private ObservableCollection<TBMON> _avgSubList;
        public ObservableCollection<TBMON> avgSubList { get => _avgSubList; set { _avgSubList = value; OnPropertyChanged(); } }
        private ObservableCollection<KETQUA> _resultList;
        public ObservableCollection<KETQUA> resultList { get => _resultList; set { _resultList = value; OnPropertyChanged(); } }

        private string _searchText;
        public string searchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("searchText");
                OnPropertyChanged("MyFilterListSemester");
                OnPropertyChanged("MyFilterListYear");
            }
        }

        public IEnumerable<semesterScore> MyFilterListSemester
        {
            get
            {
                if (searchText == null || searchText == "")
                    return semesterScoreList;
                else
                    return semesterScoreList.Where(x => (x.name.ToUpper().Contains(searchText.ToUpper())));

            }
        }
        public IEnumerable<yearScore> MyFilterListYear
        {
            get
            {
                if (searchText == null || searchText == "")
                    return yearScoreList;
                else
                    return yearScoreList.Where(x => (x.name.ToUpper().Contains(searchText.ToUpper())));

            }
        }


        public SemesterScoreViewModel(NavigationStore navigationStore)
        {
            GradeViewModel.AvailableClass classSelected = GradeViewModel.CurrentSelected;
            //navigate
            navScoreDetail = new NavigationCommand<ScoreDetailViewModel>(navigationStore, () => new ScoreDetailViewModel(navigationStore));
            navBack = new NavigationCommand<GradeViewModel>(navigationStore, () => new GradeViewModel(navigationStore));

            //command
            changeDtgYear = new RelayCommand<SemesterScore>((p) => { return true; }, (p) => _UpdateYearCbb(p, classSelected.ClassID));
            changeDtgSub = new RelayCommand<SemesterScore>((p) => { return true; }, (p) => _UpdateSubjectCbb(p, classSelected.ClassID));
            Detail = new RelayCommand<DataGrid>((p) => { return p.SelectedItem==null?false:true; }, (p) => _Detail(p, classSelected.ClassID));

            //List
            TestList = new ObservableCollection<THI>(DataProvider.Ins.DB.THIs.Where(x => x.DELETED == false));
            LearningList = new ObservableCollection<HOCTAP>(DataProvider.Ins.DB.HOCTAPs.Where(x => x.DELETED == false));
            avgSubList = new ObservableCollection<TBMON>(DataProvider.Ins.DB.TBMONs.Where(x => x.DELETED == false));
            resultList = new ObservableCollection<KETQUA>(DataProvider.Ins.DB.KETQUAs.Where(x => x.DELETED == false));

            //Title
            Title = "BẢNG ĐIỂM LỚP " + classSelected.Class;
            schoolYear = Const.SchoolYear;
            formTeacher = classSelected.Teacher;
            _UpdateTeachingTeacher(1, classSelected.ClassID);
            AmountSt = classSelected.NumofAttendants.ToString() + " học sinh";

            //Semester
            semester = Const.Semester;
            _UpdateScoreSemester(1, classSelected.ClassID, semester);
            _UpdateScoreYear(1, classSelected.ClassID);
        }

        void _Detail(DataGrid p, int malop)
        {
            selectedStudent st = new selectedStudent();
            if (p.Name == "dtg_Semester")
            {
                semesterScore sc = new semesterScore();
                sc = (semesterScore)p.SelectedItem;
                st.malop = malop; 
                st.mahs = sc.stID;
                st.hk = semester;
            }
            else
            {
                yearScore yc = (yearScore)p.SelectedItem;
                st.malop = malop;
                st.mahs = yc.stID;
                st.hk = semester;
            }
            CurrentSelected = st;
        }


        void _Update(SemesterScore p)
        {
            p.dtg_Semester.IsHitTestVisible= false;
        }
        void _UpdateYearCbb(SemesterScore p, int classID)
        {
            semester = p.cbb_Semester.SelectedIndex;
            if (p.cbb_Semester.SelectedIndex==0)
            {
                p.dtg_Semester.Visibility=Visibility.Hidden;
                p.dtg_Year.Visibility=Visibility.Visible;
                p.dtg_semesterEdit.IsHitTestVisible = false;
            }
            else
            {
                p.dtg_Semester.Visibility=Visibility.Visible;
                p.dtg_Year.Visibility=Visibility.Hidden;

            }
            _UpdateScoreSemester(p.cbb_Subject.SelectedIndex, classID, semester);
            _UpdateScoreYear(p.cbb_Subject.SelectedIndex, classID);
            p.dtg_Year.Items.Refresh();
            p.dtg_Semester.Items.Refresh();
        }
        void _UpdateSubjectCbb(SemesterScore p, int classID)
        {
            if (p.cbb_Subject.SelectedIndex == 0)
            {
                p.co_Mieng.Visibility = Visibility.Collapsed;
                p.co_Min15.Visibility = Visibility.Collapsed;
                p.co_Min45.Visibility = Visibility.Collapsed;
                p.co_Test.Visibility = Visibility.Collapsed;
                p.co_TBHK.Visibility = Visibility.Collapsed;
                p.co_Hk1.Visibility = Visibility.Collapsed;
                p.co_Hk2.Visibility = Visibility.Collapsed;
                p.co_Year.Visibility = Visibility.Collapsed;

                p.co_Hk1_tk.Visibility = Visibility.Visible;
                p.co_Hk2_tk.Visibility = Visibility.Visible;
                p.co_Year_tk.Visibility = Visibility.Visible;
                p.co_AvgSub.Visibility = Visibility.Visible;
                p.co_RankHK.Visibility = Visibility.Visible;
                p.co_ConductHK.Visibility = Visibility.Visible;
                p.co_Rank.Visibility = Visibility.Visible;
                p.co_Conduct.Visibility = Visibility.Visible;

            }
            else
            {
                p.co_Hk1_tk.Visibility = Visibility.Collapsed;
                p.co_Hk2_tk.Visibility = Visibility.Collapsed;
                p.co_Year_tk.Visibility = Visibility.Collapsed;
                p.co_AvgSub.Visibility = Visibility.Collapsed;
                p.co_RankHK.Visibility = Visibility.Collapsed;
                p.co_ConductHK.Visibility = Visibility.Collapsed;
                p.co_Rank.Visibility = Visibility.Collapsed;
                p.co_Conduct.Visibility = Visibility.Collapsed;

                p.co_Mieng.Visibility = Visibility.Visible;
                p.co_Min15.Visibility = Visibility.Visible;
                p.co_Min45.Visibility = Visibility.Visible;
                p.co_Test.Visibility = Visibility.Visible;
                p.co_TBHK.Visibility = Visibility.Visible;
                p.co_Hk1.Visibility = Visibility.Visible;
                p.co_Hk2.Visibility = Visibility.Visible;
                p.co_Year.Visibility = Visibility.Visible;

                _UpdateTeachingTeacher(p.cbb_Subject.SelectedIndex, classID);
                _UpdateScoreSemester(p.cbb_Subject.SelectedIndex, classID, semester);
                _UpdateScoreYear(p.cbb_Subject.SelectedIndex, classID);
                p.dtg_Year.Items.Refresh();
                p.dtg_Semester.Items.Refresh();
            }
        }

        void _UpdateTeachingTeacher(int subID, int classID)
        {
            var tempTeaching = DataProvider.Ins.DB.GIANGDAYs.Where(x => x.MALOP == classID && x.MAMH == subID && x.HOCKY == Const.Semester && x.DELETED == false).FirstOrDefault();
            if (tempTeaching != null)
            {
                var tempTeacher = DataProvider.Ins.DB.GIAOVIENs.Where(x => x.MAGV == tempTeaching.MAGV && x.DELETED == false).FirstOrDefault();
                teachingTeacher = tempTeacher.HOTEN;
            }
            else teachingTeacher = "";
        }

        void _UpdateScoreYear(int subID, int classID)
        {
            yearScoreList.Clear();
            int index = 0;
            foreach (HOCTAP ht in LearningList)
            {
                if (ht.MALOP ==  classID)
                {
                    yearScore yc = new yearScore();
                    index++;
                    var tempStudent = DataProvider.Ins.DB.HOCSINHs.Where(x => x.MAHS == ht.MAHS && x.DELETED == false).FirstOrDefault();
                    yc.name = tempStudent.HOTEN;
                    yc.stID = (int)ht.MAHS;
                    yc.num=index;

                    foreach (TBMON tbm in avgSubList)
                    {
                        if (tbm.MAHS == ht.MAHS && tbm.MALOP == classID && tbm.MAMH == subID)
                        {
                            switch (tbm.HOCKY)
                            {
                                case 0:
                                    yc.year = tbm.DTB.ToString(); break;
                                case 1:
                                    yc.ses1 = tbm.DTB.ToString(); break;
                                case 2:
                                    yc.ses2 = tbm.DTB.ToString(); break;
                            }
                        }
                    }
                    foreach (KETQUA kq in resultList)
                    {
                        if (kq.MAHS == ht.MAHS && kq.MALOP == classID)
                        {
                            switch (kq.HOCKY)
                            {
                                case 0:
                                    yc.year_tk = kq.DTB.ToString(); break;
                                case 1:
                                    yc.ses1_tk = kq.DTB.ToString(); break;
                                case 2:
                                    yc.ses2_tk = kq.DTB.ToString(); break;
                            }
                            yc.rank = kq.XEPLOAI;
                            yc.conduct = kq.HANHKIEM;
                        }
                    }

                    yearScoreList.Add(yc);
                }

            }
        }

        void _UpdateScoreSemester(int subID, int classID, int semester)
        {
            semesterScoreList.Clear();

            string[] scoreTemp = Enumerable.Repeat("", 3).ToArray();
            string[] scoreTemp45 = Enumerable.Repeat("", 3).ToArray();
            int num = 0;
            int index = 0, index45 = 0;
            foreach (HOCTAP ht in LearningList)
            {
                if (ht.MALOP ==  classID)
                {
                    num++;
                    semesterScore sc = new semesterScore();
                    sc.num= num;
                    var tempStudent = DataProvider.Ins.DB.HOCSINHs.Where(x => x.MAHS == ht.MAHS && x.DELETED == false).FirstOrDefault();
                    sc.name = tempStudent.HOTEN;
                    sc.stID = (int)ht.MAHS;

                    foreach (THI thi in TestList)
                    {
                        if (thi.MALOP  ==  classID && thi.MAHS ==ht.MAHS && thi.MAMH == subID && thi.HOCKY == semester)
                        {
                            switch (thi.MALD)
                            {
                                case 1:
                                    sc.mieng = thi.DIEM;
                                    break;
                                case 2:
                                    scoreTemp[index] = thi.DIEM;
                                    index++;
                                    break;
                                case 3:
                                    scoreTemp45[index45] = thi.DIEM;
                                    index45++;

                                    break;
                                case 4:
                                    sc.test = thi.DIEM;
                                    break;
                            }
                        }
                    }
                    var tempTBHK = DataProvider.Ins.DB.TBMONs.Where(x => x.MAHS == ht.MAHS && x.MALOP==classID && x.MAMH == subID  && x.HOCKY == semester&& x.DELETED == false).FirstOrDefault();
                    if (tempTBHK!=null)
                        sc.avg = tempTBHK.DTB;
                    sc.min15_1 = scoreTemp[0];
                    sc.min15_2 = scoreTemp[1];
                    sc.min15_3 = scoreTemp[2];
                    sc.min45_1= scoreTemp45[0];
                    sc.min45_2= scoreTemp45[1];
                    index=0;
                    scoreTemp = Enumerable.Repeat("", 3).ToArray();
                    index45=0;
                    scoreTemp45 = Enumerable.Repeat("", 3).ToArray();
                    var tempKQ = DataProvider.Ins.DB.KETQUAs.Where(x => x.MAHS == ht.MAHS && x.MALOP==classID && x.HOCKY == semester && x.DELETED == false).FirstOrDefault();
                    if (tempKQ!=null)
                    {
                        sc.avgSub =  tempKQ.DTB.ToString();
                        sc.rank = tempKQ.XEPLOAI;
                        sc.conduct=tempKQ.HANHKIEM;
                    }
                    semesterScoreList.Add(sc);

                }
            }

        }
    }
}
