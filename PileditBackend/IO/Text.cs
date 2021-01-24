using System.IO;

namespace PileditBackend.IO
{
    public static class EditText
    {
        public static string ReadFile(string path)
        {
            using var reader = new StreamReader(path);
            return reader.ReadToEnd();
        }
        public static void WriteFile(string path, string content)
        {
            using var writer = new StreamWriter(path);
            writer.Write(content);
        }
    }
}
