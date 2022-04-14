using System.ComponentModel;

namespace PLM.Models.ViewModels
{
    public abstract class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected ModelBase()
        {
            InitializeVariable();
        }

        public abstract void InitializeVariable();

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
