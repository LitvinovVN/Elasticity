using ElasticityClassLibrary;
using System;
using System.Collections.Generic;

namespace ElasticityConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateGridsTests();

            List<Grid> testGrids = new List<Grid>();

            for(int i=0;i<=2;i++)
            {
                Console.WriteLine($"Генерирование сетки {i}. Grid.Generate3DGrid(10m,1.5m,2m,100,10,10):\n");
                PerformanceMonitor.Start();
                var grid = Grid.Generate3DGrid(10m, 1.5m, 2m, 100, 10, 10);
                var pmResults = PerformanceMonitor.Stop();
                Console.WriteLine(grid.GetDescription);
                Console.WriteLine(pmResults.ToString());

                testGrids.Add(grid);
            }

            CheckGrids(testGrids);

            Console.WriteLine("\n-------------------------------------\n");
            Console.WriteLine("\ngrid.SetPrevAndNextNodes(false) - последовательная реализация:\n");
            PerformanceMonitor.Start();
            testGrids[0].SetPrevAndNextNodes(false);
            var pmResults2 = PerformanceMonitor.Stop();
            Console.WriteLine(pmResults2.ToString());

            Console.WriteLine("\ngrid.SetPrevAndNextNodes(true) - параллельная реализация:\n");
            PerformanceMonitor.Start();
            testGrids[1].SetPrevAndNextNodes(true);
            var pmResults3 = PerformanceMonitor.Stop();
            Console.WriteLine(pmResults3.ToString());

            Console.WriteLine("\ngrid.SetPrevAndNextNodes(true,true) - параллельная реализация с использованием Partitioner:\n");
            PerformanceMonitor.Start();
            testGrids[2].SetPrevAndNextNodes(true,true);
            var pmResults4 = PerformanceMonitor.Stop();
            Console.WriteLine(pmResults4.ToString());

            CheckGrids(testGrids);
            ///////////////


            //Console.WriteLine(grid.ToString());

            //Console.WriteLine("\nПоиск узла [1,0,2]");
            //Node findedNode = grid.GetNode(1, 0, 2);
            //Console.WriteLine(findedNode.ToString());

            //Console.WriteLine("\nПоиск несуществующего узла [1,0,-2]");
            //Node findedNode2 = grid.GetNode(1, 0, -2);
            //Console.WriteLine(findedNode2?.ToString() ?? "Узел не существует");

            //Console.WriteLine("\n-----Внутренние узлы----- ");
            //var nodesInsideSurfase = grid.NodesInternal;
            //Console.WriteLine(nodesInsideSurfase.ToStringFromList());

            //Console.WriteLine("\n-----Узлы на поверхности----- ");
            //var nodesOnTheSurface = grid.NodesOnTheSurface;
            //Console.WriteLine(nodesOnTheSurface.ToStringFromList());

            //Console.WriteLine("\n-----Внешние (фиктивные) узлы----- ");
            //var nodesUoterSurface = grid.NodesOuter;
            //Console.WriteLine(nodesUoterSurface.ToStringFromList());

            Console.ReadKey();
        }

        /// <summary>
        /// Тесты производительности алгоритмов генерирования сеток
        /// </summary>
        static void GenerateGridsTests()
        {            
            decimal sizeX = 1m;
            decimal sizeY = 1m;
            decimal sizeZ = 1m;
            int NumberNodesXmin = 1000;
            int NumberNodesYmin = 10;
            int NumberNodesZmin = 10;
            int NumberNodesXmax = 1000;
            int NumberNodesYmax = 100;
            int NumberNodesZmax = 100;
            int NumberNodesXstep = 1000;
            int NumberNodesYstep = 10;
            int NumberNodesZstep = 10;

            Console.WriteLine($"---Тест генерирования сетки t=f(NumberNodesX) ---");
            for (int x = NumberNodesXmin;x <= NumberNodesXmax; x += NumberNodesXstep)
            {                
                PerformanceMonitor.Start();
                var grid = Grid.Generate3DGrid(sizeX, sizeY, sizeZ, x, 10, 10);
                var pmResults = PerformanceMonitor.Stop();
                Console.WriteLine($"{grid.GetDescription} \t {pmResults.ElapsedMilliseconds} мс");                
            }

            

            
        }

        /// <summary>
        /// Проверка равенства содержимого сеток
        /// </summary>
        /// <param name="testGrids"></param>
        static void CheckGrids(List<Grid> testGrids)
        {
            Console.WriteLine("\n-----Проверка сеток-----");

            //// Сравнение результатов расчетов
            bool isCoordXEquals = true;
            bool isCoordYEquals = true;
            bool isCoordZEquals = true;
            for (int i = 0; i < testGrids[0].Nodes.Count; i++)
            {
                if (!(testGrids[0].Nodes[i].Coordinates.CoordX == testGrids[1].Nodes[i].Coordinates.CoordX &&
                        testGrids[0].Nodes[i].Coordinates.CoordX == testGrids[2].Nodes[i].Coordinates.CoordX))
                {
                    isCoordXEquals = false;
                    break;
                }


                if (!(testGrids[0].Nodes[i].Coordinates.CoordY == testGrids[1].Nodes[i].Coordinates.CoordY &&
                        testGrids[0].Nodes[i].Coordinates.CoordY == testGrids[2].Nodes[i].Coordinates.CoordY))
                {
                    isCoordYEquals = false;
                    break;
                }

                if (!(testGrids[0].Nodes[i].Coordinates.CoordZ == testGrids[1].Nodes[i].Coordinates.CoordZ &&
                        testGrids[0].Nodes[i].Coordinates.CoordZ == testGrids[2].Nodes[i].Coordinates.CoordZ))
                {
                    isCoordZEquals = false;
                    break;
                }
            }

            if (!isCoordXEquals)
            {
                Console.WriteLine($"Ошибка! Координаты по оси X не равны!");
            }
            else
            {
                Console.WriteLine($"ОК. Координаты по оси X равны.");
            }

            if (!isCoordYEquals)
            {
                Console.WriteLine($"Ошибка! Координаты по оси Y не равны!");
            }
            else
            {
                Console.WriteLine($"ОК. Координаты по оси Y равны.");
            }

            if (!isCoordZEquals)
            {
                Console.WriteLine($"Ошибка! Координаты по оси Z не равны!");
            }
            else
            {
                Console.WriteLine($"ОК. Координаты по оси Z равны.");
            }
        }
    }
}
