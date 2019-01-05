using ElasticityClassLibrary.Nodes;

namespace ElasticityClassLibrary.SolverNamespace
{
    /// <summary>
    /// Модельная задача
    /// </summary>
    public abstract class SolverTask
    {
        public NodeSet NodeSet { get; set; }
    }
}