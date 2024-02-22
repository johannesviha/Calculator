namespace CalculatorCounter;

public class Counter
{
    private int _counter = 0;

    public void Count()
    {
        _counter++;
    }

    public void ViewCounter()
    {
        if (_counter == 1)
        {
            Console.WriteLine($"The calculator has been used {_counter} time.");
        }
        else
        {
            Console.WriteLine($"The calculator has been used {_counter} times.");
        }
    }
}



