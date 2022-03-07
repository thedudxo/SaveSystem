using DudCo.SaveSystems;

namespace Tests.SaveSystems
{
    class MockFile : IWrappedFile
    {
        public bool doesExist = true;
        public bool Exists(string file) => doesExist;
    }
}