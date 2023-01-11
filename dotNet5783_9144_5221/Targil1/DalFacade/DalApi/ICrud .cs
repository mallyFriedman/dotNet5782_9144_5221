
namespace DalApi
{
    public interface ICrud<T>
    {
        public int Add(T item);
        public void Delete(int id);
        public T Get(int id);
        public IEnumerable<T>? Get(Func<T, bool>? foo = null);

        public T GetSingle(Func<T, bool>? foo );
        public void Update(T item);
    }
}
