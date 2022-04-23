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

        public void Create(Marker _marker) { 
            Marker marker = _marker;
        }

        public void Update(Marker value) {
            var project = Model.GetCurentProject();
            var index = project.Markers.FindIndex(x=>x.UniqleID == value.UniqleID);
            project.Markers[index] = value;
            Model.Save();
        }

        public void Delete(Marker value) {
            var project = Model.GetCurentProject();
            project.Markers.RemoveAll(x => x.UniqleID == value.UniqleID);
            Model.Save();
        }
    }
}
