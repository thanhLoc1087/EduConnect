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
        public ICommand update { get; }
        public ICommand update2 { get; }
        public ICommand selected { get; }
        public ICommand unSelected { get; }

        public MainViewModel(NavigationStore navigationStore)
        {
            selected = new RelayCommand<StackPanel>((p) => { return true; }, (p) => _UpdateSpn(p));
            unSelected = new RelayCommand<StackPanel>((p) => { return true; }, (p) => _UpdateSpn2(p));

            update = new RelayCommand<TextBlock>((p) => { return true; }, (p) => _Update(p));

            navHome = new NavigationCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            navClass = new NavigationCommand<ClassListViewModel>(navigationStore, () => new ClassListViewModel(navigationStore));
            navGrade = new NavigationCommand<GradeViewModel>(navigationStore, () => new GradeViewModel(navigationStore));
            navStatistic = new NavigationCommand<StatisticViewModel>(navigationStore, () => new StatisticViewModel(navigationStore));
            navContact = new NavigationCommand<ContactViewModel>(navigationStore, () => new ContactViewModel(navigationStore));
            navSetting = new NavigationCommand<SettingViewModel>(navigationStore, () => new SettingViewModel(navigationStore));

            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

       void _Update (TextBlock p)
        {
            //p.Background= Brushes.Red;
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
