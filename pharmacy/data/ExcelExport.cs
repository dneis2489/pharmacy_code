

using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;

namespace pharmacy.data
{
    internal class ExcelExport
    {
        private ExcelExport()
        {
        }
        private static ExcelExport instance;

        public static ExcelExport Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExcelExport();
                }
                return instance;
            }
        }

        public static void ExportData(DataTable data, SaveFileDialog saveFileDialog)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Создаем новый файл Excel
            ExcelPackage excelPackage = new ExcelPackage();

            // Добавляем лист
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            // Заполняем ячейки данными из DataTable
            for (int i = 0; i < data.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = data.Columns[i].ColumnName;
            }

            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1].Value = data.Rows[i][j];
                }
            }

            // Сохраняем файл на выбранное место
            FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
            excelPackage.SaveAs(excelFile);

            Console.WriteLine("Файл Excel успешно создан и сохранен по пути: " + excelFile.FullName);
        }
    }
}
