using System;
using System.Collections.Generic;
using System.IO;
using PIE_DB.Data_Model;

namespace PIE_BE.Excel
{
    public static class PIEUtils
    {
        public static void LoadDictionaryFromFiles(string[] filenames)
        {
            if (filenames == null || filenames.Length == 0)
            {
                return;
            }
            foreach (string filename in filenames)
            {
                if (String.IsNullOrEmpty(filename))
                {
                    return;
                }
                if (!File.Exists(filename))
                {
                    return;
                }

                IList<Vocabulary_entry> dictionaryEntries = PIEDataImportUtil.ReadDictionaryEntriesFromExcelFile(filename);
            }
        }
    }
}
