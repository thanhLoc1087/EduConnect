using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static EduConnectApp.ViewModel.ClassListViewModel;

namespace EduConnectApp.ViewUCs
{
    /// <summary>
    /// Interaction logic for ClassListUC.xaml
    /// </summary>
    public partial class ClassListUC : UserControl
    {
        public ClassListUC()
        {
            InitializeComponent();
            //ObservableCollection<Member> members = new ObservableCollection<Member>();

            //members.Add(new Member { Number = "11", Name = "John Doe", Position = "Coach", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            //members.Add(new Member { Number = "12", Name = "Reza Alavi", Position = "Administrator", Email = "reza110@hotmail.com", Phone = "254-451-7893" });
            //members.Add(new Member { Number = "13", Name = "Dennis Castillo", Position = "Coach", Email = "deny.cast@gmail.com", Phone = "125-520-0141" });
            //members.Add(new Member { Number = "14", Name = "Gabriel Cox", Position = "Coach", Email = "coxcox@gmail.com", Phone = "808-635-1221" });
            //members.Add(new Member { Number = "15", Name = "Lena Jones", Position = "Manager", Email = "lena.offi@hotmail.com", Phone = "320-658-9174" });
            //members.Add(new Member { Number = "16", Name = "Benjamin Caliword", Position = "Administrator", Email = "beni12@hotmail.com", Phone = "114-203-6258" });
            //members.Add(new Member { Number = "17", Name = "Sophia Muris", Position = "Coach", Email = "sophi.muri@gmail.com", Phone = "852-233-6854" });
            //members.Add(new Member { Number = "18", Name = "Ali Pormand", Position = "Manager", Email = "alipor@yahoo.com", Phone = "968-378-4849" });
            //members.Add(new Member { Number = "19", Name = "Frank Underwood", Position = "Manager", Email = "frank@yahoo.com", Phone = "301-584-6966" });
            //members.Add(new Member { Number = "20", Name = "Saeed Dasman", Position = "Coach", Email = "saeed.dasi@hotmail.com", Phone = "817-320-5052" });
            //datag.ItemsSource = members;
        }


        //public class Member
        //{
        //    public string Number { get; set; }
        //    public string Name { get; set; }
        //    public string Position { get; set; }
        //    public string Email { get; set; }
        //    public string Phone { get; set; }
        //}
    }
}
