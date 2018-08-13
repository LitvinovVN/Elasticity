using System;
using Xunit;
using ElasticityClassLibrary;

namespace ElasticityClassLibraryXUnitTests
{
    public class StepTransitionXUnitTests
    {
        [Fact]
        public void StepTransitionCreatingIndexCompare()
        {
            // ����������
            uint index = 15;
            decimal stepValue = 0.5m;
            decimal coordinate = 25m;

            var expected = new StepTransition { Index = 15, StepValue = stepValue, Coordinate = coordinate };

            // ��������
            var stepTransition = new StepTransition(index, stepValue, coordinate);

            // ��������
            Assert.Equal(expected.Index, stepTransition.Index);            
        }
    }
}
