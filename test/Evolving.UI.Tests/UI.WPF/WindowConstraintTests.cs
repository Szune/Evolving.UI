using System;
using Evolving.UI.Attributes;
using Evolving.UI.WPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Evolving.UI.Tests.UI.WPF
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
