using System.Diagnostics;
using Newtonsoft.Json;
using CalculatorList;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        private ResultList resultList;
        public Calculator(ResultList resultList)
        {
            this.resultList = resultList;
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    resultList.AddCalculation($"The sum of {num1} + {num2} = {result}");
                    resultList.AddResult(result);
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    resultList.AddCalculation($"The difference of {num1} - {num2} = {result}");
                    resultList.AddResult(result);
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    resultList.AddCalculation($"The product of {num1} * {num2} = {result}");
                    resultList.AddResult(result);
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        resultList.AddCalculation($"The quotient of {num1} / {num2} = {result}");
                        resultList.AddResult(result);
                    }
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
