///////////////////////////////////////////
/// Version copiada de clase
/// ////////////////////////////////////////
namespace Patterns.ObjectPool.Interfaces
{
    public interface IPool
    {
        public IPooledObject Get();
        public void Release(IPooledObject obj);
    }
}
