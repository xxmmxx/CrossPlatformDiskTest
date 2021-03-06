﻿using System.Windows.Input;
using Xamarin.Forms;

namespace Saplin.CPDT.UICore.ViewModels
{
    public abstract class PopupViewModel : BaseViewModel
    {
        private bool isVisible = false;

        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                if (value != isVisible)
                {
                    isVisible = value;
                    RaisePropertyChanged();
                    OnVisibilityChanged(value);
                }
                Count++;
            }
        }

        public int count = 0;

        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                if (value != count)
                {
                    count = value;
                    RaisePropertyChanged();
                }
            }
        }


        private ICommand show;

        public ICommand Show
        {
            get
            {
                if (show == null)
                    show = new Command((object param) => { showData = param;  IsVisible = true; });

                return show;
            }
        }

        public void DoShow(object param)
        {
            Show.Execute(param);
        }

        protected object showData;

        private ICommand close;

        public ICommand Close
        {
            get
            {
                if (close == null)
                    close = new Command(() => { IsVisible = false; });

                return close;
            }
        }

        protected virtual void OnVisibilityChanged(bool visible)
        {

        }

    }
}
