namespace BlApi
{
    static public class Factory
    {
        public static IBl Instance;
        public static IBl Get()
        {
            Instance = new BlImplementation.Bl();
            return Instance;
        }
    }
}
