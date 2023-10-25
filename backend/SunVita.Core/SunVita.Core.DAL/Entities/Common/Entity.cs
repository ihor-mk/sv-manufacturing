namespace SunVita.Core.DAL.Entities.Common
{
    public abstract class Entity<T> where T : struct
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
