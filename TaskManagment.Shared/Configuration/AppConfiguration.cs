

namespace TaskManagment.Shared.Configuration
{
    public class AppConfiguration
    {
        public string ConnectionString { get; set; }

        public Token Token { get; set; }
     
    }
   
    public class Token
    {
        public string Bearer { get; set; }
        public int Expires { get; set; }
    }
}
