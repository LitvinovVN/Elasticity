using System;
using System.Collections.Generic;
using System.Text;

namespace SparseMatrixRepSeqNamespace
{
    /// <summary>
    /// Результаты проверки корректности расчета одномерной модели
    /// </summary>
    public class CheckingResults1D
    {
        /// <summary>
        /// Набор результатов расчетов во всех точках сетки
        /// </summary>
        public List<CheckingResult1D> checkingResultsAtNodes { get; set; } = new List<CheckingResult1D>();

        /// <summary>
        /// Вывод результатов расчета в консоль
        /// </summary>
        public void PrintToConsole()
        {
            Console.WriteLine("------ Проверка корректности расчётов ------");
            Console.WriteLine($"Коорд.\tЗначение.\tВычисл.\tАбс. откл.\tОтн. откл, %");
            foreach (var item in checkingResultsAtNodes)
            {
                Console.WriteLine($"{item.X}\t{item.ValueCorrect}\t{item.ValueCalculated}\t{item.GetAbsoluteDeviation}\t{item.GetRelativeDeviation}");
            }

            Console.WriteLine("---------------------------------------------\n");
        }

        /// <summary>
        /// Добавление результатов расчета в одном узле
        /// </summary>
        /// <param name="resAtCurNode"></param>
        public void AddCheckingResultAtNode(CheckingResult1D resAtCurNode)
        {
            checkingResultsAtNodes.Add(resAtCurNode);
        }
    }
}
