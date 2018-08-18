using ElasticityClassLibrary.Derivatives;
using ElasticityClassLibrary.Infrastructure;
using System;

namespace ElasticityClassLibrary.GridNamespace
{
    /// <summary>
    /// Расчетные параметры слоя
    /// </summary>
    public class GridLayerParameters
    {
        /* 
         *      |                 |                |                 |               |               |              |
         *     i-3               i-2              i-1                i              i+1             i+2            i+3
         *         Step3Backwards   Step2Backwards   Step1Backwards    Step1Forwards    Step2Forwards   Step3Forwards
         */

        #region Величины шагов сетки
        /// <summary>
        /// Шаг сетки для расчета производных
        /// </summary>
        public decimal? H { get; set; }
        
        /// <summary>
        /// Величина первого шага вперёд по сетке
        /// (расстояние от текущего слоя (i) до следующего (i+1)
        /// </summary>
        public decimal? Step1F { get; set; }

        /// <summary>
        /// Величина второго шага вперёд по сетке
        /// (расстояние от i+1 до i+2)
        /// </summary>
        public decimal? Step2F { get; set; }

        /// <summary>
        /// Величина третьего шага вперёд по сетке
        /// (расстояние от i+2 до i+3)
        /// </summary>
        public decimal? Step3F { get; set; }

        /// <summary>
        /// Величина первого шага назад по сетке
        /// (расстояние от текущего слоя (i) до предыдущего (i-1)
        /// </summary>
        public decimal? Step1B { get; set; }
        
        /// <summary>
        /// Величина второго шага назад по сетке
        /// (расстояние от i-1 до i-2)
        /// </summary>
        public decimal? Step2B { get; set; }
        
        /// <summary>
        /// Величина третьего шага назад по сетке
        /// (расстояние от i-2 до i-3)
        /// </summary>
        public decimal? Step3B { get; set; }
        #endregion

        #region Коэффициенты соотношения величин шагов
        /// <summary>
        /// Соотношение между первым шагом вперёд и H
        /// </summary>
        public decimal? Alfa1F { get; set; }

        /// <summary>
        /// Соотношение между вторым шагом вперёд и H
        /// </summary>
        public decimal? Alfa2F { get; set; }

        /// <summary>
        /// Соотношение между третьим шагом вперёд и H
        /// </summary>
        public decimal? Alfa3F { get; set; }

        /// <summary>
        /// Соотношение между первым шагом назад и H
        /// </summary>
        public decimal? Alfa1B { get; set; }

        /// <summary>
        /// Соотношение между вторым шагом вперёд и H
        /// </summary>
        public decimal? Alfa2B { get; set; }

        /// <summary>
        /// Соотношение между третьим шагом вперёд и H
        /// </summary>
        public decimal? Alfa3B { get; set; }
        #endregion

        /// <summary>
        /// Оператор для вычисления первой производной
        /// </summary>
        public DerivativeOperator Derivative1 { get; set; }

        /// <summary>
        /// Оператор для вычисления второй производной
        /// </summary>
        public DerivativeOperator Derivative2 { get; set; }

        #region Конструкторы
        /// <summary>
        /// Стандартный конструктор - используется для сериализации
        /// </summary>
        public GridLayerParameters()
        {

        }

        /// <summary>
        /// Формирование объекта по переданным координатам
        /// </summary>
        /// <param name="coordCentral">Координата центральной точки</param>
        /// <param name="coord1Forwards">Координата первой справа точки</param>
        /// <param name="coord2Forwards">Координата второй справа точки</param>
        /// <param name="coord3Forwards">Координата третьей справа точки</param>
        /// <param name="coord1Backwards">Координата первой слева точки</param>
        /// <param name="coord2Backwards">Координата второй слева точки</param>
        /// <param name="coord3Backwards">Координата третьей слева точки</param>
        public GridLayerParameters(decimal coordCentral,
            decimal coord1Forwards,
            decimal coord2Forwards,
            decimal coord3Forwards,
            decimal coord1Backwards,
            decimal coord2Backwards,
            decimal coord3Backwards)
        {
            // Вычисляем шаги
            Step1F  = coord1Forwards - coordCentral;
            Step2F  = coord2Forwards - coord1Forwards;
            Step3F  = coord3Forwards - coord2Forwards;
            Step1B = coordCentral - coord1Backwards;
            Step2B = coord1Backwards - coord2Backwards;
            Step3B = coord2Backwards - coord3Backwards;

            // Вычисляем соотношения шагов
            Alfa1F = Step1F / Step1B;
            Alfa2F = Step2F / Step1B;
            Alfa3F = Step3F / Step1B;
            Alfa2B = Step2B / Step1B;
            Alfa3B = Step3B / Step1B;
        }        

