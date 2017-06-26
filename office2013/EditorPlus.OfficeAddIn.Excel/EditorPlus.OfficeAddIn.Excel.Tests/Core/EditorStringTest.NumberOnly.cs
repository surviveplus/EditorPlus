using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EditorPlus.OfficeAddIn.Excel.Core;

namespace EditorPlus.OfficeAddIn.Excel.Tests.Core
{
    public  partial class EditorStringTest
    {
        [TestMethod]
        public void IncrementText_正常系_テキスト1をインクリメントして2になること()
        {

            var args = "1";
            var expected = "2";

            var actual = EditorString.IncrementText(args);

            Assert.AreEqual(expected, actual);
        } // end function

        [TestMethod]
        public void IncrementText_正常系_テキスト001をインクリメントして002になること()
        {

            var args = "001";
            var expected = "002";

            var actual = EditorString.IncrementText(args);

            Assert.AreEqual(expected, actual);
        } // end function

        [TestMethod]
        public void IncrementText_正常系_テキスト009をインクリメントして010になること()
        {

            var args = "009";
            var expected = "010";

            var actual = EditorString.IncrementText(args);

            Assert.AreEqual(expected, actual);
        } // end function

        [TestMethod]
        public void IncrementText_正常系_テキスト099をインクリメントして100になること()
        {

            var args = "099";
            var expected = "100";

            var actual = EditorString.IncrementText(args);

            Assert.AreEqual(expected, actual);
        } // end function

        [TestMethod]
        public void IncrementText_正常系_テキスト999をインクリメントして1000になること()
        {

            var args = "999";
            var expected = "1000";

            var actual = EditorString.IncrementText(args);

            Assert.AreEqual(expected, actual);
        } // end function

    } // end class
} // end namespace
