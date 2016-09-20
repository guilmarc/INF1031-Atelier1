
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
	public class CategoryListFragment : ListFragment
	{

		JArray jarray;

		public CategoryListFragment(JArray jarray)
		{
			this.jarray = jarray;
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			return base.OnCreateView(inflater, container, savedInstanceState);
		}

		public override Android.Animation.Animator OnCreateAnimator(FragmentTransit transit, bool enter, int nextAnim)
		{
			return base.OnCreateAnimator(transit, enter, nextAnim);
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			ListView.Adapter = new CategoryListAdapter(Activity, this.jarray);

			View.SetBackgroundColor(Android.Graphics.Color.Black);

			SetListShown(true);
		}

		public override void OnListItemClick(ListView l, View v, int position, long id)
		{
			base.OnListItemClick(l, v, position, id);

			//TODO: Afficher un marqueur sur la carte et déplacer la caméra
			((MainActivity)Activity).OnPointOfInterestSelected(jarray[position] as JObject);
		}
	}

	#region Adapter
	public class CategoryListAdapter : BaseAdapter<JObject>
	{
		Activity context;
		JArray jarray;

		public CategoryListAdapter(Activity context, JArray jarray)
		{
			this.context = context;
			this.jarray = jarray;
		}

		public override JObject this[int position]
		{
			get
			{
				return this.jarray[position] as JObject;
			}
		}

		public override int Count
		{
			get
			{
				return jarray.Count;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null, true);

			var poiTextView = view.FindViewById<TextView>(Android.Resource.Id.Text1);

			poiTextView.Text = this[position]["name"].ToString();

			return view;
		}
	}
	#endregion

	public interface IOnPointOfInterestSelectedListener
	{
		void OnPointOfInterestSelected(JObject PointOfInterest);	
	}
}
