using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSample.Models;

namespace WpfSample.Repository
{
    public interface ITestDataRepo
    {
        IEnumerable<TestData> Get(int number);
        bool Delete(TestData item);
        bool Update(TestData item);
        bool Insert(TestData item);
    }
}
