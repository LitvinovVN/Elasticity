using ElasticityClassLibrary.Derivatives;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ElasticityClassLibraryXUnitTests
{
    /// <summary>
    /// Тесты производных
    /// </summary>
    public class DerivativesXUnitTests
    {
        /// <summary>
        /// Тест сложения двух одномерных трёхточечных
        /// операторов вычисления производных
        /// </summary>
        [Fact]
        public void DerivativeOperator1D3P_Plus_DerivativeOperator1D3P()
        {
            // Подготовка
            var d1 = new DerivativeOperator1D3P
            {
                Kim1 = 2.4m,
                Ki = 0.1m,
                Kip1 = 1.8m
            };

            var d2 = new DerivativeOperator1D3P
            {
                Kim1 = 0.1m,
                Ki = 0.2m,
                Kip1 = 0.3m
            };            

            // Действие
            var result = d1 + d2;            
            var d3 = new DerivativeOperator1D3P { Kim1 = 2.5m, Ki = 0.3m, Kip1 = 2.1m };

            // Проверка
            Assert.NotEqual(d1, d2);
            Assert.NotEqual(result, d1);
            Assert.NotEqual(result, d2);
            Assert.Equal(result.Kim1, d3.Kim1);
            Assert.Equal(result.Ki, d3.Ki);
            Assert.Equal(result.Kip1, d3.Kip1);
        }

        /// <summary>
        /// Тест произведения одномерного трёхточечного
        /// оператора вычисления производных и константы
        /// </summary>
        [Fact]
        public void DerivativeOperator1D3P_Multiplu_Decimal()
        {
            // Подготовка
            var d = new DerivativeOperator1D3P
            {
                Kim1 = 2.4m,
                Ki = 0.1m,
                Kip1 = 1.8m
            };

            decimal decimalNumber = 2.0m;

            // Действие
            var result = d * decimalNumber;
            var d3 = new DerivativeOperator1D3P { Kim1 = 4.8m, Ki = 0.2m, Kip1 = 3.6m };

            // Проверка
            Assert.NotEqual(d, d3);
            Assert.NotEqual(result, d3);            
            Assert.Equal(result.Kim1, d3.Kim1);
            Assert.Equal(result.Ki, d3.Ki);
            Assert.Equal(result.Kip1, d3.Kip1);
        }
    }
}
