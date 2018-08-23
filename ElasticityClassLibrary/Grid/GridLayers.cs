using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticityClassLibrary.GridNamespace
{
    /// <summary>
    /// Сетка со слоями (абстрактный класс)
    /// </summary>
    public abstract class GridLayers
    {
        /// <summary>
        /// Объединяет списки слоёв
        /// </summary>
        /// <param name="gridLayerList1"></param>
        /// <param name="gridLayerList2"></param>
        /// <returns></returns>
        protected List<GridLayer> MergeGridLayerLists(List<GridLayer> gridLayerList1, List<GridLayer> gridLayerList2)
        {
            if (gridLayerList1 == null || gridLayerList1.Count == 0)
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
                if (gridLayerList1Counter == 0)
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

        /// <summary>
        /// Вычисляет параметры сетки для переданного наборов слоёв
        /// </summary>
        /// <param name="gridLayerList"></param>
        protected void CalculateGridLayerParameters(List<GridLayer> gridLayerList)
        {
            if (gridLayerList == null) return;
            uint gridLayersCount = (uint)gridLayerList.Count;
            if (gridLayersCount < 7) return;

            GridLayer backwards3Layer;
            GridLayer backwards2Layer;
            GridLayer backwards1Layer;
            GridLayer currentLayer;
            GridLayer forwards1Layer;
            GridLayer forwards2Layer;
            GridLayer forwards3Layer;

            for (int i = 0; i < gridLayersCount; i++)
            {
                #region Инициализация слоёв на данной итерации
                if ((i - 3) < 0)
                {
                    backwards3Layer = null;
                }
                else
                {
                    backwards3Layer = gridLayerList[i - 3];
                }

                if ((i - 2) < 0)
                {
                    backwards2Layer = null;
                }
                else
                {
                    backwards2Layer = gridLayerList[i - 2];
                }

                if ((i - 1) < 0)
                {
                    backwards1Layer = null;
                }
                else
                {
                    backwards1Layer = gridLayerList[i - 1];
                }

                currentLayer = gridLayerList[i];

                if ((i + 1) >= gridLayersCount)
                {
                    forwards1Layer = null;
                }
                else
                {
                    forwards1Layer = gridLayerList[i + 1];
                }

                if ((i + 2) >= gridLayersCount)
                {
                    forwards2Layer = null;
                }
                else
                {
                    forwards2Layer = gridLayerList[i + 2];
                }

                if ((i + 3) >= gridLayersCount)
                {
                    forwards3Layer = null;
                }
                else
                {
                    forwards3Layer = gridLayerList[i + 3];
                }
                #endregion

                GridLayerParameters glp = new GridLayerParameters(backwards3Layer, backwards2Layer, backwards1Layer, currentLayer, forwards1Layer, forwards2Layer, forwards3Layer);
                gridLayerList[i].GridLayerParameters = glp;
            }
        }
    }
}
