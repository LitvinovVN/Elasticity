using System;
using System.Collections.Generic;
using System.Text;

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
        /// Генерирует сетку для тела в форме параллелепипеда по заданным размерам и количеству узлов для каждой оси
        /// </summary>
        /// <typeparam name="T">Тип данных размера тела</typeparam>
        /// <param name="SizeX">Размер тела по оси X</param>
        /// <param name="SizeY">Размер тела по оси Y</param>
        /// <param name="SizeZ">Размер тела по оси Z</param>
        /// <param name="NumberNodesX">Количество узлов по оси X</param>
        /// <param name="NumberNodesY">Количество узлов по оси Y</param>
        /// <param name="NumberNodesZ">Количество узлов по оси Z</param>
        /// <returns></returns>
        public static Grid Generate3DGrid(decimal SizeX, decimal SizeY, decimal SizeZ, int NumberNodesX, int NumberNodesY, int NumberNodesZ)
        {
            var grid = new Grid();
            
            // Вычисляем шаги сетки для осей X, Y, Z
            var gridStepX = SizeX / (NumberNodesX - 1);
            var gridStepY = SizeY / (NumberNodesY - 1);
            var gridStepZ = SizeZ / (NumberNodesZ - 1);

            Node nextNodeX;
            Node nextNodeY;
            Node nextNodeZ;
            Node prevNodeX;
            Node prevNodeY;
            Node prevNodeZ;

            prevNodeZ = null;
            for (int z = 0; z < NumberNodesZ; z++)
            {
                prevNodeY = null;
                for (int y = 0; y < NumberNodesY; y++)
                {
                    prevNodeX = null;
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
                        node.PrevNodeX = prevNodeX;
                        grid.AddNode(node);
                                                
                        prevNodeX = node;
                    }
                }
            }

            return grid;
        }


        /// <summary>
        /// Добавление узла в сетку
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(Node node)
        {
            Nodes.Add(node);
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
