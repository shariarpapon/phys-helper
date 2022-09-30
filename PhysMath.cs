namespace PhysHelperCore
{
    internal readonly struct PhysMath
    {
        public static double PotDiff_Osc(double q, double amp, double k) => k * amp * amp / (2 * q); 
        public static double EField_PD(double potDiff, double d) => potDiff / d;
        public static double PotDiff(double work, double q) => work / q;
        public static double ElecPotDiff(double q, double r) => EField(q, r) * r;
        public static double ESMaxDisp(double Fe, double k) => 2 * Fe / k;
        public static double CVelocity_EG(double ke_pe, double m) => System.Math.Sqrt(ke_pe * 2d / m);
        public static double CWork(double q, double ef, double d) => q * ef * d;
        public static double FDWork(double f, double d) => f * d;
        public static double EField(double q, double r) => (PhysConstants.K * q) / (r * r);
        public static double CForce(double q1, double q2, double r) =>System.Math.Abs(PhysConstants.K * q1 * q2) / (r * r);
        public static double CForce_EF(double q, double ef) => q * ef;
        public static double SForce(double k, double x) => k * x;
        public static double SEnergy(double k, double x) => 0.5d * k * x * x;
        public static double Mag(double a, double b) => System.Math.Sqrt(a * a + b * b);
        public static double CEnergy(double q, double r) => PhysConstants.K * (q / r);
        public static double ElecPotEnergy(double q1, double q2, double r) => PhysConstants.K * q1 * q2 / r;
        public static double KE_DroppedMass(double q1, double q2, double ri, double rf) => PhysConstants.K * q1 * q2 * (1/ri - 1/rf);
        public static double Vel_DroppedMass(double q1, double q2, double ri, double rf, double m) => System.Math.Sqrt(KE_DroppedMass(q1, q2, ri, rf) * 2 / m);
        public static double Capacitance(double area, double d) => PhysConstants.EPS_NAUGHT * area / d;
        public static double Average(double[] values) 
        {
            double sum = 0;
            for (int i = 0; i < values.Length; i++) sum += values[i];
            return sum / values.Length;
        }
    }
}
