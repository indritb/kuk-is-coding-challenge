namespace NotesApp.Lib.Shared
{
    public static class FileManager
    {
        public static string GetFileFullPath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) 
                return string.Empty;

            if (File.Exists(fileName)) 
                return Path.GetFullPath(fileName);

            return Directory.GetFiles(Directory.GetCurrentDirectory(), fileName, SearchOption.AllDirectories).FirstOrDefault();
        }
    }
}