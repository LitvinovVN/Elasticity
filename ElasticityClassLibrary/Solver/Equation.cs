using System.Collections.Generic;

namespace ElasticityClassLibrary.SolverNamespace
{
    /// <summary>
    /// Линейное алгебраическое уравнение
    /// </summary>
    public class Equation
    {
        /// <summary>
        /// Список компонентов левой части уравнения (неизвестные)
        /// </summary>
        public List<EquationElement> EquationElementList { get; set; }

        /// <summary>
        /// Правая часть уравнения (значение)
        /// </summary>
        public decimal RightSideOfEquation { get; set; }
    }
}