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
    public class GridWithGeometryPreCalculated3D : GridWithGeometryPreCalculated
    {
        #region Слои сетки
        /// <summary>
        /// Объект, содержащий сведения о списках слоёв сетки
        /// </summary>
        public GridLayers3D GridLayers3D { get; set; } = new GridLayers3D();        
        #endregion

        #region Конструкторы
        public GridWithGeometryPreCalculated3D(Grid3D grid, Geometry3D geometry)
        {
            GenerateLayersFromGridAndGeometry(grid, geometry);
            CalculateGridLayers3DParamaters();
        }
                
        public GridWithGeometryPreCalculated3D()
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
        private void GenerateLayersFromGridAndGeometry(Grid3D grid, Geometry3D geometry)
        {
            InsertGridLayersFromGrid(grid);
            InsertGridLayersFromGeometry(geometry);
        }
                
        /// <summary>
        /// Генерирует списки слоёв из объекта Grid
        /// </summary>
        /// <param name="grid">Объект сетки Grid</param>
        private void InsertGridLayersFromGrid(Grid3D grid)
        {
            GridLayers3D.GridLayersX = StepTransition.GenerateGridLayerListFromStepTransitionList(grid.StepTransitionsX);
            GridLayers3D.GridLayersY = StepTransition.GenerateGridLayerListFromStepTransitionList(grid.StepTransitionsY);
            GridLayers3D.GridLayersZ = StepTransition.GenerateGridLayerListFromStepTransitionList(grid.StepTransitionsZ);
        }

        
        /// <summary>
        /// Добавляет слои из объекта геометрии
        /// </summary>
        /// <param name="geometry">Объект Geometry</param>
        private void InsertGridLayersFromGeometry(Geometry3D geometry)
        {
            if (geometry == null) return;

            GridLayers3D gridLayers3D = geometry.GetGridLayers3D;
            GridLayers3D.Merge(gridLayers3D);
        }

        public override NodeSet GenerateNodeSetFromGridLayersAndGeometry(GridLayers gridLayers, Geometry geometry)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
