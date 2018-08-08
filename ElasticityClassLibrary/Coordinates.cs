namespace ElasticityClassLibrary
{
    /// <summary>
    /// Координаты узла
    /// </summary>
    public class Coordinates<T>
    {
        /// <summary>
        /// Координата X
        /// </summary>
        public T CoordX { get; set; }

        /// <summary>
        /// Координата Y
        /// </summary>
        public T CoordY { get; set; }

        /// <summary>
        /// Координата Z
        /// </summary>
        public T CoordZ { get; set; }

        public override string ToString()
        {
            return $"[{CoordX:#####0.00000},{CoordY:#####0.00000},{CoordZ:#####0.00000}]";
        }
    }
}