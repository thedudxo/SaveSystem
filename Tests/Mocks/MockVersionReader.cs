using DudCo.SaveSystems;

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