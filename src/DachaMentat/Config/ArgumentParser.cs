namespace DachaMentat.Config
{
    /// <summary>
    /// Simple implementation of Run Argument Parser. 
    /// </summary>
    public class ArgumentParser
    {

        private Dictionary<string, string> _arguments;

        public ArgumentParser(string[] args) {
            _arguments = ParseArguments(args);
        }

        static Dictionary<string, string> ParseArguments(string[] input)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            for (int i = 0; i < input.Length; i++)
            {
                string key = input[i].ToLower();
                if (IsArgumentIsKey(key))
                {
                    key = key.Substring(2);
                    if (!values.ContainsKey(key))
                    {
                        values.Add(key, string.Empty);
                    }


                    if (i + 1 < input.Length)
                    {
                        string value = input[i + 1];
                        if (!IsArgumentIsKey(value))
                        {
                            values[key] = value;
                            i++;
                            continue;
                        }
                    }
                }
            }

            return values;
        }

        private static bool IsArgumentIsKey(string key)
        {
            return key.StartsWith("--");
        }

        public bool TryGetArgumentByName(string name, out string value)
        {
            if (_arguments.ContainsKey(name.ToLower()))
            {
                value = _arguments[name.ToLower()];
                return true;                
            } 
            
            value = string.Empty;
            return false;
        }

        public OperationalSettings Parse()
        {
            var res = new OperationalSettings();
            res.WorkAsProxy = false;
            res.ProxySettings = new ProxySettings();

            var workAsProxySettings = "ProxyMode".ToLower();
            if (_arguments.ContainsKey(workAsProxySettings))
            {
                if (_arguments[workAsProxySettings] == "true")
                {
                    res.WorkAsProxy = true;
                }
            }

            var proxyUrlSetting = "ProxyHost".ToLower();
            if (_arguments.ContainsKey(proxyUrlSetting))
            {
                res.ProxySettings.BaseUrl = _arguments[proxyUrlSetting];
            }

            return res; 
        }
        
    }
}
