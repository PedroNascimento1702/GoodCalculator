namespace GoodCalculator
{
    interface IConta
    {
        double Calcula(double n1, double n2);
    }

    class ContaSoma : IConta
    {
        public double Calcula(double n1,double n2)
        {
            return n1 + n2;
        }
    }

    class ContaSubtracao : IConta
    {
        public double Calcula(double n1, double n2)
        {
            return n1 - n2;
        }
    }

    class ContaMulti : IConta
    {
        public double Calcula(double n1, double n2)
        {
            return n1 * n2;
        }
    }

    class ContaDivisao : IConta
    {
        public double Calcula(double n1, double n2)
        {
            return n1 / n2;
        }
    }

    class ContaDefault : IConta
    {
        public double Calcula(double n1,double n2)
        {
            return 0;
        }
    }
}
