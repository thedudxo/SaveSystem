using DudCo.Wrappers;

namespace Tests.SaveSystems
{
    class MockDirectoryWrapper : IWrappedDirectory
    {
        public bool doesExist;
        public bool created = false;
        public string createdDirectory;
        public bool Exists(string directory) => doesExist;
        public void Create(string directory)
        {
            createdDirectory = directory;
            created = true;
        }
    }
}