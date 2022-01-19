namespace wordleHelper;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        var eval = new Evaluator();
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm(eval));
    }
}