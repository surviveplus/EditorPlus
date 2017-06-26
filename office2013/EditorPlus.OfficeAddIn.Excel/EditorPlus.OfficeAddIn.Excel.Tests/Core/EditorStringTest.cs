using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EditorPlus.OfficeAddIn.Excel.Core;

namespace EditorPlus.OfficeAddIn.Excel.Tests.Core
{
    [TestClass]
    public partial class EditorStringTest
    {
        [TestMethod]
        public void IncrementText_正常系_テキストa1をインクリメントしてa2になること()
        {

            var args = "a1";
            var expected = "a2";

            var actual = EditorString.IncrementText(args);

            Assert.AreEqual(expected, actual);
        } // end function

        [TestMethod]
        public void IncrementText_正常系_テキストa001をインクリメントしてa002になること()
        {

            var args = "a001";
            var expected = "a002";

            var actual = EditorString.IncrementText(args);

            Assert.AreEqual(expected, actual);
        } // end function

        [TestMethod]
        public void IncrementText_正常系_テキストa009をインクリメントしてa010になること()
        {

            var args = "a009";
            var expected = "a010";

            var actual = EditorString.IncrementText(args);

            Assert.AreEqual(expected, actual);
        } // end function

        [TestMethod]
        public void IncrementText_正常系_テキストa099をインクリメントしてa100になること()
        {

            var args = "a099";
            var expected = "a100";

            var actual = EditorString.IncrementText(args);

            Assert.AreEqual(expected, actual);
        } // end function

        [TestMethod]
        public void IncrementText_正常系_テキストa999をインクリメントしてa1000になること()
        {

            var args = "a999";
            var expected = "a1000";

            var actual = EditorString.IncrementText(args);

            Assert.AreEqual(expected, actual);
        } // end function

    } // end class
} // end namespace
