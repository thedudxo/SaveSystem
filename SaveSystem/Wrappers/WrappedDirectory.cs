using System.IO;

namespace DudCo.SaveSystems
{
    public class WrappedDirectory : IWrappedDirectory
    {
        public void Create(string directory)
        {
            Directory.CreateDirectory(directory);
        }

        public bool Exists(string directory)
        {
            return Directory.Exists(directory);
        }
    }
}
