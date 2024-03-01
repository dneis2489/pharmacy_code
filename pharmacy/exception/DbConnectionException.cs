using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmacy.exception
{
    public class DbConnectionException
    {
        //  TODO: вынести сюда ошибку подключения к БД и протестить
        public static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)  
        {
            Exception exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                // Обработка необработанного исключения
                Console.WriteLine($"Unhandled Exception: {exception.Message}");
            }
            else
            {
                // Если исключение не является объектом типа Exception
                Console.WriteLine("Unhandled Exception of unknown type");
            }            
        }
    }
}
