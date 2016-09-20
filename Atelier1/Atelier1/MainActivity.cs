using Android.App;
using Android.Widget;
using Android.OS;
using System.Reflection;
using System;
using Newtonsoft.Json.Linq;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace Atelier1
{
	[Activity(Label = "Atelier1", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity, IOnPointOfInterestSelectedListener, IOnMapReadyCallback
	{
		GoogleMap map;
		Marker marker;

		public void OnMapReady(GoogleMap googleMap)
		{
			map = googleMap;
		}

		public void OnPointOfInterestSelected(JObject PointOfInterest)
		{

			//S'il existe déjà un marqueur on le retire de la carte
			if (marker != null) marker.Remove();

			//On extrait les données du JSON
			var name = PointOfInterest["name"].ToString();
			//var address = PointOfInterest["address"].ToString();
			//var website = PointOfInterest["website"].ToString();
			//var phone = PointOfInterest["phone"].ToString();
			var latlng = new LatLng((double)PointOfInterest["latlon"][0], (double)PointOfInterest["latlon"][1]);

			//Ajout du marqueur sur la carte
			var options = new MarkerOptions()
				.SetTitle(name)
				.SetPosition(latlng);
			marker = map.AddMarker(options);

			//Animation de la caméra vers le point d'intérêt
			map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(latlng, 15.0f));
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);


			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.MainLayout);

			var fragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.DetailFragment);
			fragment.GetMapAsync(this);
			            
		}

	}
}

