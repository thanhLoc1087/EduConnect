using EduConnectApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnectApp.Model
{
    public class Const : BaseViewModel
    {
        public static int ID { get; set; }
        public static string USERNAME { get; set; }
        public static int KeyID { get; set; }
        public static bool IsAdmin { get; set; } = false;
        public static string AVA { get; set; }
        public static string SchoolYear { get { return "2022-2023"; }  }
        public static string School { get { return "THPT ĐỐC BINH KIỀU"; }  }
        public static int Semester { get { return 1; }  }
        public static string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
    }
}
