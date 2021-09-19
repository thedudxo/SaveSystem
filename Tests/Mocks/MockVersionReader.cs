using DudCo.SaveSystems;
using DudCo.Wrappers;

namespace Tests.SaveSystems
{
    class MockVersionReader : IVersionReader
    {
        public IVersion ReadFromFile(IReader reader)
        {
            return new MockGameVersion(reader.ReadInt32());
        }
    }
}