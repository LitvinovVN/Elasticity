﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.SolverNamespase
{
    /// <summary>
    /// Значение в узле - известная постоянная величина
    /// </summary>
    public class NodeValueConst : NodeValue
    {
        public decimal Value { get; set; }
    }
}
