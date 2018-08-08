namespace ElasticityClassLibrary
{
    /// <summary>
    /// Узел сетки
    /// </summary>
    public class Node
    {
        public Coordinates<decimal> Coordinates { get; set; }

        /// <summary>
        /// Индекс узла по оси X
        /// </summary>
        public int IndexX { get; set; }

        /// <summary>
        /// Индекс узла по оси Y
        /// </summary>
        public int IndexY { get; set; }

        /// <summary>
        /// Индекс узла по оси Z
        /// </summary>
        public int IndexZ { get; set; }

        ///////////// Навигационные свойства ///////////
        /// <summary>
        /// Следующий узел сетки по оси X
        /// </summary>
        public Node NextNodeX { get; set; }

        /// <summary>
        /// Следующий узел сетки по оси Y
        /// </summary>
        public Node NextNodeY { get; set; }

        /// <summary>
        /// Следующий узел сетки по оси Z
        /// </summary>
        public Node NextNodeZ { get; set; }

        /// <summary>
        /// Предыдущий узел сетки по оси X
        /// </summary>
        public Node PrevNodeX { get; set; }

        /// <summary>
        /// Предыдущий узел сетки по оси Y
        /// </summary>
        public Node PrevNodeY { get; set; }

        /// <summary>
        /// Предыдущий узел сетки по оси Z
        /// </summary>
        public Node PrevNodeZ { get; set; }

        public override string ToString()
        {
            string result = $"Узел [{IndexX},{IndexY},{IndexZ}]. Координаты {Coordinates.ToString()}";

            if(PrevNodeX!=null)
            {
                result += $"\n\tPrevNodeX: [{PrevNodeX?.IndexX},{PrevNodeX?.IndexY},{PrevNodeX?.IndexZ}]";
            }
            else
            {
                result += $"\n\tPrevNodeX: [-,-,-]";
            }

            if (NextNodeX != null)
            {
                result += $"\n\tNextNodeX: [{NextNodeX?.IndexX},{NextNodeX?.IndexY},{NextNodeX?.IndexZ}]";
            }
            else
            {
                result += $"\n\tNextNodeX: [-,-,-]";
            }

            return result;
        }
    }
}