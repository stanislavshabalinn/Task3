namespace Task3;

internal class Program
{
    static void Main(string[] args)
    {
        TimeSpan interval = TimeSpan.FromMinutes(30);
        Console.WriteLine("Укажите путь до каталога:");
        string path = Console.ReadLine();
       // Если каталог существует
        if (Directory.Exists(path))                                 
        { 
    // Проверка пустоты
    static void DirInspection(string path)
    {
        // Путь есть
        if (!Directory.Exists(path)) return;
        // Нвая информация
        var d = new DirectoryInfo(path);
        // Определяем размер 
        var start = DirSize(d);
        // Исходник
        Console.WriteLine($"Исходный размер папки: {start / 1024}mb");
        // Удоляем
        DeleteDir(path);
        //Конечный размер
        var end = DirSize(d);
        Console.WriteLine($"Освобождено: {(start - end) / 1024}mb");
        Console.WriteLine($"Текущий размер папки: {end}mb");
    }
    /// <summary>
    /// Расчёт объёма директории
    /// </summary>
    /// <param name="directoryInfo">Класс DirectoryInfo</param>
    /// <returns></returns>
    static long DirSize(DirectoryInfo directoryInfo)
    {
        // Определяем размер 
        long size = 0;
        try
        {
            // Размер каждого файла.
            var fis = directoryInfo.GetFiles();
            foreach (var fi in fis)
            {
                size += fi.Length;
            }
            // Размер папки.
            var dis = directoryInfo.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return size;
    }
    /// <summary>
    /// Очистка директории
    /// </summary>
    /// <param name="path">Путь к директории</param>
    static void DeleteDir(string path)
    {
        var del = new int[] { };
        //Список под папок
        var dirs = Directory.GetDirectories(path);
        //Удаление под каталогов
        foreach (var dir in dirs)
            try
            {
                Directory.Delete(dir, true);
            }
            catch (IOException e)
            {
                Console.WriteLine($"Для директории {dir} установлен статус {e.Message}");
            }

        //Список файлов
        var files = Directory.GetFiles(path);
        //Удаление файлов
        foreach (var file in files)
            try
            {
                File.Delete(file);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Для файла {file} установлен статус {e.Message}");
                throw;
            }

            }


            }
    }

}