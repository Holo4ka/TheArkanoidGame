namespace ArkanoidGame
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            GameEngine eng = new GameEngine(800, 500);
            StartForm f = new StartForm(eng);
            f.Show();
            Application.Run();

        }
    }
}