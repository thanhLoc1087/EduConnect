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
using static EduConnectApp.ViewModel.SemesterScoreViewModel;

namespace EduConnectApp.ViewModel
{
    public class ScoreDetailViewModel : BaseViewModel
    {
        //struct
        public struct scoreTable
        {
            public string subject { get; set; }
            public string mieng { get; set; }
            public string min15_1 { get; set; }
            public string min15_2 { get; set; }
            public string min15_3 { get; set; }
            public string min45_1 { get; set; }
            public string min45_2 { get; set; }
            public string test { get; set; }
            public string avg { get; set; }

        }
        public struct scoreTable_Year
        {
            public string subject { get; set; }
            public string avg_1 { get; set; }
            public string avg_2 { get; set; }
            public string avg_Year { get; set; }

        }
        //commnand
        public ICommand navBack { get; }
        public ICommand navEdit { get; }
        public ICommand changeScoreTb { get; }


        //List
        private List<scoreTable> _scoreTableList = new List<scoreTable>();
        public List<scoreTable> scoreTableList { get => _scoreTableList; set { _scoreTableList = value; OnPropertyChanged(); } }
        private List<scoreTable_Year> _scoreTableList_Year = new List<scoreTable_Year>();
        public List<scoreTable_Year> scoreTableList_Year { get => _scoreTableList_Year; set { _scoreTableList_Year = value; OnPropertyChanged(); } }


        //variable binding
        private string _name;
        public string name { get => _name; set { _name = value; OnPropertyChanged(); } }
        private string _className;
        public string className { get => _className; set { _className = value; OnPropertyChanged(); } }
        private string _formTeacher;
        public string formTeacher { get => _formTeacher; set { _formTeacher = value; OnPropertyChanged(); } }

        private string _schoolYear;
        public string schoolYear { get => _schoolYear; set { _schoolYear = value; OnPropertyChanged(); } }

        private int _semester;
        public int semester { get => _semester; set { _semester = value; OnPropertyChanged(); } }
        private string _avgSemester;
        public string avgSemester { get => _avgSemester; set { _avgSemester = value; OnPropertyChanged(); } }
        private string _conduct;
        public string conduct { get => _conduct; set { _conduct = value; OnPropertyChanged(); } }
        private string _rank;
        public string rank { get => _rank; set { _rank = value; OnPropertyChanged(); } }
        private string _achievements;
        public string achievements { get => _achievements; set { _achievements = value; OnPropertyChanged(); } }
        private string _comment;
        public string comment { get => _comment; set { _comment = value; OnPropertyChanged(); } }
        private ObservableCollection<MONHOC> _subjectList;
        public ObservableCollection<MONHOC> subjectList { get => _subjectList; set { _subjectList = value; OnPropertyChanged(); } }

        private ObservableCollection<THI> _testList;
        public ObservableCollection<THI> testList { get => _testList; set { _testList = value; OnPropertyChanged(); } }
        private ObservableCollection<TBMON> _avgList;
        public ObservableCollection<TBMON> avgList { get => _avgList; set { _avgList = value; OnPropertyChanged(); } }

        public ScoreDetailViewModel(NavigationStore navigationStore)
        {
            SemesterScoreViewModel.selectedStudent selectedStuddent = SemesterScoreViewModel.CurrentSelected;

            //navigate
            navBack = new NavigationCommand<SemesterScoreViewModel>(navigationStore, () => new SemesterScoreViewModel(navigationStore));
            navEdit = new NavigationCommand<EditScoreViewModel>(navigationStore, () => new EditScoreViewModel(navigationStore));

            //infor
            name = DataProvider.Ins.DB.HOCSINHs.Where(x => x.MAHS == selectedStuddent.mahs && x.DELETED == false).FirstOrDefault().HOTEN;
            var tempClass = DataProvider.Ins.DB.LOPs.Where(x => x.MALOP == selectedStuddent.malop && x.DELETED == false).FirstOrDefault();
            className = tempClass.TENLOP;
            formTeacher  = DataProvider.Ins.DB.GIAOVIENs.Where(x => x.MAGV == tempClass.GVCN && x.DELETED == false).FirstOrDefault().HOTEN;
            schoolYear = "NIÊN KHÓA " + Const.SchoolYear;
            semester = 1;

            //list
            subjectList = new ObservableCollection<MONHOC>(DataProvider.Ins.DB.MONHOCs.Where(x => x.DELETED == false));
            testList = new ObservableCollection<THI>(DataProvider.Ins.DB.THIs.Where(x => x.DELETED == false));
            avgList = new ObservableCollection<TBMON>(DataProvider.Ins.DB.TBMONs.Where(x => x.DELETED == false));

            //datagrid
            _UpdateScoreTable(selectedStuddent.mahs, selectedStuddent.malop);
            _UpdateScoreTable_Year(selectedStuddent.mahs, selectedStuddent.malop);

            //changeCbb
            changeScoreTb = new RelayCommand<ScoreDetail>((p) => { return true; }, (p) => _cbbChanged(p, selectedStuddent.mahs, selectedStuddent.malop));

        }

