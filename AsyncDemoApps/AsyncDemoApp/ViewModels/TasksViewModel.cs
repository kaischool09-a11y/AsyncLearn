
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        private int _taskCounter = 0;
        private int _completedTasksCount = 0;

        public ObservableCollection<TaskItemViewModel> Tasks { get; set; }

        public int CompletedTasksCount
        {
            get { return _completedTasksCount; }
            set
            {
                _completedTasksCount = value;
                OnPropertyChanged();
            }
        }

        public ICommand StartTaskCommand { get; }
        public ICommand ClearAllCommand { get; }

        public TasksViewModel()
        {
            Title = "Async Tasks";

            Tasks = new ObservableCollection<TaskItemViewModel>();

            StartTaskCommand = new Command(async () => await StartNewTaskAsync());

            ClearAllCommand = new Command(async () => await ClearAllTasksAsync());
        }

        private async Task StartNewTaskAsync()
        {
            _taskCounter++;

            TaskItemViewModel newTask = new TaskItemViewModel(_taskCounter);

            Tasks.Add(newTask);

            bool finished = await newTask.RunAsync();

            if (finished)
            {
                CompletedTasksCount++;
            }
        }

        private async Task ClearAllTasksAsync()
        {
            bool answer = await ShowConfirmAsync(
                "Clear tasks",
                "Are you sure you want to clear all tasks?",
                "Yes",
                "No");

            if (answer)
            {
                Tasks.Clear();
                CompletedTasksCount = 0;
            }
        }
    }
}
