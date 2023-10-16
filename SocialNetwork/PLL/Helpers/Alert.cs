namespace SocialNetwork.PLL.Helpers
{
    public static class Alert
    {
        public static void SuccesMessage(string message)
        {
            ChangeMessageColor(message, ConsoleColor.Green);
        }

        public static void ErrorMessage(string message)
        {
            ChangeMessageColor(message, ConsoleColor.Red);
        }

        private static void ChangeMessageColor(string message, ConsoleColor color)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = currentColor;
        }
    }
}
