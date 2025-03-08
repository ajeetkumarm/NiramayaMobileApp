using Kadam.Services;

namespace Kadam
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine(ex.ToString());
            }
        }


        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}