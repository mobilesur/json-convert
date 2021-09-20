	public static class ConvertExtensions
	{

		public static string ToJson(this object Object)
		{

			if (Object != null)
				return JsonConvert.SerializeObject(Object, 
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


		public static string ToJsonIgnoreNull(this object Object)
		{

			if (Object != null)
				return JsonConvert.SerializeObject(Object,
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

		public static long AsLongDateTime(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return 0;

			long.TryParse(str.Replace("/", "").Replace(":", "").Replace(" ", ""), out long res);
			return res;
		}
	}
