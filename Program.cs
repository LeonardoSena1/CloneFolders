class Program
{
    static void Main(string[] args)
    {
        string sourcePath = @"E:";
        string targetPath = @"C:\Users\leona\Downloads\teste";

        try
        {
            CopyFolder(sourcePath, targetPath);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new Exception($"Access denied: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        Console.WriteLine("Disk cloning complete.");
    }

    static void CopyFolder(string sourcePath, string targetPath)
    {
        if (!Directory.Exists(targetPath))
            Directory.CreateDirectory(targetPath);

        string[] files = Directory.GetFiles(sourcePath);
        foreach (string file in files)
            File.Copy(
                file,
                Path.Combine(targetPath, Path.GetFileName(file)).ToString(),
                true);

        string[] subDirectories = Directory.GetDirectories(sourcePath);
        foreach (string subDir in subDirectories)
            if (!subDir.Contains("System Volume Information"))
                CopyFolder(subDir, Path.Combine(targetPath, Path.GetFileName(subDir)));
    }
}