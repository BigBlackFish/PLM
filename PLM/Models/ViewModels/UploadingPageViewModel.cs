using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PLM.Models.ViewModels
{
    public class UploadingPageViewModel : ModelBase
    {
        private bool emptyState;
        private int groupCount;
        private int fileCount;

        public ObservableCollection<FileGroupViewModel> Files { get; set; }

        public bool EmptyState
        {
            get => emptyState;
            set
            {
                emptyState = value;
                OnPropertyChanged(nameof(EmptyState));
            }
        }

        public int GroupCount
        {
            get => groupCount;
            set
            {
                groupCount = value;
                OnPropertyChanged(nameof(GroupCount));
            }
        }

        public int FileCount
        {
            get => fileCount;
            set
            {
                fileCount = value;
                OnPropertyChanged(nameof(FileCount));
            }
        }

        public override void InitializeVariable()
        {
            Files = new ObservableCollection<FileGroupViewModel>();
            EmptyState = true;
            Files.CollectionChanged += Files_CollectionChanged;
        }

        private void Files_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            EmptyState = Files.Count == 0;
            GroupCount = Files.Count;
            int sum = 0;
            foreach (FileGroupViewModel item in Files)
            {
                sum += item.FileViews.Count;
            }
            FileCount = sum;
        }
    }
}
