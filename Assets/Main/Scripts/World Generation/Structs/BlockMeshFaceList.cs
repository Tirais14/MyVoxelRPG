using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMeshFaceList
{
	private List<BlockMeshFace> faces;
	public BlockMeshFace this[int index] => faces[index];

	public void AddBlockMeshFace(BlockMeshFace face) => faces.Add(face);

	public void Clear() => faces.Clear();
}
