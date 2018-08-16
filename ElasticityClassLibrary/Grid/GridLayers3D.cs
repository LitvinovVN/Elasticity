using System;
using System.Collections.Generic;

namespace ElasticityClassLibrary.GridNamespace
{
    /// <summary>
    /// Списки слоёв по осям X, Y, Z
    /// </summary>
    [Serializable]
    public class GridLayers3D
    {
        /// <summary>
        /// Набор слоёв по оси X
        /// </summary>
        public List<GridLayer> GridLayersX { get; set; } = new List<GridLayer>();

        /// <summary>
        /// Набор слоёв по оси Y
        /// </summary>
        public List<GridLayer> GridLayersY { get; set; } = new List<GridLayer>();

        /// <summary>
        /// Набор слоёв по оси Z
        /// </summary>
        public List<GridLayer> GridLayersZ { get; set; } = new List<GridLayer>();

        public GridLayers3D()
        {

        }

        /// <summary>
        /// Добавляет слои из переданного объекта в текущий
        /// </summary>
        /// <param name="gridLayers3D">Объект GridLayers3D</param>
        public void Merge(GridLayers3D gridLayers3D)
        {
            if (gridLayers3D == null) return;

            GridLayersX = MergeGridLayerLists(GridLayersX, gridLayers3D.GridLayersX);
            GridLayersY = MergeGridLayerLists(GridLayersY, gridLayers3D.GridLayersY);
            GridLayersZ = MergeGridLayerLists(GridLayersZ, gridLayers3D.GridLayersZ);
        }

        /// <summary>
        /// Объединяет списки слоёв
        /// </summary>
        /// <param name="gridLayerList1"></param>
        /// <param name="gridLayerList2"></param>
        /// <returns></returns>
        private List<GridLayer> MergeGridLayerLists(List<GridLayer> gridLayerList1, List<GridLayer> gridLayerList2)
        {
            if (gridLayerList1 == null || gridLayerList1.Count==0)
            {
                return gridLayerList2;
            }
            if (gridLayerList2 == null || gridLayerList2.Count == 0)
            {
                return gridLayerList1;
            }

            var gridLayerListResult = new List<GridLayer>();
            int gridLayerList1Counter = gridLayerList1.Count;
            int gridLayerList2Counter = gridLayerList2.Count;
            int gridLayerList1CurrentIndex = 0;
            int gridLayerList2CurrentIndex = 0;

            List<GridLayer> leftList;
            List<GridLayer> rightList;

            uint addingGridLayerIndex = 0;

            while (gridLayerList1Counter > 0 || gridLayerList2Counter > 0)
            {
                // Если первый список уже пуст, добавляем элементы из второго списка
                if(gridLayerList1Counter==0)
                {
                    var addingGridLayer = new GridLayer();
                    addingGridLayer.Index = addingGridLayerIndex;
                    addingGridLayer.Coordinate = gridLayerList2[gridLayerList2CurrentIndex].Coordinate;
                    gridLayerListResult.Add(addingGridLayer);

                    addingGridLayerIndex++;                    
                    gridLayerList2Counter--;                    
                    gridLayerList2CurrentIndex++;
                    continue;
                }

                // Если второй список уже пуст, добавляем элементы из первого списка
                if (gridLayerList2Counter == 0)
                {
                    var addingGridLayer = new GridLayer();
                    addingGridLayer.Index = addingGridLayerIndex;
                    addingGridLayer.Coordinate = gridLayerList1[gridLayerList1CurrentIndex].Coordinate;
                    gridLayerListResult.Add(addingGridLayer);

                    addingGridLayerIndex++;
                    gridLayerList1Counter--;
                    gridLayerList1CurrentIndex++;
                    continue;
                }

                // Если координаты в обоих списках одинаковые - добавляем только один элемент и изменяем счетчики у обоих списков
                if (gridLayerList1[gridLayerList1CurrentIndex].Coordinate == gridLayerList2[gridLayerList2CurrentIndex].Coordinate)
                {
                    var addingGridLayer = new GridLayer();
                    addingGridLayer.Index = addingGridLayerIndex;
                    addingGridLayer.Coordinate = gridLayerList1[gridLayerList1CurrentIndex].Coordinate;
                    gridLayerListResult.Add(addingGridLayer);

                    addingGridLayerIndex++;
                    gridLayerList1Counter--;
                    gridLayerList2Counter--;
                    gridLayerList1CurrentIndex++;
                    gridLayerList2CurrentIndex++;
                    continue;
                }
                // Если координаты отличаются, устанавливаем левый и правый списки
                if (gridLayerList1[gridLayerList1CurrentIndex].Coordinate < gridLayerList2[gridLayerList2CurrentIndex].Coordinate)
                {
                    leftList = gridLayerList1;
                    rightList = gridLayerList2;

                    while (leftList[gridLayerList1CurrentIndex].Coordinate < rightList[gridLayerList2CurrentIndex].Coordinate)
                    {
                        var addingGridLayer = new GridLayer();
                        addingGridLayer.Index = addingGridLayerIndex;
                        addingGridLayer.Coordinate = leftList[gridLayerList1CurrentIndex].Coordinate;
                        gridLayerListResult.Add(addingGridLayer);

                        addingGridLayerIndex++;
                        gridLayerList1Counter--;
                        gridLayerList1CurrentIndex++;

                        if (gridLayerList1Counter <= 0 || gridLayerList1CurrentIndex >= gridLayerList1.Count)
                        {
                            gridLayerList1Counter = 0;
                            break;
                        }
                    }
                }
                else
                {
                    leftList = gridLayerList2;
                    rightList = gridLayerList1;

                    while (leftList[gridLayerList2CurrentIndex].Coordinate < rightList[gridLayerList1CurrentIndex].Coordinate)
                    {
                        var addingGridLayer = new GridLayer();
                        addingGridLayer.Index = addingGridLayerIndex;
                        addingGridLayer.Coordinate = leftList[gridLayerList2CurrentIndex].Coordinate;
                        gridLayerListResult.Add(addingGridLayer);

                        addingGridLayerIndex++;
                        gridLayerList2Counter--;
                        gridLayerList2CurrentIndex++;

                        if (gridLayerList2Counter <= 0 || gridLayerList2CurrentIndex >= gridLayerList2.Count)
                        {
                            gridLayerList2Counter = 0;
                            break;
                        }
                    }
                }
            }
            return gridLayerListResult;
        }
    }
}