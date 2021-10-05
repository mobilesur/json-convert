using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Moghimi.Core.Json
{

    /// <summary>
    /// Defines a custom resolver to ignore virtual properties while serializing
    /// </summary>
    public class IgnoreVirtualResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            var propInfo = member as PropertyInfo;
            if (propInfo != null)
            {
                if (propInfo.GetMethod.IsVirtual && !propInfo.GetMethod.IsFinal)
                {
                    prop.ShouldSerialize = obj => false;
                }
            }
            return prop;
        }
    }


    /// <summary>
    /// Defines a set of different settings to use with the our ConvertExtensions
    /// </summary>
    public static class JsonSettings
    {
        /// <summary>
        /// Setting description: With default naming strategy, not formating, and null values will ignored.
        /// </summary>
        internal static JsonSerializerSettings Default => new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new DefaultNamingStrategy()
            },
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore
        };


        /// <summary>
        /// Setting description: With camelcase naming strategy, indented formating, and null values will ignored.
        /// </summary>
        internal static JsonSerializerSettings CamelCaseIndented => new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// Setting description: With snakecase naming strategy, indented formating, and null values will ignored.
        /// </summary>
        internal static JsonSerializerSettings SnakeCaseIndented => new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        /*
         * Create more settings based on your needs
         */
    }


    public enum ContractResolver
    { 
        Default = 0,
        CamelCase = 1,
        SnakeCase = 2,
        IgnoreVirtual = 3,
    }

    public static class ConvertExtensions
    {
        /// <summary>
        /// Defines an extension to serialize any object into a json string based on setting set 'JsonSettings.Default'
        /// </summary>
        /// <param name="Object"> Source object </param>
        /// <returns> json string </returns>
        public static string ToJson(this object Object)
        {
            if (Object != null)
                return JsonConvert.SerializeObject(Object,
                    Formatting.None,
                    JsonSettings.Default);
            else
                return null;
        }


        /// <summary>
        /// Defines an extension to serialize any object into a json string with custom settings as input parameter
        /// </summary>
        /// <param name="Object"> Source object </param>
        /// <param name="indented"> Indicatea that the output json created with indented formatting or not </param>
        /// <param name="ignoreNulls"> Indicates that properties with null values ignored from output or not </param>
        /// <param name="resolver">Selects the type of resolver </param>
        /// <returns></returns>
        public static string ToJson(this object Object, bool indented = false, bool ignoreNulls = true, ContractResolver resolver = ContractResolver.Default)
        {
            if (Object != null)
                return JsonConvert.SerializeObject(Object,
                    (indented ? Formatting.Indented : Formatting.None),
                    new JsonSerializerSettings()
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.None,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        NullValueHandling = ignoreNulls ? NullValueHandling.Ignore : NullValueHandling.Include,
                        ContractResolver = resolver == ContractResolver.Default ? new DefaultContractResolver()
                        {
                            NamingStrategy = new DefaultNamingStrategy()
                        } : resolver == ContractResolver.CamelCase ? new DefaultContractResolver()
                        {
                            NamingStrategy = new CamelCaseNamingStrategy()
                        } : resolver == ContractResolver.SnakeCase ? new DefaultContractResolver()
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        } : resolver == ContractResolver.IgnoreVirtual ? new IgnoreVirtualResolver() : null,
                        //DefaultValueHandling = DefaultValueHandling.Include,
                    });
            else
                return null;
        }

        /// <summary>
        /// Defines an extension to deserialize a json string into an onject with type T 
        /// </summary>
        /// <typeparam name="T"> The desired output type </typeparam>
        /// <param name="json"> Source string </param>
        /// <returns></returns>
        public static T To<T>(this string json)	=> JsonConvert.DeserializeObject<T>(json);

    }
}
