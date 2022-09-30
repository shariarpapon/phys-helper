using System.Collections.Generic;

namespace PhysHelperCore
{
    internal readonly struct Parser
    {
        private const char ARG_SEPERATOR = ',';
        private static readonly char[] INDEX_RECALL_GATE_TOKENS = new char[] { '<', '>'};
        private static readonly char[] OPR_GATE_TOKENS = new char[] { '[', ']' };
        private static readonly char[] OPR_ARG_GATE_TOKENS = new char[] { '{', '}' };
        private const string INDEX_NUMBERS = "0123456789";

        public static string ParseExpression(string rawInput, List<string> resultHistory)
        {
            string parsedExpression = "";
            int i = 0;
            while (i < rawInput.Length)
            {
                char c = rawInput[i];
                string currentTerm = c.ToString();
                

                if (c == INDEX_RECALL_GATE_TOKENS[0])
                {
                    string index_str = "";
                    char ik = rawInput[i];                   
                    i++;
                    while (ik != INDEX_RECALL_GATE_TOKENS[1])
                    {
                        if (i < rawInput.Length)
                        {
                            char id_char = rawInput[i];
                            if (INDEX_NUMBERS.Contains(id_char)) index_str += id_char;
                            i++;
                            if (i < rawInput.Length) ik = rawInput[i];
                            else break;
                        }
                    }

                    if (int.TryParse(index_str, out int id_num))
                        currentTerm = resultHistory[id_num];
                }
                parsedExpression += currentTerm;
                i++;
            }

            parsedExpression = ParseOperation(parsedExpression);

            return parsedExpression;
        }

        private static string ParseOperation(string parsedExpr) 
        {
            string parsedOpr = "";
            int i = 0;
            while (i < parsedExpr.Length)
            {
                char c = parsedExpr[i];
                string currentTerm = c.ToString();

                //
                if (c == OPR_GATE_TOKENS[0])
                {
                    char k = OPR_GATE_TOKENS[0];
                    string opr = "";
                    i++;
                    while (k != OPR_GATE_TOKENS[1])
                    {
                        if (i < parsedExpr.Length)
                        {
                            opr += parsedExpr[i];
                            i++;
                            k = parsedExpr[i];
                        }
                    }
                    currentTerm = EvaluateOperation(opr);
                }
                //
                parsedOpr += currentTerm;
                i++;
            }

            return parsedOpr;
        }

        public static string EvaluateOperation(string opeartion)
        {
            opeartion = opeartion.ToLower().Replace(" ", "");

            string consts = ParseConstants(opeartion);
            if (consts != null) return consts;

            string evalFormula = ParseFormulaOperation(opeartion);
            if (evalFormula != null) return evalFormula;

            string opr = "";
            for (int i = 0; i < opeartion.Length; i++)
            {
                if (opeartion[i] == OPR_ARG_GATE_TOKENS[0]) break;
                opr += opeartion[i];
            }

            opeartion = opeartion.Replace(opr, "");
            opeartion = opeartion.Replace(OPR_ARG_GATE_TOKENS[0], ' ');
            opeartion = opeartion.Replace(OPR_ARG_GATE_TOKENS[1], ' ');
            opeartion.Replace(" ", "");

            if (double.TryParse(opeartion, out double a))
            {
                double num = a;
                switch (opr.ToLower())
                {
                    case "uc": num = Conversion.uc(a); break;
                    case "nc": num = Conversion.nc(a); break;
                    case "cm": num = Conversion.cm(a); break;
                    case "mm": num = Conversion.mm(a); break;
                    case "nm": num = Conversion.nm(a); break;
                    case "um": num = Conversion.um(a); break;
                    case "g": num = Conversion.g(a); break;
                    case "mg": num = Conversion.mg(a); break;
                    case "m_cm": num = Conversion.m_cm(a); break;
                    case "km": num = Conversion.km(a); break;
                    case "hr": num = Conversion.hr(a); break;
                    case "min": num = Conversion.min(a); break;
                }
                return num.ToString();
            }

            return null;
        }

        public static string ParseFormulaOperation(string opr) 
        {
            string[] oprParts = opr.Split(OPR_ARG_GATE_TOKENS[0]);
            string rawArgs = oprParts[1].Replace(OPR_ARG_GATE_TOKENS[1].ToString(), "").ToLower();
            string[] args_str = rawArgs.Split(ARG_SEPERATOR);
            double[] args = new double[args_str.Length];
            for (int i = 0; i < args_str.Length; i++)
                if (double.TryParse(args_str[i], out double n))
                    args[i] = n;

            return oprParts[0].ToLower() switch
            {
                "cf" => PhysMath.CForce(args[0], args[1], args[2]).ToString(),
                "cf_ef"=> PhysMath.CForce_EF(args[0], args[1]).ToString(),
                "ef" => PhysMath.EField(args[0], args[1]).ToString(),
                "ef_pd" => PhysMath.EField_PD(args[0], args[1]).ToString(),
                "pd" => PhysMath.PotDiff(args[0], args[1]).ToString(),
                "pd_osc" => PhysMath.PotDiff_Osc(args[0], args[1], args[2]).ToString(),
                "cw" =>PhysMath.CWork(args[0], args[1], args[2]).ToString(),
                "fd_w" => PhysMath.FDWork(args[0], args[1]).ToString(),
                "mag" => PhysMath.Mag(args[0], args[1]).ToString(),
                "snot" => Conversion.snot(args[0], (int)args[1]).ToString(),
                "deg" => Conversion.ToDeg(args[0]).ToString(),
                "rad" => Conversion.ToRad(args[0]).ToString(),
                "ceg" => PhysMath.CEnergy(args[0], args[1]).ToString(),
                "e_pd" => PhysMath.ElecPotDiff(args[0], args[1]).ToString(),
                "ep_eg" => PhysMath.ElecPotEnergy(args[0], args[1], args[2]).ToString(),
                "ke_dm" => PhysMath.KE_DroppedMass(args[0], args[1], args[2], args[3]).ToString(),
                "vel_dm" => PhysMath.Vel_DroppedMass(args[0], args[1], args[2], args[3], args[4]).ToString(),
                "cap" => PhysMath.Capacitance(args[0], args[1]).ToString(),
                "avg" => PhysMath.Average(args).ToString(),
                _ => null,
            };
        }

        public static string ParseConstants(string input)
        {
            return input.ToLower() switch
            {
                "k" => PhysConstants.K.ToString(),
                "charge" => PhysConstants.CHARGE.ToString(),
                "p_m" => PhysConstants.P_MASS.ToString(),
                "e_m" => PhysConstants.E_MASS.ToString(),
                "a_m"=> PhysConstants.A_MASS.ToString(),
                "eps_n" => PhysConstants.EPS_NAUGHT.ToString(),
                "elec_s"=> PhysConstants.ELECTRO_STATIC.ToString(), 
                "avog" => PhysConstants.AVOGADRO.ToString(),
                _=> null,
            };
        }
    }

}
