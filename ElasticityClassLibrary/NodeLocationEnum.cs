namespace ElasticityClassLibrary.Infrastructure
{
    /// <summary>
    /// Перечисление типов расположения узлов на сетке
    /// </summary>
    public enum NodeLocationEnum
    {
        /// <summary>
        /// Внутренний узел
        /// </summary>
        Internal,
        /// <summary>
        /// Узел на поверхности
        /// </summary>
        OnTheSurface,
        /// <summary>
        /// Узел, расположенный за контуром сетки
        /// (фиктивный узел)
        /// </summary>
        Outer
    }
}