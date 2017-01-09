using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIE_BE.Common
{
    public class ExcelDictionaryColumn
    {
        public static readonly ExcelDictionaryColumn ENTRY_NUMBER = new ExcelDictionaryColumn("A");
        public static readonly ExcelDictionaryColumn LINE_NUMBER = new ExcelDictionaryColumn("B");
        public static readonly ExcelDictionaryColumn LEMMA = new ExcelDictionaryColumn("C");
        public static readonly ExcelDictionaryColumn LANGUAGE = new ExcelDictionaryColumn("D");
        public static readonly ExcelDictionaryColumn RECONSTRUCTED_FORM = new ExcelDictionaryColumn("E");
        public static readonly ExcelDictionaryColumn VARIANT = new ExcelDictionaryColumn("F");
        public static readonly ExcelDictionaryColumn ZERO_POSITION = new ExcelDictionaryColumn("G");
        public static readonly ExcelDictionaryColumn POSITION_1 = new ExcelDictionaryColumn("H");
        public static readonly ExcelDictionaryColumn POSITION_2 = new ExcelDictionaryColumn("I");
        public static readonly ExcelDictionaryColumn POSITION_3 = new ExcelDictionaryColumn("J");
        public static readonly ExcelDictionaryColumn POSITION_4 = new ExcelDictionaryColumn("K");
        public static readonly ExcelDictionaryColumn POSITION_5 = new ExcelDictionaryColumn("L");
        public static readonly ExcelDictionaryColumn POSITION_6 = new ExcelDictionaryColumn("M");
        public static readonly ExcelDictionaryColumn POSITION_7 = new ExcelDictionaryColumn("N");
        public static readonly ExcelDictionaryColumn POSITION_8 = new ExcelDictionaryColumn("O");
        public static readonly ExcelDictionaryColumn POSITION_9 = new ExcelDictionaryColumn("P");
        public static readonly ExcelDictionaryColumn POSITION_10 = new ExcelDictionaryColumn("Q");
        public static readonly ExcelDictionaryColumn POSITION_11 = new ExcelDictionaryColumn("R");
        public static readonly ExcelDictionaryColumn POSITION_0 = new ExcelDictionaryColumn("S");
        public static readonly ExcelDictionaryColumn RECONSTRUCTED_MEANING = new ExcelDictionaryColumn("T");
        public static readonly ExcelDictionaryColumn COMMENT = new ExcelDictionaryColumn("U");
        public static readonly ExcelDictionaryColumn LINK = new ExcelDictionaryColumn("V");

        private string column;

        public static IEnumerable<ExcelDictionaryColumn> Values
        {
            get
            {
                yield return ENTRY_NUMBER;
                yield return LINE_NUMBER;
                yield return LEMMA;
                yield return LANGUAGE;
                yield return RECONSTRUCTED_FORM;
                yield return VARIANT;
                yield return ZERO_POSITION;
                yield return POSITION_1;
                yield return POSITION_2;
                yield return POSITION_3;
                yield return POSITION_4;
                yield return POSITION_5;
                yield return POSITION_6;
                yield return POSITION_7;
                yield return POSITION_8;
                yield return POSITION_9;
                yield return POSITION_10;
                yield return POSITION_11;
                yield return POSITION_0;
                yield return RECONSTRUCTED_MEANING;
                yield return COMMENT;
                yield return LINK;
            }
        }

        public string Column
        {
            get
            {
                return column;
            }

            set
            {
                column = value;
            }
        }

        private ExcelDictionaryColumn(string column)
        {
            this.Column = column;
        }

    }
}
