using LiveCharts;
using LiveCharts.Wpf;
using EduConnectApp.Store;
using EduConnectApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Data.Entity;
using System.Collections.ObjectModel;
using EduConnectApp.UserControlCustom;
using EduConnectApp.UCViewModel;
using System.Collections.Specialized;
using EduConnectApp.ViewUCs;

namespace EduConnectApp.ViewModel
{
    public class StatisticViewModel : BaseViewModel
    {
        public struct Student
        {
            public string Grade { get; set; }
            public string Class { get; set; }
            public string Name { get; set; }
            public string Range { get; set; }
            public string Score { get; set; }

        }

        private List<Student> _MyStudentList = new List<Student>();
        public List<Student> MyStudentList { get => _MyStudentList; set { _MyStudentList = value; OnPropertyChanged(); } }

        public int _cbbClassIndex;
        public int cbbClassIndex { get => _cbbClassIndex; set { _cbbClassIndex = value; OnPropertyChanged(); } }

        public string _cbbClassValue;
        public string cbbClassValue { get => _cbbClassValue; set { _cbbClassValue = value; OnPropertyChanged(); } }

        public string _lineValue;
        public string lineValue { get => _lineValue; set { _lineValue = value; OnPropertyChanged(); } }

        public int _cbbSubjectIndex;
        public int cbbSubjectIndex { get => _cbbSubjectIndex; set { _cbbSubjectIndex = value; OnPropertyChanged(); } }

        public ICommand cbb1Changed { get; set; }

        private ObservableCollection<LOP> _ClassList;
        public ObservableCollection<LOP> ClassList { get => _ClassList; set { _ClassList = value; OnPropertyChanged(); } }

        private List<string> _MyClassList = new List<string>();
        public List<string> MyClassList { get => _MyClassList; set { _MyClassList = value; OnPropertyChanged(); } }

        private ObservableCollection<KHOI> _GradeList;
        public ObservableCollection<KHOI> GradeList { get => _GradeList; set { _GradeList = value; OnPropertyChanged(); } }

        private ObservableCollection<HOCSINH> _StudentList;
        public ObservableCollection<HOCSINH> StudentList { get => _StudentList; set { _StudentList = value; OnPropertyChanged(); } }


        private List<string> _MyGradeList = new List<string>();
        public List<string> MyGradeList { get => _MyGradeList; set { _MyGradeList = value; OnPropertyChanged(); } }

        private ObservableCollection<KETQUA> _ResultList;
        public ObservableCollection<KETQUA> ResultList { get => _ResultList; set { _ResultList = value; OnPropertyChanged(); } }


        public int count;
        public int count1;
        public int classid;
        public string grade;

        public static SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; } = new string[3];

        public StatisticViewModel(NavigationStore navigationStore)
        {
            ClassList = new ObservableCollection<LOP>(DataProvider.Ins.DB.LOPs.Where(x => x.DELETED == false));
            GradeList = new ObservableCollection<KHOI>(DataProvider.Ins.DB.KHOIs.Where(x => x.DELETED == false));
            ResultList = new ObservableCollection<KETQUA>(DataProvider.Ins.DB.KETQUAs.Where(x => x.DELETED == false));
            StudentList = new ObservableCollection<HOCSINH>(DataProvider.Ins.DB.HOCSINHs.Where(x => x.DELETED == false));

            count = 0;
            count1 = 0;

            foreach(LOP lop in ClassList)
            {
                MyClassList.Add(lop.TENLOP);
            }

            foreach (KHOI khoi in GradeList)
            {
                MyGradeList.Add(khoi.TENKHOI);
            }

            Labels[0] = "";
            Labels[1] = "Học kỳ 1";
            Labels[2] = "Học kỳ 2";

            changeClassList("10A1");
            changeChart("10A1");

            cbb1Changed = new RelayCommand<ComboBox>((p) => { return true; }, (p) => _cbbClassChanged(p));
        }

