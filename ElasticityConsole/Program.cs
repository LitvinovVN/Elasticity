using ElasticityClassLibrary;
using System;

namespace ElasticityConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = Grid.Generate3DGrid(10m,1.5m,2m,4,5,3);
                       

            Console.WriteLine(grid.ToString());

            

            Console.ReadKey();
        }
    }
}
