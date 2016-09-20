using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace Atelier1
{
	public class ModelManager
	{

		private static ModelManager instance;

		private ModelManager() { }

		public static ModelManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ModelManager();
				}
				return instance;
			}
		}

		public JArray getCategories()
		{
			return JArray.Parse(getJSON());
		}

		public string getJSON()
		{
			// Read the contents of our asset
			using (var stream = Android.App.Application.Context.Assets.Open("poi.json", Android.Content.Res.Access.Streaming))
			{
				using (var reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		}


	}
}
