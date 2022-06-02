using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class MarkerController : IController<Marker>
    {
        private Model Model { get; set; }

        public MarkerController(Model model)
        {
            this.Model = model;
        }

        public void Create(Marker value) { 
            var list = Model.CurentProject.Markers;
            list.Add(value);
            Model.Save();
        }

        public void Update(Marker value) {
            var project = Model.CurentProject;
            var index = project.Markers.FindIndex(x=>x.UniqleID == value.UniqleID);
            project.Markers[index] = value;
            Model.Save();
        }

        public void Delete(Marker value) {
            var project = Model.CurentProject;
            project.Markers.RemoveAll(x => x.UniqleID == value.UniqleID);
            Model.Save();
        }
    }
}
