using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjectSample.Models;

namespace NinjectSample.Repository
{
    public class TestDataRepo : ITestDataRepo
    {
        public bool Delete(TestData item)
        {
            return true;
        }

        public IEnumerable<TestData> Get(int number)
        {
            var rand = new Random();
            for (int i = 0; i < number; i++)
            {
                yield return new TestData
                {
                    Id = i + 1,
                    DateTime = DateTime.Now,
                    DoubleProperty = rand.NextDouble(),
                    StringProperty = "TestString " + i.ToString($"D{number.ToString().Length}"),
                };
            }
        }

        public bool Insert(TestData item)
        {
            return true;
        }

        public bool Update(TestData item)
        {
            return true;
        }
    }
}
