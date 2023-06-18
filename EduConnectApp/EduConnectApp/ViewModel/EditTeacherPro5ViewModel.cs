using EduConnectApp.Commands;
using EduConnectApp.Model;
using EduConnectApp.Store;
using EduConnectApp.ViewUCs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EduConnectApp.ViewModel
{
    public class EditTeacherPro5ViewModel:BaseViewModel
    {
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
        private string _Ava;
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }
        public string[] GTList { get; set; } = { "Nam", "Nữ" };
        public ICommand EditCommand { get; set; }
        public ICommand UpdateAva { get; set; }
        public ICommand ReloadAva { get; set; }
        public ICommand navBack { get; set; }
        public EditTeacherPro5ViewModel( NavigationStore navigationStore) {

            //Navigate
            navBack = new NavigationCommand<TeacherPro5ViewModel>(navigationStore, () => new TeacherPro5ViewModel(navigationStore));

            //Update Avatar
            UpdateAva = new RelayCommand<ImageBrush>((p) => { return true; },(p) => _updateAva(p)); ;

            //Reload Avatar in MainWindow
            ReloadAva = new RelayCommand<MainWindow>((p) => true, (p) =>
            {
                if (Ava != "")
                {
                    p.ava.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(Ava);
                }
            });

            if (!Const.IsAdmin)
            {
                Title = "Mã GV";
                ID = Const.KeyID.ToString();
                var temp = DataProvider.Ins.DB.GIAOVIENs.Where(x => x.MAGV == Const.KeyID && x.DELETED == false).FirstOrDefault();
                HoTen = temp.HOTEN;
                NgaySinh = temp.NTNS.ToString();
                DiaChi = temp.DIACHI;
                SDT = temp.SDT;
                if (temp.GIOITINH == true)
                    GioiTinh = "Nữ";
                else
                    GioiTinh = "Nam";
                Email = temp.EMAIL;
                Ava = temp.AVA;
            }
            else
            {
                Title = "Mã Admin";
                ID = Const.KeyID.ToString();
                var temp = DataProvider.Ins.DB.ADMINs.Where(x => x.MAAD == Const.KeyID && x.DELETED == false).FirstOrDefault();
                HoTen = temp.TENAD;
                Ava = temp.AVA;
            }

            EditCommand = new RelayCommand<object>((p)=> 
            { 
                if (string.IsNullOrEmpty(HoTen) || string.IsNullOrEmpty(NgaySinh) ||  string.IsNullOrEmpty(GioiTinh) || string.IsNullOrEmpty(SDT) || string.IsNullOrEmpty (Email))
                {
                    MessageBox.Show("Bạn phải điền đầy đủ thông tin!");
                    return false;
                }
                return true;
            }, (p)=>
            {
                var usr = DataProvider.Ins.DB.GIAOVIENs.Where(x => x.MAGV == Const.KeyID && x.DELETED != true).FirstOrDefault();
                usr.HOTEN = HoTen;
                usr.NTNS = DateTime.Parse(NgaySinh);
                usr.SDT = SDT;
                usr.DIACHI = DiaChi;
                usr.EMAIL = Email;
                if (GioiTinh == "Nam")
                    usr.GIOITINH = false;
                else usr.GIOITINH = true;
                usr.AVA = Ava;

                MessageBox.Show("Lưu thông tin thành công!");
                DataProvider.Ins.DB.SaveChanges();

            });
        }

        void _updateAva(ImageBrush p)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";
            if (open.ShowDialog() == true)
            {
                var linkImg = open.FileName;
                Uri fileURI = new Uri(linkImg, UriKind.Relative);
                p.ImageSource = new BitmapImage(fileURI); 
                Ava = linkImg.ToString();
            }
        }
    }
}
