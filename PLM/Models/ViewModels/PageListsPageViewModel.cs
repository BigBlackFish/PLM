

using System.Collections.ObjectModel;

namespace PLM.Models.ViewModels
{
    public class PageListsPageViewModel : ModelBase
    {

        private bool emptyState;
        private string fileName;
        private string createStartTime;
        private string createEndTime;
        private string createNickName;
        private int numberofPages;
        private int selectPage;
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

        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        public string CreateStartTime
        {
            get => createStartTime;
            set
            {
                createStartTime = value;
                OnPropertyChanged(nameof(CreateStartTime));
            }
        }

        public string CreateEndTime
        {
            get => createEndTime;
            set
            {
                createEndTime = value;
                OnPropertyChanged(nameof(CreateEndTime));
            }
        }


        public string CreateNickName
        {
            get => createNickName;
            set
            {
                createNickName = value;
                OnPropertyChanged(nameof(CreateNickName));
            }
        }

        public int NumberofPages
        {
            get => numberofPages;
            set
            {
                numberofPages = value;
                OnPropertyChanged(nameof(NumberofPages));
            }
        }

        public int SelectPage
        {
            get => selectPage;
            set
            {
                selectPage = value;
                OnPropertyChanged(nameof(selectPage));
            }
        }
        public override void InitializeVariable()
        {
            EmptyState = true;
            fileName = string.Empty;
            SelectPage = 1;
            createStartTime =string.Empty;
            createEndTime=string.Empty;
            createNickName=string.Empty;
            Files = new ObservableCollection<PageFileListViewModel>();
            numberofPages = 0;
        }
    }
}
