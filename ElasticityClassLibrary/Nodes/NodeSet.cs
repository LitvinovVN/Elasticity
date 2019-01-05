using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.Nodes
{
    /// <summary>
    /// Набор узлов
    /// (абстрактный)
    /// </summary>
    public abstract class NodeSet
    {
        /// <summary>
        /// Список узлов
        /// </summary>
        public List<Node> Nodes { get; set; } = new List<Node>();
                
        /// <summary>
        /// Объединяет текущий набор узлов
        /// с передаваемым через параметр
        /// </summary>
        /// <param name="nodeSet">Добавляемый набор узлов</param>
        public void Merge(NodeSet nodeSet)
        {
            foreach (var addingNode in nodeSet.Nodes)
            {
                AddNode(addingNode);
            }
        }

        /// <summary>
        /// Добавляет узел к набору
        /// </summary>
        /// <param name="addingNode"></param>
        public abstract void AddNode(Node addingNode);
    }
}
