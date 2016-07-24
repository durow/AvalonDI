using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ayx.AvalonDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AvalonDITests.TestModels;

namespace Ayx.AvalonDI.Tests
{
    [TestClass()]
    public class DIContainerTests
    {
        [TestMethod()]
        public void WireTest()
        {
            var di = new DIContainer();
            Assert.AreEqual(di.Count, 0);
            di.Wire<ITest, TestA>();
            Assert.AreEqual(di.Count, 1);
            di.Wire<TestB>();
            Assert.AreEqual(di.Count, 2);

            var a1 = di.Get<ITest>();
            var a2 = di.Get<ITest>();
            Assert.AreNotSame(a1, a2);

            var b1 = di.Get<TestB>();
            var b2 = di.Get<TestB>();
            Assert.AreNotSame(b1, b2);
        }

        [TestMethod()]
        public void WireSingletonTest()
        {
            var di = new DIContainer();
            var single = new TestA();
            di.WireSingleton(single);
            var actual = di.Get<TestA>();
            Assert.AreSame(single, actual);

            di.WireSingleton<TestB>();
            var b1 = di.Get<TestB>();
            var b2 = di.Get<TestB>();
            Assert.AreSame(b1, b2);

            di.WireSingleton<IComputer, AddComputer>();
            var computer1 = di.Get<IComputer>();
            var computer2 = di.Get<IComputer>();
            Assert.AreSame(computer1, computer2);
        }

        [TestMethod()]
        public void WireVMTest()
        {
            var di = new DIContainer();
            di.WireVM<TestView, TestViewModel>();
            var vm = di.GetVM<TestView>();
            Assert.AreEqual(vm.GetType(), typeof(TestViewModel));

            var view = di.GetView<TestView>();
            Assert.AreEqual(view.DataContext.GetType(), typeof(TestViewModel));

            var testVM = vm as TestViewModel;
            Assert.AreEqual("asdf", testVM.TestString);
        }

        [TestMethod]
        public void TokenTest()
        {
            var di = new DIContainer();
            di.Wire<ITest, TestA>("a");
            di.Wire<ITest, TestB>("b");

            var a = di.Get<ITest>("a");
            var b = di.Get<ITest>("b");
            var test = di.Get<ITest>();

            Assert.AreEqual("TestA", a.Show());
            Assert.AreEqual("TestB", b.Show());
            Assert.IsNotNull(test);
        }

        [TestMethod]
        public void ConstructureInjectionTest()
        {
            var di = new DIContainer();
            di.Wire<IComputer, AddComputer>();
            di.WireSingleton<Logger>();

            var test = di.Get<InjectModel>();
            var result = test.Compute(4, 5);
            Assert.AreEqual(9, result);

            var text = "test";
            var expected = "console:" + text;
            var actual = test.Log(text);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PropertyInjectTest()
        {
            var di = new DIContainer();
            di.Wire<IComputer, AddComputer>();
            di.WireSingleton<Logger>();

            var test = di.Get<AttributeModel>();
            var result = test.Compute(4, 5);
            Assert.AreEqual(9, result);

            var text = "test";
            var expected = "console:" + text;
            var actual = test.Log(text);
            Assert.AreEqual(expected, actual);
        }
    }
}