using EduConnectApp.UCViewModel;
using EduConnectApp.ViewModel;
using System;
using System.Collections.Generic;
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

namespace EduConnectApp.UserControlCustom
{
    /// <summary>
    /// Interaction logic for MemberCard.xaml
    /// </summary>
    public partial class MemberCard : UserControl
    {
        //public static readonly DependencyProperty TeacherProperty = DependencyProperty.Register(nameof(MemberCard.teacher), typeof(string),
        //   typeof(MemberCardViewModel),
        //   new FrameworkPropertyMetadata(default(MemberCardViewModel.Teacher), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public MemberCard()
        {
            InitializeComponent();
            //this.DataContext = this;
        }



        public MemberCardViewModel.Teacher MyProperty
        {
            get { return (MemberCardViewModel.Teacher)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(MemberCardViewModel.Teacher), typeof(MemberCard), new PropertyMetadata("TestValue"));



        //public MemberCardViewModel.Teacher teacher { 
        //    get => (MemberCardViewModel.Teacher)GetValue(TeacherProperty); 
        //    set => SetValue(TeacherProperty, value); }
    }
}
