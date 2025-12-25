using System.Windows;

namespace SecurityCompanyWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Инициализация логирования или других сервисов
            InitializeServices();
        }

        private void InitializeServices()
        {
            // Здесь можно инициализировать сервисы, базу данных и т.д.
        }
    }
}