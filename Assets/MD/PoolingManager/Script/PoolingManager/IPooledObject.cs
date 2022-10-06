namespace MD.ObjectPooling
{
    public interface IPooledObject
    {
        public bool IsUsing { get; set; }
        public void Reset();
        public void Init();
    }
}