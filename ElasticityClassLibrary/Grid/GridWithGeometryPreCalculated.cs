using ElasticityClassLibrary.GeometryNamespase;
using ElasticityClassLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ElasticityClassLibrary.GridNamespace
{
    /// <summary>
    /// Сетка с учетом геометрии исследуемого объекта
    /// с предварительно рассчитанными параметрами слоёв
    /// </summary>
    [Serializable]
    public class GridWithGeometryPreCalculated
    {
        #region Слои сетки
        /// <summary>
        /// Объект, содержащий сведения о списках слоёв сетки
        /// </summary>
        public GridLayers3D GridLayers3D { get; set; } = new GridLayers3D();        
        #endregion

        #region Конструкторы
        public GridWithGeometryPreCalculated(Grid grid, Geometry geometry)
        {
            GenerateLayersFromGridAndGeometry(grid, geometry);
            CalculateGridLayers3DParamaters();
        }
                
        public GridWithGeometryPreCalculated()
        {

        }
        #endregion

        /// <summary>
        /// Рассчитывает параметры (GridLayerParameters)
        /// для свойства GridLayers3D
        /// </summary>
        private void CalculateGridLayers3DParamaters()
        {
            GridLayers3D.CalculateGridLayers3DParameters();
        }

       

        #region Генерирование слоёв
        /// <summary>
        /// Генерирует наборы слоёв из переданных объектов
        /// сетки и геометрии
        /// </summary>
        /// <param name="grid">Объект Grid</param>
        /// <param name="geometry">Объект Geometry</param>
        private void GenerateLayersFromGridAndGeometry(Grid grid, Geometry geometry)
        {
            InsertGridLayersFromGrid(grid);
            InsertGridLayersFromGeometry(geometry);
        }
                
        /// <summary>
        /// Генерирует списки слоёв из объекта Grid
        /// </summary>
        /// <param name="grid">Объект сетки Grid</param>
        private void InsertGridLayersFromGrid(Grid grid)
        {
            GridLayers3D.GridLayersX = GenerateGridLayerListFromStepTransitionList(grid.StepTransitionsX);
            GridLayers3D.GridLayersY = GenerateGridLayerListFromStepTransitionList(grid.StepTransitionsY);
            GridLayers3D.GridLayersZ = GenerateGridLayerListFromStepTransitionList(grid.StepTransitionsZ);
        }

        /// <summary>
        /// Создаёт список слоёв на основе переданного списка переходов
        /// </summary>
        /// <param name="stepTransitionsX">Список переходов (список List объектов StepTransition)</param>
        /// <returns></returns>
        private List<GridLayer> GenerateGridLayerListFromStepTransitionList(List<StepTransition> stepTransitionList)
        {
            var resultGridLayerList = new List<GridLayer>();
            uint resultGridLayerListItemIndex = 0;

            if (stepTransitionList == null) return resultGridLayerList;
            if (stepTransitionList.Count == 0) return resultGridLayerList;

            StepTransition prevStepTransition = new StepTransition(0, 0, 0);
            GridLayer prevLayer = new GridLayer();
            prevLayer.Index = 0;
            prevLayer.Coordinate = 0;
            resultGridLayerList.Add(prevLayer);
            for (int stepTransitionCounter = 0; stepTransitionCounter < stepTransitionList.Count; stepTransitionCounter++)
            {
                StepTransition currentStepTransition = stepTransitionList[stepTransitionCounter];
                decimal distanceBetweenStepTransitions = currentStepTransition.Coordinate - prevStepTransition.Coordinate;
                uint numLayersBetweenStepTransitions = Convert.ToUInt32(distanceBetweenStepTransitions / currentStepTransition.StepValue);
                for (int layerCounter = 0; layerCounter < numLayersBetweenStepTransitions; layerCounter++)
                {
                    GridLayer curLayer = new GridLayer();
                    curLayer.Index = ++resultGridLayerListItemIndex;
                    curLayer.Coordinate = prevLayer.Coordinate + currentStepTransition.StepValue;
                    resultGridLayerList.Add(curLayer);

                    prevLayer = curLayer;
                }
                prevStepTransition = currentStepTransition;
            }

            return resultGridLayerList;
        }

        /// <summary>
        /// Добавляет слои из объекта геометрии
        /// </summary>
        /// <param name="geometry">Объект Geometry</param>
        private void InsertGridLayersFromGeometry(Geometry geometry)
        {
            if (geometry == null) return;

            GridLayers3D gridLayers3D = geometry.GetGridLayers3D;
            GridLayers3D.Merge(gridLayers3D);
        }

        #endregion


        #region Импорт/экспорт
        /// <summary>
        /// Экспортирует объект в файл XML
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Флаг успешности сериализации</returns>
        public bool ExportToXML(string path, string fileName)
        {
            return Xml.ExportToXML(typeof(GridWithGeometryPreCalculated), this, path, fileName);
        }

        /// <summary>
        /// Импортирует объект из файла XML
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Импортированный объект GridWithGeometryPreCalculated.
        /// Null в случае неудачи.</returns>
        public static GridWithGeometryPreCalculated ImportFromXML(string path, string fileName)
        {
            GridWithGeometryPreCalculated importedGeometry = Xml.ImportFromXML(typeof(GridWithGeometryPreCalculated), path, fileName) as GridWithGeometryPreCalculated;            
            return importedGeometry;
        }
        #endregion
    }
}
