
using System.Windows.Input;

namespace ViewModels
{
    public class TaskItemViewModel : BaseViewModel
    {
        private double _progress;
        private string _statusText = "";
        private bool _isCancelEnabled = true;

        private CancellationTokenSource _cts;

        public int TaskId { get; }

        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                OnPropertyChanged();
            }
        }

        public bool IsCancelEnabled
        {
            get { return _isCancelEnabled; }
            set
            {
                _isCancelEnabled = value;
                OnPropertyChanged();
            }
        }

        public ICommand CancelCommand { get; }

        public TaskItemViewModel(int id)
        {
            TaskId = id;

            _cts = new CancellationTokenSource();

            StatusText = "Running...";

            CancelCommand = new Command(CancelTask);
        }

        private void CancelTask()
        {
            if (IsCancelEnabled)
            {
                _cts.Cancel();
                IsCancelEnabled = false;
            }
        }

        public async Task<bool> RunAsync()
        {
            try
            {
                for (int i = 1; i <= 100; i++)
                {
                    if (_cts.IsCancellationRequested)
                    {
                        StatusText = "Cancelled";
                        return false;
                    }

                    await Task.Delay(50);

                    Progress = i / 100.0;
                }

                StatusText = "Done!";

                return true;
            }
            finally
            {
                IsCancelEnabled = false;
                _cts.Dispose();
            }
        }
    }
}
