using ElasticityClassLibrary;
using ElasticityClassLibrary.Derivatives;
using ElasticityClassLibrary.GeometryNamespase;
using ElasticityClassLibrary.GridNamespace;
using ElasticityClassLibrary.Infrastructure;
using ElasticityClassLibrary.NagruzkaNamespace;
using ElasticityClassLibrary.SolverNamespace;
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
                Console.WriteLine("2. GridInsertionsRandom();");
                Console.WriteLine("3. GeometryParallelepipedTests();");
                Console.WriteLine("4. GridWithGeometryPreCalculatedTests();");
                Console.WriteLine("5. SolverTask1DTests();");
                key = Console.ReadKey(true).KeyChar;
                Console.Clear();

                switch (key)
                {
                    case '1':
                        GenerateGridTests();
                        break;
                    case '2':
                        GridInsertionsRandom();
                        break;
                    case '3':
                        GeometryParallelepipedTests();
                        break;
                    case '4':
                        GridWithGeometryPreCalculatedTests();
                        break;
                    case '5':
                        SolverTask1DTests();
                        break;
                }
                ////////////////////////////            
                WaitForUserClickAnyButton();
            }
            while ( key!='q' && key!='й');
                       
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
            Grid3D grid = new Grid3D(10m, 1m, 0.5m, 10, 5, 5);            
            Console.WriteLine($"Проверка корректности сетки: {grid.GridValidity()}");

            var result = grid.InsertLayerX(10m);
            result = grid.InsertLayerX(0.3m);
            result = grid.InsertLayerX(0.2m);
            result = grid.InsertLayerX(0.1m);
            result = grid.InsertLayerX(9.0m);
            result = grid.InsertLayerX(1.5m);
            result = grid.InsertLayerX(4.3m);
            result = grid.InsertLayerX(9.7m);
            result = grid.InsertLayerX(9.75m);

            result = grid.InsertLayerY(0.5m);

            result = grid.InsertLayerZ(0.43m);

            Console.WriteLine($"Проверка корректности сетки после вставки слоёв: {grid.GridValidity()}");

            /////////////////////////
            XmlSerializer formatter = new XmlSerializer(typeof(Grid3D));
            using (FileStream fs = new FileStream("grid1D.xml", FileMode.Create))
            {
                formatter.Serialize(fs, grid);

                Console.WriteLine("Объект сериализован");
            }            
        }


        /// <summary>
        /// Тесты алгоритмов вставки слоёв в сетку
        /// </summary>
        static void GridInsertionsRandom()
        {
            Grid3D grid = new Grid3D(10m, 1m, 0.5m, 100, 100, 100);
            Console.WriteLine($"Сгенерирована сетка 10*1*0.5 с числом узлов 100, 100, 100");
            Console.WriteLine($"Проверка корректности сетки: {grid.GridValidity()}");
                        
            // Исследование скорости вставки случайных слоёв
            Console.WriteLine("Вставка случайных слоёв по оси X");
            var r = new Random(DateTime.Now.Millisecond);
            uint numInsertions = 20000;
            PerformanceMonitor.Start();
            for (uint i = 0;i< numInsertions;i++)
            {
                // Генерируем случайную координату для вставки
                decimal coord = Convert.ToDecimal(r.NextDouble())*grid.GridSizeX;
                grid.InsertLayerX(coord);
            }
            var res = PerformanceMonitor.Stop();
            Console.WriteLine(res.ToString());

            Console.WriteLine($"Проверка корректности сетки после вставки слоёв: {grid.GridValidity()}");

            // Пересчет индексов
            grid.CorrectIndexes();
            Console.WriteLine($"Проверка корректности сетки после корректировки индексов: {grid.GridValidity()}");

            /////////////////////////
            XmlSerializer formatter = new XmlSerializer(typeof(Grid3D));
            using (FileStream fs = new FileStream("GridInsertionsRandom.xml", FileMode.Create))
            {
                formatter.Serialize(fs, grid);

                Console.WriteLine("Объект сериализован");
            }            
        }

        /// <summary>
        /// Исследование характеристик работы с геометрией
        /// в виде одиночного параллелепипеда
        /// </summary>
        private static void GeometryParallelepipedTests()
        {
            // Примитив - параллелепипед
            Coordinate3D coordinate3D = new Coordinate3D(1m, 1m, 1m);
            GeometryPrimitive3DParallelepiped parallelepiped = new GeometryPrimitive3DParallelepiped(coordinate3D, 5m, 1m, 0.5m);

            NagruzkaRaspredRavnomern nagr1 = new NagruzkaRaspredRavnomern();
            nagr1.Q = new Q(200000, 100000, 50000);
            var areaRectangle1 = new AreaRectangle();
            areaRectangle1.Coordinate3DPointMinDistant = new Coordinate3D(1m, 2m, 0.5m);
            areaRectangle1.Coordinate3DPointMaxDistant = new Coordinate3D(6m, 2m, 1.5m);
            nagr1.Area = areaRectangle1;
            parallelepiped.NagruzkaList.Add(nagr1);

            GeometryElement geometryElement = new GeometryElement3D(new Coordinate3D(0.5m, 1m, 1.5m));
            geometryElement.GeometryPrimitives.Add(parallelepiped);
            geometryElement.GeometryPrimitives.Add(new GeometryPrimitive3DCube(new Coordinate3D(1.1m,1.2m,1.1m),0.15m,true));

            Geometry3D geometry = new Geometry3D();
            geometry.GeometryElements.Add(geometryElement);

            /////////////////////////            
            Console.WriteLine("Сериализация объекта: {0}", geometry.ExportToXML("", "GeometryParallelepiped.xml"));
            Geometry3D g2 = (Geometry3D)Geometry.ImportFromXML("", "GeometryParallelepiped.xml");
            Console.WriteLine("Проверка на равенство серализованного и десериализованного объектов: {0}", Geometry3D.IsGeometryValuesEquals(geometry, g2));            
        }

        /// <summary>
        /// Исследование характеристик работы с объектом GridWithGeometryPreCalculated
        /// </summary>
        private static void GridWithGeometryPreCalculatedTests()
        {
            Grid3D grid = new Grid3D(10m, 10m, 10m, 5, 10, 20);

            Geometry3D geometry = new Geometry3D();
            GeometryElement geometryElement = new GeometryElement3D(new Coordinate3D(1m, 2m, 3m));
            geometryElement.GeometryPrimitives.Add(new GeometryPrimitive3DParallelepiped(new Coordinate3D(0.1m, 0.2m, 0.3m), 3m, 1m, 2m));
            geometry.GeometryElements.Add(geometryElement);

            var gridWithGeometryPreCalculated = new GridWithGeometryPreCalculated3D(grid, geometry);

            /////////////////////////            
            Console.WriteLine("Сериализация объекта GridWithGeometryPreCalculated: {0}", gridWithGeometryPreCalculated.ExportToXML("", "GridWithGeometryPreCalculated.xml"));
            GridWithGeometryPreCalculated3D g2 = (GridWithGeometryPreCalculated3D)GridWithGeometryPreCalculated.ImportFromXML("", "GridWithGeometryPreCalculated.xml");
            //Console.WriteLine("Проверка на равенство серализованного и десериализованного объектов: {0}", GridWithGeometryPreCalculated.IsGridWithGeometryPreCalculatedValuesEquals(gridWithGeometryPreCalculated, g2));           
        }

        /// <summary>
        /// Исследование характеристик работы решателя одномерных задач
        /// </summary>
        private static void SolverTask1DTests()
        {
            // Задаём дифференциальный оператор
            var op = new DerivativeOperator1D3P();
            op.Kim1 = - 1m / (2m * 0.5m);
            op.Ki   =   2m;
            op.Kip1 =   1m / (2m * 0.5m);

            // Создаём одномерную расчетную область
            var raschOblast1D = new Grid1D(1.5m, 11, AxisEnum.X);
            // Создаём исследуемый объект
            var lineSegment = new GeometryPrimitive1DLineSegment(new Coordinate1D(0m),0.5m);

            var raschObjectGeometry1D = new Geometry1D();
            var geometryElement = new GeometryElement1D(new Coordinate1D(0.5m));
            geometryElement.AddGeometryPrimitive(lineSegment);
            raschObjectGeometry1D.AddGeometryElement(geometryElement);

            var gridWithGeometryPrecalculated1D = new GridWithGeometryPreCalculated1D(raschOblast1D, raschObjectGeometry1D);

            var nodeSet1D = gridWithGeometryPrecalculated1D.NodeSet1D;

            // Создаём задачу
            var task = new SolverTask1D();
            task.NodeSet1D = nodeSet1D;

            SolverResult result = Solver.Calculate(task);
        }
    }
}
