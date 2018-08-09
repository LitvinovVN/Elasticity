using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticityClassLibrary
{
    /// <summary>
    /// Сетка
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Узлы сетки
        /// </summary>
        public List<Node> Nodes { get; set; } = new List<Node>();

        /// <summary>
        /// Внутренние узлы
        /// </summary>
        public List<Node> NodesInternal
        {
            get
            {
                return GetNodes(NodeLocationEnum.Internal);
            }
        }

        /// <summary>
        /// Узлы на поверхности
        /// </summary>
        public List<Node> NodesOnTheSurface
        {
            get
            {
                return GetNodes(NodeLocationEnum.OnTheSurface);
            }
        }

        /// <summary>
        /// Законтурные узлы (фиктивные)
        /// </summary>
        public List<Node> NodesOuter
        {
            get
            {
                return GetNodes(NodeLocationEnum.Outer);
            }
        }

        /// <summary>
        /// Возвращает все узлы сетки с заданным расположением
        /// </summary>
        /// <param name="onTheSurface"></param>
        /// <returns></returns>
        public List<Node> GetNodes(NodeLocationEnum nodeLocation)
        {
            List<Node> nodes = new List<Node>();
            nodes = Nodes.Where(n => n.NodeLocationEnum == nodeLocation).ToList();
            return nodes;
        }

        /// <summary>
        /// Возвращает краткое описание сетки
        /// </summary>
        public string GetDescription
        {
            get
            {
                string result = $"Кол-во узлов: {Nodes.Count}, в т.ч. {NodesInternal.Count} внутренних, {NodesOnTheSurface.Count} на поверхности, {NodesOuter.Count} внешних (фиктивных)";
                return result;
            }
        }

        /// <summary>
        /// Генерирует сетку для тела в форме параллелепипеда по заданным размерам и количеству узлов для каждой оси
        /// </summary>
        /// <typeparam name="T">Тип данных размера тела</typeparam>
        /// <param name="SizeX">Размер тела по оси X</param>
        /// <param name="SizeY">Размер тела по оси Y</param>
        /// <param name="SizeZ">Размер тела по оси Z</param>
        /// <param name="NumberNodesX">Количество узлов по оси X</param>
        /// <param name="NumberNodesY">Количество узлов по оси Y</param>
        /// <param name="NumberNodesZ">Количество узлов по оси Z</param>
        /// <returns>Объект Grid</returns>
        public static Grid Generate3DGrid(decimal SizeX, decimal SizeY, decimal SizeZ, int NumberNodesX, int NumberNodesY, int NumberNodesZ)
        {
            var grid = new Grid();
            
            // Вычисляем шаги сетки для осей X, Y, Z
            var gridStepX = SizeX / (NumberNodesX - 1);
            var gridStepY = SizeY / (NumberNodesY - 1);
            var gridStepZ = SizeZ / (NumberNodesZ - 1);
                        
            for (int z = 0; z < NumberNodesZ; z++)
            {                
                for (int y = 0; y < NumberNodesY; y++)
                {                    
                    for (int x = 0; x < NumberNodesX; x++)
                    {
                        Node node = new Node();
                        node.IndexX = x;
                        node.IndexY = y;
                        node.IndexZ = z;
                        node.Coordinates = new Coordinates<decimal>();
                        node.Coordinates.CoordX = x * gridStepX;
                        node.Coordinates.CoordY = y * gridStepY;
                        node.Coordinates.CoordZ = z * gridStepZ;

                        if (x == 0 || y == 0 || z == 0 || x == NumberNodesX - 1 || y == NumberNodesY - 1 || z == NumberNodesZ - 1)
                            node.NodeLocationEnum = NodeLocationEnum.OnTheSurface;                        

                        grid.AddNode(node);                        
                    }
                }
            }

            //grid.SetPrevAndNextNodes();

            return grid;
        }

        /// <summary>
        /// Устанавливает ссылки на все предшествующие и последующие узлы для всех узлов сетки
        /// </summary>
        /// <param name="IsParallel">Флаг использования параллельного алгоритма</param>
        public void SetPrevAndNextNodes(bool IsParallel=true, bool IsPartitionerUsed=false)
        {
            if(IsParallel)
            {
                if (IsPartitionerUsed)
                {
                    // Параллельная реализация с использованием класса Partitioner
                    var nodePartitioner = Partitioner.Create(0, Nodes.Count - 1);
                    Parallel.ForEach(nodePartitioner, (range, loopState) =>
                    {
                        for(int i=range.Item1;i<range.Item2;i++)
                        {
                            SetPrevAndNextNodesForSingleNode(Nodes[i]);
                        }
                    });
                }
                else
                {
                    // Параллельная реализация
                    Parallel.ForEach(Nodes, (node) => SetPrevAndNextNodesForSingleNode(node));
                }                
            }
            else
            {
                // Последовательная реализация
                Nodes.ForEach((node) => SetPrevAndNextNodesForSingleNode(node));
                //foreach (var node in Nodes)
                //{
                //    node.PrevNodeX = GetNode(node.IndexX - 1, node.IndexY, node.IndexZ);
                //    node.PrevNodeY = GetNode(node.IndexX, node.IndexY - 1, node.IndexZ);
                //    node.PrevNodeZ = GetNode(node.IndexX, node.IndexY, node.IndexZ - 1);
                //    node.NextNodeX = GetNode(node.IndexX + 1, node.IndexY, node.IndexZ);
                //    node.NextNodeY = GetNode(node.IndexX, node.IndexY + 1, node.IndexZ);
                //    node.NextNodeZ = GetNode(node.IndexX, node.IndexY, node.IndexZ + 1);
                //}
            }            
        }

        /// <summary>
        /// Устанавливает все предыдущие и последующие узлы для переданного узла
        /// </summary>
        /// <param name="node"></param>
        public void SetPrevAndNextNodesForSingleNode(Node node)
        {
            node.PrevNodeX = GetNode(node.IndexX - 1, node.IndexY, node.IndexZ);
            node.PrevNodeY = GetNode(node.IndexX, node.IndexY - 1, node.IndexZ);
            node.PrevNodeZ = GetNode(node.IndexX, node.IndexY, node.IndexZ - 1);
            node.NextNodeX = GetNode(node.IndexX + 1, node.IndexY, node.IndexZ);
            node.NextNodeY = GetNode(node.IndexX, node.IndexY + 1, node.IndexZ);
            node.NextNodeZ = GetNode(node.IndexX, node.IndexY, node.IndexZ + 1);
        }


        /// <summary>
        /// Добавление узла в сетку
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }

        /// <summary>
        /// Возвращает узел по заданному индексу 
        /// </summary>
        /// <param name="IndexX"></param>
        /// <param name="IndexY"></param>
        /// <param name="IndexZ"></param>
        /// <returns></returns>
        public Node GetNode(int IndexX, int IndexY, int IndexZ)
        {
            Node node = new Node();

            node = Nodes.SingleOrDefault(n=>
                n.IndexX==IndexX && 
                n.IndexY==IndexY && 
                n.IndexZ==IndexZ);

            return node;
        }



        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();            
            foreach (var node in Nodes)
            {
                stringBuilder.Append($"{node.ToString()??"-"}\n");
            }
            return stringBuilder.ToString();
        }
    }
}
