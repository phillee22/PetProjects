using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicEvalTests
{
    using LogicEvalLib;

    [TestClass]
    public class LogicTreeParsingTests
    {
        [TestMethod]
        public void SimpleParsingTest()
        {
            LogicTree tree = new LogicTree();
            string s = "p^q";
            LogicNode root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "p^q";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "p^qVr";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "!p^r";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "pV!r";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "p^(qVr)";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "(p^q)Vr";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "!(p^!q)Vr";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "(!p^!q)V!(!r^!s)";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "p^(qVr)^s";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "(p^(qV!r))";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "((p^q)Vr)";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "!((p^q))";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "(p^!q)V(!(r^s))";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "(p^(!(qVr)))";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "((p^q))";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "(!(p^q))";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

        }

        [TestMethod]
        public void ComplexParsingTest()
        {
            LogicTree tree = new LogicTree();

            string s = "((p^(qVr)))";
            LogicNode root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

        }

        [TestMethod]
        public void othertests()
        {
            LogicTree tree = new LogicTree();
            string s = "p";
            LogicNode root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "!p";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "(!p)";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "!(p)";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "!p^q";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "p^qVr";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "(!qVr)";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "!(qVr)";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "p^(qVr)";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "p^!(qVr)";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "(p^q)Vr";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "p^(qVr)^s";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "(p^(qVr))";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "!(p^(qVr))";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "((p^q)Vr)";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s = "(!(p^(qVr)))";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());

            s= "(p^(!(qVr)))";
            root = tree.Parse(s);
            Assert.AreEqual(s, root.ToString());
        }
    }
}
