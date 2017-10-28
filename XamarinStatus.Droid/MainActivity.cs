using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using XamarinStatus.Interfaces;
using XamarinStatus.Models;

namespace XamarinStatus.Droid
{
    [Activity(Label = "XamarinStatus", MainLauncher = true)]
    public class MainActivity : Activity
    {
        IPackageManager _pm;

        Switch _switcher;
        ListView _supportLibraries;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main_activity);

            _pm = new PackageManager(this);

            _switcher = FindViewById<Switch>(Resource.Id.monoRuntimeSwitcher);
            _supportLibraries = FindViewById<ListView>(Resource.Id.supportLibrariesList);

            InitializeControls();
        }

        async void InitializeControls()
        {
            await _pm.InitializeAsync();

            _switcher.Checked = _pm.MonoRuntime.IsEnabled;
            _supportLibraries.Adapter = new ArrayAdapter<SupportLibrary>(this,
                Android.Resource.Layout.SimpleExpandableListItem1,
                _pm.SupportLibraries.ToArray());
        }
    }
}

