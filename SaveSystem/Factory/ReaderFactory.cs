using System.IO;

namespace DudCo.SaveSystems
{
    public class ReaderFactory
    {
        readonly IReader customReader;
        readonly bool usingCustomReader = false;

        public ReaderFactory(IReader customReader)
        {
            this.customReader = customReader;
            usingCustomReader = true;
        }
        public ReaderFactory() { }

        public IReader CreateReader(string filename)
        {
            if (usingCustomReader)
                return customReader;

            return CreateDefaultReader(filename);
        }

        private IReader CreateDefaultReader(string filename)
        {
            var stream = File.Open(filename, FileMode.Open);
            return new WrappedReader(stream);
        }
    }
}