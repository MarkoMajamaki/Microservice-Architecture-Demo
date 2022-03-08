using Microsoft.Extensions.Configuration;

namespace Shared.Api
{
    public class AddKeyPerFileConfigurationProvider : ConfigurationProvider
    {
        private string _sectionDivider;
        private string _path;

        public AddKeyPerFileConfigurationProvider(string path, string sectionDivider)
        {
            _sectionDivider = sectionDivider;
            _path = path;
        }

        public override void Load()
        {
            // Read secret files from path. Set file name as key and content as value. Replace section 
            // divider with ":" string if divider is giving.
            foreach(string filePath in Directory.GetFiles(_path))
            {
                string fileContent = File.ReadAllText(Path.Combine(_path, filePath));
                string fileName = Path.GetFileName(filePath);

                if (string.IsNullOrEmpty(_sectionDivider))
                {
                    Data.Add(fileName, fileContent);
                }
                else
                {
                    string fileNameWithCorrectDivider = fileName.Replace(_sectionDivider, ":");
                    Data.Add(fileNameWithCorrectDivider, fileContent);
                }
            }            
        }
    }

    public class AddKeyPerFileConfigurationProviderSource : IConfigurationSource
    {
        private readonly string _sectionDivider;
        private readonly string _path;

        public AddKeyPerFileConfigurationProviderSource(string path, string sectionDivider)
        {
            _sectionDivider = sectionDivider;
            _path = path;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AddKeyPerFileConfigurationProvider(_path, _sectionDivider);
        }
    }

    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddKeyPerFile(
            this IConfigurationBuilder builder, string path, string sectionDivider)
        {
            return builder.Add(new AddKeyPerFileConfigurationProviderSource(path, sectionDivider));
        }
    }
}

