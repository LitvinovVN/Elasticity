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
    public class GridWithGeometryPreCalculated2D : GridWithGeometryPreCalculated
    {
        #region Слои сетки
        /// <summary>
        /// Объект, содержащий сведения о списках слоёв сетки
        /// </summary>
        public GridLayers2D GridLayers2D { get; set; } = new GridLayers2D();        
        #endregion

        #region Конструкторы
        public GridWithGeometryPreCalculated2D(Grid2D grid, Geometry2D geometry)
        {
            GenerateLayersFromGridAndGeometry(grid, geometry);
            CalculateGridLayers2DParamaters();
        }
                
        public GridWithGeometryPreCalculated2D()
        {

        }
        #endregion

        /// <summary>
        /// Рассчитывает параметры (GridLayerParameters)
        /// для свойства GridLayers2D
        /// </summary>
        private void CalculateGridLayers2DParamaters()
        {
            GridLayers2D.CalculateGridLayers2DParameters();
        }

       

        #region Генерирование слоёв
        /// <summary>
        /// Генерирует наборы слоёв из переданных объектов
        /// сетки и геометрии
        /// </summary>
        /// <param name="grid">Объект Grid</param>
        /// <param name="geometry">Объект Geometry</param>
        private void GenerateLayersFromGridAndGeometry(Grid2D grid, Geometry2D geometry)
        {
            InsertGridLayersFromGrid(grid);
            InsertGridLayersFromGeometry(geometry);
        }
                
        /// <summary>
        /// Генерирует списки слоёв из объекта Grid
        /// </summary>
        /// <param name="grid">Объект сетки Grid</param>
        private void InsertGridLayersFromGrid(Grid2D grid)
        {
            GridLayers2D.GridLayers1 = StepTransition.GenerateGridLayerListFromStepTransitionList(grid.StepTransitions1);
            GridLayers2D.GridLayers2 = StepTransition.GenerateGridLayerListFromStepTransitionList(grid.StepTransitions2);            
        }

        
        /// <summary>
        /// Добавляет слои из объекта геометрии
        /// </summary>
        /// <param name="geometry">Объект Geometry</param>
        private void InsertGridLayersFromGeometry(Geometry2D geometry)
        {
            if (geometry == null) return;

            GridLayers2D gridLayers2D = geometry.GetGridLayers2D;
            GridLayers2D.Merge(gridLayers2D);
        }
        #endregion

        public override NodeSet GenerateNodeSetFromGridLayersAndGeometry(GridLayers gridLayers, Geometry geometry)
        {
            throw new NotImplementedException();
        }
    }
}
