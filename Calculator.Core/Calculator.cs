using System;

namespace Calculator.Core
{
    public class Calculator : ICalculator
    {
        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Subtract(double a, double b)
        {
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            return a * b;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Деление на ноль недопустимо");
            }
            return a / b;
        }

        public double Power(double a, double exponent)
        {
            return Math.Pow(a, exponent);
        }
    }
}