        void _cbbChanged(ScoreDetail p, int n, int m)
        {
            semester = p.cbb_Semester.SelectedIndex;
            if (semester == 0)
            {
                p.dtg_Scoretable_Year.Visibility = Visibility.Visible;
                p.dtg_Scoretable.Visibility = Visibility.Hidden;
                p.brd_year.Visibility = Visibility.Visible;
                _UpdateScoreTable_Year(n, m);
                p.dtg_Scoretable_Year.Items.Refresh();

            }
            else
            {
                p.dtg_Scoretable.Visibility = Visibility.Visible;
                p.dtg_Scoretable_Year.Visibility = Visibility.Hidden;
                p.brd_year.Visibility = Visibility.Hidden;
                _UpdateScoreTable(n, m);
                p.dtg_Scoretable.Items.Refresh();
            }

        }
        void _UpdateScoreTable_Year( int mahs, int classID)
        {
            scoreTableList_Year.Clear();
            foreach (MONHOC mh in subjectList)
            {
                scoreTable_Year sc = new scoreTable_Year();
                sc.subject = mh.TENMH;
                foreach (TBMON tbm in avgList)
                {
                    if (tbm.MAMH == mh.MAMH  && tbm.MAHS  == mahs && tbm.MALOP == classID)
                    {
                        switch (tbm.HOCKY)
                        {
                            case 1:
                                sc.avg_1= tbm.DTB;
                                break;
                            case 2:
                                sc.avg_2= tbm.DTB;
                                break;
                            case 0:
                                sc.avg_Year= tbm.DTB;
                                break;
                        }
                    }
                }
                scoreTableList_Year.Add(sc);
            }

            var tempKQ = DataProvider.Ins.DB.KETQUAs.Where(x => x.MAHS == mahs && x.MALOP==classID && x.HOCKY == semester && x.DELETED == false).FirstOrDefault();
            if (tempKQ!=null)
            {
                avgSemester =  tempKQ.DTB.ToString();
                rank = tempKQ.XEPLOAI;
                conduct=tempKQ.HANHKIEM;
            }
            var tempTT = DataProvider.Ins.DB.THANHTICHes.Where(x => x.MAHS == mahs && x.MALOP==classID && x.DELETED == false).FirstOrDefault();
            if (tempTT!=null)
            {
                achievements = tempTT.TENTT;
            }            
            var tempNX = DataProvider.Ins.DB.NHANXETs.Where(x => x.MAHS == mahs && x.MALOP==classID && x.HOCKY == semester &&x.DELETED == false).FirstOrDefault();
            if (tempNX!=null)
            {
                comment = tempNX.NHANXET1;
            }
            else comment = "";
        }
        void _UpdateScoreTable( int mahs, int classID)
        {
            scoreTableList.Clear();

            string[] scoreTemp = Enumerable.Repeat("", 3).ToArray();
            string[] scoreTemp45 = Enumerable.Repeat("", 3).ToArray();
            int index = 0, index45 = 0;

            foreach (MONHOC mh in subjectList)
            {
                scoreTable sc = new scoreTable();
                sc.subject = mh.TENMH;
                foreach (THI thi in testList)
                {
                    if (thi.MAMH == mh.MAMH && thi.HOCKY == semester && thi.MAHS  == mahs && thi.MALOP == classID)
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
                var tempTBHK = DataProvider.Ins.DB.TBMONs.Where(x => x.MAHS == mahs && x.MALOP==classID && x.MAMH == mh.MAMH  && x.HOCKY == semester&& x.DELETED == false).FirstOrDefault();
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

                scoreTableList.Add(sc);
            }

            var tempKQ = DataProvider.Ins.DB.KETQUAs.Where(x => x.MAHS == mahs && x.MALOP==classID && x.HOCKY == semester && x.DELETED == false).FirstOrDefault();
            if (tempKQ!=null)
            {
                avgSemester =  tempKQ.DTB.ToString();
                rank = tempKQ.XEPLOAI;
                conduct=tempKQ.HANHKIEM;
            }
            var tempTT = DataProvider.Ins.DB.THANHTICHes.Where(x => x.MAHS == mahs && x.MALOP==classID && x.DELETED == false).FirstOrDefault();
            if (tempTT!=null)
            {
                achievements = tempTT.TENTT;
            }            
            var tempNX = DataProvider.Ins.DB.NHANXETs.Where(x => x.MAHS == mahs && x.MALOP==classID && x.HOCKY == semester &&x.DELETED == false).FirstOrDefault();
            if (tempNX!=null)
            {
                comment = tempNX.NHANXET1;
            }
            else comment = "";
        }
    }
}
