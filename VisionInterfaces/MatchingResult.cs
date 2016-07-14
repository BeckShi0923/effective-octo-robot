using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace VisionInterfaces
{
    public class MatchingResult
    {
        public double[] Row { get; set; }
        public double[] Column { get; set; }
        public double[] Score { get; set; }
        public double[] Angle { get; set; }
        
    }
}
