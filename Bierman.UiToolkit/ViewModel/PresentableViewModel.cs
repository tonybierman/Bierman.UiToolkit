using Bierman.UiToolkit.Command;
using Bierman.UiToolkit.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bierman.UiToolkit.ViewModel
{
    public abstract class PresentableViewModel : VerifiableViewModel, ICloseable
    {
        private readonly string formName;
        private ICommand? _parentViewLoadedCommand;
        private ICommand? _dialogViewLoadedCommand;

        private string? _heading;
        private string? _description;
        private string? _image;

        public string? Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        public string? Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }


        public string? Heading
        {
            get
            {
                return _heading;
            }
            set
            {
                _heading = value;
                OnPropertyChanged("Heading");
            }
        }

        public ICommand ParentViewLoadedCommand
        {
            get
            {
                if (_parentViewLoadedCommand == null)
                    _parentViewLoadedCommand = new RelayCommand<object>((a) => ParentViewLoadedExecute(a));

                return _parentViewLoadedCommand;
            }
        }

        public ICommand DialogViewLoadedCommand
        {
            get
            {
                if (_dialogViewLoadedCommand == null)
                    _dialogViewLoadedCommand = new RelayCommand<object>((a) => DialogViewLoadedExecute(a));

                return _dialogViewLoadedCommand;
            }
        }

        /// <summary>
        /// Gets the name of the form.
        /// </summary>
        public string FormName
        {
            get
            {
                return formName;
            }
        }

        protected PresentableViewModel(string formName)
        {
            this.formName = formName;
            PropertyChangedCompleted(string.Empty);
        }

        protected override void PropertyChangedCompleted(string propertyName)
        {
            // test prevent infinite loop while settings IsValid 
            // (which causes an PropertyChanged to be raised)
            if (propertyName != "IsValid")
            {
                // update the isValid status
                if (string.IsNullOrEmpty(Error) && ValidPropertiesCount == TotalPropertiesWithValidationCount)
                {
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                }
            }
        }

        //protected virtual void ParentViewLoadedExecute(object obj)
        //{
        //    if (obj == null)
        //        throw new ArgumentNullException();

        //    UserControl uc = obj as UserControl;
        //    if (uc == null)
        //        throw new NullReferenceException();

        //    ICloseable closer = this as ICloseable;
        //    if (closer != null)
        //    {
        //        Window parentWindow = Window.GetWindow(uc);
        //        parentWindow.DataContext = closer;
        //        parentWindow.Title = closer.WindowTitle;
        //        parentWindow.UpdateLayout();
        //        closer.CloseRequest += (s, e) =>
        //        {
        //            parentWindow.Close();
        //        };
        //    }
        //}

        protected virtual void ParentViewLoadedExecute(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException();

            UserControl uc = obj as UserControl;
            if (uc == null)
                throw new NullReferenceException();

            ICloseable closer = this as ICloseable;
            if (closer != null)
            {
                Window parentWindow = Window.GetWindow(uc);
                parentWindow.Title = closer.WindowTitle;
                closer.CloseRequest += (s, e) =>
                {
                    parentWindow.DataContext = closer;
                    parentWindow.DialogResult = true;
                    parentWindow.Close();
                };
                closer.CancelRequest += (s, e) =>
                {
                    parentWindow.DataContext = closer;
                    parentWindow.DialogResult = false;
                    parentWindow.Close();
                };
            }
        }

        protected virtual void DialogViewLoadedExecute(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException();

            UserControl uc = obj as UserControl;
            if (uc == null)
                throw new NullReferenceException();

            ICloseable closer = this as ICloseable;
            if (closer != null)
            {
                Window parentWindow = Window.GetWindow(uc);
                parentWindow.Title = closer.WindowTitle;
                closer.CloseRequest += (s, e) =>
                {
                    parentWindow.DataContext = closer;
                    parentWindow.DialogResult = true;
                    parentWindow.Close();
                };
                closer.CancelRequest += (s, e) =>
                {
                    parentWindow.DataContext = closer;
                    parentWindow.DialogResult = false;
                    parentWindow.Close();
                };
            }
        }

        #region ICloseable Implementation

        public event EventHandler? CloseRequest;
        protected void RaiseCloseRequest()
        {
            var handler = CloseRequest;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler? CancelRequest;
        protected void RaiseCancelRequest()
        {
            var handler = CancelRequest;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected string _windowTitle;

        public string WindowTitle
        {
            get
            {
                return _windowTitle;
            }
            set
            {
                _windowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }
        #endregion

        #region SaveFormCommand
        protected ICommand? _saveFormCommand;
        public ICommand SaveFormCommand
        {
            get
            {
                if (_saveFormCommand == null)
                    _saveFormCommand = new RelayCommand(SaveFormExecute, SaveFormCanExecute);

                return _saveFormCommand;
            }
        }
        protected virtual bool SaveFormCanExecute()
        {
            return true;
        }
        protected virtual void SaveFormExecute() => RaiseCloseRequest();
        #endregion

        #region CancelFormCommand
        protected ICommand? _cancelFormCommand;
        public ICommand CancelFormCommand
        {
            get
            {
                if (_cancelFormCommand == null)
                    _cancelFormCommand = new RelayCommand(CancelFormExecute, CancelFormCanExecute);

                return _cancelFormCommand;
            }
        }
        protected virtual bool CancelFormCanExecute() => true;
        protected virtual void CancelFormExecute() => RaiseCancelRequest();
        #endregion
    }
}
