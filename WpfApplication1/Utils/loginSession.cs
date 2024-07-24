namespace WpfApplication1.Utils
{
    internal static class loginSession
    {
        private static string username = "";
        private static string password = "";
        private static int id;
        private static int id_role;

        public static string Username
        {
            get => loginSession.username;
            set => loginSession.username = value;
        }

        public static string Password
        {
            get => loginSession.password;
            set => loginSession.password = value;
        }

        public static int Id
        {
            get => loginSession.id;
            set => loginSession.id = value;
        }

        public static int Id_role
        {
            get => loginSession.id_role;
            set => loginSession.id_role = value;
        }
    }
}