namespace Domain.Interfaces
{
    public interface ISettingsRepository
    {
        string GetModelFilePath();
        void SetModelFilePath(string path);

        string GetModelFileType();
        void SetModelFileType(string fileType);

        uint GetNGram();
        void SetNGram(uint nGram);
    }
}
