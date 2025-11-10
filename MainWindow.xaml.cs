using System;
using System.Windows;
using System.Windows.Navigation;
using _222_Titorenko.Pages; // 👈 нужно для доступа к AuthPage и другим страницам

namespace _222_Titorenko
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 🚀 При запуске открываем страницу авторизации
            MainFrame.Navigate(new AuthPage());

            // 👇 Обновляем видимость кнопки "Назад" при переходах
            MainFrame.Navigated += MainFrame_Navigated;
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Кнопка "Назад" активна только если есть история переходов
            BackButton.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (o, t) =>
            {
                DateTimeNow.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            };
            timer.Start();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show(
                "Вы уверены, что хотите закрыть окно?",
                "Подтверждение выхода",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void ChangeTheme(string themeFile)
        {
            try
            {
                var uri = new Uri(themeFile, UriKind.Relative);
                var resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при смене темы: {ex.Message}");
            }
        }

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            ChangeTheme("Dictionary.xaml");
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            ChangeTheme("DictionaryDark.xaml");
        }
    }
}
