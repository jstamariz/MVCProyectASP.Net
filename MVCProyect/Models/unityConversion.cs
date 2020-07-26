using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMeTieneJarto.Models
{
    public class unityConversion
    {
        public decimal val { get; set; }
        public decimal result { get; set; }
        public unityConversion(decimal val, string to)
        {
            this.val = val;
            if (to == "C")
            {
                this.result = ((val - 32) / (5/9));
            }
            else if (to == "F")
            {
                this.result = (val * (9 / 5) + 32);
            }
        }
    }
}