namespace ElasticityClassLibrary.SolverNamespase
{
    /// <summary>
    /// Класс одномерной навигации по узлам
    /// </summary>
    public class NodeNavigation3D : NodeNavigation2D
    {
        /// <summary>
        /// Предыдущий узел по оси Z
        /// </summary>
        public Node NodeBackward { get; set; }

        /// <summary>
        /// Последующий узел по оси Z
        /// </summary>
        public Node NodeForward { get; set; }
    }
}