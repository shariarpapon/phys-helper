namespace PhysHelperCore
{
    internal readonly struct PhysConstants
    {
        public const double K = 8990000000d;
        public static readonly double EPS_NAUGHT = Conversion.snot(8.854d, -12);
        public static readonly double CHARGE = Conversion.snot(1.60217663d, -19);
        public static readonly double P_MASS = Conversion.snot(1.67d, -27);
        public static readonly double E_MASS = Conversion.snot(9.1093837d, -31);
        public static readonly double A_MASS = Conversion.snot(6.644657d, -27);
        public static readonly double ELECTRO_STATIC = Conversion.snot(8.98755d, 9);
        public static readonly double AVOGADRO = Conversion.snot(6.0221408d, 23);
    }
}
