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
            var list = Model.CurentProject.Tasks;
            Delete(task);
            list[Column].Add(task);
            Model.Save();
        }

        public void Create(Task value) {
            var list = Model.CurentProject.Tasks;
            list[0].Add(value);
            Model.Save();
        }

        public void Update(Task value) {
            var project = Model.CurentProject;
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
            var list = Model.CurentProject.Tasks;
            Model.CurentProject.DeletedTasks.Add(value);
            foreach (var column in list)
                column.RemoveAll(x => x.UniqleID == value.UniqleID);

            var project = Model.CurentProject;
            project.Tasks = list;
            Model.Save();
        }

        #region Subtask
        public void Subtask_Create(Subtask subtask, Task task) {
            task.Subtasks.Add(subtask);
            Model.Save();
        }

        public void Subtask_Update(Subtask subtask, Task task) {
            var list = task.Subtasks;
            int index = list.FindIndex(x => x.UniqleID == subtask.UniqleID);
            list[index] = subtask;
            Model.Save();
        }

        public void Subtask_Delete(Subtask subtask, Task task) {
            task.Subtasks.RemoveAll(x => x.UniqleID == subtask.UniqleID);
            Model.Save();
        }
        #endregion
    }
}
