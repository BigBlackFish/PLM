

using System.Collections.ObjectModel;

namespace PLM.Models.ViewModels
{
    public class PageListsPageViewModel : ModelBase
    {

        private bool emptyState;
        public ObservableCollection<PageFileListViewModel> Files { get; set; }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool EmptyState
        {
            get => emptyState;
            set
            {
                emptyState = value;
                OnPropertyChanged(nameof(EmptyState));
            }
        }
        public override void InitializeVariable()
        {
            EmptyState = true;
            Files = new ObservableCollection<PageFileListViewModel>();
        }
    }
}
