using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Net.Surviveplus.TextMacro.Test
{
    [TestClass]
    public class CamelWordsTest
    {
        #region GetWords

        [TestMethod]
        public void GetWordsTest1()
        {

            var text = "Hello World !";
            var expected = new string[] { "Hello", "World", "!" };

            var actual = CamelWords.GetWords(text).ToArray();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            } // next i

        } // end function

        [TestMethod]
        public void GetWordsTest2()
        {

            var text = "Hello-World  HELLO_WORLD";
            var expected = new string[] { "Hello", "World", "HELLO", "WORLD" };

            var actual = CamelWords.GetWords(text).ToArray();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            } // next i

        } // end function

        [TestMethod]
        public void GetWordsTest3()
        {

            var text = "\"Hello World\"";
            var expected = new string[] { "Hello", "World" };

            var actual = CamelWords.GetWords(text).ToArray();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            } // next i

        } // end function


        [TestMethod]
        public void GetWordsTest4()
        {

            var text = "HelloWorld !";
            var expected = new string[] { "Hello", "World", "!" };

            var actual = CamelWords.GetWords(text).ToArray();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            } // next i

        } // end function

        #endregion

        #region GetUpperCamelWord

        [TestMethod]
        public void GetUpperCamelWordTest1()
        {

            var text = "Hello World !";
            var expected = "HelloWorld!";

            var actual = CamelWords.GetUpperCamelWord(text);
            Assert.AreEqual(expected, actual);

        } // end function

        [TestMethod]
        public void GetUpperCamelWordTest2()
        {

            var text = "Hello-World-HelloWorld";
            var expected = "HelloWorldHelloWorld";

            var actual = CamelWords.GetUpperCamelWord(text);
            Assert.AreEqual(expected, actual);

        } // end function
        #endregion

        #region GetLowerCamelWord

        [TestMethod]
        public void GetLowerCamelWordTest1()
        {

            var text = "Hello World !";
            var expected = "helloWorld!";

            var actual = CamelWords.GetLowerCamelWord(text);
            Assert.AreEqual(expected, actual);

        } // end function

        [TestMethod]
        public void GetLowerCamelWordTest2()
        {

            var text = "Hello-World-HelloWorld";
            var expected = "helloWorldHelloWorld";

            var actual = CamelWords.GetLowerCamelWord(text);
            Assert.AreEqual(expected, actual);

        } // end function
        #endregion

    } // end class
} // end namespace
