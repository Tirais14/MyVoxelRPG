public interface IContainerItem
{
	Item Item { get; }

	int Count { get; }

	ContainerItemTemp GetContainerItemTemp();

	void EnableRaycastTarget();

	void DisableRaycastTarget();

	void EnableImage();

	void DisableImage();

	void Show();

	void Hide();

}
