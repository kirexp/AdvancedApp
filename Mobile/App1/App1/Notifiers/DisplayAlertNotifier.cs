using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App1.ViewModels;
using Xamarin.Forms;

namespace App1.Notifiers
{
    public class DisplayAlertNotifier : INotifier
    {
        private readonly Page _page;

        public DisplayAlertNotifier(Page page)
        {
            _page = page;
        }
        public void Notify(string caption, string text)
        {
            _page.DisplayAlert(caption, text, "OK");
        }

        public async Task<string> ActionSheet(string caption, string[] list)
        {
            var result = await _page.DisplayActionSheet(caption, "Отмена", null, list);
            return result;
        }

        public async Task<bool> Question(string caption, string text, string accept, string calcel)
        {
            return await _page.DisplayAlert(caption, text, accept, calcel);
        }
    }
}
