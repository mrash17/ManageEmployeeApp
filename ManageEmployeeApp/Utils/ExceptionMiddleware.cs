namespace ManageEmployeeApp.Utils
{
    public static class ExceptionMiddleware
    {
        public static async Task RunAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
        private static void LogException(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n===== UNHANDLED EXCEPTION =====");
            Console.WriteLine($"Message    : {ex.Message}");
            Console.ResetColor();
        }
    }
}
