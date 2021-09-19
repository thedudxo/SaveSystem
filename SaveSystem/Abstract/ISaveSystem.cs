namespace DudCo.SaveSystems
{
    public interface ISaveSystem
    {
        void Load();
        void Save();
        void SetFile(string file);
    }
}