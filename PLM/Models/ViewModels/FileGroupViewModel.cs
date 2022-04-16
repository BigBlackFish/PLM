using System.Collections.ObjectModel;

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
            set => isSelect = value;
        }

        public ObservableCollection<FileViewModel> FileViews { get; set; }

        public override void InitializeVariable()
        {
            IsSelect = false;
            FileViews = new ObservableCollection<FileViewModel>();
        }
    }
}
