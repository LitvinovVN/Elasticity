namespace ElasticityClassLibrary.SolverNamespase
{
    /// <summary>
    /// Класс одномерной навигации по узлам
    /// </summary>
    public class NodeNavigation1D : NodeNavigation
    {
        /// <summary>
        /// Предыдущий узел по оси X
        /// </summary>
        public Node NodeLeft { get; set; }

        /// <summary>
        /// Последующий узел по оси X
        /// </summary>
        public Node NodeRight { get; set; }
    }
}