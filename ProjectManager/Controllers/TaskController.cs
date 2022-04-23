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

        public void Move(Task task, int Column) {
            var list = Model.GetCurentProject().Tasks;
            Delete(task);
            list[Column].Add(task);
            Model.Save();
        }

        public void Create(Task value) {
            var list = Model.GetCurentProject().Tasks;
            list[0].Add(value);
            Model.Save();
        }

        public void Update(Task value) {
            var project = Model.GetCurentProject();
            var list = project.Tasks;
            foreach (var column in list) {
                var index = column.FindIndex(x=>x.UniqleID == value.UniqleID);
                if (index >= 0)
                    column[index] = value;
            }
            project.Tasks = list;
            Model.Save();
        }

        public void Delete(Task value) {
            var list = Model.GetCurentProject().Tasks;
            foreach (var column in list)
                column.RemoveAll(x => x.UniqleID == value.UniqleID);

            var project = Model.GetCurentProject();
            project.Tasks = list;
            Model.Save();
        }
    }
}
