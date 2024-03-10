using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;

namespace pharmacy.data
{
    internal class ExcelExport
    {
        public static void ExportDataFromDataTable(DataTable data, SaveFileDialog saveFileDialog, List<string> exportColumns)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Создаем новый файл Excel
            ExcelPackage excelPackage = new ExcelPackage();

            // Добавляем лист
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            //Заполняем шапку таблицы
            exportColumns.ForEach(columnName =>
            worksheet.Cells[1, exportColumns.IndexOf(columnName) + 1].Value = columnName);

            List<int> indexesForExportColumns = GetColumnIndexForExport(exportColumns, data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = 0; j < exportColumns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1].Value = data.Rows[i][indexesForExportColumns[j]];
                }
            }

            // Сохраняем файл на выбранное место
            FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
            excelPackage.SaveAs(excelFile);

            Console.WriteLine("Файл Excel успешно создан и сохранен по пути: " + excelFile.FullName);
        }

        public static void ExportOrderFromDataTable(DataTable data, string mainOrderData, SaveFileDialog saveFileDialog, List<string> exportColumns)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Создаем новый файл Excel
            ExcelPackage excelPackage = new ExcelPackage();

            // Добавляем лист
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            //Заполняем шапку таблицы
            exportColumns.ForEach(columnName =>
            worksheet.Cells[1, exportColumns.IndexOf(columnName) + 1].Value = columnName);

            List<int> indexesForExportColumns = GetColumnIndexForExport(exportColumns, data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = 0; j < exportColumns.Count; j++)
                {
                    if(j == exportColumns.Count-1)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = mainOrderData;
                    }
                    else
                    {
                        worksheet.Cells[i + 2, j + 1].Value = data.Rows[i][indexesForExportColumns[j]];
                    }
                    
                }
            }

            // Сохраняем файл на выбранное место
            FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
            excelPackage.SaveAs(excelFile);

            Console.WriteLine("Файл Excel успешно создан и сохранен по пути: " + excelFile.FullName);
        }

        public static void ExportDataFromStat(DataTable exportData, SaveFileDialog saveFileDialog)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Создаем новый файл Excel
            ExcelPackage excelPackage = new ExcelPackage();

            // Добавляем лист
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            for (int i = 0; i < exportData.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = exportData.Columns[i].ColumnName;
            }

            for (int i = 0; i < exportData.Rows.Count; i++)
            {
                for (int j = 0; j < exportData.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j+1].Value = exportData.Rows[i][j].ToString();
                }
            }
            // Сохраняем файл на выбранное место
            FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
            excelPackage.SaveAs(excelFile);

            Console.WriteLine("Файл Excel успешно создан и сохранен по пути: " + excelFile.FullName);
        }

        private static List<int> GetColumnIndexForExport(List<string> exportColumns, DataTable data)
        {
            List<int> indexes = new List<int>();
            DataColumnCollection basketColumn = data.Columns;

            for (int i = 0; i < basketColumn.Count; i++)
            {
                if (exportColumns.Contains(basketColumn[i].ColumnName)) // TODO: тут мб будет баг из-за неполного совпадения
                {
                    indexes.Add(i);
                }
            }

            return indexes;
        }
    }
}
