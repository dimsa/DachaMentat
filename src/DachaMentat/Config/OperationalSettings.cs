namespace DachaMentat.Config
{
    /// <summary>
    /// Settings for operation of Mentat Service
    /// </summary>
    public class OperationalSettings
    {
        public bool WorkAsProxy { get; set; }

        public ProxySettings ProxySettings { get; set; }
    }
}
