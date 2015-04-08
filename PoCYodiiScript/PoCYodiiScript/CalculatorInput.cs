using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PoCYodiiScript
{
    [ComVisible( true )]
    public class CalculatorInput : ICalculatorInputService
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }
}
