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
using static EduConnectApp.ViewModel.ClassViewModel;
using static EduConnectApp.ViewModel.SemesterScoreViewModel;

namespace EduConnectApp.ViewModel
{
    public class InputScoreViewModel : BaseViewModel
    {
        public struct StudentScore
        {
            public int number { get; set; }
            public int studentID { get; set; }
            public string name { get; set; }
            public string[] score { get; set; }
        }

        public ICommand navBack { get; }
        public ICommand InputCommand { get; }
        //public ICommand changeDtgYear { get; }
        public ICommand changeDtgSub { get; }
        public ICommand InputCommnand { get; }
        public ICommand LoadUC { get; }


        private int _semester;
        public int semester { get => _semester; set { _semester = value; OnPropertyChanged(); } }

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
        private int _cbbSelected;
        public int cbbSelected { get => _cbbSelected; set { _cbbSelected = value; OnPropertyChanged(); } }
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

        public IEnumerable<StudentScore> MyFilterListSemester
        {
            get
            {
                if (searchText == null || searchText == "")
                    return StudentScoreList;
                else
                    return StudentScoreList.Where(x => (x.name.ToUpper().Contains(searchText.ToUpper())));

            }
        }


        private List<StudentScore> _StudentScoreList = new List<StudentScore>();
        public List<StudentScore> StudentScoreList { get => _StudentScoreList; set { _StudentScoreList = value; OnPropertyChanged(); } }
        private ObservableCollection<HOCTAP> _LearningList;
        public ObservableCollection<HOCTAP> LearningList { get => _LearningList; set { _LearningList = value; OnPropertyChanged(); } }

        private ObservableCollection<THI> _TestList;
        public ObservableCollection<THI> TestList { get => _TestList; set { _TestList = value; OnPropertyChanged(); } }
        private ObservableCollection<TBMON> _avgSubList;
        public ObservableCollection<TBMON> avgSubList { get => _avgSubList; set { _avgSubList = value; OnPropertyChanged(); } }
        private ObservableCollection<KETQUA> _resultList;
        public ObservableCollection<KETQUA> resultList { get => _resultList; set { _resultList = value; OnPropertyChanged(); } }
        private ObservableCollection<GIANGDAY> _teachingList;
        public ObservableCollection<GIANGDAY> teachingList { get => _teachingList; set { _teachingList = value; OnPropertyChanged(); } }


