using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.PM;
using XamarinStatus.Interfaces;
using XamarinStatus.Models;

namespace XamarinStatus
{
    public class PackageManager : IPackageManager
    {
        private readonly Context _context;
        private readonly MonoRuntime _monoRuntime;
        private readonly IList<SupportLibrary> _supportLibraries;

        public MonoRuntime MonoRuntime => _monoRuntime;
        public IEnumerable<SupportLibrary> SupportLibraries => _supportLibraries;

        public PackageManager(Context context)
        {
            _context = context;
            _monoRuntime = new MonoRuntime();
            _supportLibraries = new List<SupportLibrary>();
        }

        public Task InitializeAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                var monoPackages = AllInstalledApplications.Where(p => p.DataDir.Contains("Mono")).ToArray();

                FindMonoRuntime(monoPackages);

                FindAndroidSupportLibraries(monoPackages);
            });
        }

        private IEnumerable<ApplicationInfo> AllInstalledApplications =>
            _context.PackageManager.GetInstalledApplications(PackageInfoFlags.MetaData);

        protected void FindMonoRuntime(IEnumerable<ApplicationInfo> monoPackages)
        {
            var monoRuntime = monoPackages.FirstOrDefault(p => p.DataDir.Contains("Android.DebugRuntime"));
            if (monoRuntime != null)
            {
                _monoRuntime.IsInstalled = true;
                _monoRuntime.IsEnabled = monoRuntime.Enabled;
            }
        }

        protected void FindAndroidSupportLibraries(IEnumerable<ApplicationInfo> monoPackages)
        {
            foreach (var package in monoPackages.Where(p => p.DataDir.Contains("Android.Platform")))
            {
                _supportLibraries.Add(new SupportLibrary
                {
                    Name = package.PackageName,
                    IsEnabled = package.Enabled,
                    SdkVersion = (int)package.TargetSdkVersion
                });
            }
        }
    }
}