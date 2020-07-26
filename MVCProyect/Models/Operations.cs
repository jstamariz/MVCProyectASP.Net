using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMeTieneJarto.Models
{
    public class Operations
    {
        public decimal result { get; set; }
        public Operations(int val1, int val2, string Opp)
        {
            if (Opp == "suma") this.result = val1 + val2;
            if (Opp == "multi") this.result = val1 * val2;
            if (Opp == "res") this.result = val1 - val2;
            if (Opp == "div")
            {
                if (val2 != 0)
                {
                    this.result = val1 / val2;
                }
                else
                {
                    this.result = 0;
                }
            }
        }
    }
}