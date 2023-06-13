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
using System.Windows.Controls;
using System.Windows.Input;

namespace EduConnectApp.ViewModel
{
    public class TeacherPro5ViewModel :BaseViewModel
    {
        public ICommand navEdit { get; }

        private string _LopCN;
        public string LopCN { get => _LopCN; set { _LopCN = value; OnPropertyChanged(); } }
        private string _LopGD;
        public string LopGD { get => _LopGD; set { _LopGD = value; OnPropertyChanged(); } }
        private string _MonGD;
        public string MonGD { get => _MonGD; set { _MonGD = value; OnPropertyChanged(); } }
        private string _To;
        public string To { get => _To; set { _To = value; OnPropertyChanged(); } }
        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }
        private string _ID;
        public string ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }
        private string _HoTen;
        public string HoTen { get => _HoTen; set { _HoTen = value; OnPropertyChanged(); } }
        private string _NgaySinh;
        public string NgaySinh { get => _NgaySinh; set { _NgaySinh = value; OnPropertyChanged(); } }
        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        private string _GioiTinh;
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        public TeacherPro5ViewModel(NavigationStore navigationStore) 
        {
            navEdit = new NavigationCommand<EditTeacherPro5ViewModel>(navigationStore, () => new EditTeacherPro5ViewModel(navigationStore));

            if (!Const.IsAdmin)
            {
                Title = "Mã GV";
                ID = Const.KeyID.ToString();
                var temp = DataProvider.Ins.DB.GIAOVIENs.Where(x => x.MAGV == Const.KeyID && x.DELETED == false).FirstOrDefault();
                HoTen = temp.HOTEN;
                DateTime dateTime = (DateTime)temp.NTNS;
                NgaySinh = dateTime.ToString("dd/MM/yyyy");
                DiaChi = temp.DIACHI;
                SDT = temp.SDT;
                if (temp.GIOITINH == true)
                    GioiTinh = "Nữ";
                else
                    GioiTinh = "Nam";
                Email = temp.EMAIL;

                var MaTo = temp.MATO;
                To = DataProvider.Ins.DB.TO1.Where(x => x.MATO == MaTo && x.DELETED == false).FirstOrDefault().TENTO.ToString();

                LopCN = DataProvider.Ins.DB.LOPs.Where(x => x.GVCN == temp.MAGV && x.DELETED == false).FirstOrDefault().TENLOP.ToString();

                var GD = DataProvider.Ins.DB.GIANGDAYs.Where(x => x.MAGV == Const.KeyID && x.DELETED == false).ToArray();
                int[] MaMonGD = new int[GD.Length];
                int[] MaLopGD = new int[GD.Length];


                string[] TenMonGD = new string[MaMonGD.Length];
                string[] TenLopGD = new string[MaLopGD.Length];

                for (int i = 0; i < GD.Length; i++)
                {
                    int k = GD[i].MAMH;
                    int j = GD[i].MALOP;
                    TenMonGD[i] = DataProvider.Ins.DB.MONHOCs.Where(x=>x.MAMH == k && x.DELETED == false).FirstOrDefault().TENMH.ToString();
                    TenLopGD[i] = DataProvider.Ins.DB.LOPs.Where(x=>x.MALOP == j && x.DELETED == false).FirstOrDefault().TENLOP.ToString();
                }    
                TenMonGD = TenMonGD.Distinct().ToArray();
                TenLopGD = TenLopGD.Distinct().ToArray();

                MonGD = string.Join(" - ", TenMonGD);
                LopGD = string.Join("\t", TenLopGD);
            }
            else
            {
                Title = "Mã Admin";
                ID = Const.KeyID.ToString();
                var temp = DataProvider.Ins.DB.ADMINs.Where(x => x.MAAD == Const.KeyID && x.DELETED == false).FirstOrDefault();
                HoTen = temp.TENAD;
            }    
        }
    }
}
