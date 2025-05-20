using System;
using System.Windows.Forms;
using I_fucking_hate_this_class.Handlers;

namespace I_fucking_hate_this_class
{
    internal static class Program
    {
        /// <summary>
        ///  Eu odeio C# :heart:
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ApplicationConfiguration.Initialize();

            Logger.Info("Application started.");
            Logger.Debug("Entering Main method.");

            try
            {
                Logger.Debug("Calling DataBaseHandler.Initialize().");
                DataBaseHandler.Initialize();
                Logger.Info("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                Logger.Error($"Database initialization error: {ex.Message}");
                MessageBox.Show($"Database initialization failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Fatal("Application will exit due to fatal error.");
                return; 
            }

            Logger.Info("Starting main form.");
            Application.Run(new Form1());

            Logger.Info("Application exited.");
        }
    }
}
