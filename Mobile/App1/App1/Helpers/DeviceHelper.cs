using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App1.ViewModels;
using Xamarin.Forms;

namespace App1.Helpers
{
    public static class DeviceHelper
    {
        public static Task BeginInvokeOnMainThreadAsync(this ContentPage page, Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            Device.BeginInvokeOnMainThread(() => {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }); return tcs.Task;
        }
        public static Task BeginInvokeOnMainThreadAsync(this BaseViewModel page, Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            Device.BeginInvokeOnMainThread(() => {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }); return tcs.Task;
        }

        public static Task<T> BeginInvokeOnMainThreadAsync<T>(this BaseViewModel page, Func<T> func)
        {
            var tcs = new TaskCompletionSource<T>();
            Device.BeginInvokeOnMainThread(() => {
                try
                {
                    var result = func();
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }); return tcs.Task;
        }
        public static Task BeginInvokeOnMainThreadAsync(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            Device.BeginInvokeOnMainThread(() => {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }); return tcs.Task;
        }
    }
}
