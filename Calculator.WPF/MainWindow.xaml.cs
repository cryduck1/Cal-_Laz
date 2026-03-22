using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Calculator.Core;

namespace Calculator.WPF
{
    public partial class MainWindow : Window
    {
        private readonly ICalculator _calculator;
        private string _currentInput = "0";
        private double _firstOperand = 0;
        private string _operation = "";
        private bool _isNewInput = true;

        public MainWindow()
        {
            InitializeComponent();
            _calculator = new Calculator.Core.Calculator();
        }

        private double ParseNumber(string input)
        {
            double result;
            string normalizedInput = input.Replace('.', ',');
            double.TryParse(normalizedInput, NumberStyles.Number, CultureInfo.CurrentCulture, out result);
            return result;
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string number = button.Content.ToString();

                if (_isNewInput)
                {
                    _currentInput = number;
                    _isNewInput = false;
                }
                else
                {
                    if (_currentInput == "0")
                        _currentInput = number;
                    else
                        _currentInput = _currentInput + number;
                }

                DisplayText.Text = _currentInput;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _currentInput = "0";
            _firstOperand = 0;
            _operation = "";
            _isNewInput = true;
            DisplayText.Text = "0";
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (_isNewInput)
            {
                _currentInput = "0.";
                _isNewInput = false;
            }
            else if (!_currentInput.Contains(".") && !_currentInput.Contains(","))
            {
                _currentInput += ".";
            }
            DisplayText.Text = _currentInput;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            SetOperation("+");
        }

        private void Subtract_Click(object sender, RoutedEventArgs e)
        {
            SetOperation("-");
        }

        private void Multiply_Click(object sender, RoutedEventArgs e)
        {
            SetOperation("×");
        }

        private void Divide_Click(object sender, RoutedEventArgs e)
        {
            SetOperation("÷");
        }

        private void SetOperation(string op)
        {
            try
            {
                double result = ParseNumber(_currentInput);
                _firstOperand = result;
                _operation = op;
                _isNewInput = true;
            }
            catch
            {
                DisplayText.Text = "Ошибка";
                _currentInput = "0";
                _isNewInput = true;
                _operation = "";
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_operation))
                return;

            double secondOperand;
            try
            {
                secondOperand = ParseNumber(_currentInput);
            }
            catch
            {
                DisplayText.Text = "Ошибка";
                _currentInput = "0";
                _isNewInput = true;
                _operation = "";
                return;
            }

            double result = 0;
            bool success = true;

            try
            {
                switch (_operation)
                {
                    case "+":
                        result = _calculator.Add(_firstOperand, secondOperand);
                        break;
                    case "-":
                        result = _calculator.Subtract(_firstOperand, secondOperand);
                        break;
                    case "×":
                        result = _calculator.Multiply(_firstOperand, secondOperand);
                        break;
                    case "÷":
                        result = _calculator.Divide(_firstOperand, secondOperand);
                        break;
                    default:
                        success = false;
                        break;
                }

                if (success)
                {
                   
                    _currentInput = result.ToString("G15", CultureInfo.InvariantCulture).Replace(',', '.');
                    DisplayText.Text = _currentInput;
                    _operation = "";
                    _isNewInput = true;
                }
            }
            catch (DivideByZeroException)
            {
                DisplayText.Text = "Ошибка";
                _currentInput = "0";
                _isNewInput = true;
                _operation = "";
            }
            catch (Exception)
            {
                DisplayText.Text = "Ошибка";
                _currentInput = "0";
                _isNewInput = true;
                _operation = "";
            }
        }

        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = ParseNumber(_currentInput);
                _currentInput = (value / 100).ToString("G15", CultureInfo.InvariantCulture).Replace(',', '.');
                DisplayText.Text = _currentInput;
            }
            catch
            {
                DisplayText.Text = "Ошибка";
                _currentInput = "0";
                _isNewInput = true;
            }
        }

        private void PlusMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = ParseNumber(_currentInput);
                _currentInput = (-value).ToString("G15", CultureInfo.InvariantCulture).Replace(',', '.');
                DisplayText.Text = _currentInput;
            }
            catch
            {
                DisplayText.Text = "Ошибка";
                _currentInput = "0";
                _isNewInput = true;
            }
        }
    }
}