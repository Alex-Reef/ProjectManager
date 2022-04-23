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

        public void Create() { 
            Marker marker = new Marker();
        }

        public void Update(Marker value) { 
        
        }

        public void Delete(Marker value) { 
            
        }
    }
}
