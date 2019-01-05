using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.TheoryOfElasticity
{
    /// <summary>
    /// Тензор напряжений
    /// </summary>
    public class StressTensor
    {
        public decimal Sigma11 { get; set; }
        public decimal Sigma12 { get; set; }
        public decimal Sigma13 { get; set; }

        public decimal Sigma21 { get; set; }
        public decimal Sigma22 { get; set; }
        public decimal Sigma23 { get; set; }

        public decimal Sigma31 { get; set; }
        public decimal Sigma32 { get; set; }
        public decimal Sigma33 { get; set; }

    }
}
