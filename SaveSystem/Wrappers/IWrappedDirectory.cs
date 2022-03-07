namespace DudCo.SaveSystems
{
    public interface IWrappedDirectory
    {
        bool Exists(string directory);
        void Create(string directory);
    }
}
