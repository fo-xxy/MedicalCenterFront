using MedicalCenter.Views;
using System.Windows.Input;

namespace MedicalCenter
{
    public partial class AppShell : Shell
    {
        public ICommand LogoutCommand { get; }

        public AppShell()
        {
            InitializeComponent();

           
        }

       
    }
}
