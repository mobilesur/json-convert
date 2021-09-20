using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Moghimi.Core.Json
{
    public static class ConvertExtensions
    {
        //A simple object extention to convert its data to json
        public static string ToJson(this object Object)
        {
            if (Object != null)
                return JsonConvert.SerializeObject(Object, 
                    //Use Formatting.Indented for much more readable:
                    Formatting.None,
                    new JsonSerializerSettings()
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.None,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ContractResolver = new CustomResolver(),
                    });
            else
                return null;
        }

        //Same as above extenstion but null values with be ignored
        public static string ToJsonIgnoreNull(this object Object)
        {
            if (Object != null)
                return JsonConvert.SerializeObject(Object,
                    //Use Formatting.Indented for much more readable:
                    Formatting.None,
                    new JsonSerializerSettings()
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.None,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ContractResolver = new CustomResolver(),
                        NullValueHandling = NullValueHandling.Ignore,
                    });
            else
                return null;
        }
    }
}
