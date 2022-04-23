using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class TaskController : IController<Task>
    {
        private Model Model { get; set; }
        public TaskController(Model model)
        {
            this.Model = model;
        }

        public void Create() { 
            Task task = new Task();
        }

        public void Update(Task value) { 
        
        }

        public void Delete(Task value) { 
        
        }
    }
}
