namespace Task3;

internal class Program
{
    public static void Main()
    {
        Console.WriteLine("Укажите полный путь до директории");
        var path = Console.ReadLine();
        DirInspection(path!);
    }
    static void DirInspection(string path)
    {
        if (!Directory.Exists(path)) return;
        var d = new DirectoryInfo(path);
        var start = DirSize(d);
        Console.WriteLine($"Исходный размер папки: {start / 1024}mb");
        DeleteDir(path);
        var end = DirSize(d);
        Console.WriteLine($"Освобождено: {(start - end) / 1024}mb");
        Console.WriteLine($"Текущий размер папки: {end}mb");
    }
    /// <summary>
    /// Расчёт объёма директории
    /// </summary>
    /// <param name="directoryInfo">Класс DirectoryInfo</param>
    /// <returns></returns>
    private static long DirSize(DirectoryInfo directoryInfo)
    {
        long size = 0;
        try
        {
            // Размер файла.
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
    private static void DeleteDir(string path)
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