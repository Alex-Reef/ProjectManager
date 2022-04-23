using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class ProjectController : IController<Project>
    {
        private Model Model { get; set; }
        public ProjectController(Model model)
        {
            this.Model = model;
        }

        public void Create() { 
            Project project = new Project();
        }

        public void Update(Project value) {
            
        }
        public void Delete(Project value) {
            
        }
    }
}
