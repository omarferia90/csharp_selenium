using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutomationLibrary
{
    public class ExcelHelper
    {
        private readonly string filePath;
        private readonly string sheetName;
        private readonly Dictionary<string, int> headers;
        private readonly SLDocument document;
        private readonly int headerRow = 1;
        private readonly string datasetColumnName = "Set";

        public ExcelHelper(string filePath, string sheetName = null)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Excel File was not found", filePath);

            this.filePath = filePath;
            document = new SLDocument(filePath);

            var availableSheets = document.GetSheetNames();

            if (!string.IsNullOrEmpty(sheetName))
            {
                if (availableSheets.Contains(sheetName))
                {
                    document.SelectWorksheet(sheetName);
                    this.sheetName = sheetName;
                }
                else
                {
                    string available = string.Join(", ", availableSheets);
                    throw new ArgumentException($"Sheet '{sheetName}' not exist in the excel file. Available sheets are: {available}");
                }
            }
            else
            {
                // Use default sheet
                this.sheetName = availableSheets.FirstOrDefault();
            }

            headers = LoadHeaders();
        }

        private Dictionary<string, int> LoadHeaders()
        {
            var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            int col = 1;
            while (true)
            {
                string colName = document.GetCellValueAsString(headerRow, col);
                if (string.IsNullOrWhiteSpace(colName)) break;
                if (!dict.ContainsKey(colName))
                    dict.Add(colName, col);
                col++;
            }
            return dict;
        }

        /// <summary>
        /// Returns all rows that match with dataset value as dictionary list
        /// </summary>
        public List<Dictionary<string, string>> GetRowsByDataset(string datasetValue)
        {
            var result = new List<Dictionary<string, string>>();
            if (!headers.ContainsKey(datasetColumnName))
                return result; // Column "Set" no exist

            int datasetColIndex = headers[datasetColumnName];
            int row = headerRow + 1;

            while (true)
            {
                string dsValue = document.GetCellValueAsString(row, datasetColIndex);
                if (string.IsNullOrEmpty(dsValue)) break;

                if (dsValue.Equals(datasetValue, StringComparison.OrdinalIgnoreCase))
                {
                    var rowDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var header in headers)
                    {
                        string cellValue = document.GetCellValueAsString(row, header.Value);
                        rowDict.Add(header.Key, cellValue);
                    }
                    result.Add(rowDict);
                }
                row++;
            }
            return result;
        }

        /// <summary>
        /// Returns the value of the column specified,with default value if column not exist
        /// </summary>
        public string GetValue(Dictionary<string, string> row, string columnName, string defaultValue = "")
        {
            if (row != null && row.ContainsKey(columnName))
            {
                string cellValue = row[columnName];

                // if emptym then use default value
                if (string.IsNullOrWhiteSpace(cellValue))
                    return defaultValue;

                // if value == TODAY, returns today's date
                if (cellValue.Trim().ToUpper() == "TODAY")
                    return DateTime.Today.ToString("MM/dd/yyyy");

                // if value starts with +/-, returns future/past date
                if (cellValue.StartsWith("+") || cellValue.StartsWith("-"))
                {
                    if (double.TryParse(cellValue, out double daysOffset))
                    {
                        DateTime dtNewDate = DateTime.Today.AddDays(daysOffset);
                        return dtNewDate.ToString("MM/dd/yyyy");
                    }
                }
                return cellValue;
            }
            else
            {
                return defaultValue;
            }
        }



        /// <summary>
        /// Function to save a new value into the sheet provided
        /// </summary>
        /// <param name="pstrPath"></param>
        /// <param name="pstrSheet"></param>
        /// <param name="pstrColumn"></param>
        /// <param name="pintRow"></param>
        /// <param name="pstrValue"></param>
        public void fnSaveValue(string pstrPath, string pstrSheet, string pstrColumn, int pintRow, string pstrValue, bool RemoveCharacters = true)
        {
            SLDocument _document = new SLDocument(pstrPath);
            if (!string.IsNullOrEmpty(pstrSheet))
            {
                if (_document.GetSheetNames().Contains(pstrSheet)) { _document.SelectWorksheet(pstrSheet); }
                Dictionary<string, int> _dicHeaderE = LoadHeaders();
                if (_dicHeaderE.ContainsKey(pstrColumn))
                {
                    _document.SetCellValue(pintRow, _dicHeaderE[pstrColumn], pstrValue);
                    _document.Save();
                }
            }
        }
    }
}
