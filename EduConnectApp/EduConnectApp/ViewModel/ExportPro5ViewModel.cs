using EduConnectApp.Commands;
using EduConnectApp.Model;
using EduConnectApp.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using static EduConnectApp.ViewModel.SemesterScoreViewModel;

namespace EduConnectApp.ViewModel
{

    public class ExportPro5ViewModel : BaseViewModel
    {
        private string _TenCha;
        public string TenCha { get => _TenCha; set { _TenCha = value; OnPropertyChanged(); } }
        private string _NgheCha;
        public string NgheCha { get => _NgheCha; set { _NgheCha = value; OnPropertyChanged(); } }
        private string _SDTCha;
        public string SDTCha { get => _SDTCha; set { _SDTCha = value; OnPropertyChanged(); } }
        private string _TenMe;
        public string TenMe { get => _TenMe; set { _TenMe = value; OnPropertyChanged(); } }
        private string _NgheMe;
        public string NgheMe { get => _NgheMe; set { _NgheMe = value; OnPropertyChanged(); } }
        private string _SDTMe;
        public string SDTMe { get => _SDTMe; set { _SDTMe = value; OnPropertyChanged(); } }
        private string _MaHS;
        public string MaHS { get => _MaHS; set { _MaHS = value; OnPropertyChanged(); } }
        private string _Lop;
        public string Lop { get => _Lop; set { _Lop = value; OnPropertyChanged(); } }
        private string _ChinhSach;
        public string ChinhSach { get => _ChinhSach; set { _ChinhSach = value; OnPropertyChanged(); } }
        private string _HoTen;
        public string HoTen { get => _HoTen; set { _HoTen = value; OnPropertyChanged(); } }
        private string _NgaySinh;
        public string NgaySinh { get => _NgaySinh; set { _NgaySinh = value; OnPropertyChanged(); } }
        private string _NoiSinh;
        public string NoiSinh { get => _NoiSinh; set { _NoiSinh = value; OnPropertyChanged(); } }
        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        private string _GioiTinh;
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }
        private string _DanToc;
        public string DanToc { get => _DanToc; set { _DanToc = value; OnPropertyChanged(); } }
        private string _TonGiao;
        public string TonGiao { get => _TonGiao; set { _TonGiao = value; OnPropertyChanged(); } }
        private string _Ava;
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }
        private string _GVCN;
        public string GVCN { get => _GVCN; set { _GVCN = value; OnPropertyChanged(); } }
        private string _NhanXet;
        public string NhanXet { get => _NhanXet; set { _NhanXet = value; OnPropertyChanged(); } }
        public string[] GTList { get; set; } = { "Nam", "Nữ" };
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
        //List
        private ObservableCollection<MONHOC> _subjectList;
        public ObservableCollection<MONHOC> subjectList { get => _subjectList; set { _subjectList = value; OnPropertyChanged(); } }

        private ObservableCollection<THI> _testList;
        public ObservableCollection<THI> testList { get => _testList; set { _testList = value; OnPropertyChanged(); } }
        private ObservableCollection<TBMON> _avgList;
        public ObservableCollection<TBMON> avgList { get => _avgList; set { _avgList = value; OnPropertyChanged(); } }
        private List<scoreTable> _scoreTableList_1 = new List<scoreTable>();
        public List<scoreTable> scoreTableList_1 { get => _scoreTableList_1; set { _scoreTableList_1 = value; OnPropertyChanged(); } }
        private List<scoreTable> _scoreTableList_2 = new List<scoreTable>();
        public List<scoreTable> scoreTableList_2 { get => _scoreTableList_2; set { _scoreTableList_2 = value; OnPropertyChanged(); } }
        private List<scoreTable_Year> _scoreTableList_Year = new List<scoreTable_Year>();
        public List<scoreTable_Year> scoreTableList_Year { get => _scoreTableList_Year; set { _scoreTableList_Year = value; OnPropertyChanged(); } }
        private string _avgSemester;
        public string avgSemester { get => _avgSemester; set { _avgSemester = value; OnPropertyChanged(); } }
        private string _conduct;
        public string conduct { get => _conduct; set { _conduct = value; OnPropertyChanged(); } }
        private string _rank;
        public string rank { get => _rank; set { _rank = value; OnPropertyChanged(); } }
        private string _achievements;
        public string achievements { get => _achievements; set { _achievements = value; OnPropertyChanged(); } }
        public ICommand navBack { get; set; }
        public ExportPro5ViewModel(NavigationStore navigationStore)
        {
            //navigate
            navBack = new NavigationCommand<StudentPro5ViewModel>(navigationStore, () => new StudentPro5ViewModel(navigationStore));

            //list
            subjectList = new ObservableCollection<MONHOC>(DataProvider.Ins.DB.MONHOCs.Where(x => x.DELETED == false));
            testList = new ObservableCollection<THI>(DataProvider.Ins.DB.THIs.Where(x => x.DELETED == false));
            avgList = new ObservableCollection<TBMON>(DataProvider.Ins.DB.TBMONs.Where(x => x.DELETED == false));

            //value
            HOCSINH studentSelected = DataProvider.Ins.DB.HOCSINHs.Where(x => x.MAHS == ClassListViewModel.CurrentSelected.ID && x.DELETED == false).SingleOrDefault();
            MaHS = studentSelected.MAHS.ToString();
            HoTen = studentSelected.HOTEN;
            DateTime dateTime = (DateTime)studentSelected.NTNS;
            NgaySinh = dateTime.ToString("dd/MM/yyyy");
            if (studentSelected.GIOITINH == true)
                GioiTinh = "Nữ";
            else
                GioiTinh = "Nam";
            NoiSinh = studentSelected.NOISINH;
            DanToc = studentSelected.DANTOC;
            TonGiao = studentSelected.TONGIAO;
            SDT = studentSelected.SDT;
            DiaChi = studentSelected.DIACHI;
            ChinhSach = studentSelected.CHINHSACH;
            Ava = studentSelected.AVA;
            var temp = DataProvider.Ins.DB.HOCTAPs.Where(x => x.MAHS == ClassListViewModel.CurrentSelected.ID && x.DELETED == false).SingleOrDefault();
            var tempLop = DataProvider.Ins.DB.LOPs.Where(x => x.MALOP == temp.MALOP && x.DELETED == false).SingleOrDefault();
            Lop = tempLop.TENLOP;
            GVCN = DataProvider.Ins.DB.GIAOVIENs.Where(x => x.MAGV == tempLop.GVCN && x.DELETED == false).SingleOrDefault().HOTEN;
            NhanXet = DataProvider.Ins.DB.NHANXETs.Where(x => x.MAHS == ClassListViewModel.CurrentSelected.ID && x.DELETED == false && x.HOCKY == Const.Semester).SingleOrDefault().NHANXET1;

            var PH = DataProvider.Ins.DB.PHUHUYNHs.Where(x => x.MAHS == ClassListViewModel.CurrentSelected.ID && x.DELETED == false).SingleOrDefault();
            TenCha = PH.HOTENBO;
            TenMe = PH.HOTENME;
            NgheCha = PH.NGHEBO;
            NgheMe = PH.NGHEME;
            SDTCha = PH.SDTBO;
            SDTMe = PH.SDTME;
            _UpdateScoreTable1(studentSelected.MAHS, tempLop.MALOP);
            _UpdateScoreTable2(studentSelected.MAHS, tempLop.MALOP);
            _UpdateScoreTable_Year(studentSelected.MAHS, tempLop.MALOP);
        }
        void _UpdateScoreTable1(int mahs, int classID)
        {
            scoreTableList_1.Clear();

            string[] scoreTemp = Enumerable.Repeat("", 3).ToArray();
            string[] scoreTemp45 = Enumerable.Repeat("", 3).ToArray();
            int index = 0, index45 = 0;

            foreach (MONHOC mh in subjectList)
            {
                scoreTable sc = new scoreTable();
                sc.subject = mh.TENMH;
                foreach (THI thi in testList)
                {
                    if (thi.MAMH == mh.MAMH && thi.HOCKY == 1 && thi.MAHS == mahs && thi.MALOP == classID)
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
                var tempTBHK = DataProvider.Ins.DB.TBMONs.Where(x => x.MAHS == mahs && x.MALOP == classID && x.MAMH == mh.MAMH && x.HOCKY == 1 && x.DELETED == false).FirstOrDefault();
                if (tempTBHK != null)
                    sc.avg = tempTBHK.DTB;
                sc.min15_1 = scoreTemp[0];
                sc.min15_2 = scoreTemp[1];
                sc.min15_3 = scoreTemp[2];
                sc.min45_1 = scoreTemp45[0];
                sc.min45_2 = scoreTemp45[1];
                index = 0;
                scoreTemp = Enumerable.Repeat("", 3).ToArray();
                index45 = 0;
                scoreTemp45 = Enumerable.Repeat("", 3).ToArray();

                scoreTableList_1.Add(sc);
            }

            var tempKQ = DataProvider.Ins.DB.KETQUAs.Where(x => x.MAHS == mahs && x.MALOP == classID && x.HOCKY == 1 && x.DELETED == false).FirstOrDefault();
            if (tempKQ != null)
            {
                avgSemester = tempKQ.DTB.ToString();
                rank = tempKQ.XEPLOAI;
                conduct = tempKQ.HANHKIEM;
            }
            var tempTT = DataProvider.Ins.DB.THANHTICHes.Where(x => x.MAHS == mahs && x.MALOP == classID && x.DELETED == false).FirstOrDefault();
            if (tempTT != null)
            {
                achievements = tempTT.TENTT;
            }
        }
        void _UpdateScoreTable2(int mahs, int classID)
        {
            scoreTableList_2.Clear();

            string[] scoreTemp = Enumerable.Repeat("", 3).ToArray();
            string[] scoreTemp45 = Enumerable.Repeat("", 3).ToArray();
            int index = 0, index45 = 0;

            foreach (MONHOC mh in subjectList)
            {
                scoreTable sc = new scoreTable();
                sc.subject = mh.TENMH;
                foreach (THI thi in testList)
                {
                    if (thi.MAMH == mh.MAMH && thi.HOCKY == 2 && thi.MAHS == mahs && thi.MALOP == classID)
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
                var tempTBHK = DataProvider.Ins.DB.TBMONs.Where(x => x.MAHS == mahs && x.MALOP == classID && x.MAMH == mh.MAMH && x.HOCKY == 2 && x.DELETED == false).FirstOrDefault();
                if (tempTBHK != null)
                    sc.avg = tempTBHK.DTB;
                sc.min15_1 = scoreTemp[0];
                sc.min15_2 = scoreTemp[1];
                sc.min15_3 = scoreTemp[2];
                sc.min45_1 = scoreTemp45[0];
                sc.min45_2 = scoreTemp45[1];
                index = 0;
                scoreTemp = Enumerable.Repeat("", 3).ToArray();
                index45 = 0;
                scoreTemp45 = Enumerable.Repeat("", 3).ToArray();

                scoreTableList_2.Add(sc);
            }

            var tempKQ = DataProvider.Ins.DB.KETQUAs.Where(x => x.MAHS == mahs && x.MALOP == classID && x.HOCKY == 2 && x.DELETED == false).FirstOrDefault();
            if (tempKQ != null)
            {
                avgSemester = tempKQ.DTB.ToString();
                rank = tempKQ.XEPLOAI;
                conduct = tempKQ.HANHKIEM;
            }
            var tempTT = DataProvider.Ins.DB.THANHTICHes.Where(x => x.MAHS == mahs && x.MALOP == classID && x.DELETED == false).FirstOrDefault();
            if (tempTT != null)
            {
                achievements = tempTT.TENTT;
            }
        }
        void _UpdateScoreTable_Year(int mahs, int classID)
        {
            scoreTableList_Year.Clear();
            foreach (MONHOC mh in subjectList)
            {
                scoreTable_Year sc = new scoreTable_Year();
                sc.subject = mh.TENMH;
                foreach (TBMON tbm in avgList)
                {
                    if (tbm.MAMH == mh.MAMH && tbm.MAHS == mahs && tbm.MALOP == classID)
                    {
                        switch (tbm.HOCKY)
                        {
                            case 1:
                                sc.avg_1 = tbm.DTB;
                                break;
                            case 2:
                                sc.avg_2 = tbm.DTB;
                                break;
                            case 0:
                                sc.avg_Year = tbm.DTB;
                                break;
                        }
                    }
                }
                scoreTableList_Year.Add(sc);
            }

            var tempKQ = DataProvider.Ins.DB.KETQUAs.Where(x => x.MAHS == mahs && x.MALOP == classID && x.HOCKY == 3 && x.DELETED == false).FirstOrDefault();
            if (tempKQ != null)
            {
                avgSemester = tempKQ.DTB.ToString();
                rank = tempKQ.XEPLOAI;
                conduct = tempKQ.HANHKIEM;
            }
            var tempTT = DataProvider.Ins.DB.THANHTICHes.Where(x => x.MAHS == mahs && x.MALOP == classID && x.DELETED == false).FirstOrDefault();
            if (tempTT != null)
            {
                achievements = tempTT.TENTT;
            }
            var tempNX = DataProvider.Ins.DB.NHANXETs.Where(x => x.MAHS == mahs && x.MALOP == classID && x.HOCKY == 3 && x.DELETED == false).FirstOrDefault();
        }
    }
}
