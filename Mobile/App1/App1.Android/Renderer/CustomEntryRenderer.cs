﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.Graphics.Drawables;
//using Android.OS;
//using Android.Runtime;
//using Android.Util;
//using Android.Views;
//using Android.Widget;
//using App1.Droid.Renderer;
//using App1.Renderer.App1.Entry;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
//namespace App1.Droid.Renderer
//{
//    public class CustomEntryRenderer : EntryRenderer
//    {
//        public CustomEntryRenderer(Context context) : base(context) { }
//        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
//        {
//            base.OnElementChanged(e);
//            if (e.NewElement != null)
//            {
//                if (!(Element is ElipsEntry)) {
//                    return;
//                }
//                var view = (ElipsEntry)Element;
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

