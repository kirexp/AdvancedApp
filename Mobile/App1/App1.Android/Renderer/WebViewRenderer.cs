using Android.Content;
using Android.Webkit;
using App1.Droid.Renderer;
using App1.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WebView = Xamarin.Forms.WebView;
[assembly: ExportRenderer(typeof(WebView), typeof(NoChacheWebViewRenderer))]

namespace App1.Droid.Renderer
{
       public class NoChacheWebViewRenderer : WebViewRenderer
        {
            public NoChacheWebViewRenderer(Context context) : base(context)
            {

            }

            protected override void OnElementChanged(ElementChangedEventArgs<WebView> e) {
            base.OnElementChanged(e);
                if (this.Control == null) return;

                this.Control.ClearCache(true);
                this.Control.Settings.SetAppCacheEnabled(false);
                this.Control.Settings.CacheMode = CacheModes.NoCache;
            }
      }
    
}
