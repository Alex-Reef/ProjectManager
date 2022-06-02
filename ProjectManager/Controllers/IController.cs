namespace ProjectManager.Controllers
{
    public interface IController<T>
    {
        void Create(T value);
        void Update(T value);
        void Delete(T value);
    }
}
