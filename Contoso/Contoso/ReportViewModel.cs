using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Contoso
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Title { get; set; }

        public Dictionary<string, decimal> Data { get; set; } = new Dictionary<string, decimal>();


        public DateTime Date { get; set; }

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
