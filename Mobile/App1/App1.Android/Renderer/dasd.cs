using Android.Content;
using Android.Graphics;
using App1.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace App1.Droid.Renderer
{
    public class CustomEntryRenderer : EntryRenderer {
        private readonly EntryRendererManager _manager;

        public CustomEntryRenderer(Context context) : base(context) {
            this._manager=new EntryRendererManager();
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
                _manager.Execute(this,e);
        }
    }
}