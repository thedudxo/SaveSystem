using System.IO;

namespace DudCo.SaveSystems
{
    public class WrappedFile : IWrappedFile
    {
        public bool Exists(string file)
        {
            return File.Exists(file);
        }
    }
}
