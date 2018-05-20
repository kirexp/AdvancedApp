using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.Renderer
{
    public class WhiteLinedEntry : Xamarin.Forms.Entry
    {

    }
        //    <entry:ElipsEntry Grid.Row="1" CornerRadius= "0"
        //                   IsCurvedCornersEnabled= "True"
        //                   BorderColor= "White"
        //                   HorizontalTextAlignment= "Center"
        //                   FontSize= "17"
        //                   HeightRequest= "40"
        //                   Placeholder= "Логин"
        //                   PlaceholderColor= "White"
        //                   TextColor= "White"
        //                   FontAttributes= "Bold"
        //                   WidthRequest= "100"
        //                   Text= "{Binding UserName.Value, Mode=TwoWay}" >
        //    < Entry.Behaviors >
        //        < behaviors:EventToCommandBehavior EventName = "TextChanged" Command= "{Binding ValidateUserNameCommand}" />
        //    </ Entry.Behaviors >
        //    < Entry.Triggers >
        //        < DataTrigger
        //            TargetType= "Entry"
        //            Binding= "{Binding UserName.IsValid}"
        //            Value= "False" >
        //            < Setter Property= "behaviors:LineColorBehavior.LineColor" Value= "White" />
        //        </ DataTrigger >
        //    </ Entry.Triggers >
        //</ entry:ElipsEntry>
}
