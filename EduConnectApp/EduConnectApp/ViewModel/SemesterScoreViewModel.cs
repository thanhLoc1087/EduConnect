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
using System.Windows.Input;

namespace EduConnectApp.ViewModel
{
    public class SemesterScoreViewModel : BaseViewModel
    {
        public struct semesterScore
        {
            public int num { get; set; }
            public string name { get; set; }
            public string mieng { get; set; }
            public string min15_1 { get; set; }
            public string min15_2 { get; set; }
            public string min15_3 { get; set; }
            public string min45_1 { get; set; }
            public string min45_2 { get; set; }
            public string test { get; set; }
            public string tbhk { get; set; }
        }

        public ICommand changeDtg { get; }

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

        private List<semesterScore> _semesterScoreList = new List<semesterScore>();
        public List<semesterScore> semesterScoreList { get => _semesterScoreList; set { _semesterScoreList = value; OnPropertyChanged(); } }

        //private string _num;
        //public string num { get => _num; set { _num = value; OnPropertyChanged(); } }
        //private string _Name;
        //public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        //private string _Mieng;
        //public string Mieng { get => _Mieng; set { _Mieng = value; OnPropertyChanged(); } }
        //private string _min15;
        //public string min15 { get => _min15; set { _min15 = value; OnPropertyChanged(); } }
        //private string _min45;
        //public string min45 { get => _min45; set { _min45 = value; OnPropertyChanged(); } }
        //private string _test;
        //public string test { get => _test; set { _test = value; OnPropertyChanged(); } }
        //private string _avg;
        //public string avg { get => _avg; set { _avg = value; OnPropertyChanged(); } }
        //private string _type;
        //public string type { get => _type; set { _type = value; OnPropertyChanged(); } }


        private ObservableCollection<THI> _TestList;
        public ObservableCollection<THI> TestList { get => _TestList; set { _TestList = value; OnPropertyChanged(); } }
        private ObservableCollection<HOCTAP> _LearningList;
        public ObservableCollection<HOCTAP> LearningList { get => _LearningList; set { _LearningList = value; OnPropertyChanged(); } }


        public SemesterScoreViewModel(NavigationStore navigationStore)
        {
            GradeViewModel.AvailableClass classSelected = GradeViewModel.CurrentSelected;

            changeDtg = new RelayCommand<SemesterScore>((p) => { return true; }, (p) => _UpdateDtg(p));

            //List
            TestList = new ObservableCollection<THI>(DataProvider.Ins.DB.THIs.Where(x => x.DELETED == false));
            LearningList = new ObservableCollection<HOCTAP>(DataProvider.Ins.DB.HOCTAPs.Where(x => x.DELETED == false));

            //Title
            Title = "BẢNG ĐIỂM LỚP " + classSelected.Class;
            schoolYear = Const.SchoolYear;
            formTeacher = classSelected.Teacher;
            var tempClass = DataProvider.Ins.DB.GIANGDAYs.Where(x => x.MALOP == classSelected.ClassID && x.DELETED == false ).FirstOrDefault();
            var tempTeacher = DataProvider.Ins.DB.GIAOVIENs.Where(x => x.MAGV == tempClass.MAGV && x.DELETED == false).FirstOrDefault();
            teachingTeacher= tempTeacher.HOTEN;
            AmountSt = classSelected.NumofAttendants.ToString() + " học sinh";

            //Semester
            //string[] score15 = new string[3];
            //for (int i = 0; i <score15.Length; i++)
            //    score15[i] = "";
            string [] scoreTemp = Enumerable.Repeat("", 3).ToArray();
            string [] scoreTemp45 = Enumerable.Repeat("", 3).ToArray();
            semesterScore sc = new semesterScore();
            sc.num = 0;
            int index=0, index45 = 0;
            foreach (HOCTAP ht in LearningList)
            {
                if (ht.MALOP ==  classSelected.ClassID)
                {
                    var tempStudent = DataProvider.Ins.DB.HOCSINHs.Where(x => x.MAHS == ht.MAHS && x.DELETED == false).FirstOrDefault();
                    sc.name = tempStudent.HOTEN;
                    sc.num++;

                    foreach (THI thi in TestList)
                    {
                        if (thi.MALOP  ==  classSelected.ClassID && thi.MAHS ==ht.MAHS && thi.MAMH == 1)
                        {
                            switch (thi.MALD)
                            {
                                case 1:
                                    sc.mieng = thi.DIEM;
                                    break;
                                case 2:
                                    //switch(flag)
                                    //{
                                    //    case 0:
                                    //        sc.min15_1= thi.DIEM;
                                    //        flag=1;
                                    //        break;
                                    //    case 1:
                                    //        sc.min15_2= thi.DIEM;
                                    //        flag=2;
                                    //        break;
                                    //    case 2:
                                    //        sc.min15_3= thi.DIEM;
                                    //        flag=3;
                                    //        break; 
                                    //}
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
                        if (thi.MAHS==20 && thi.MAMH==13 && thi.MALD==4 )
                        {  }
                    }
                    sc.min15_1 = scoreTemp[0];
                    sc.min15_2 = scoreTemp[1];
                    sc.min15_3 = scoreTemp[2];
                    sc.min45_1= scoreTemp45[0];
                    sc.min45_2= scoreTemp45[1];
                    index=0;
                    scoreTemp = Enumerable.Repeat("", 3).ToArray();
                    index45=0;
                    scoreTemp45 = Enumerable.Repeat("", 3).ToArray();

                    semesterScoreList.Add(sc);
                }
            }

            //foreach (THI thi in TestList)
            //{
            //    if (thi.MALOP  ==  classSelected.ClassID)
            //    {
            //        var tempStudent = DataProvider.Ins.DB.HOCSINHs.Where(x => x.MAHS == thi.MAHS && x.DELETED == false).FirstOrDefault();
            //        var tempThi = DataProvider.Ins.DB.THIs.Where(x => x.MAHS == thi.MAHS && x.DELETED == false && x.MALD == 1).FirstOrDefault();
            //        sc.name = tempStudent.HOTEN;
            //        sc.mieng = tempThi.DIEM;
            //    }
            //}

        }

        void _UpdateDtg(SemesterScore p)
        {
            if (p.cbb_Semester.SelectedIndex==0)
            {
                p.dtg_Semester.Visibility=Visibility.Hidden;
                p.dtg_Year.Visibility=Visibility.Visible;
            }
            else
            {
                p.dtg_Semester.Visibility=Visibility.Visible;
                p.dtg_Year.Visibility=Visibility.Hidden;
            }
        }
    }
}
