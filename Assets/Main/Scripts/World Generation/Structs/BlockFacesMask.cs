using System;

public struct BlockFacesMask
{
	public bool this [BlockMeshSide face]
	{
		get
		{
			if (face == BlockMeshSide.Back)
			{ return back; }
			else if (face == BlockMeshSide.Up)
			{ return up; }
			else if (face == BlockMeshSide.Front)
			{ return front; }
			else if (face == BlockMeshSide.Down)
			{ return down; }
			else if (face == BlockMeshSide.Right)
			{ return right; }
			else if (face == BlockMeshSide.Left)
			{ return left; }

			throw new IndexOutOfRangeException($"Index of {nameof(BlockFacesMask)} out of range");
		}
	}
	public const int length = 6;
	private bool front;
	private bool right;
	private bool back;
	private bool left;
	private bool up;
	private bool down;

	public BlockFacesMask(params BlockMeshSide[] blockFaces)
	{
		front = false;
		right = false;
		back = false;
		left = false;
		up = false;
		down = false;

		for (int i = 0; i < blockFaces.Length; i++)
		{
			if (blockFaces[i] == BlockMeshSide.Front)
			{ front = true; }
			else if (blockFaces[i] == BlockMeshSide.Right)
			{ right = true; }
			else if (blockFaces[i] == BlockMeshSide.Back)
			{ back = true; }
			else if (blockFaces[i] == BlockMeshSide.Left)
			{ left = true; }
			else if (blockFaces[i] == BlockMeshSide.Up)
			{ up = true; }
			else if (blockFaces[i] == BlockMeshSide.Down)
			{ down = true; }
		}
	}

	public readonly bool IsEmpty() => !front && !right && !back && !left && !up && !down;

	public readonly bool IsFull() => front && right && back && left && up && down;

	public void AddFace(BlockMeshSide face)
	{
		if (face == BlockMeshSide.Front)
		{ front = true; }
		else if (face == BlockMeshSide.Right)
		{ right = true; }
		else if (face == BlockMeshSide.Back)
		{ back = true; }
		else if (face == BlockMeshSide.Left)
		{ left = true; }
		else if (face == BlockMeshSide.Up)
		{ up = true; }
		else if (face == BlockMeshSide.Down)
		{ down = true; }
	}

	public bool Contains(BlockMeshSide face)
	{
		if (face == BlockMeshSide.Front)
		{ front = true; }
		else if (face == BlockMeshSide.Right)
		{ right = true; }
		else if (face == BlockMeshSide.Back)
		{ back = true; }
		else if (face == BlockMeshSide.Left)
		{ left = true; }
		else if (face == BlockMeshSide.Up)
		{ up = true; }
		else if (face == BlockMeshSide.Down)
		{ down = true; }

		return false;
	}
}
