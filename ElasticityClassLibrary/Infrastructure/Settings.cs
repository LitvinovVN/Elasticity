namespace ElasticityClassLibrary.Infrastructure
{
    /// <summary>
    /// Настройки 
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Режим инициализации шага сетки (параметра H)
        /// </summary>
        public static ModeEnum GridLayerParameters_H_InitMode { get; set; } = ModeEnum.Calculated;

        /// <summary>
        /// Значение шага сетки (параметра H).
        /// Используется, когда GridLayerParameters_H_InitMode == ModeEnum.Manual
        /// </summary>
        public static decimal GridLayerParameters_H { get; set; } = 1m;
    }
}