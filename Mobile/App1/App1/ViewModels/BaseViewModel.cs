using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App1.Helpers;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        protected readonly INavigation _navigation;
        private readonly INotifier _notifier;
        bool _isBusy = false;

        public bool IsBusy
        {
            get { return _isBusy; }
            set {
                SetProperty(ref _isBusy, value);
                this.IsNotBusy = !_isBusy;
            }
        }

        private bool _isNotBusy = true;

        public bool IsNotBusy
        {
            get { return _isNotBusy; }
            set { SetProperty(ref _isNotBusy, value); }
        }

        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string _title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        protected BaseViewModel(INavigation navigation, INotifier notifier)
        {
            _navigation = navigation;
            _notifier = notifier;
        }

        public async Task BusyExecute<T>(Func<T> func)
        {
            if (IsBusy)
            {
                return;
            }
            this.IsBusy = true;
            try
            {
                await Task.FromResult(func.Invoke());
            }
            catch (Exception ex)
            {
                ex = ex;
            }
            finally
            {
                this.IsBusy = false;
            }
        }
        public async Task BusyExecute<T>(Func<Task<bool>> func)
        {
            if (IsBusy)
            {
                return;
            }
            this.IsBusy = true;
            try
            {
                await func.Invoke();
            }
            catch (Exception ex)
            {
                ex = ex;
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        public async void Alert(string caption, string message)
        {
            await this.BeginInvokeOnMainThreadAsync(() => {
                _notifier.Notify(caption, message);
            });
        }
        public async Task<bool> Question(string caption, string message, string accept, string cancel)
        {
            return await _notifier.Question(caption, message, accept, cancel);
        }
    }
    public interface INotifier
    {
        void Notify(string caption, string text);
        Task<string> ActionSheet(string caption, params string[] textLines);
        Task<bool> Question(string caption, string text, string accept, string calcel);
    }
}
