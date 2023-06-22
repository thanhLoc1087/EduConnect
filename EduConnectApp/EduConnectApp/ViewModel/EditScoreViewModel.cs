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
using System.Xml.Linq;
using static EduConnectApp.ViewModel.ScoreDetailViewModel;
using static EduConnectApp.ViewModel.SemesterScoreViewModel;

namespace EduConnectApp.ViewModel
{
    public class EditScoreViewModel : BaseViewModel
    {
        public struct scoreTable
        {
            public int subjectID { get; set; }
            public string subject { get; set; }
            //public string[] otherScore { get; set; }  //1: mieng, 2: thi, 3: tbhk
            //public string[] min15 { get; set; }
            //public string[] min45 { get; set; }
            public string[] score { get; set; }

        }


        public struct scoreTable_Year
        {
            public int subjectID { get; set; }
            public string subject { get; set; }
            //public string avg_1 { get; set; }
            //public string avg_2 { get; set; }
            public string[] avg { get; set; }
            public string avg_Year { get; set; }

        }
        //commnand
        public ICommand navBack { get; }
        public ICommand navEdit { get; }
        public ICommand changeScoreTb { get; }
        public ICommand EditCommand { get; }


        //List
        private List<scoreTable> _scoreTableList = new List<scoreTable>();
        public List<scoreTable> scoreTableList { get => _scoreTableList; set { _scoreTableList = value; OnPropertyChanged(); } }
        private List<scoreTable_Year> _scoreTableList_Year = new List<scoreTable_Year>();
        public List<scoreTable_Year> scoreTableList_Year { get => _scoreTableList_Year; set { _scoreTableList_Year = value; OnPropertyChanged(); } }



        //variable binding
        private string _studentName;
        public string studentName { get => _studentName; set { _studentName = value; OnPropertyChanged(); } }
        private string _className;
        public string className { get => _className; set { _className = value; OnPropertyChanged(); } }
        private string _formTeacher;
        public string formTeacher { get => _formTeacher; set { _formTeacher = value; OnPropertyChanged(); } }

        private string _schoolYear;
        public string schoolYear { get => _schoolYear; set { _schoolYear = value; OnPropertyChanged(); } }

        //0: ca nam, 1: hk1, 2: hk2
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

        private int _cbbSelected;
        public int cbbSelected { get => _cbbSelected; set { _cbbSelected = value; OnPropertyChanged(); } }

        public struct Subject
        {
            public string name { get; set; }
        }
        //private List<Subject> _SubjectList = new List<Subject>();
        //public List<Subject> SubjectList { get => _SubjectList; set { _SubjectList = value; OnPropertyChanged(); } }
        private ObservableCollection<MONHOC> _subjectList;
        public ObservableCollection<MONHOC> subjectList { get => _subjectList; set { _subjectList = value; OnPropertyChanged(); } }

        private ObservableCollection<THI> _testList;
        public ObservableCollection<THI> testList { get => _testList; set { _testList = value; OnPropertyChanged(); } }
        private ObservableCollection<TBMON> _avgList;
        public ObservableCollection<TBMON> avgList { get => _avgList; set { _avgList = value; OnPropertyChanged(); } }


        public EditScoreViewModel(NavigationStore navigationStore)
        {
            SemesterScoreViewModel.selectedStudent selectedStuddent = SemesterScoreViewModel.CurrentSelected;

            //navigate
            navBack = new NavigationCommand<ScoreDetailViewModel>(navigationStore, () => new ScoreDetailViewModel(navigationStore));

            //infor
            studentName = DataProvider.Ins.DB.HOCSINHs.Where(x => x.MAHS == selectedStuddent.mahs && x.DELETED == false).FirstOrDefault().HOTEN;
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
            changeScoreTb = new RelayCommand<EditScore>((p) => { return true; }, (p) => _cbbChanged(p, selectedStuddent.mahs, selectedStuddent.malop));

            //Edit
            EditCommand = new RelayCommand<DataGrid>((p) =>
            {
                return true;

            }, (p) => _EditSave(p, selectedStuddent.mahs, selectedStuddent.malop));
        }
        void _EditSave(object p, int mahs, int classID)
        {
            int flagRank = 0;  //0: gioi, 1: kha, 2: tb, 3: yeu

            //Save Scoretable
            int index15 = 1, index45 = 4;
            foreach (scoreTable sc in scoreTableList)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (sc.score[i] == null || sc.score[i] == "")
                        sc.score[i] = "0.0";
                    if (sc.score[i] =="KĐ")
                        sc.score[7] = "KĐ";
                    else sc.score[7] = "Đ";
                }

