using System;
using System.Collections;
using System.Collections.Generic;

public struct BlockMeshSideList : IList<BlockMeshSide?>
{
	private BlockMeshSide? back;
	private BlockMeshSide? up;
	private BlockMeshSide? front;
	private BlockMeshSide? down;
	private BlockMeshSide? right;
	private BlockMeshSide? left;
	private const string OutOfRangeExceptionMessage = "Index of BlockMeshSideList must be equal or more than 0 and less than 6";
	public const int Size = 6;

	public BlockMeshSide? this[int index]
	{
		readonly get
		{
			return index switch
			{
				0 => back,
				1 => up,
				2 => front,
				3 => down,
				4 => right,
				5 => left,
				_ => throw new IndexOutOfRangeException(OutOfRangeExceptionMessage)
			};
		}
		set
		{
			if (index >= 0 && index < Size)
			{
				switch (index)
				{
					case 0:
						if (back == null)
						{ count++; }
						back = value;
						break;
					case 1:
						if (up == null)
						{ count++; }
						up = value;
						break;
					case 2:
						if (front == null)
						{ count++; }
						front = value;
						break;
					case 3:
						if (down == null)
						{ count++; }
						down = value;
						break;
					case 4:
						if (right == null)
						{ count++; }
						right = value;
						break;
					case 5:
						if (left == null)
						{ count++; }
						left = value;
						break;
				}
			}
			else
			{ throw new IndexOutOfRangeException(OutOfRangeExceptionMessage); }
		}
	}

	public readonly bool IsFixedSize => true;

	public readonly bool IsReadOnly => false;
	private int count;

	public readonly int Count => count;

	public readonly bool IsSynchronized => false;

	/// <summary>!Not Supported!</summary>
	public readonly object SyncRoot => throw new NotSupportedException();

	/// <summary>!Not Supported!</summary>
	public void Add(BlockMeshSide? item)
	{
		for (int i = 0; i < Size; i++)
		{
			if (this[i] == null)
			{ 
				this[i] = item;
				count++;
				return;
			}
		}

		throw new Exception("There is no free space to add");
	}

	public void Clear()
	{
		for (int i = 0; i < Size; i++)
		{ this[i] = null; }
	}

	public readonly bool Contains(BlockMeshSide? item)
	{
		for (int i = 0; i < Size; i++)
		{
			if (this[i] == item)
			{ return true; }
		}

		return false;
	}

	public readonly BlockMeshSide[] ToArray()
	{
		if (count == 0)
		{ return null; }

		BlockMeshSide[] blockMeshSides = new BlockMeshSide[Count];
		int addedCount = 0;
		for (int i = 0; i < Size; i++)
		{
			if (this[i] != null)
			{
				blockMeshSides[addedCount] = (BlockMeshSide)this[i];
				addedCount++;
			}
		}

		return blockMeshSides;
	}

	/// <summary>!Not Supported!</summary>
	public readonly void CopyTo(BlockMeshSide?[] array, int arrayIndex) => throw new NotSupportedException();

	public readonly int IndexOf(BlockMeshSide? item)
	{
		if (item == null)
		{  throw new ArgumentNullException(nameof(item)); }

		for (int i = 0; i < Size; i++)
		{
			if (this[i] == item)
			{ return i; }
		}

		return -1;
	}

	/// <summary>!Not Supported!</summary>
	public readonly void Insert(int index, BlockMeshSide? item) => throw new NotSupportedException();

	public bool Remove(BlockMeshSide? item)
	{
		if (item == null)
		{ throw new ArgumentNullException(nameof(item)); }

		for (int i = 0; i < Size; i++)
		{
			if (this[i] == item)
			{
				this[i] = null;
				count--;
				return true;
			}
		}

		return false;
	}

	public void RemoveAt(int index)
	{
		if (index < 0 || index >= Size)
		{ throw new IndexOutOfRangeException(OutOfRangeExceptionMessage); }
		else if (this[index] == null)
		{ throw new NullReferenceException($"{nameof(BlockMeshSide)} by index {index} is null"); }

		this[index] = null;
		count--;
	}

	public readonly IEnumerator<BlockMeshSide?> GetEnumerator() => new Enumerator(this);

	readonly IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);


	public struct Enumerator : IEnumerator<BlockMeshSide?>
	{
		private BlockMeshSideList blockMeshSides;
		private BlockMeshSide? blockMeshSide;
		private int position;

		public Enumerator(BlockMeshSideList blockMeshSides)
		{
			this.blockMeshSides = blockMeshSides;
			blockMeshSide = null;
			position = -1;
		}

		public readonly BlockMeshSide? Current => blockMeshSide;

		readonly object IEnumerator.Current => Current;

		public readonly void Dispose() { }

		public bool MoveNext()
		{
			if (++position >= Size)
			{ return false; }

			if (blockMeshSides[position] == null)
			{
				do
				{
					position++;
				} while (blockMeshSides[position] == null && position < Size);

				if (position >= Size)
				{ return false; }
			}

			blockMeshSide = blockMeshSides[position];
			return true;
		}

		public void Reset()
		{
			blockMeshSide = null;
			position = -1;
		}
	}
}