using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeInspectorSchedule.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentInfo : ContentPage
    {
        public AppointmentInfo(string id)
        {
            InitializeComponent();
            test.Text = id;
        }
    }
}