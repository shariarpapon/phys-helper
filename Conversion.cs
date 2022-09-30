namespace PhysHelperCore
{
    internal readonly struct Conversion
    {
        public static double ToDeg(double r) => r * 57.2958d;
        public static double ToRad(double d) => d * 0.0174533d;
        public static double snot(double a, int p) => a * System.Math.Pow(10, p);
        public static double uc(double a) => snot(a, -6);
        public static double nc(double a) => snot(a, -9);
        public static double cm(double a) => snot(a, -2);
        public static double mm(double a) => snot(a, -3);
        public static double nm(double a) => snot(a, -9);
        public static double um(double a) => snot(a, -6);
        public static double g(double a) => snot(a, -3);
        public static double mg(double a) => snot(a, -6);
        public static double m_cm(double a) => snot(a, 2);
        public static double km(double a) => snot(a, 3);
        public static double hr(double a) => a * 3600;
        public static double min(double a) => a * 60;
    }
}
