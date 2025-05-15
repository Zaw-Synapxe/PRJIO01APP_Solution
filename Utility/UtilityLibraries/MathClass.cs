using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLibraries
{
    public class MathClass
    {
        public MathClass() { }

        public double Calculate(double num1, double num2, string op)
        {
            double result = 0;
            switch (op)
            {
                case "Add":
                    result = num1 + num2;
                    break;
                case "Sub":
                    result = num1 + num2;
                    break;
                case "Mul":
                    result = num1 * num2;
                    break;
                case "Div":
                    result = (double)num1 / num2;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
