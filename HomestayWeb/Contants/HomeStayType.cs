namespace HomestayWeb.Contants
{
    public class HomeStayType
    {
        private static List<string> instance = null;

        public static List<string> Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new List<string>()
                    {
                        "ORDERED",
                        "AVAILABLE",
                        "CLEANING"
                    };
                }
                return instance;
            }
        }
    }
}
