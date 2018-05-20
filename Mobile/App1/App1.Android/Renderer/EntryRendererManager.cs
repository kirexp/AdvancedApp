using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using App1.Renderer;
using App1.Renderer.App1.Entry;
using Java.Util;
using Xamarin.Forms.Platform.Android;

namespace App1.Droid.Renderer
{
    public class EntryRendererManager {
        public void Execute(EntryRenderer renderer, ElementChangedEventArgs<Xamarin.Forms.Entry> e) {
            if (renderer.Element is WhiteLinedEntry)
            {
                if (renderer.Control == null || e.NewElement == null) return;

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    renderer.Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
                else
                    renderer.Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);
            }
            else if (renderer.Element is ElipsEntry)
            {
                if (e.NewElement != null)
                {
                    var view = (ElipsEntry)renderer.Element;
                    if (view.IsCurvedCornersEnabled)
                    {
                        // creating gradient drawable for the curved background  
                        var _gradientBackground = new GradientDrawable();
                        _gradientBackground.SetShape(ShapeType.Rectangle);
                        _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                        // Thickness of the stroke line  
                        _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                        // Radius for the curves  
                        _gradientBackground.SetCornerRadius(
                            DpToPixels(renderer.Context, Convert.ToSingle(view.CornerRadius)));

                        // set the background of the   
                        renderer.Control.SetBackground(_gradientBackground);
                    }
                    // Set padding for the internal text from border  
                    renderer.Control.SetPadding(
                        (int)DpToPixels(renderer.Context, Convert.ToSingle(12)), renderer.Control.PaddingTop,
                        (int)DpToPixels(renderer.Context, Convert.ToSingle(12)), renderer.Control.PaddingBottom);
                }
            }
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}