using EduConnectApp.Store;
using EduConnectApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using EduConnectApp.Model;
using System.Windows.Threading;

namespace EduConnectApp.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        private string _NameUsr;
        public string NameUsr { get => _NameUsr; set { _NameUsr = value; OnPropertyChanged(); } }
        private string _Role;
        public string Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }
        public bool IsLoaded = false;
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand LoadedMainWd { get; set; }
        public ICommand navHome { get; }
        public ICommand navClass { get; }
        public ICommand navGrade { get; }
        public ICommand navStatistic { get; }
        public ICommand navContact { get; }
        public ICommand navSetting { get; }
        public ICommand navProfile { get; }
        public ICommand updateTabName { get; }
        public ICommand updateTabPro5 { get; }
        public ICommand update2 { get; }
        public ICommand selected { get; }
        public ICommand unSelected { get; }

        public ICommand navClass1 { get; }

        public ICommand navInputScore { get; }
        public ICommand navEditScore { get; }
        public ICommand LogOut { get; }
        private string _Ava;
        public string Ava
        {
            get => _Ava;
            set
            {
                _Ava = value; OnPropertyChanged();
            }
        }
        public MainViewModel(NavigationStore navigationStore)
        {
            LoadedMainWd = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                if (p == null)
                    return;
                p.Hide();

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;
                if (loginVM.IsLogin)
                {
                    if (Const.IsAdmin)
                    {
                        NameUsr = DataProvider.Ins.DB.ADMINs.Where(x => x.MAAD == Const.KeyID && x.DELETED != true).ToList()[0].TENAD.ToString();
                        Role = "ADMIN";
                    }
                    else
                    {
                        NameUsr = DataProvider.Ins.DB.GIAOVIENs.Where(x => x.MAGV == Const.KeyID && x.DELETED != true).ToList()[0].HOTEN.ToString();
                        Role = "GIÁO VIÊN";
                    }
                    Ava = Const.AVA;
                    if (Ava != null)
                    {
                        //string str = Const._localLink + Const.AVA.Remove(0, 2);
                        p.ava.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(Ava);
                    }
                    p.Show();
                }
                else 
                {
                    p.Close();
                }
            });

            selected = new RelayCommand<StackPanel>((p) => { return true; }, (p) => _UpdateSpn(p));
            unSelected = new RelayCommand<StackPanel>((p) => { return true; }, (p) => _UpdateSpn2(p));

            updateTabName = new RelayCommand<MainWindow>((p) => { return true; }, (p) => _UpdateTabName(p));
            updateTabPro5 = new RelayCommand<MainWindow>((p) => { return true; }, (p) => _UpdateTabPro5(p));

            navHome = new NavigationCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            navClass = new NavigationCommand<ClassViewModel>(navigationStore, () => new ClassViewModel(navigationStore));
            navGrade = new NavigationCommand<GradeViewModel>(navigationStore, () => new GradeViewModel(navigationStore));
            navStatistic = new NavigationCommand<StatisticViewModel>(navigationStore, () => new StatisticViewModel(navigationStore));
            navContact = new NavigationCommand<ContactViewModel>(navigationStore, () => new ContactViewModel(navigationStore));
            navSetting = new NavigationCommand<SettingViewModel>(navigationStore, () => new SettingViewModel(navigationStore));
            navProfile = new NavigationCommand<TeacherPro5ViewModel>(navigationStore, () => new TeacherPro5ViewModel(navigationStore));

            navClass1 = new NavigationCommand<ClassViewModel>(navigationStore, () => new ClassViewModel(navigationStore));
            navInputScore = new NavigationCommand<InputScoreViewModel>(navigationStore, () => new InputScoreViewModel(navigationStore));
            navEditScore = new NavigationCommand<EditScoreViewModel>(navigationStore, () => new EditScoreViewModel(navigationStore));
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;


            LogOut = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                if (MessageBox.Show("Do you want to LogOut?", "Log Out", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Const.IsAdmin = false;
                    Const.ID = new int();
                    Const.KeyID = new int();
                    p.Hide();
                    LoginWindow loginWindow = new LoginWindow();
                    var loginVM = loginWindow.DataContext as LoginViewModel;
                    loginVM.UserName = Const.USERNAME;
                    loginVM.Password = "";
                    loginVM.IsLogin = false;
                    loginWindow.ShowDialog();
                    p.rdBtn_Home.IsChecked= true;
                    if (loginVM.IsLogin)
                    {
                        if (Const.IsAdmin)
                        {
                            NameUsr = DataProvider.Ins.DB.ADMINs.Where(x => x.MAAD == Const.KeyID && x.DELETED != true).ToList()[0].TENAD.ToString();
                            Role = "ADMIN";
                        }
                        else
                        {
                            NameUsr = DataProvider.Ins.DB.GIAOVIENs.Where(x => x.MAGV == Const.KeyID && x.DELETED != true).ToList()[0].HOTEN.ToString();
                            Role = "GIÁO VIÊN";
                        }
                        Ava = Const.AVA;
                        if (Ava != null)
                        {
                            //string str = Const._localLink + Const.AVA.Remove(0, 2);
                            p.ava.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(Ava);
                        }
                        p.Show();
                    }
                    else
                    {
                        p.Close();
                    }
                }
            });
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

       void _UpdateTabName (MainWindow p)
        {
            if (p.rdBtn_Home.IsChecked== true) { p.txtbl_Tabname.Text = "TRANG CHỦ"; }
            if (p.rdBtn_Class.IsChecked== true) { p.txtbl_Tabname.Text = "LỚP"; }
            if (p.rdBtn_Grade.IsChecked== true) { p.txtbl_Tabname.Text = "ĐIỂM"; }
            if (p.rdBtn_Statistic.IsChecked== true) { p.txtbl_Tabname.Text = "THỐNG KÊ"; }
            if (p.rdBtn_Contact.IsChecked== true) { p.txtbl_Tabname.Text = "LIÊN LẠC"; }
            if (p.rdBtn_Setting.IsChecked== true) { p.txtbl_Tabname.Text = "CÀI ĐẶT"; }
        }

        void _UpdateTabPro5(MainWindow p)
        {
            p.txtbl_Tabname.Text = "TÀI KHOẢN";
            p.rdBtn_Home.IsChecked= false;
            p.rdBtn_Class.IsChecked= false;
            p.rdBtn_Grade.IsChecked= false;
            p.rdBtn_Statistic.IsChecked= false;
            p.rdBtn_Contact.IsChecked= false;
            p.rdBtn_Setting.IsChecked= false;
        }

        void _UpdateSpn(StackPanel p)
        {
            p.Margin = new Thickness(20, 0, 0, 0);
        }
        void _UpdateSpn2(StackPanel p)
        {
            p.Margin = new Thickness(0, 0, 0, 0);
        }
    }
}
