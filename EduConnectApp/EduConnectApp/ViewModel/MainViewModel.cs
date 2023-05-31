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

namespace EduConnectApp.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
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

        public MainViewModel(NavigationStore navigationStore)
        {
            selected = new RelayCommand<StackPanel>((p) => { return true; }, (p) => _UpdateSpn(p));
            unSelected = new RelayCommand<StackPanel>((p) => { return true; }, (p) => _UpdateSpn2(p));

            updateTabName = new RelayCommand<MainWindow>((p) => { return true; }, (p) => _UpdateTabName(p));
            updateTabPro5 = new RelayCommand<MainWindow>((p) => { return true; }, (p) => _UpdateTabPro5(p));

            navHome = new NavigationCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            navClass = new NavigationCommand<ClassListViewModel>(navigationStore, () => new ClassListViewModel(navigationStore));
            navGrade = new NavigationCommand<GradeViewModel>(navigationStore, () => new GradeViewModel(navigationStore));
            navStatistic = new NavigationCommand<StatisticViewModel>(navigationStore, () => new StatisticViewModel(navigationStore));
            navContact = new NavigationCommand<ContactViewModel>(navigationStore, () => new ContactViewModel(navigationStore));
            navSetting = new NavigationCommand<SettingViewModel>(navigationStore, () => new SettingViewModel(navigationStore));
            navProfile = new NavigationCommand<TeacherPro5ViewModel>(navigationStore, () => new TeacherPro5ViewModel(navigationStore));

            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
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