                if (sc.subjectID != 13)
                    for (int i = 1; i < 7; i++)
                        try { decimal.Parse(sc.score[i]); }
                        catch
                        { return; }
                foreach (THI thi in testList)
                {
                    if (thi.MAMH == sc.subjectID && thi.MAHS == mahs && thi.MALOP == classID && thi.HOCKY == semester)
                    {
                        switch (thi.MALD)
                        {
                            case 1:
                                thi.DIEM =  sc.score[0];
                                break;

                            case 2:
                                thi.DIEM = sc.score[index15];
                                index15++;
                                break;
                            case 3:
                                thi.DIEM = sc.score[index45];
                                index45++;
                                break;
                            case 4:
                                thi.DIEM = sc.score[6];
                                break;
                        }
                    }

                }

                DataProvider.Ins.DB.SaveChanges();
                decimal tempScore = 0;
                if (sc.subjectID!=13)
                {
                    foreach (THI thi in testList)
                        if (thi.MAMH == sc.subjectID && thi.MAHS == mahs && thi.MALOP == classID && thi.HOCKY == semester)
                            switch (thi.MALD)
                            {
                                case 1:
                                    tempScore += decimal.Parse(thi.DIEM);
                                    break;

                                case 2:
                                    tempScore += decimal.Parse(thi.DIEM);
                                    break;
                                case 3:
                                    tempScore += decimal.Parse(thi.DIEM)*2;
                                    break;
                                case 4:
                                    tempScore += decimal.Parse(thi.DIEM)*3;
                                    break;
                            }

                    sc.score[7] =  String.Format("{0:0.00}", (tempScore/11));
                    //    decimal.Parse(sc.score[1]) + decimal.Parse(sc.score[2]) + decimal.Parse(sc.score[3]) +
                    //    decimal.Parse(sc.score[4])*2 + decimal.Parse(sc.score[5]) * 2 + decimal.Parse(sc.score[6])*3)/11));
                    var tempTBHK = DataProvider.Ins.DB.TBMONs.Where(x => x.MAHS == mahs && x.MALOP==classID && x.MAMH == sc.subjectID  && x.HOCKY == semester&& x.DELETED == false).FirstOrDefault();
                    if (tempTBHK != null)
                        tempTBHK.DTB =  sc.score[7];
                }
                else
                {
                    var tempTBHK = DataProvider.Ins.DB.TBMONs.Where(x => x.MAHS == mahs && x.MALOP==classID && x.MAMH == sc.subjectID  && x.HOCKY == semester&& x.DELETED == false).FirstOrDefault();
                    if(tempTBHK != null) 
                    tempTBHK.DTB =  "Đ";
                }

                index15 = 1; index45 = 4;

            }
            foreach (scoreTable sc in scoreTableList)
            {
                if (sc.subjectID == 1 || sc.subjectID == 5 || sc.subjectID == 8)
                {
                    if (float.Parse(sc.score[7]) < 8 && flagRank < 1)
                        flagRank = 1;
                    if (float.Parse(sc.score[7]) < 6.5 && flagRank < 2)
                        flagRank = 2;
                    if (float.Parse(sc.score[7]) < 5 && flagRank < 3)
                        flagRank = 3;
                }
                else if (sc.subjectID != 13)
                {
                    if (float.Parse(sc.score[7]) < 6.5 && flagRank < 1)
                        flagRank = 1;
                    if (float.Parse(sc.score[7]) < 5 && flagRank < 2)
                        flagRank = 2;
                    if (float.Parse(sc.score[7]) < 3.5 && flagRank < 3)
                        flagRank = 3;
                }
            }

            //Save Achivement
            var tempTT = DataProvider.Ins.DB.THANHTICHes.Where(x => x.MAHS ==  mahs && x.MALOP== classID && x.DELETED == false).FirstOrDefault();
            if (tempTT != null)
            {

                tempTT.TENTT = achievements;
            }
            else
            {
                THANHTICH tt = new THANHTICH();
                tt.TENTT = achievements;
                tt.MAHS = mahs;
                tt.MALOP =  classID;
                tt.DELETED = false;
                DataProvider.Ins.DB.THANHTICHes.Add(tt);
            }

            //Save Commnent
            var tempNX = DataProvider.Ins.DB.NHANXETs.Where(x => x.MAHS == mahs && x.MALOP==classID && x.HOCKY == semester &&x.DELETED == false).FirstOrDefault();
            if (tempNX!=null)
                tempNX.NHANXET1 = comment;
            else
            {
                NHANXET nx = new NHANXET();
                nx.NHANXET1 = comment;
                nx.MAHS = mahs;
                nx.MALOP =  classID;
                nx.HOCKY = semester;
                nx.DELETED = false;
                DataProvider.Ins.DB.NHANXETs.Add(nx);
            }

            DataProvider.Ins.DB.SaveChanges();

