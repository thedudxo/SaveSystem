namespace Tests.SaveSystems
{
    class MockFile : DudCo.Wrappers.IWrappedFile
    {
        public bool doesExist = true;
        public bool Exists(string file) => doesExist;
    }
}