        public InputScoreViewModel(NavigationStore navigationStore)
        {
            GradeViewModel.AvailableClass classSelected = GradeViewModel.CurrentSelected;
            semester = Const.Semester;

            //List
            TestList = new ObservableCollection<THI>(DataProvider.Ins.DB.THIs.Where(x => x.DELETED == false));
            LearningList = new ObservableCollection<HOCTAP>(DataProvider.Ins.DB.HOCTAPs.Where(x => x.DELETED == false));
            avgSubList = new ObservableCollection<TBMON>(DataProvider.Ins.DB.TBMONs.Where(x => x.DELETED == false));
            resultList = new ObservableCollection<KETQUA>(DataProvider.Ins.DB.KETQUAs.Where(x => x.DELETED == false));
            teachingList = new ObservableCollection<GIANGDAY>(DataProvider.Ins.DB.GIANGDAYs.Where(x => x.DELETED == false));

            //command
            LoadUC = new RelayCommand<InputScore>((p) => { return true; }, (p) =>
            {
                foreach (GIANGDAY gd in teachingList)
                {
                    if (gd.MAGV == Const.KeyID && gd.MALOP == classSelected.ClassID && gd.HOCKY == semester)
                        switch (gd.MAMH)
                        {
                            case 1:
                                p.cbbToan.IsEnabled= true; break;
                            case 2:
                                p.cbbLy.IsEnabled= true; break;
                            case 3:
                                p.cbbHoa.IsEnabled= true; break;
                            case 4:
                                p.cbbSinh.IsEnabled= true; break;
                            case 5:
                                p.cbbAnh.IsEnabled= true; break;
                            case 6:
                                p.cbbSu.IsEnabled= true; break;
                            case 7:
                                p.cbbDia.IsEnabled= true; break;
                            case 8:
                                p.cbbVan.IsEnabled= true; break;
                            case 9:
                                p.cbbCnghe.IsEnabled= true; break;
                            case 10:
                                p.cbbGDCD.IsEnabled= true; break;
                            case 11:
                                p.cbbTin.IsEnabled= true; break;
                            case 12:
                                p.cbbGDQP.IsEnabled= true; break;
                            case 13:
                                p.cbbTheDuc.IsEnabled= true; break;
                        }
                }
            });
            InputCommnand = new RelayCommand<ComboBox>((p) =>
            {
                return true;

            }, (p) => _InputSave(classSelected.ClassID, p.SelectedIndex+1));

            //changeDtgYear = new RelayCommand<SemesterScore>((p) => { return true; }, (p) => _UpdateYearCbb(p, classSelected.ClassID));
            changeDtgSub = new RelayCommand<InputScore>((p) => { return true; }, (p) => _UpdateSubjectCbb(p, classSelected.ClassID));

            //navigation
            navBack = new NavigationCommand<GradeViewModel>(navigationStore, () => new GradeViewModel(navigationStore));


            foreach (GIANGDAY gd in teachingList)
            {
                if (gd.MAGV == Const.KeyID && gd.MALOP == classSelected.ClassID && gd.HOCKY == semester)
                    cbbSelected = gd.MAMH-1;

            }
            //Title
            Title = "BẢNG ĐIỂM LỚP " + classSelected.Class;
            schoolYear = Const.SchoolYear;
            formTeacher = classSelected.Teacher;
            _UpdateTeachingTeacher(cbbSelected+1, classSelected.ClassID);
            _UpdateScoreSemester(cbbSelected+1, classSelected.ClassID, semester);
            AmountSt = classSelected.NumofAttendants.ToString() + " học sinh";

        }
        void _UpdateSubjectCbb(InputScore p, int classID)
        {
            //if (MessageBox.Show("Điểm chưa được lưu, tiếp tục?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    {
            //        _UpdateTeachingTeacher(p.cbb_Subject.SelectedIndex+1, classID);
            //        _UpdateScoreSemester(p, p.cbb_Subject.SelectedIndex+1, classID, semester);
            //        p.dtg_ScoreTable.Items.Refresh();
            //        cbbSelected = p.cbb_Subject.SelectedIndex;
            //    }
            //    else
            //    {
            //        p.cbb_Subject.SelectedIndex = cbbSelected;
            //        return;
            //    }
            _UpdateTeachingTeacher(p.cbb_Subject.SelectedIndex+1, classID);
            _UpdateScoreSemester( p.cbb_Subject.SelectedIndex+1, classID, semester);
            p.dtg_ScoreTable.Items.Refresh();
        }

        void _createThi(int mahs, int classID, int subID, int mald, string diem)
        {
            THI newThi = new THI();
            newThi.MAHS = mahs;
            newThi.MALOP= classID;
            newThi.MAMH = subID;
            newThi.MALD = mald;
            newThi.DIEM = diem;
            newThi.HOCKY = semester;
            newThi.DELETED = false;
            DataProvider.Ins.DB.THIs.Add(newThi);
        }

