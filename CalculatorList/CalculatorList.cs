namespace CalculatorList
{
    public class ResultList
    {
        private List<string> _list = new List<string>();
        private List<double> _resultList = new List<double>();

        public void AddCalculation(string result)
        {
            _list.Add(result);
        }

        public void AddResult(double result)
        {
            _resultList.Add(result);
        }

        public bool IsListEmpty()
        {
            if (_list.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ViewCalculationList()
        {
            foreach (var item in _list)
            {
                Console.WriteLine(item);
            }
        }

        public void ViewResultList()
        {
            for (var i = 0; i < _resultList.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. {_resultList[i]}");
            }
        }

        public int GetResultCount()
        {
            return _resultList.Count;
        }

        public double GetResult(int index)
        {
            if (index >= 0 && index < _resultList.Count)
            {
                return _resultList[index];
            }
            else
            {
                throw new IndexOutOfRangeException("Index is out of bounds.");
            }
        }
        public void DeleteList()
        {
            _list.Clear();
        }
    }

}