using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Contoso
{
    public sealed partial class Report : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<ReportViewModel> Reports { get; set; } = new ObservableCollection<ReportViewModel>();

        public Report()
        {
            this.InitializeComponent();

            var report5 = new ReportViewModel { Name = "Initial", Title = "Synergistically initiate interdependent web-readiness", Date = DateTime.Now };
            report5.Data.Add("Budget", 16000M);

            var report1 = new ReportViewModel { Name = "Primary", Title = "Energistically optimize cross-media products" };
            report1.Data.Add("Budget", 6000M);

            var report2 = new ReportViewModel { Name = "Secondary", Title = "Rapidiously extend orthogonal mindshare" };
            report2.Data.Add("Budget", 5000M);

            var report3 = new ReportViewModel { Name = "Tertiary", Title = "Continually parallel task next-generation resources" };
            report3.Data.Add("Budget", 7000M);

            var report4 = new ReportViewModel { Name = "Results", Title = "Phosfluorescently unleash effective portals" };
            report4.Data.Add("Budget", 2000000M);

            this.Reports.Add(report1);
            this.Reports.Add(report2);
            this.Reports.Add(report3);
            this.Reports.Add(report4);
            this.Reports.Add(report5);
        }

        private ReportViewModel selectedReport;

        public ReportViewModel SelectedReport
        {
            get { return selectedReport; }
            set
            {
                if (value != selectedReport)
                {
                    selectedReport = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string GetFormattedText(ReportViewModel data)
        {
            if (data == null)
            {
                return null;
            }

            return data.Data["Budget"].ToString("C");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string property = "")
        {
            if (!string.IsNullOrEmpty(property))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
