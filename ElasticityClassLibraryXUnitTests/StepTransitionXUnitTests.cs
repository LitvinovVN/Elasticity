using System;
using Xunit;
using ElasticityClassLibrary;
using ElasticityClassLibrary.GridNamespace;

namespace ElasticityClassLibraryXUnitTests
{
    public class StepTransitionXUnitTests
    {
        [Fact]
        public void StepTransitionCreatingIndexCompare()
        {
            // Подготовка
            uint index = 15;
            decimal stepValue = 0.5m;
            decimal coordinate = 25m;

            var expected = new StepTransition { Index = 15, StepValue = stepValue, Coordinate = coordinate };

            // Действие
            var stepTransition = new StepTransition(index, stepValue, coordinate);

            // Проверка
            Assert.Equal(expected.Index, stepTransition.Index);            
        }
    }
}
