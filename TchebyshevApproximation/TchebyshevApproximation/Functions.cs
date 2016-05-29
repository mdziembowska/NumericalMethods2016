using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TchebyshevApproximation
{
    public class Functions
    {

        public delegate double FUNC(double x);
        public delegate double FUNCf(double x, FUNC f);
        public List<double> nodes = new List<double>() { 0.832498, 0.374541, 0.0, -0.374541, -0.832498 };
        public List<double> lambdas = new List<double>();
        public List<double> suma = new List<double>();
        public int n=5;
        public int nr;
        // chyba wystarczy jedna funkcja, skoro wybieram funkcję do całkowania i przekazuję jako parmetr :v
        //ta nizej jest spoko
        // muszę przerobić funkcje na postać PI*f(x)* 


        public static void chooseFunction(int nr, int n, List<double> nods, List<double> lambdas, List<double> suma)
        {
            while (nr != '5')
            {
                
                switch (nr)
                {
                    case '1':
                        lambdas = Functions.lambdaK_n(n, nods, Functions.linear);
                        for (int i = 0; i < lambdas.Count; i++)
                        {
                            Console.WriteLine(lambdas.ElementAt(i));
                        }
                        break;
                    case '2':
                        lambdas = Functions.lambdaK_n(n, nods, Functions.absoluteWeight);
                        for (int i = 0; i < lambdas.Count; i++)
                        {
                            Console.WriteLine(lambdas.ElementAt(i));
                        }
                        break;
                    case '3':
                        lambdas = Functions.lambdaK_n(n, nods, Functions.polynomial);
                        for (int i = 0; i < lambdas.Count; i++)
                        {
                            Console.WriteLine(lambdas.ElementAt(i));
                        }
                        break;
                    case '4':
                        lambdas = Functions.lambdaK_n(n, nods, Functions.sinus);
                        for (int i = 0; i < lambdas.Count; i++)
                        {
                            Console.WriteLine(lambdas.ElementAt(i));
                        }
                        break;
                }
                suma.Add(lambdas.ElementAt(0) - lambdas.ElementAt(2) + lambdas.ElementAt(4));
                suma.Add(lambdas.ElementAt(1) - 3 * lambdas.ElementAt(3));
                suma.Add(2 * lambdas.ElementAt(2) - 8 * lambdas.ElementAt(4));
                suma.Add( 4 * lambdas.ElementAt(3));
                suma.Add( 8 * lambdas.ElementAt(4));
            }
        }

        public static List<double> lambdaK_n(int n, List<double> nods, FUNC f)
        {
            List<double> lst = new List<double>();
            lst.Add(GaussCheb(nods, n, czebPolFx0, f) / Math.PI);
            lst.Add(GaussCheb(nods, n, czebPolFx1, f) / (Math.PI / 2.0));
            lst.Add(GaussCheb(nods, n, czebPolFx2, f) / (Math.PI / 2.0));
            lst.Add(GaussCheb(nods, n, czebPolFx3, f) / (Math.PI / 2.0));
            lst.Add(GaussCheb(nods, n, czebPolFx4, f) / (Math.PI / 2.0));


            return lst;

        }

        public static double GaussCheb(List<double> nods, int n, FUNCf fx, FUNC f)//
        {
            double suma = 0.0;
            for (int i = 0; i < nods.Count; i++)
            {
                suma += fx(nods.ElementAt(i), f);
                //if(i>0)
                //Console.WriteLine("Wynik dla " + (i + 1) + " węzłów: " + absolute(suma));
            }
            suma = 2.0 / nods.Count * suma;
            return suma;
        }

        public static double weight(double x)
        {
            return 1 / Math.Sqrt(1 - Math.Pow(x, 2));

        }
        public static double x1weight(double x)
        {
            return x * x / Math.Sqrt(1 - Math.Pow(x, 2));
        }
        public static double x2weight(double x)
        {
            return Math.Pow((2 * x * x - 1), 2) / Math.Sqrt(1 - Math.Pow(x, 2));
        }
        public static double x3weight(double x)
        {
            return Math.Pow((4 * Math.Pow(x, 3) - 3 * x), 2) / Math.Sqrt(1 - Math.Pow(x, 2));
        }
        public static double x4weight(double x)
        {
            return Math.Pow((8 * Math.Pow(x, 4) - 8 * x * x + 1), 2) / Math.Sqrt(1 - Math.Pow(x, 2));

        }

        public static double linearWeight(double x)// y=2x+3
        {
            return Math.PI * (x + 1) * Math.Sqrt(1 - Math.Pow(x, 2));
        }

        public static double czebPolFx0(double x, FUNC f)
        {
            return Math.PI * f(x);
        }
        public static double czebPolFx1(double x, FUNC f)
        {
            return Math.PI * x * f(x);
        }
        public static double czebPolFx2(double x, FUNC f)
        {
            return Math.PI * (2 * x * x - 1) * f(x);
        }
        public static double czebPolFx3(double x, FUNC f)
        {
            return Math.PI * (4 * x * x * x - 3 * x) * f(x);
        }
        public static double czebPolFx4(double x, FUNC f)
        {
            return Math.PI * (8 * x * x * x * x - 8 * x * x + 1) * f(x);
        }

        public static double absoluteWeight(double x)
        {
            if (x >= 0)
                return x * Math.Sqrt(1 - Math.Pow(x, 2));
            else
                return -x * Math.Sqrt(1 - Math.Pow(x, 2));
        }
        public static double polynomialWeight(double x)
        {

            return Math.PI * (Math.Pow(x, 4) + 2 * Math.Pow(x, 2) + 1) * Math.Sqrt(1 - Math.Pow(x, 2));
        }
        public static double sinusWeight(double x)//y=x^4+2x^2+1
        {
            return Math.PI * Math.Sin(x) * Math.Sqrt(1 - Math.Pow(x, 2));
        }




        public static double sinus(double x)
        {
            return Math.Sin(x);
        }
        public static double absoluteAproximate(double x)
        {
            if (x >= 0)
                return Math.PI * x;
            else
                return -x * Math.PI;
        }
        public static double linear(double x)
        {
            return (x + 1.0);
        }
        public static double polynomial(double x)
        {
            return (x * x * x * x + 2 * x * x + 1);
        }
        public static double absolute(double x)
        {
            if (x >= 0)
                return x;
            else
                return -x;
        }
        public static double func(double x)//funkcja wagowa
        {
            return 1 / Math.Sqrt(1 - Math.Pow(x, 2));
        }






    }
}