        void _InputSave(int classID, int subID)
        {
            foreach (StudentScore sc in StudentScoreList)
            {
                if (subID != 13)
                {
                    for (int i = 1; i < 7; i++)
                    {
                        if (sc.score[i] == null || sc.score[i] == "")
                            sc.score[i] = "0";
                        try { decimal.Parse(sc.score[i]); }

                        catch
                        {
                            MessageBox.Show("Vui lòng nhập điểm hợp lệ.", "Confirmation");
                            return;
                        }
                        if (double.Parse(sc.score[i]) > 10 || double.Parse(sc.score[i]) < 0)
                        {
                            MessageBox.Show("Vui lòng nhập điểm hợp lệ.", "Confirmation");
                            return;

                        }
                    }
                }
                //for (int i = 0; i < 7; i++)
                //{
                //    if (sc.score[i] == null || sc.score[i] == "")
                //        sc.score[i] = "-1";
                //}
                //if (subID != 13)
                //    for (int i = 1; i < 7; i++)
                //        try { decimal.Parse(sc.score[i]); }
                //        catch
                //        {
                //            MessageBox.Show("Thông tin lỗi chưa được lưu", "Confirmation");
                //            return;
                //        }

                int[] index = new int[3];
                index[0] = 0;
                index[1] = 1;
                index[2] = 4;
                foreach (THI thi in TestList)
                    if (thi.MAHS == sc.studentID && thi.MALOP ==classID && thi.MAMH == subID && thi.HOCKY == semester)
                        switch (thi.MALD)

                        {
                            case 1:
                                thi.DIEM = sc.score[0];
                                index[0]= 6;
                                break;
                            case 2:
                                thi.DIEM= sc.score[index[1]];
                                index[1]++;
                                if (index[1] == 4)
                                    index[1]= -1;
                                break;
                            case 3:
                                thi.DIEM = sc.score[index[2]];
                                index[2]++;
                                if (index[2] == 6)
                                    index[2]= -1;
                                break;
                            case 4:
                                if (index[0] == 6)
                                {
                                    thi.DIEM = sc.score[6];
                                    index[0]=-1;
                                }
                                break;
                        }
                foreach (int i in index)
                {
                    if (i == -1)
                        continue;
                    if (decimal.Parse(sc.score[i]) > 0)
                    {
                        if (i == 0)
                            _createThi(sc.studentID, classID, subID, 1, sc.score[i]);
                        if (i >= 1 && i <=3)
                            _createThi(sc.studentID, classID, subID, 2, sc.score[i]);
                        if (i == 4 || i ==5)
                            _createThi(sc.studentID, classID, subID, 3, sc.score[i]);
                        if (i == 6)
                            _createThi(sc.studentID, classID, subID, 4, sc.score[i]);
                    }
                }

                for (int i = 0; i < 7; i++)
                {
                    if (sc.score[i] == "-1")
                        sc.score[i] = "0";
                }

                sc.score[7] = String.Format("{0:0.00}", ((decimal.Parse(sc.score[0]) +
                    decimal.Parse(sc.score[1]) + decimal.Parse(sc.score[2]) + decimal.Parse(sc.score[3]) +
                      decimal.Parse(sc.score[4])*2 + decimal.Parse(sc.score[5]) * 2 + decimal.Parse(sc.score[6])*3)/11));
                var tempTBHK = DataProvider.Ins.DB.TBMONs.Where(x => x.MAHS == sc.studentID && x.MALOP==classID && x.MAMH == subID  && x.HOCKY == semester&& x.DELETED == false).FirstOrDefault();
                if (tempTBHK != null)
                    tempTBHK.DTB =  sc.score[7];
                DataProvider.Ins.DB.SaveChanges();

                //flag rank
                int flagRank = 0;
                foreach (TBMON tbm in avgSubList)
                {
                    if (tbm.MAMH == 1 || tbm.MAMH == 5 || tbm.MAMH == 8)
                    {
                        if (float.Parse(tbm.DTB) < 8 && flagRank < 1)
                            flagRank = 1;
                        if (float.Parse(tbm.DTB) < 6.5 && flagRank < 2)
                            flagRank = 2;
                        if (float.Parse(tbm.DTB) < 5 && flagRank < 3)
                            flagRank = 3;
                    }
                    else if (tbm.MAMH != 13)
                    {
                        if (float.Parse(tbm.DTB) < 6.5 && flagRank < 1)
                            flagRank = 1;
                        if (float.Parse(tbm.DTB) < 5 && flagRank < 2)
                            flagRank = 2;
                        if (float.Parse(tbm.DTB) < 3.5 && flagRank < 3)
                            flagRank = 3;
                    }
                }

                //dtb - rank
                decimal tempTB = 0;
                var tempKQ = DataProvider.Ins.DB.KETQUAs.Where(x => x.MAHS == sc.studentID && x.MALOP==classID && x.HOCKY == semester && x.DELETED == false).FirstOrDefault();
                if (tempKQ!=null)
                {
                    foreach (TBMON tbm in avgSubList)
                    {
                        if (tbm.MAHS == sc.studentID && tbm.MALOP == classID && tbm.HOCKY == semester && tbm.MAMH !=13)
                            tempTB += decimal.Parse(tbm.DTB);
                    }
                    tempKQ.DTB = (decimal)(tempTB/12);
                    if (tempKQ.DTB>=(decimal)8 && flagRank == 0)
                        tempKQ.XEPLOAI = "Giỏi";
                    if (tempKQ.DTB>=(decimal)6.5 && tempKQ.DTB <= (decimal)8 && flagRank == 1)
                        tempKQ.XEPLOAI = "Khá";
                    if (tempKQ.DTB>=(decimal)5 && tempKQ.DTB <= (decimal)6.5  && flagRank == 2)
                        tempKQ.XEPLOAI = "Trung Bình";
                }

            }
            DataProvider.Ins.DB.SaveChanges();
            MessageBoxResult result = MessageBox.Show("Thông tin đã được lưu.", "Confirmation");
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

        void _UpdateScoreSemester(int subID, int classID, int semester)
        {
            StudentScoreList.Clear();

            //string[] scoreTemp = Enumerable.Repeat("", 3).ToArray();
            //string[] scoreTemp45 = Enumerable.Repeat("", 3).ToArray();
            int num = 0;
            int index15 = 1, index45 = 4;
            foreach (HOCTAP ht in LearningList)
            {
                if (ht.MALOP ==  classID)
                {
                    num++;
                    StudentScore sc = new StudentScore();
                    sc.score = new string[8];
                    sc.number= num;
                    var tempStudent = DataProvider.Ins.DB.HOCSINHs.Where(x => x.MAHS == ht.MAHS && x.DELETED == false).FirstOrDefault();
                    sc.name = tempStudent.HOTEN;
                    sc.studentID = (int)ht.MAHS;

                    foreach (THI thi in TestList)
                    {
                        if (thi.MALOP  ==  classID && thi.MAHS ==ht.MAHS && thi.MAMH == subID && thi.HOCKY == semester)
                        {
                            switch (thi.MALD)
                            {
                                case 1:
                                    sc.score[0] = thi.DIEM;
                                    break;
                                case 2:
                                    sc.score[index15] = thi.DIEM;
                                    index15++;
                                    break;
                                case 3:
                                    sc.score[index45] = thi.DIEM;
                                    index45++;

                                    break;
                                case 4:
                                    sc.score[6] = thi.DIEM;
                                    break;
                            }
                        }
                    }
                    //var tempTBHK = DataProvider.Ins.DB.TBMONs.Where(x => x.MAHS == ht.MAHS && x.MALOP==classID && x.MAMH == subID  && x.HOCKY == semester&& x.DELETED == false).FirstOrDefault();
                    //if (tempTBHK!=null)
                    //    sc.avg = tempTBHK.DTB;
                    index15=1;
                    //scoreTemp = Enumerable.Repeat("", 3).ToArray();
                    index45=4;
                    //scoreTemp45 = Enumerable.Repeat("", 3).ToArray();
                    //var tempKQ = DataProvider.Ins.DB.KETQUAs.Where(x => x.MAHS == ht.MAHS && x.MALOP==classID && x.HOCKY == semester && x.DELETED == false).FirstOrDefault();
                    //if (tempKQ!=null)
                    //{
                    //    sc.avgSub =  tempKQ.DTB.ToString();
                    //    sc.rank = tempKQ.XEPLOAI;
                    //    sc.conduct=tempKQ.HANHKIEM;
                    //}
                    StudentScoreList.Add(sc);

                }
            }

        }

    }
}