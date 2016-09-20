
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;

namespace Atelier1
{
	public class CategoriesListFragment : ListFragment
	{
		JArray array;

		public CategoriesListFragment()
		{

		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
			this.array = ModelManager.Instance.getCategories();
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			return base.OnCreateView(inflater, container, savedInstanceState);

		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			View.SetBackgroundColor(Android.Graphics.Color.Black);

			ListView.Adapter = new CategoriesAdapter(Activity, this.array);
			SetListShown(true);
		}

		public override void OnListItemClick(ListView l, View v, int position, long id)
		{
			base.OnListItemClick(l, v, position, id);

			var fragment = new CategoryListFragment(this.array[position]["list"] as JArray);

			var transaction = FragmentManager.BeginTransaction();
			transaction.Replace(Resource.Id.MasterFragment, fragment);
			transaction.AddToBackStack(null);
			transaction.Commit();
		}
	}

	public class CategoriesAdapter : BaseAdapter<JObject>
	{

		Activity context;
		JArray array;

		public CategoriesAdapter(Activity context, JArray array)
		{
			this.array = array;
			this.context = context;
		}

		public override JObject this[int position]
		{
			get
			{
				return this.array[position] as JObject;
			}
		}

		public override int Count
		{
			get
			{
				return this.array.Count;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null, true);

			var categoryTextView = view.FindViewById<TextView>(Android.Resource.Id.Text1);

			categoryTextView.Text = this[position]["category"].ToString();

			return view;
		}
	}
}
