using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ElasticityClassLibrary
{
    /// <summary>
    /// Монитор производительности
    /// </summary>
    public static class PerformanceMonitor
    {
        static Stopwatch timer = new Stopwatch();
        static long bytesPhysicalBefore = 0;
        static long bytesVirtualBefore = 0;

        /// <summary>
        /// Запускает монитор
        /// </summary>
        public static void Start()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            bytesPhysicalBefore = Process.GetCurrentProcess().WorkingSet64;
            bytesVirtualBefore = Process.GetCurrentProcess().VirtualMemorySize64;
            timer.Restart();
        }

        /// <summary>
        /// Останавливает монитор и возвращает объект с результатами мониторинга
        /// </summary>
        public static PerformanceMonitorResults Stop()
        {
            timer.Stop();
            long bytesPhysicalAfter = Process.GetCurrentProcess().WorkingSet64;
            long bytesVirtualAfter = Process.GetCurrentProcess().VirtualMemorySize64;

            PerformanceMonitorResults results = new PerformanceMonitorResults();
            results.BytesPhysicalUsed = bytesPhysicalAfter - bytesPhysicalBefore;
            results.BytesVirtualUsed = bytesVirtualAfter - bytesVirtualBefore;
            results.Elapsed = timer.Elapsed;
            results.ElapsedMilliseconds = timer.ElapsedMilliseconds;

            return results;
        }
    }    
}
