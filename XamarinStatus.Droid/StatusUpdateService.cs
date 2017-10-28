using System;
using System.Threading.Tasks;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace XamarinStatus.Droid
{
    [Service]
    public class StatusUpdateService : Service
    {
        public override async void OnStart(Intent intent, int startId)
        {
            // Build the widget update for today
            var updateViews = await BuildUpdate(this);

            // Push update for this widget to the home screen
            ComponentName thisWidget = new ComponentName(this, Java.Lang.Class.FromType(typeof(StatusWidget)).Name);
            AppWidgetManager manager = AppWidgetManager.GetInstance(this);
            manager.UpdateAppWidget(thisWidget, updateViews);
        }

        public override IBinder OnBind(Intent intent)
        {
            // We don't need to bind to this service
            return null;
        }


        // Build a widget update to show the current status
        public async Task<RemoteViews> BuildUpdate(Context context)
        {
            var updateViews = new RemoteViews(context.PackageName, Resource.Layout.status_widget);

            try
            {
                var pm = new PackageManager(this);
                await pm.InitializeAsync();

                updateViews.SetTextViewText(Resource.Id.statusLabel, pm.MonoRuntime.IsEnabled ? "On" : "Off");
            }
            catch (Exception e)
            {
                Log.Error(nameof(StatusWidget), e.ToString());
            }

            // When user clicks on widget, launch to Wiktionary definition page
            //    Intent defineIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(entry.Link));
            //    PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, defineIntent, 0);
            //    updateViews.SetOnClickPendingIntent(Resource.Id.widget, pendingIntent);

            return updateViews;
        }
    }
}