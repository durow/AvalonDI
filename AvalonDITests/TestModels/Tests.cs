using Ayx.AvalonDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvalonDITests.TestModels
{

    public class InjectModel
    {
        private IComputer _computer;
        private Logger _logger;

        public InjectModel(IComputer computer, Logger logger)
        {
            _computer = computer;
            _logger = logger;
        }

        public string Log(string text)
        {
            return _logger.ConsoleOut(text);
        }

        public double Compute(double a, double b)
        {
            return _computer.Compute(a, b);
        }
    }

    public class AttributeModel
    {
        [AutoInject]
        public IComputer Computer { get; set; }
        [AutoInject]
        public Logger Logger { get; set; }

        public string Log(string text)
        {
            return Logger.ConsoleOut(text);
        }

        public double Compute(double a, double b)
        {
            return Computer.Compute(a, b);
        }
    }

    public class TestA : ITest
    {
        public string Show()
        {
            return "TestA";
        }
    }

    public class TestB : ITest
    {
        public string Show()
        {
            return "TestB";
        }
    }

    public class AddComputer:IComputer
    {
        public double Compute(double a, double b)
        {
            return a + b;
        }
    }

    public class MinusComputer : IComputer
    {
        public double Compute(double a, double b)
        {
            return a - b;
        }
    }

    public class Logger
    {
        public string ConsoleOut(string content)
        {
            var result = "console:" + content;
            Console.WriteLine(result);
            return result;
        }
    }
}
