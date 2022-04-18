﻿

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
        public override void InitializeVariable()
        {
            EmptyState = true;
            fileName = string.Empty;
            createStartTime=string.Empty;
            createEndTime=string.Empty;
            createNickName=string.Empty;
            Files = new ObservableCollection<PageFileListViewModel>();
        }
    }
}
