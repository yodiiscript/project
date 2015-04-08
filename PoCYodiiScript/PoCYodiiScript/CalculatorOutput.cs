using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PoCYodiiScript
{
    [ComVisible( true )]
    public class CalculatorOutput : ICalculatorOutputService, INotifyPropertyChanged 
    {
        public string TheResult {get; private set;}
        public void SetResult( string result )
        {
            TheResult = result;

            PropertyChanged( this, new PropertyChangedEventArgs( "TheResult" ) );
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
