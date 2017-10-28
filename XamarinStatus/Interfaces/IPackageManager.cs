using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinStatus.Models;

namespace XamarinStatus.Interfaces
{
    public interface IPackageManager
    {
        /// <summary>
        /// Mono Shared MonoRuntime
        /// </summary>
        MonoRuntime MonoRuntime { get; }

        /// <summary>
        /// Xamarin.Android API-XX Support
        /// </summary>
        IEnumerable<SupportLibrary> SupportLibraries { get; }

        Task InitializeAsync();
    }
}