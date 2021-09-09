using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEx3b
{
    class Valute
    {
       

        public Valute(int Nominal, string Name, double Value) {
            
            this.Nominal = Nominal;
            this.Name = Name;
            this.Value = Value;
        }
        
        public int Nominal { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }
}
