namespace ProjectManager.Models
{
    public class Column
    {
        public string Name { get; private set; }

        public Column(string Name)
        {
            this.Name = Name;
        }
    }
}
