namespace ElasticityClassLibrary
{
    /// <summary>
    /// Узел сетки
    /// </summary>
    public class Node
    {
        public Coordinates<decimal> Coordinates { get; set; }

        public NodeLocationEnum NodeLocationEnum { get; set; }

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
            string result = $"Узел {GetStringIndexXYZ}. Координаты {Coordinates.ToString()}";

            if(PrevNodeX!=null)
            {
                result += $"\n\tPrevNodeX: {PrevNodeX.GetStringIndexXYZ}";
            }
            else
            {
                result += $"\n\tPrevNodeX: [-,-,-]";
            }

            if (PrevNodeY != null)
            {
                result += $"\n\tPrevNodeY: {PrevNodeY.GetStringIndexXYZ}";
            }
            else
            {
                result += $"\n\tPrevNodeY: [-,-,-]";
            }

            if (PrevNodeZ != null)
            {
                result += $"\n\tPrevNodeZ: {PrevNodeZ.GetStringIndexXYZ}";
            }
            else
            {
                result += $"\n\tPrevNodeZ: [-,-,-]";
            }

            if (NextNodeX != null)
            {
                result += $"\n\tNextNodeX: {NextNodeX.GetStringIndexXYZ}";
            }
            else
            {
                result += $"\n\tNextNodeX: [-,-,-]";
            }

            if (NextNodeY != null)
            {
                result += $"\n\tNextNodeY: {NextNodeY.GetStringIndexXYZ}";
            }
            else
            {
                result += $"\n\tNextNodeY: [-,-,-]";
            }

            if (NextNodeZ != null)
            {
                result += $"\n\tNextNodeZ: {NextNodeZ.GetStringIndexXYZ}";
            }
            else
            {
                result += $"\n\tNextNodeZ: [-,-,-]";
            }

            result += $"\n\tNodeLocationEnum: {NodeLocationEnum}";

            return result;
        }

        /// <summary>
        /// Строковое представление индексов узла
        /// </summary>
        public string GetStringIndexXYZ
        {
            get
            {
                return $"[{IndexX},{IndexY},{IndexZ}]";
            }            
        }
    }
}