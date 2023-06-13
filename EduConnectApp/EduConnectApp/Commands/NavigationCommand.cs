using EduConnectApp.Store;
using EduConnectApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnectApp.Commands
{
    public class NavigationCommand<TViewModel> : CommandBase
    where TViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        public NavigationCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }
        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
    public class NavigationCommandWithCondition<TViewModel> : CommandBase
    where TViewModel : BaseViewModel
    {
        private readonly Predicate<TViewModel> _canExecute;
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        public NavigationCommandWithCondition(Predicate<TViewModel> canExecute, NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _canExecute = canExecute;
        }
        public override bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute == null ? true : _canExecute((TViewModel)parameter);
            }
            catch
            {
                return true;
            }
        }
        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }

    }
}
