using CalculatorLibrary;
using CalculatorCounter;
using CalculatorList;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            ResultList resultList = new ResultList();
            Calculator calculator = new Calculator(resultList);
            Counter counter = new Counter();

            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endApp)
            {
                // Declare variables and set to empty.
                string numInput1 = "";
                string numInput2 = "";
                double selectedResult = 0;
                double result = 0;
                double cleanNum1 = 0;


                bool pastCalculationsPrompt = false;
                bool pastCalculationsPrompt2 = false;
                bool usePreviousResult = false;
                while (!pastCalculationsPrompt)
                {

                    if (resultList.IsListEmpty())
                    {
                        pastCalculationsPrompt = true;
                    }
                    else
                    {
                        Console.WriteLine("Do you want to view past calculations?");
                        Console.WriteLine("You can also delete the list with [D] for delete.");
                        Console.WriteLine("[Y] for yes. [N] for no.");
                        string userInput = Console.ReadLine();
                        if (userInput.ToLower() == "y")
                        {
                            resultList.ViewCalculationList();
                            Console.WriteLine();
                            Console.WriteLine("Do you want to use the previous results to make a new calculation?");
                            Console.WriteLine("[Y] for yes and use in my next calculation. [N] for no.");
                            string userInput2 = Console.ReadLine();

                            while (!pastCalculationsPrompt2)
                            {


                                if (userInput2.ToLower() == "y")
                                {
                                    Console.WriteLine($"Here are the previous results. Pick which result to use by choosing a number.");
                                    resultList.ViewResultList();

                                    // Ask the user to choose a number.
                                    string chooseResult = Console.ReadLine();

                                    int selectedIndex;
                                    while (!int.TryParse(chooseResult, out selectedIndex) || selectedIndex < 1 || selectedIndex > resultList.GetResultCount())
                                    {
                                        Console.WriteLine("Invalid input. Please enter a valid index:");
                                        chooseResult = Console.ReadLine();
                                    }

                                    selectedResult = resultList.GetResult(selectedIndex - 1);

                                    usePreviousResult = true;
                                    pastCalculationsPrompt = true;
                                    pastCalculationsPrompt2 = true;
                                }
                                else if (userInput2.ToLower() == "n")
                                {
                                    pastCalculationsPrompt = true;
                                    pastCalculationsPrompt2 = true;
                                }
                                else
                                {
                                    Console.WriteLine("That's not a valid choice. Choose again.");
                                    continue;
                                }
                            }


                        }
                        else if (userInput.ToLower() == "n")
                        {
                            pastCalculationsPrompt = true;
                        }
                        else if (userInput.ToLower() == "d")
                        {
                            Console.WriteLine("The past calculations list has been deleted.");
                            resultList.DeleteList();
                        }
                        else
                        {
                            Console.WriteLine("That's not a valid choice. Choose again.");
                            continue;
                        }
                    }

                }

                if (usePreviousResult == true)
                {
                    numInput1 = selectedResult.ToString().Trim();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }
                }
                else
                {
                    // Ask the user to type the first number.
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();


                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }
                }


                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your option? ");

                string op = Console.ReadLine();

                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                // Run a counter that counts how many times the calculator has been used and log it.
                counter.Count();
                counter.ViewCounter();

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.

            }

            // Add call to close the JSON writer before return
            calculator.Finish();
            return;
        }
    }
}