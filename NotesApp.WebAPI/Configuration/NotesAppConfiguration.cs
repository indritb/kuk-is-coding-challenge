using NotesApp.Lib.Shared;

namespace NotesApp.WebAPI.Configuration
{
    public static class NotesAppConfiguration
    {
        private static IWebHostBuilder _webHostBuilder { get; set; }
        private static IConfiguration _configuration { get; set; }
        private static ConfigurationBuilder _configurationBuilder { get; set; }

        public static IWebHostBuilder InitializeApp(this IWebHostBuilder webHostBuilder)
        {
            _webHostBuilder = webHostBuilder;
            LoadConfiguration();
            _webHostBuilder.UseConfiguration(_configuration);
            return _webHostBuilder;
        }

        private static void LoadConfiguration()
        {
            SetupConfiguration();
            AddConfigurationSettingsFile("appsettings.json", true, true);
            AddConfigurationSettingsFile($"appsettings.{_configuration[$"environment"]}.json", true, true);
        }

        private static void AddConfigurationSettingsFile(string filename, bool optional, bool reloadOnChange)
        {
            SetupConfiguration();

            var fileName = FileManager.GetFileFullPath(filename);

            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName)) return;

            _configurationBuilder.AddJsonFile(filename, optional, reloadOnChange);
            _configuration = _configurationBuilder.Build();
        }

        private static void SetupConfiguration()
        {
            _configurationBuilder = new ConfigurationBuilder();
            _configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            _configuration = _configurationBuilder.Build();
        }
    }
}
