using DudCo.SaveSystems;

namespace Tests.SaveSystems
{
    class MockVersionWriter : IVersionWriter
    {
        MockGameVersion mockGameVersion;

        public MockVersionWriter(MockGameVersion mockGameVersion)
        {
            this.mockGameVersion = mockGameVersion;
        }

        public void WriteToFile(IWriter writer)
        {
            writer.Write(mockGameVersion.version);
        }
    }
}