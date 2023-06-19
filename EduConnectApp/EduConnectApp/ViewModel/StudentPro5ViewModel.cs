using EduConnectApp.Commands;
using EduConnectApp.Model;
using EduConnectApp.Store;
using EduConnectApp.ViewUCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EduConnectApp.ViewModel
{
    public class StudentPro5ViewModel : BaseViewModel
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
        public string[] GTList { get; set; } = { "Nam", "Nữ" };

        public ICommand navBack { get; }
        public ICommand navEdit { get; }
        public ICommand navExport { get; }
        public ICommand DeleteCommand { get; }

        public StudentPro5ViewModel(NavigationStore navigationStore)
        {
            //navigate
            navBack = new NavigationCommand<ClassListViewModel>(navigationStore, () => new ClassListViewModel(navigationStore));
            navEdit = new NavigationCommand<EditStudentPro5ViewModel>(navigationStore, () => new EditStudentPro5ViewModel(navigationStore));
            navExport = new NavigationCommand<ExportPro5ViewModel>(navigationStore, () => new ExportPro5ViewModel(navigationStore));

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
            Lop = DataProvider.Ins.DB.LOPs.Where(x => x.MALOP == temp.MALOP && x.DELETED == false).SingleOrDefault().TENLOP;

            var PH = DataProvider.Ins.DB.PHUHUYNHs.Where(x => x.MAHS == ClassListViewModel.CurrentSelected.ID && x.DELETED == false).SingleOrDefault();
            TenCha = PH.HOTENBO;
            TenMe = PH.HOTENME;
            NgheCha = PH.NGHEBO;
            NgheMe = PH.NGHEME;
            SDTCha = PH.SDTBO;
            SDTMe = PH.SDTME;

            // Delete
            DeleteCommand = new RelayCommand<ClassListUC>((p) => { return true; }, (p) =>
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa học sinh này khỏi danh sách?", "Xác nhận!", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;

                studentSelected.DELETED = true;
                var tempHT = DataProvider.Ins.DB.HOCTAPs.Where(x => x.MAHS == studentSelected.MAHS && x.MALOP == ClassViewModel.CurrentSelected.ClassID && x.DELETED == false).FirstOrDefault();
                tempHT.DELETED = true;
                DataProvider.Ins.DB.SaveChanges();
            });
        }
    }
}
