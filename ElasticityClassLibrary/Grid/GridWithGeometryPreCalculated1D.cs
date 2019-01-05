using ElasticityClassLibrary.GeometryNamespase;
using ElasticityClassLibrary.Infrastructure;
using ElasticityClassLibrary.Nodes;
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
    public class GridWithGeometryPreCalculated1D : GridWithGeometryPreCalculated
    {
        #region Слои сетки
        /// <summary>
        /// Объект, содержащий сведения о списках слоёв сетки
        /// </summary>
        public GridLayers1D GridLayers1D { get; set; } = new GridLayers1D();

        /// <summary>
        /// Набор узлов, участвующих в расчете (узлы исследуемого объекта)
        /// </summary>
        public NodeSet1D NodeSet1D { get; set; } = new NodeSet1D();
        #endregion

        #region Конструкторы
        public GridWithGeometryPreCalculated1D(Grid1D grid, Geometry1D geometry)
        {
            GenerateLayersFromGridAndGeometry(grid, geometry);
            CalculateGridLayers1DParamaters();
            NodeSet1D = (NodeSet1D)GenerateNodeSetFromGridLayersAndGeometry(GridLayers1D, geometry);
        }        

        public GridWithGeometryPreCalculated1D()
        {

        }
        #endregion

        /// <summary>
        /// Рассчитывает параметры (GridLayerParameters)
        /// для свойства GridLayers2D
        /// </summary>
        private void CalculateGridLayers1DParamaters()
        {
            GridLayers1D.CalculateGridLayers1DParameters();
        }

       

        #region Генерирование слоёв
        /// <summary>
        /// Генерирует наборы слоёв из переданных объектов
        /// сетки и геометрии
        /// </summary>
        /// <param name="grid">Объект Grid</param>
        /// <param name="geometry">Объект Geometry</param>
        private void GenerateLayersFromGridAndGeometry(Grid1D grid, Geometry1D geometry)
        {
            InsertGridLayersFromGrid(grid);
            InsertGridLayersFromGeometry(geometry);
        }
                
        /// <summary>
        /// Генерирует списки слоёв из объекта Grid
        /// </summary>
        /// <param name="grid">Объект сетки Grid</param>
        private void InsertGridLayersFromGrid(Grid1D grid)
        {
            GridLayers1D.GridLayers = StepTransition.GenerateGridLayerListFromStepTransitionList(grid.StepTransitions);                     
        }

        
        /// <summary>
        /// Добавляет слои из объекта геометрии
        /// </summary>
        /// <param name="geometry">Объект Geometry</param>
        private void InsertGridLayersFromGeometry(Geometry1D geometry)
        {
            if (geometry == null) return;

            GridLayers1D gridLayers1D = geometry.GetGridLayers1D;
            GridLayers1D.Merge(gridLayers1D);
        }

        #endregion

        /// <summary>
        /// Создаёт объект NodeSet1D по переданным объектам слоёв сетки и геометрии
        /// </summary>
        /// <param name="gridLayers"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public override NodeSet GenerateNodeSetFromGridLayersAndGeometry(GridLayers gridLayers, Geometry geometry)
        {
            NodeSet1D resultNodeSet1D = new NodeSet1D();
            resultNodeSet1D = (NodeSet1D)geometry.GetNodeSet(gridLayers);

            return resultNodeSet1D;
        }
    }
}
