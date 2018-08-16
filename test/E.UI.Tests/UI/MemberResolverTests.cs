using System;
using System.Linq;
using E.UI.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace E.UI.Tests.UI
{
    [TestClass]
    public class MemberResolverTests
    {
        [UIClass]
        class ClassWithUIAttribute
        {
            public int First;
        }

        [TestMethod]
        public void GetMembers()
        {
            var resolver = new MemberResolver();
            var members = resolver.GetMembers(new ClassWithUIAttribute());
            
            Assert.IsTrue(members.All(m => m.Name.Equals("First")));
        }
    }
}
