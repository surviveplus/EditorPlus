using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Net.Surviveplus.TextMacro.Test
{

    /// <summary>
    /// TextFormatMacro のテスト クラスです。すべての TextFormatMacro 単体テストテストをここに含めます。
    /// </summary>
    /// <remarks>Team Foundation Server, Visual Studio Online の自動ビルド・自動テストに登録するため、クラス名は必ず Test で終わる必要があります。</remarks>
    [TestClass]
    public partial class TextFormatMacroTest
    {

        /// <summary>
        /// 現在のテストの実行についての情報および機能を提供するテスト コンテキストを取得または設定します。
        /// </summary>
        public TestContext TestContext { get; set; }

        #region 追加のテスト属性

        /// <summary>
        ///  テストを作成するときに、次の追加属性を使用することができます:
        ///  クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        } // end function

        /// <summary>
        /// クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
        /// </summary>
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
        } // end function

        /// <summary>
        /// 各テストを実行する前にコードを実行するには、TestInitialize を使用
        /// </summary>
        [TestInitialize()]
        public void MyTestInitialize()
        {
        } // end function

        /// <summary>
        /// 各テストを実行した後にコードを実行するには、TestCleanup を使用
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup()
        {
        } // end function

        #endregion


        #region 正常系

        [TestMethod()]
        #region コード分析（命名規則）抑制
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly"),
          SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"),
          SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        #endregion
        public void Format_正常系_パラメータと値の数が一致しているとき正常にフォーマット出来ること1()
        {
            var formatText = "{0}";
            var valuesText = "1";
            var result = TextFormatMacro.Format(formatText, valuesText);

            Assert.AreEqual("1", result);
        } // end function

        [TestMethod()]
        #region コード分析（命名規則）抑制
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly"),
          SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"),
          SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        #endregion
        public void Format_正常系_パラメータと値の数が一致しているとき正常にフォーマット出来ること2()
        {
            var formatText = "{0}{1}";
            var valuesText = "1\t2";
            var result = TextFormatMacro.Format(formatText, valuesText);

            Assert.AreEqual("12", result);
        } // end function

        [TestMethod()]
        #region コード分析（命名規則）抑制
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly"),
          SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"),
          SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        #endregion
        public void Format_正常系_パラメータと値の数が一致しているとき正常にフォーマット出来ること3()
        {
            var formatText = "{0}{1}{2}";
            var valuesText = "1\t2\t3";
            var result = TextFormatMacro.Format(formatText, valuesText);

            Assert.AreEqual("123", result);
        } // end function

        #endregion

        #region 準正常系

        [TestMethod()]
        #region コード分析（命名規則）抑制
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly"),
          SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"),
          SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        #endregion
        public void Format_準正常系_フォーマットにプレースホルダではない中括弧が含まれるときに無視してフォーマットされること1()
        {
            var formatText = "{{0}}";
            var valuesText = "1";
            var result = TextFormatMacro.Format(formatText, valuesText);

            Assert.AreEqual("{1}", result);
        } // end function

        [TestMethod()]
        #region コード分析（命名規則）抑制
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly"),
          SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"),
          SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        #endregion
        public void Format_準正常系_フォーマットにプレースホルダではない中括弧が含まれるときに無視してフォーマットされること2()
        {
            var formatText = "{{0}";
            var valuesText = "1";
            var result = TextFormatMacro.Format(formatText, valuesText);

            Assert.AreEqual("{1", result);
        } // end function

        [TestMethod()]
        #region コード分析（命名規則）抑制
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly"),
          SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"),
          SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        #endregion
        public void Format_準正常系_フォーマットにプレースホルダではない中括弧が含まれるときに無視してフォーマットされること3()
        {
            var formatText = "{0}}";
            var valuesText = "1";
            var result = TextFormatMacro.Format(formatText, valuesText);

            Assert.AreEqual("1}", result);
        } // end function

        [TestMethod()]
        #region コード分析（命名規則）抑制
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly"),
          SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"),
          SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        #endregion
        public void Format_準正常系_フォーマットにプレースホルダではない中括弧が含まれるときに無視してフォーマットされること4()
        {
            var formatText = "{0}{}";
            var valuesText = "1";
            var result = TextFormatMacro.Format(formatText, valuesText);

            Assert.AreEqual("1{}", result);
        } // end function

        #endregion


    } // end class
} // end namespace
