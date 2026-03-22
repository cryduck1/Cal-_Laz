using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Core;
using System;

namespace Calculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private ICalculator _calc;

        [TestInitialize]
        public void Setup()
        {
            _calc = new Calculator.Core.Calculator();
        }

        [TestMethod]
        public void Add_PositiveNumbers_ReturnsCorrectResult()
        {
            double result = _calc.Add(2, 3);
            Assert.AreEqual(5.0, result, 0.0001);
        }

        [TestMethod]
        public void Add_NegativeNumbers_ReturnsCorrectResult()
        {
            double result = _calc.Add(-2, -3);
            Assert.AreEqual(-5.0, result, 0.0001);
        }

        [TestMethod]
        public void Subtract_PositiveNumbers_ReturnsCorrectResult()
        {
            double result = _calc.Subtract(5, 4);
            Assert.AreEqual(1.0, result, 0.0001);
        }

        [TestMethod]
        public void Multiply_PositiveNumbers_ReturnsCorrectResult()
        {
            double result = _calc.Multiply(3, 4);
            Assert.AreEqual(12.0, result, 0.0001);
        }

        [TestMethod]
        public void Divide_PositiveNumbers_ReturnsCorrectResult()
        {
            double result = _calc.Divide(10, 4);
            Assert.AreEqual(2.5, result, 0.0001);
        }

        [TestMethod]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            Assert.ThrowsException<DivideByZeroException>(() => _calc.Divide(5, 0));
        }

        [TestMethod]
        public void Power_PositiveExponent_ReturnsCorrectResult()
        {
            double result = _calc.Power(2, 3);
            Assert.AreEqual(8.0, result, 0.0001);
        }

        [TestMethod]
        public void Power_ZeroExponent_ReturnsOne()
        {
            double result = _calc.Power(5, 0);
            Assert.AreEqual(1.0, result, 0.0001);
        }
    }
}