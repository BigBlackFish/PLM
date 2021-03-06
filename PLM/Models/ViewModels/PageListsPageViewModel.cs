

using System;
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
        private string id;
        private string totalcounts;
        private string timequantum;
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


        public string TimeQuantum
        {
            get => timequantum;
            set
            {
                timequantum = value;
                OnPropertyChanged(nameof(TimeQuantum));
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
                OnPropertyChanged(nameof(SelectPage));
            }
        }

        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Totalcounts
        {
            get => totalcounts;
            set
            {
                totalcounts = value;
                OnPropertyChanged(nameof(Totalcounts));
            }
        }
        public override void InitializeVariable()
        {
            EmptyState = true;
            fileName = string.Empty;
            SelectPage = 1;
            createStartTime = string.Empty;
            createEndTime = string.Empty;
            createNickName = string.Empty;
            timequantum = string.Empty;
            Files = new ObservableCollection<PageFileListViewModel>();
            numberofPages = 0;
            totalcounts = "0";
        }
    }
}
