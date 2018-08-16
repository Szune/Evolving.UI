using E.UI.WPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using E.UI.Attributes;

namespace E.UI.Tests.UI.WPF
{
    public class WindowConstraintTests
    {
        private EditorWindow _window;
        
        [TestClass]
        public class WhenAttributeIsNotAppliedToAnyMember : WindowConstraintTests
        {
            private class AttributeNotApplied
            {
            }

            [TestMethod]
            public void ThrowException()
            {
                var obj = new AttributeNotApplied();
                Action act = () => _window = new EditorWindow(obj);
                Assert.ThrowsException<InvalidOperationException>(act);
            }
        }

        [TestClass]
        public class WhenAttributeIsAppliedToAMember : WindowConstraintTests
        {
            private class AttributeApplied
            {
                [ShowInEditor]
                // ReSharper disable once UnusedMember.Local
                // ReSharper disable once UnassignedGetOnlyAutoProperty
                public string Test { get; }
            }

            [TestMethod]
            public void DoNotThrowException()
            {
                var obj = new AttributeApplied();
                _window = new EditorWindow(obj);
            }
        }
    }
}
