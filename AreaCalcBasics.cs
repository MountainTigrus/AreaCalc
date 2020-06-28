using System;

namespace AreaCalc
{
    public static class Basics
    {
        // ----- Радиус круга ----------------------------
        public static double CircleArea(double Radius)
        {
            return Math.Round(Math.PI * Radius * Radius, 3);
        }

        // ----- Радиус сектора круга --------------------
        public static double CircleArea(double Radius, double AngleRadian)
        {
            return (Math.Abs(AngleRadian) > 2 * Math.PI)
                ? CircleArea(Radius)
                : Math.Round(CircleArea(Radius) * Math.Abs(AngleRadian) / (2 * Math.PI), 3);
        }

        // ----- Площадь треугольника по 3м сторонам -----
        public static double TriangleArea(double Len_Side1, double Len_Side2, double Len_Side3)
        {
            if (Len_Side1 <= 0 | Len_Side2 <= 0 | Len_Side3 <= 0)
                throw new Exception("Длина стороны меньше или равна нулю");

            if (Len_Side1 > Len_Side2 + Len_Side3 | Len_Side2 > Len_Side1 + Len_Side3 | Len_Side3 > Len_Side1 + Len_Side2)
                throw new Exception("Длины сторон не соответствуют треугольнику");

            return Math.Round(
                Math.Sqrt(Math.Pow(Len_Side1 * Len_Side1 + Len_Side2 * Len_Side2 + Len_Side3 * Len_Side3, 2)
                - 2 * (Math.Pow(Len_Side1, 4) + Math.Pow(Len_Side2, 4) + Math.Pow(Len_Side3, 4))) / 4
                , 3);
        }

        // ----- Проверка прямоугольности треугольника ---
        public static bool IsRectangular(double Len_Side1, double Len_Side2, double Len_Side3)
        {
            if (Len_Side1 <= 0 | Len_Side2 <= 0 | Len_Side3 <= 0)
                throw new Exception("Длина стороны меньше или равна нулю");

            if (Len_Side1 > Len_Side2 + Len_Side3 | Len_Side2 > Len_Side1 + Len_Side3 | Len_Side3 > Len_Side1 + Len_Side2)
                throw new Exception("Длины сторон не соответствуют треугольнику");

            if (Len_Side1 * Len_Side1 == Len_Side2 * Len_Side2 + Len_Side3 * Len_Side3 |
                Len_Side2 * Len_Side2 == Len_Side1 * Len_Side1 + Len_Side3 * Len_Side3 |
                Len_Side3 * Len_Side3 == Len_Side1 * Len_Side1 + Len_Side2 * Len_Side2)
                return true;
            else return false;
        }


        // --------------------------------------------------------
        // ---------- Площадь n-угольника -------------------------
        // --------------------------------------------------------

        // ----- Проверка пересечения рёбер --------------
        private static bool Crossing((double, double) P1, (double, double) P2, (double, double) Q1, (double, double) Q2)
        {
            double DirectedArea((double, double) A, (double, double) B, (double, double) C)
            {
                return (B.Item1 - A.Item1) * (C.Item2 - A.Item2) - (B.Item2 - A.Item2) * (C.Item1 - A.Item1);
            }

            return DirectedArea(P1, P2, Q1) * DirectedArea(P1, P2, Q2) < 0 &
                DirectedArea(Q1, Q2, P1) * DirectedArea(Q1, Q2, P2) < 0;
        }
        // ----- Основная функция ------------------------
        public static double PolygonArea((double, double)[] Vertex)
        {
            // проверка отсутствия дубля последовательных точек
            for (int i = 0; i < Vertex.Length - 1; i++)
                for (int j = i + 1; j < Vertex.Length; j++)
                    if (Vertex[i].Item1 == Vertex[j].Item1 & Vertex[i].Item2 == Vertex[j].Item2)
                        throw new Exception("Множественные многоугольники: две вершины совпадают.");

            // Проверка пересечений отрезков
            for (int i = 0; i < Vertex.Length - 2; i++)
                for (int j = i + 1; j < Vertex.Length - 1; j++)
                    if (Crossing(Vertex[i], Vertex[i + 1], Vertex[j], Vertex[j + 1]))
                        throw new Exception("Множественные многоугольники: пересечение рёбер.");
            for (int i = 0; i < Vertex.Length - 1; i++)
                if (Crossing(Vertex[0], Vertex[Vertex.Length - 1], Vertex[i], Vertex[i + 1]))
                    throw new Exception("Множественные многоугольники: пересечение рёбер.");

            // Вычисление по формуле Гаусса
            double S = 0;
            for (int i = 0; i < Vertex.Length - 1; i++)
                S += Vertex[i].Item1 * Vertex[i + 1].Item2 - Vertex[i].Item2 * Vertex[i + 1].Item1;
            S += Vertex[Vertex.Length - 1].Item1 * Vertex[0].Item2 - Vertex[Vertex.Length - 1].Item2 * Vertex[0].Item1;

            return Math.Round(Math.Abs(S / 2), 3);
        }
    }
}