            // Save DTK - Rank  - Conduct
            decimal tempTB = 0;
            var tempKQ = DataProvider.Ins.DB.KETQUAs.Where(x => x.MAHS == mahs && x.MALOP==classID && x.HOCKY == semester && x.DELETED == false).FirstOrDefault();
            if (tempKQ!=null)
            {
                foreach (TBMON tbm in avgList)
                {
                    if (tbm.MAHS == mahs && tbm.MALOP == classID && tbm.HOCKY == semester && tbm.MAMH !=13)
                        tempTB += decimal.Parse(tbm.DTB);
                }
                tempKQ.DTB = (decimal)(tempTB/12);
                if (tempKQ.DTB>=(decimal)8 && flagRank == 0)
                    tempKQ.XEPLOAI = "Giỏi";
                if (tempKQ.DTB>=(decimal)6.5 && tempKQ.DTB <= (decimal)8 && flagRank == 1)
                    tempKQ.XEPLOAI = "Khá";
                if (tempKQ.DTB>=(decimal)5 && tempKQ.DTB <= (decimal)6.5  && flagRank == 2)
                    tempKQ.XEPLOAI = "Trung Bình";
                if (tempKQ.DTB<=(decimal)5 && flagRank==3) tempKQ.XEPLOAI = "Yếu";
                tempKQ.HANHKIEM = conduct;
            }

            DataProvider.Ins.DB.SaveChanges();
        }
        void _cbbChanged(EditScore p, int n, int m)
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
        void _UpdateScoreTable_Year(int mahs, int classID)
        {
            scoreTableList_Year.Clear();
            foreach (MONHOC mh in subjectList)
            {
                scoreTable_Year sc = new scoreTable_Year();
                sc.avg = new string[2];
                sc.subject = mh.TENMH;
                sc.subjectID = mh.MAMH;
                foreach (TBMON tbm in avgList)
                {
                    if (tbm.MAMH == mh.MAMH  && tbm.MAHS  == mahs && tbm.MALOP == classID)
                    {
                        switch (tbm.HOCKY)
                        {
                            case 1:
                                sc.avg[0]= tbm.DTB;
                                break;
                            case 2:
                                sc.avg[1]= tbm.DTB;
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
                avgSemester =   String.Format("{0:0.00}", tempKQ.DTB);
                if (semester < Const.Semester)
                {
                    rank = tempKQ.XEPLOAI;
                    conduct=tempKQ.HANHKIEM;
                }
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
        void _UpdateScoreTable(int mahs, int classID)
        {
            scoreTableList.Clear();


            //string[] scoreTemp = Enumerable.Repeat("", 3).ToArray();
            //string[] scoreTemp45 = Enumerable.Repeat("", 3).ToArray();
            int index15 = 1, index45 = 4;

            foreach (MONHOC mh in subjectList)
            {
                //sc.otherScore = Enumerable.Repeat("0", 3).ToArray();
                //sc.min15 = Enumerable.Repeat("0", 3).ToArray();
                //sc.min45 = Enumerable.Repeat("0", 3).ToArray()];
                scoreTable sc = new scoreTable();
                //sc.otherScore = new string[3];
                //sc.min15 = new string[3];
                //sc.min45 = new string[2];
                sc.score = new string[8];
                //sc.min15[0] = "20";
                sc.subject = mh.TENMH;
                sc.subjectID = mh.MAMH;
                foreach (THI thi in testList)
                {
                    if (thi.MAMH == mh.MAMH && thi.HOCKY == semester && thi.MAHS  == mahs && thi.MALOP == classID)
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
                var tempTBHK = DataProvider.Ins.DB.TBMONs.Where(x => x.MAHS == mahs && x.MALOP==classID && x.MAMH == mh.MAMH  && x.HOCKY == semester&& x.DELETED == false).FirstOrDefault();
                if (tempTBHK!=null)
                    sc.score[7] = tempTBHK.DTB;
                //sc.min15_1 = scoreTemp[0];
                //sc.min15_2 = scoreTemp[1];
                //sc.min15_3 = scoreTemp[2];
                //sc.min45_1= scoreTemp45[0];
                //sc.min45_2= scoreTemp45[1];
                index15=1;
                //scoreTemp = Enumerable.Repeat("", 3).ToArray();
                index45=4;
                //scoreTemp45 = Enumerable.Repeat("", 3).ToArray();

                scoreTableList.Add(sc);
            }

            var tempKQ = DataProvider.Ins.DB.KETQUAs.Where(x => x.MAHS == mahs && x.MALOP==classID && x.HOCKY == semester && x.DELETED == false).FirstOrDefault();
            if (tempKQ!=null)
            {
                avgSemester = String.Format("{0:0.00}", tempKQ.DTB);
                if (semester  < Const.Semester)
                {
                    rank = tempKQ.XEPLOAI;
                    conduct=tempKQ.HANHKIEM;
                    switch (conduct)
                    {
                        case "Tốt":
                            cbbSelected = 0;
                            break;
                        case "Khá":
                            cbbSelected = 1;
                            break;
                        case "Trung bình":
                            cbbSelected = 2;
                            break;
                        case "Yếu":
                            cbbSelected = 3;
                            break;
                    }
                }

            }
            else avgSemester = "";
            rank = "";
            conduct = "";
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




