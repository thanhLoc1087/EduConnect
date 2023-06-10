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

namespace EduConnectApp.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {

        public bool IsLogin { get; set; } = false;

        private string _UserName;
        public string UserName { get { return _UserName; } set { _UserName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get { return _Password; } set { _Password = value; OnPropertyChanged(); } }
        public int ID { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public LoginViewModel()
        {
            UserName = "";
            Password = "";
            LoginCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Login(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }
        void Login(Object p)
        {
            if (p == null) return;
            var accCount = DataProvider.Ins.DB.LOGINs.Where(x => x.USERNAME == UserName && x.USERPASS == Password && x.DELETED == false).Count();

            if (accCount > 0)
            {
                IsLogin = true;
                var user = DataProvider.Ins.DB.LOGINs.Where(x => x.USERNAME == UserName && x.USERPASS == Password).ToList();
                ID = user[0].ID;
                Const.ID = ID;
                Const.USERNAME = UserName;
                
                if(DataProvider.Ins.DB.ADMINs.Where(x => x.MALOGIN == ID && x.DELETED == false && x.DELETED == false).Count() > 0)
                {
                    Const.IsAdmin = true;
                    Const.KeyID = DataProvider.Ins.DB.ADMINs.Where(x => x.MALOGIN == ID && x.DELETED == false).ToList()[0].MAAD;
                }
                else { Const.KeyID = DataProvider.Ins.DB.GIAOVIENs.Where(x => x.MALOGIN == ID && x.DELETED == false).ToList()[0].MAGV; }

                FrameworkElement window = GetWindowParent(p);
                var w = (window as Window);
                if (w != null)
                    w.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Wrong account or password");
            }
        }
        FrameworkElement GetWindowParent(Object u)
        {
            UserControl p = (u as UserControl);
            FrameworkElement parent = p;
            while (parent.Parent != null)
                parent = parent.Parent as FrameworkElement;
            return parent;
        }
    }
}
