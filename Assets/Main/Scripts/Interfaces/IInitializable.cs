public interface IInitializable
{
	void Initialize();
}

public interface IInitializable<T>
{
	void Initialize(params T[] parameters);
}
public interface IInitializable<inType, OutType>
{
	OutType Initialize(params inType[] parameters);
}
