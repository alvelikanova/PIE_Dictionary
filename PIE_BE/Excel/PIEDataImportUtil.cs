using System;
using System.Collections.Generic;
using PIE_DB.Data_Model;
using PIE_DB.Repositories;
using PIE_BE.Common;

namespace PIE_BE.Excel
{
    static class PIEDataImportUtil
    {
        private const int ACTIVE_SHEET = 1;
        private const int FIRST_ROW = 1;
        private static UnitOfWork unitOfWork = new UnitOfWork();

        public static IList<Vocabulary_entry> ReadDictionaryEntriesFromExcelFile(string path)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = false;

            Microsoft.Office.Interop.Excel.Workbook excelAppWorkbook = excelApp.Workbooks.Open(path);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = excelAppWorkbook.Sheets[ACTIVE_SHEET];
            if (worksheet == null)
            {
                return null;
            }

            int totalColumns = worksheet.UsedRange.Columns.Count;
            int totalRows = worksheet.UsedRange.Rows.Count;

            SortedList<int, Vocabulary_entry> entries = new SortedList<int, Vocabulary_entry>();

            for (int row = FIRST_ROW; row < totalRows; row++)
            {
                int curEntryNumber = tryParseEntryNumberDictionaryCell(row, worksheet);
                if (curEntryNumber == 0)
                {
                    // invalid value
                    continue;
                }

                Vocabulary_entry vocabularyEntry = null;
                if (!entries.ContainsKey(curEntryNumber))
                {
                    vocabularyEntry = new Vocabulary_entry();
                    vocabularyEntry.Number_vocabulary_entry = curEntryNumber;
                    entries.Add(curEntryNumber, vocabularyEntry);
                }
                else
                {
                    vocabularyEntry = entries[curEntryNumber];
                }

                // parse entry conponents in order of appearance
                if (vocabularyEntry != null)
                {
                    // entry line number
                    Dictionary_conformity dictionaryConformity = tryGetNewDictionaryConformity(row, worksheet);
                    if (dictionaryConformity == null)
                    {
                        continue;
                    }

                    // lemma - may be absent, it appears one time for each vocabilary entry
                    tryParseAndSetLemmaDictionaryCell(curEntryNumber, vocabularyEntry, worksheet);

                    // language
                    Language language = tryParseAndSetLanguageDictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    if (language == null)
                    {
                        continue;
                    }

                    string reconstructedForm = tryParseAndSetReconstructedFormDictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    if (String.IsNullOrEmpty(reconstructedForm))
                    {
                        continue;
                    }

                    tryParseAndSetVariantDictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetZeroPositionDictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetPosition1DictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetPosition2DictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetPosition3DictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetPosition4DictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetPosition5DictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetPosition6DictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetPosition7DictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetPosition8DictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetPosition9DictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetPosition10DictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetPosition11DictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetPosition_0DictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetReconstructedMeaningDictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetCommentDictionaryCell(curEntryNumber, dictionaryConformity, worksheet);
                    tryParseAndSetLinkDictionaryCell(curEntryNumber, dictionaryConformity, worksheet);

                    vocabularyEntry.Dictionary_conformity.Add(dictionaryConformity);
                }
            }

            excelAppWorkbook.Close();
            excelApp.Quit();

            return entries.Values;
        }

        private static int tryParseEntryNumberDictionaryCell(int rowNumber, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            int entryNumber = 0;

            string columnNumber = ExcelDictionaryColumn.ENTRY_NUMBER.Column;
            Microsoft.Office.Interop.Excel.Range dataRange = worksheet.Cells[rowNumber, columnNumber];
            string cellValue = dataRange.Value2.ToString();

            bool parsed = Int32.TryParse(cellValue, out entryNumber);
            if (!parsed)
            {
                return 0;
            }

            return entryNumber;
        }

        private static Dictionary_conformity tryGetNewDictionaryConformity(int rowNumber, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            int lineNumber = 0;

            string columnNumber = ExcelDictionaryColumn.LINE_NUMBER.Column;
            Microsoft.Office.Interop.Excel.Range dataRange = worksheet.Cells[rowNumber, columnNumber];
            string cellValue = dataRange.Value2.ToString();

            bool parsed = Int32.TryParse(cellValue, out lineNumber);
            if (!parsed)
            {
                return null;
            }

            Dictionary_conformity conformity = new Dictionary_conformity();
            conformity.Number_string = lineNumber;

            return conformity;
        }

        private static string tryParseAndSetLemmaDictionaryCell(int rowNumber, Vocabulary_entry vocabularyEntry, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.LEMMA.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            vocabularyEntry.Lemma = cellValue;
            return cellValue;
        }

        private static Language tryParseAndSetLanguageDictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.LANGUAGE.Column;
            Microsoft.Office.Interop.Excel.Range dataRange = worksheet.Cells[rowNumber, columnNumber];
            string cellValue = dataRange.Value2.ToString();
            IEnumerable<Language> possibleLanguages = unitOfWork.LanguageRepository.Get();
            if (possibleLanguages == null)
            {
                return null;
            }
            Language firstSuitable = null;
            IEnumerator<Language> possibleLangEnumerator = possibleLanguages.GetEnumerator();
            if (possibleLangEnumerator.MoveNext())
            {
                firstSuitable = possibleLangEnumerator.Current;
            }
            dictionaryConformity.Language = firstSuitable;
            dictionaryConformity.ID_Language = firstSuitable.ID_Language;
            return firstSuitable;
        }

        private static string tryParseAndSetReconstructedFormDictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.RECONSTRUCTED_FORM.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.Phorma = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetVariantDictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.VARIANT.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.Variant = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetZeroPositionDictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.ZERO_POSITION.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_00 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetPosition1DictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.POSITION_1.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_1 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetPosition2DictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.POSITION_2.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_2 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetPosition3DictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.POSITION_3.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_3 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetPosition4DictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.POSITION_4.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_4 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetPosition5DictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.POSITION_5.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_5 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetPosition6DictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.POSITION_6.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_6 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetPosition7DictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.POSITION_7.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_7 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetPosition8DictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.POSITION_8.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_8 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetPosition9DictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.POSITION_9.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_9 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetPosition10DictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.POSITION_10.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_10 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetPosition11DictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.POSITION_11.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_11 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetPosition_0DictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.POSITION_0.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.p_0 = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetReconstructedMeaningDictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.RECONSTRUCTED_MEANING.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.Meaning = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetCommentDictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.COMMENT.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.Comment = cellValue;
            return cellValue;
        }

        private static string tryParseAndSetLinkDictionaryCell(int rowNumber, Dictionary_conformity dictionaryConformity, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            string columnNumber = ExcelDictionaryColumn.LINK.Column;
            string cellValue = parseStringValue(rowNumber, columnNumber, worksheet);
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            dictionaryConformity.Link = cellValue;
            return cellValue;
        }

        private static string parseStringValue(int rowNumber, string columnNumber, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            Microsoft.Office.Interop.Excel.Range dataRange = worksheet.Cells[rowNumber, columnNumber];
            string cellValue = dataRange.Value2.ToString();
            if (String.IsNullOrEmpty(cellValue))
            {
                return null;
            }
            return cellValue;
        }
    }
}
