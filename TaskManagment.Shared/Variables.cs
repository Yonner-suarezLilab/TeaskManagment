namespace TaskManagment.Shared
{
    public static class Variables
    {
        public static class Constantes
        {
            
        }
        public static class Results
        {
            public const int OK = 200;
            public const int CREATED = 201;
            public const int NOCONTENT = 204;
            public const int ERROR = 500;
            public const int BADREQUEST = 400;
            public const int ERROR_VALIDATION = 420;
            public const int NOAUTORIZADO = 403;
        }
        public static class Messages
        {
            public const string OK = "La operación se realizó con éxito";
            public const string ERROR = "Lo sentimos, algo salió mal. Intenta de nuevo mas tarde";
        }
    }
}