        void _cbbClassChanged(ComboBox p)
        {
            if (p.SelectedIndex < 0) return;
            cbbClassIndex = p.SelectedIndex;
            cbbClassValue = p.SelectedValue.ToString();
            changeChart(cbbClassValue);
            changeClassList(cbbClassValue);
        }

        void changeClassList(string n)
        {
            MyStudentList.Clear();
            MyStudentList = new List<Student>();
            foreach(LOP lop in ClassList)
            {
                if (lop.TENLOP == n)
                {
                    classid = lop.MALOP;
                    foreach(KHOI khoi in GradeList)
                    {
                        if (khoi.MAKHOI == lop.MAKHOI)
                            grade = khoi.TENKHOI;
                    }
                }
            }

            foreach(KETQUA kq in ResultList)
            {
                if(kq.MALOP == classid)
                {
                    Student student = new Student();
                    student.Class = n;
                    student.Grade = grade;
                    student.Score = kq.DTB.ToString();
                    student.Range = kq.XEPLOAI;
                    if (kq.MALOP == classid)
                    {
                        foreach (HOCSINH hs in StudentList)
                        {
                            if (hs.MAHS == kq.MAHS)
                            {
                                student.Name = hs.HOTEN;
                            }
                        }

                    }
                    MyStudentList.Add(student);
                }

            }


        }


        void changeChart(string n)
        {
            count++;
            ChartValues<int> ExcellentNumber = new ChartValues<int>();
            ChartValues<int> GoodNumber = new ChartValues<int>();
            ChartValues<int> AverageNumber = new ChartValues<int>();
            ChartValues<int> PoorNumber = new ChartValues<int>();
            if (count == 1)
            {
                SeriesCollection = new SeriesCollection();
            }

            if (count > 1)
            {
                if (SeriesCollection.Chart != null)
                    SeriesCollection.Clear();
            }

            int[] excellentValues = new int[2];
            int[] goodValues = new int[2];
            int[] averageValues = new int[2];
            int[] poorValues = new int[2];

            ExcellentNumber.Clear();
            GoodNumber.Clear();
            AverageNumber.Clear();
            PoorNumber.Clear();
            int dem1 = 0;
            int dem2 = 0;
            int dem3 = 0;
            int dem4 = 0;

            for (int i = 0; i < 2; i++)
            {
                dem1 = 0;
                dem2 = 0;
                dem3 = 0;
                dem4 = 0;
                foreach (LOP lop in ClassList)
                {
                    if (lop.TENLOP == n)
                    {
                        foreach (KETQUA kq in ResultList)
                        {
                            if (lop.MALOP == kq.MALOP && kq.HOCKY == i + 1)
                            {
                                if (kq.XEPLOAI == "Giỏi")
                                    dem1++;
                                else if (kq.XEPLOAI == "Khá")
                                    dem2++;
                                else if (kq.XEPLOAI == "Trung bình")
                                    dem3++;
                                else
                                    dem4++;

                            }
                        }
                    }
                }
                excellentValues[i] = dem1;
                goodValues[i] = dem2;
                averageValues[i] = dem3;
                poorValues[i] = dem4;
            }

            ExcellentNumber.Add(0);
            GoodNumber.Add(0);
            AverageNumber.Add(0);
            PoorNumber.Add(0);

            foreach(int x in excellentValues)
            {
                ExcellentNumber.Add(x);
            }

            foreach (int x in goodValues)
            {
                GoodNumber.Add(x);
            }

            foreach (int x in averageValues)
            {
                AverageNumber.Add(x);
            }

            foreach (int x in poorValues)
            {
                PoorNumber.Add(x);
            }

            SeriesCollection.Add(new LineSeries()
            {
                Title = "Giỏi",
                Values = ExcellentNumber,
            });

            SeriesCollection.Add(new LineSeries()
            {
                Title = "Khá",
                Values = GoodNumber,
            });

            SeriesCollection.Add(new LineSeries()
            {
                Title = "Trung bình",
                Values = AverageNumber,
            });

            SeriesCollection.Add(new LineSeries()
            {
                Title = "Yếu",
                Values = PoorNumber,
            });
        }
    }
}

