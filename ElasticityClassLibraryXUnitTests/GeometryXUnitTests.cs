using ElasticityClassLibrary.GeometryNamespase;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ElasticityClassLibraryXUnitTests
{
    public class GeometryXUnitTests
    {
        [Fact]
        public void Coordinate3D_GetStringCoordinates()
        {
            // Подготовка
            var coordinate3D = new Coordinate3D(0m,1.5m,2.23456m);

            // Действие
            var sCoordinate3D = coordinate3D.GetStringCoordinates;

            // Проверка
            Assert.Equal("[0,1.5,2.23456]", sCoordinate3D);
        }
    }
}