        /// <summary>
        /// Формирование объекта по переданным слоям
        /// </summary>
        /// <param name="backwards3Layer">Третий слой слева</param>
        /// <param name="backwards2Layer">Второй слой слева</param>
        /// <param name="backwards1Layer">Первый слой слева</param>
        /// <param name="currentLayer">Центральный слой</param>
        /// <param name="forwards1Layer">Первый слой справа</param>
        /// <param name="forwards2Layer">Второй слой справа</param>
        /// <param name="forwards3Layer">Третий слой справа</param>
        public GridLayerParameters(GridLayer backwards3Layer,
            GridLayer backwards2Layer,
            GridLayer backwards1Layer,
            GridLayer currentLayer,
            GridLayer forwards1Layer,
            GridLayer forwards2Layer,
            GridLayer forwards3Layer)
        {
            if (currentLayer == null) return;
            if (forwards1Layer == null && backwards1Layer == null) return;
            
            InitBackwardsSteps(backwards3Layer, backwards2Layer,
                backwards1Layer, currentLayer);
            InitForwardsSteps(currentLayer, forwards1Layer,
                forwards2Layer, forwards3Layer);
            InitH();
            InitAlfa();
            InitDerivatives();
        }

        /// <summary>
        /// Расчет производных
        /// </summary>
        private void InitDerivatives()
        {
            if (Alfa1B!=null && Alfa1F!=null && H!=null)
            {
                Derivative1 = Derivative.GetDerivative1Operator((decimal)Alfa1B,
                    (decimal)Alfa1F, (decimal)H);
                Derivative2 = Derivative.GetDerivative2Operator((decimal)Alfa1B,
                    (decimal)Alfa1F, (decimal)H);
            }
        }

        /// <summary>
        /// Инициализация коэффициентов, равных отношению шагов к H
        /// </summary>
        private void InitAlfa()
        {
            if (H == null) return;

            if (Step1B != null) Alfa1B = Step1B / H;
            if (Step2B != null) Alfa2B = Step2B / H;
            if (Step3B != null) Alfa3B = Step3B / H;

            if (Step1F != null) Alfa1F = Step1F / H;
            if (Step2F != null) Alfa2F = Step2F / H;
            if (Step3F != null) Alfa3F = Step3F / H;
        }

        /// <summary>
        /// Инициализирует параметр H (расчетный шаг сетки)
        /// </summary>
        private void InitH()
        {
            if(Settings.GridLayerParameters_H_InitMode == ModeEnum.Manual)
            {
                H = Settings.GridLayerParameters_H;
            }
            else
            {
                if (Step1B != null)
                {
                    H = Step1B;
                }
                else if (Step1F != null)
                {
                    H = Step1F;
                }
            }
        }
        #endregion

        /// <summary>
        /// Рассчитывает шаги слоёв слева от центрального слоя
        /// </summary>
        /// <param name="backwards3Layer"></param>
        /// <param name="backwards2Layer"></param>
        /// <param name="backwards1Layer"></param>
        /// <param name="currentLayer"></param>
        void InitBackwardsSteps(GridLayer backwards3Layer,
            GridLayer backwards2Layer,
            GridLayer backwards1Layer,
            GridLayer currentLayer)
        {
            if (currentLayer == null) return;

            if (backwards1Layer == null) return;
            Step1B = currentLayer.Coordinate - backwards1Layer.Coordinate;

            if (backwards2Layer == null) return;
            Step2B = backwards1Layer.Coordinate - backwards2Layer.Coordinate;

            if (backwards3Layer == null) return;
            Step3B = backwards2Layer.Coordinate - backwards3Layer.Coordinate;
        }

        /// <summary>
        /// Рассчитывает шаги слоёв справа от центрального слоя
        /// </summary>
        /// <param name="currentLayer"></param>
        /// <param name="forwards1Layer"></param>
        /// <param name="forwards2Layer"></param>
        /// <param name="forwards3Layer"></param>
        void InitForwardsSteps(GridLayer currentLayer,
            GridLayer forwards1Layer,
            GridLayer forwards2Layer,
            GridLayer forwards3Layer)
        {
            if (currentLayer == null) return;

            if (forwards1Layer == null) return;
            Step1F = forwards1Layer.Coordinate - currentLayer.Coordinate;

            if (forwards2Layer == null) return;
            Step2F = forwards2Layer.Coordinate - forwards1Layer.Coordinate;

            if (forwards3Layer == null) return;
            Step3F = forwards3Layer.Coordinate - forwards2Layer.Coordinate;
        }
    }
}