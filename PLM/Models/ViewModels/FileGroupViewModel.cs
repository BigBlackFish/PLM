using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PLM.Models.ViewModels
{
    public class FileGroupViewModel : ModelBase
    {
        private bool isSelect;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelect
        {
            get => isSelect;
            set
            {
                isSelect = value;
                OnPropertyChanged(nameof(IsSelect));
            }
        }

        public ObservableCollection<FileViewModel> FileViews { get; set; }

        public bool IsUpload { get; set; } = true;

        public override void InitializeVariable()
        {
            IsSelect = false;
            FileViews = new ObservableCollection<FileViewModel>();
            FileViews.CollectionChanged += FileViews_CollectionChanged;
        }

        private void FileViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (IsUpload)
                {
                    (e.NewItems[0] as FileViewModel).FileUpload();
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                (e.OldItems[0] as FileViewModel).CancelTransmission();
            }
        }
    }
}
