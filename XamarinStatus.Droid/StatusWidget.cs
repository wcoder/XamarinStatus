using Android.App;
using Android.Appwidget;
using Android.Content;

namespace XamarinStatus.Droid
{
    [BroadcastReceiver(Label = "@string/app_name")]
    [IntentFilter(new[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/status_widget")]
    public class StatusWidget : AppWidgetProvider
    {
        // https://developer.android.com/guide/topics/appwidgets/index.html
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            // To prevent any ANR timeouts, we perform the update in a service
            context.StartService(new Intent(context, typeof(StatusUpdateService)));
        }
    }
}