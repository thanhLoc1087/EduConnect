using EduConnectApp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnectApp.ViewModel
{
    public class EditScoreViewModel : BaseViewModel
    {
        public struct Subject
        {
            public string name {  get; set; }
        }
        private List<Subject> _SubjectList = new List<Subject>();
        public List<Subject> SubjectList { get => _SubjectList; set { _SubjectList = value; OnPropertyChanged(); } }
        public EditScoreViewModel(NavigationStore navigationStore)
        {
            SubjectList.Add(new Subject { name = "Toán"});
            SubjectList.Add(new Subject { name = "Lý" });
            SubjectList.Add(new Subject { name = "Hóa" });
        }
    }
}
