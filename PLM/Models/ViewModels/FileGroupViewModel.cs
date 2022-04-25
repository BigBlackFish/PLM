using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PLM.Models.ViewModels
{
    public class FileGroupViewModel : ModelBase
    {
        private bool isSelect;
        private bool transferComplete;
        private bool isTransfer;

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

        public string Message { get; set; }

        public string Remark { get; set; }

        public FileViewModel SourceFile { get; set; }

        public FileViewModel SummaryFile { get; set; }

        public bool TransferComplete
        {
            get => transferComplete;
            set
            {
                transferComplete = value;
                OnPropertyChanged(nameof(TransferComplete));
            }
        }

        /// <summary>
        /// 是否在传输过程中
        /// </summary>
        public bool IsTransfer
        {
            get => isTransfer;
            set
            {
                isTransfer = value;
                OnPropertyChanged(nameof(IsTransfer));
            }
        }

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
                else
                {
                    (e.NewItems[0] as FileViewModel).FileDownload();
                }
                IsTransfer = true;
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                (e.OldItems[0] as FileViewModel).CancelTransmission();
                IsTransfer = false;
            }
        }
    }
}
