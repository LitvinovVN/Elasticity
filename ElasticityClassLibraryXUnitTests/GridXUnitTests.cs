using System;
using Xunit;
using ElasticityClassLibrary;
using ElasticityClassLibrary.GridNamespace;

namespace ElasticityClassLibraryXUnitTests
{
    public class GridXUnitTests
    {
        [Fact]
        public void GridCreating()
        {
            // Подготовка
            var Grid1 = new Grid3D(10, 10, 10, 10, 10, 10);

            // Действие
            var Grid2 = new Grid3D(10, 10, 10, 10, 10, 10);

            // Проверка
            Assert.Equal(Grid1.GridSizeX, Grid2.GridSizeX);
        }
    }
}
