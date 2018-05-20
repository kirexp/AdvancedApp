//using System;
//using System.Collections.Generic;
//using System.Text;
//using Android.Content;
//using Android.Content.Res;
//using Android.Graphics;
//using Android.Graphics.Drawables;
//using Android.OS;
//using Android.Util;
//using App1.Rendered;
//using App1.Renderer.App1.Entry;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
//using Color = Xamarin.Forms.Color;
//[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
//namespace App1.Rendered {
//    public class CustomEntryRenderer : EntryRenderer
//    {
//        public CustomEntryRenderer(Context context) : base(context) { }
//        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
//        {
//            base.OnElementChanged(e);
//            if (e.NewElement != null)
//            {
//                if (!(Element is CustomEntry)) {
//                    return
//                }
//                var view = (CustomEntry)Element;
//                if (view.IsCurvedCornersEnabled)
//                {
//                    // creating gradient drawable for the curved background  
//                    var _gradientBackground = new GradientDrawable();
//                    _gradientBackground.SetShape(ShapeType.Rectangle);
//                    _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

//                    // Thickness of the stroke line  
//                    _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

//                    // Radius for the curves  
//                    _gradientBackground.SetCornerRadius(
//                        DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));

//                    // set the background of the   
//                    Control.SetBackground(_gradientBackground);
//                }
//                // Set padding for the internal text from border  
//                Control.SetPadding(
//                    (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingTop,
//                    (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);
//            }
//        }
//        public static float DpToPixels(Context context, float valueInDp)
//        {
//            DisplayMetrics metrics = context.Resources.DisplayMetrics;
//            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
//        }
//    }

//}
