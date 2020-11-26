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
    public partial class MetricsReport : ContentPage
    {
        public MetricsReport()
        {
            InitializeComponent();
        }

        private async void test_Clicked(object sender, EventArgs e)
        {
            Metrics metrics = new Metrics();
            var list = await metrics.RealtorMetrics();
            foreach (var l in list)
            {
                testlabel.Text += l.ToString() + " ";
            }
        }
    }
}