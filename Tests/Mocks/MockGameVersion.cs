using DudCo.SaveSystems;

namespace Tests.SaveSystems
{
    class MockGameVersion : IVersion
    {
        public int version;

        public MockGameVersion(int version)
        {
            this.version = version;
        }

        public int CompareTo(object obj)
        {
            MockGameVersion other = obj as MockGameVersion;
            if (other == null) throw new System.ArgumentException();
            return version.CompareTo(other.version);
        }
    }
}