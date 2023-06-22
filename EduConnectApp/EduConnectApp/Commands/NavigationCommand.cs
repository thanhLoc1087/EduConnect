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
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        private readonly Predicate<TViewModel> _condition;

        public NavigationCommandWithCondition(Predicate<TViewModel> condition, NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _condition = condition;
        }

        public override void Execute(object parameter)
        {
            TViewModel viewModel = _createViewModel();

            if (_condition(viewModel))
            {
                _navigationStore.CurrentViewModel = viewModel;
            }
        }
    }



}
