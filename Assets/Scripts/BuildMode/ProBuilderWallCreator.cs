using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ProBuilderWallCreator
{
	public float xSize, ySize, zSize;
	public Vector3 offset;

	#region node1
	Vector3 bottomLeft1;
	Vector3 bottomRight1;
	Vector3 topLeft1;
	Vector3 topRight1;
	#endregion node1

	#region node2
	Vector3 bottomLeft2;
	Vector3 bottomRight2;
	Vector3 topLeft2;
	Vector3 topRight2;
	#endregion node2

	/// <summary>
	/// Create a cube mesh
	/// </summary>
	/// <param name="meshFilter">MeshFilter to edit</param>
	/// <param name="x">Width of the cube</param>
	/// <param name="y">Height of the cube</param>
	/// <param name="z">Length of the cube</param>
	public void Generate(ref ProBuilderMesh meshFilter, float x, float y, float z)
	{
		this.xSize = x;
		this.ySize = y;
		this.zSize = z;

		bottomLeft1 = new Vector3(x * (-0.5f), 0, 0);
		bottomRight1 = new Vector3(x * (0.5f), 0, 0);
		bottomRight2 = new Vector3(x * (0.5f), 0, z);
		bottomLeft2 = new Vector3(x * (-0.5f), 0, z);

		topLeft1 = new Vector3(x * (-0.5f), y, 0);
		topRight1 = new Vector3(x * (0.5f), y, 0);
		topRight2 = new Vector3(x * (0.5f), y, z);
		topLeft2 = new Vector3(x * (-0.5f), y, z);

		List<Vector3> vertices = new List<Vector3>();
		vertices.Add(bottomLeft1);
		vertices.Add(bottomRight1);
		vertices.Add(bottomRight2);
		vertices.Add(bottomLeft2);

		vertices.Add(topLeft1);
		vertices.Add(topRight1);
		vertices.Add(topRight2);
		vertices.Add(topLeft2);

		List<Face> faces = new List<Face>();
		faces.Add(new Face(new int[] { 0, 1, 2, 0, 2, 3,	//bottom quad
									6, 2, 1, 1, 5, 6,		//right quad
									7, 6, 5, 4, 7, 5,		//top quad
									3, 7, 4, 0, 3, 4		//left quad
		}));
		meshFilter.RebuildWithPositionsAndFaces(vertices, faces);
		//ProBuilderMesh.Create(vertices, faces);

		//ProBuilderMesh quad = ProBuilderMesh.Create(
		//	new Vector3[] {
		//		new Vector3(0f, 0f, 0f),
		//		new Vector3(1f, 0f, 0f),
		//		new Vector3(0f, 1f, 0f),
		//		new Vector3(1f, 1f, 0f)
		//	},
		//	new Face[] { new Face(new int[] { 0, 1, 2, 1, 3, 2 } )
		//});


		//meshFilter = ProBuilderMesh.Create();
		//mesh.name = "Generated wall mesh";
		//CreateVertices();
		//CreateTriangles();
		//mesh.RecalculateNormals();
	}
}
