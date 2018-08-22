namespace ElasticityClassLibrary.SolverNamespase
{
    /// <summary>
    /// Отдельный компонент левой части уравнения
    /// (одно из неизвестных уравнения)
    /// </summary>
    public class EquationElement
    {
        /// <summary>
        /// Ссылка на узел сетки, для которого
        /// рассчитываются значения
        /// </summary>
        public Node Node { get; set; }

        /// <summary>
        /// Коэффициент
        /// </summary>
        public decimal Coefficient { get; set; }
    }
}