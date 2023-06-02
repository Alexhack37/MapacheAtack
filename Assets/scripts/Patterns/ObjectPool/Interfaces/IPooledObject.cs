///////////////////////////////////////////
/// Version copiada de clase
/// ////////////////////////////////////////
namespace Patterns.ObjectPool.Interfaces
{
    public interface IPooledObject
    {
        public bool Active
        {
            get;
            set;
        }

        public IPooledObject Clone();
        public void Reset();
    }
}