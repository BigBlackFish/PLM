namespace PLM.Models.ViewModels
{
    public class UploadFilePageViewModel : ModelBase
    {
        private string fileLeft;
        private string fileRight;
        private string message;
        private string remark;

        public string FileLeft
        {
            get => fileLeft;
            set
            {
                fileLeft = value;
                OnPropertyChanged(nameof(FileLeft));
            }
        }

        public string FileRight
        {
            get => fileRight;
            set
            {
                fileRight = value;
                OnPropertyChanged(nameof(FileRight));
            }
        }

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public string Remark
        {
            get => remark;
            set
            {
                remark = value;
                OnPropertyChanged(nameof(Remark));
            }
        }

        public override void InitializeVariable()
        {
            FileLeft = string.Empty;
            FileRight = string.Empty;
            Message = string.Empty;
            Remark = string.Empty;
        }
    }
}
