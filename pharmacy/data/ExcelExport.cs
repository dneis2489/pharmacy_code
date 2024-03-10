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

        public static void ExportDataFromChart(System.Windows.Forms.DataVisualization.Charting.Chart chart, SaveFileDialog saveFileDialog, List<string> columnNames)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Создаем новый файл Excel
            ExcelPackage excelPackage = new ExcelPackage();

            // Добавляем лист
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            //Заполняем шапку таблицы
            for (int i = 0; i < columnNames.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = columnNames[i];
            }

            for (int i = 0; i < chart.Series["DataPoints"].Points.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = chart.Series["DataPoints"].Points[i].XValue;
                worksheet.Cells[i + 2, 2].Value = chart.Series["DataPoints"].Points[i].YValues;
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
