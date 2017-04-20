using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net.Surviveplus.TextMacro
{
    /// <summary>
    /// Text Format の機能を司るクラスです。
    /// </summary>
    /// <remarks>
    /// <para>変更履歴</para>
    /// <para>2015年2月8日 - EmEditor のマクロから移植。</para>
    /// </remarks>
    public class TextFormatMacro
    {
        /// <summary>
        /// 指定されたフォーマット用lのテキストと、タブ区切りの値のテキストを組み合わせて、フォーマットされたテキストを返します。
        /// </summary>
        /// <param name="formatText">
        /// パラーメータのプレースホルダ {0}, {1} ... を含むテキストを指定します。
        /// formatText に null 参照 ( Visual Basic では Nothing ) や空文字を指定すると、このメソッドは空文字を返します。
        /// </param>
        /// <param name="valuesText">
        /// タブ文字で区切られた、値の一覧のテキストを指定します。
        /// valuesText に null 参照 ( Visual Basic では Nothing ) や空文字を指定すると、このメソッドは formatText をそのまま返します。
        /// </param>
        /// <returns>
        /// formatText のプレースホルダに、valuesText の値を埋め込んだテキストを返します。
        /// プレースホルダの個数と値の個数が一致しない場合、そのまま無視され、使われません。
        /// </returns>
        /// <remarks>
        /// C# の String.Format では、フォーマットテキストに不備があったときに例外になるが、強制的に変換をかけたいので EmEditor マクロと同じで、前から順に置換しています。
        /// </remarks>
        public static string Format(string formatText, string valuesText)
        {
            if (string.IsNullOrEmpty(formatText)) return string.Empty;
            if (string.IsNullOrWhiteSpace(valuesText)) return valuesText;

            // 参考：複合書式設定
            // http://msdn.microsoft.com/ja-jp/library/txafckwd(v=vs.110).aspx

            var result = formatText;
            var args = valuesText.Split('\t');
            for (int i = 0; i < args.Length; i++)
            {
                result = result.Replace("{" + i.ToString() + "}", args[i]);
            } // next i

            return result;
        } // end function
    } // end class
} // end namespace
