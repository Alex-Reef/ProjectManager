using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class ArchiveController : IController<Task>
    {
        private Model Model { get; set; }

        public ArchiveController(Model Model) {
            this.Model = Model;
        }

        public void Create(Task task) {
            Model.CurentProject.DeletedTasks.Add(task);
            Model.Save();
        }

        public void Delete(Task task) {
            Model.CurentProject.DeletedTasks.RemoveAll(x => x.UniqleID == task.UniqleID);
            Model.Save();
        }

        public void Update(Task task) {
            int index = Model.CurentProject.DeletedTasks.FindIndex(x => x.UniqleID == task.UniqleID);
            Model.CurentProject.DeletedTasks[index] = task;
            Model.Save();
        }
    }
}
