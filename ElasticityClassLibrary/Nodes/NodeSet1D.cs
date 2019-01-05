using ElasticityClassLibrary.GeometryNamespase;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.Nodes
{
    /// <summary>
    /// Набор одномерных узлов
    /// </summary>
    public class NodeSet1D : NodeSet
    {
        public override void AddNode(Node addingNode)
        {
            var addingNode1D = (Node1D)addingNode;
            AddNode1D(addingNode1D);
        }

        public void AddNode1D(Node1D addingNode1D)
        {
            Coordinate1D addingNode1DCoordinate1D = addingNode1D.Coordinate1D;
            int nodesCount = Nodes.Count;

            // Если список узлов пуст - добавляем узел addingNode1D
            if(nodesCount==0)
            {
                Nodes.Add(addingNode1D);
                return;
            }

            for (int i=0; i < nodesCount ;i++)
            {
                var nodeCoordinate = (Coordinate1D)Nodes[i].Coordinate;
                // Если узел с указанной координатой уже существует - ничего не делаем
                if (nodeCoordinate.GetStringCoordinates == addingNode1D.Coordinate1D.GetStringCoordinates)
                {
                    return;
                }

                if (nodeCoordinate.X > addingNode1DCoordinate1D.X)
                {
                    Nodes.Insert(i, addingNode1D);
                    return;
                }

                if(i == nodesCount-1)
                {
                    Nodes.Add(addingNode1D);
                    return;
                }
            }
        }
    }
}
