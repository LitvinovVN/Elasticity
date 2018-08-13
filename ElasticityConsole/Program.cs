using ElasticityClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ElasticityConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            char key;
            do
            {                
                Console.Clear();
                Console.WriteLine("Выберите номер команды или 'q' для выхода:");
                Console.WriteLine("1. GenerateGridTests();");
                Console.WriteLine("2. GenerateGridsTests();");
                //Console.WriteLine("3. SetPrevAndNextNodesTests();");
                key = Console.ReadKey(true).KeyChar;
                Console.Clear();

                switch (key)
                {
                    case '1':
                        GenerateGridTests();
                        break;
                    case '2':
                        GenerateGridsTests();
                        break;
                    case '3':
                        //SetPrevAndNextNodesTests();
                        break;
                }
            }
            while (key!='q');
                       
        }

        #region Вспомогательные методы
        /// <summary>
        /// Запрос ввода значения типа decimal
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static decimal RequestingUserInputDecimalValue(string message)
        {
            decimal userInput = 0;
            bool isCorrectValue = false;
            do
            {
                try
                {
                    Console.Write(message);
                    userInput = Convert.ToDecimal(Console.ReadLine());
                    isCorrectValue = true;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Ошибка! Введенную последовательность символов не удалось преобразовать в тип decimal.");
                }
            }
            while (!isCorrectValue);
            return userInput;
        }

        /// <summary>
        /// Запрос ввода значения типа long
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static long RequestingUserInputLongValue(string message)
        {
            long userInput = 0;
            bool isCorrectValue = false;
            do
            {
                try
                {
                    Console.Write(message);
                    userInput = Convert.ToInt64(Console.ReadLine());
                    isCorrectValue = true;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Ошибка! Введенную последовательность символов не удалось преобразовать в тип long.");
                }
            }
            while (!isCorrectValue);
            return userInput;
        }               

        /// <summary>
        /// Ожидание нажатия пользователем любой клавиши
        /// с очисткой консоли
        /// </summary>
        private static void WaitForUserClickAnyButton()
        {
            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey(true);
            Console.Clear();
        }
        #endregion

        /// <summary>
        /// Исследование создания одномерных сеток
        /// </summary>
        private static void GenerateGridTests()
        {
            Grid grid = new Grid(10m, 1m, 0.5m, 10, 5, 5);            
            Console.WriteLine($"Проверка корректности сетки: {grid.GridValidity()}");

            var result = grid.InsertLayerX(10m);
            result = grid.InsertLayerX(0.3m);
            result = grid.InsertLayerX(0.2m);
            result = grid.InsertLayerX(0.1m);
            result = grid.InsertLayerX(9.0m);
            result = grid.InsertLayerX(1.5m);
            result = grid.InsertLayerX(4.3m);
            result = grid.InsertLayerX(9.7m);

            /////////////////////////
            XmlSerializer formatter = new XmlSerializer(typeof(Grid));
            using (FileStream fs = new FileStream("grid1D.xml", FileMode.Create))
            {
                formatter.Serialize(fs, grid);

                Console.WriteLine("Объект сериализован");
            }

            ////////////////////////////
            WaitForUserClickAnyButton();
        }


        /// <summary>
        /// Тесты производительности алгоритмов генерирования сеток
        /// </summary>
        static void GenerateGridsTests()
        {
            Console.Clear();
           

            WaitForUserClickAnyButton();
        }

        
    }
}
