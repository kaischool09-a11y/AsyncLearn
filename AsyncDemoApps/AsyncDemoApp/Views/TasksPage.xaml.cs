
using ViewModels;

namespace Views
{
    public partial class TasksPage : ContentPage
    {
        public TasksPage() : this(new TasksViewModel())
        {
        }

        public TasksPage(TasksViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}
