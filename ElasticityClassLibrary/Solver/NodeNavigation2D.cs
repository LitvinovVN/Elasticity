namespace ElasticityClassLibrary.SolverNamespase
{
    /// <summary>
    /// Класс одномерной навигации по узлам
    /// </summary>
    public class NodeNavigation2D : NodeNavigation1D
    {
        /// <summary>
        /// Предыдущий узел по оси Y
        /// </summary>
        public Node NodeDown { get; set; }

        /// <summary>
        /// Последующий узел по оси Y
        /// </summary>
        public Node NodeUp { get; set; }
    }
}