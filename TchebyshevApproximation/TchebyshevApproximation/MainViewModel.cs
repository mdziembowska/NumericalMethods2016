using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TchebyshevApproximation
{
    using OxyPlot;
    using System.ComponentModel;

    class MainViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MainViewModel()
        {
            this.Title = "Approximation for Chebyshev's polynomials";
            this.BaseFunction = new List<DataPoint>();
            this.ApproximateFunction = new List<DataPoint>();

            for (double x = -2.0; x < 5.0; x += 0.05)
            {
                this.BaseFunction.Add(new DataPoint(x, Functions.linear(x)));
                this.ApproximateFunction.Add(new DataPoint(x, Functions.sinus(x)));
               
            }
           
        }

        public string Title { get; private set; }
        ComboBoxItemList list = new ComboBoxItemList();

        public IList<DataPoint> BaseFunction { get; private set; }
        public IList<DataPoint> ApproximateFunction { get; private set; }
        

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
