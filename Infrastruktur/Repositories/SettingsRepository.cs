using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        public string GetModelFilePath() => Properties.Settings.Default.ModelFilePath;
        public void SetModelFilePath(string path)
        {
            Properties.Settings.Default.ModelFilePath = path;
            Properties.Settings.Default.Save();
        }

        public uint GetNGram() => Properties.Settings.Default.NGram;
        public void SetNGram(uint nGram)
        {
            Properties.Settings.Default.NGram = nGram;
            Properties.Settings.Default.Save();
        }

        public string GetModelFileType() => Properties.Settings.Default.ModelFileType;
        public void SetModelFileType(string fileType)
        {
            Properties.Settings.Default.ModelFileType = fileType;
            Properties.Settings.Default.Save();
        }
    }
}
