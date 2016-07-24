using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvalonDITests.TestModels
{
    public interface ITest
    {
        string Show();
    }

    public interface IComputer
    {
        double Compute(double a, double b);
    }
}
