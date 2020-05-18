using UnityEngine;

//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshCubeBuilder //: MonoBehaviour
{

	public float xSize, ySize, zSize;
	int xInt, yInt, zInt;
	bool xLessThanTwo, yLessThanTwo, zLessThanTwo;
	Vector3 offset;

	private Mesh mesh;
	private Vector3[] vertices;

	//private void Awake()
	//{
	//	//Generate();
	//}

	//private void Generate()
	//{
	//	GetComponent<MeshFilter>().mesh = mesh = new Mesh();
	//	mesh.name = "Procedural Cube";
	//	CreateVertices();
	//	CreateTriangles();
	//	mesh.RecalculateNormals();
	//}

	public void Generate(ref MeshFilter meshFilter, float x, float y, float z)
	{
		this.xSize = x;
		this.ySize = y;
		this.zSize = z;
		xInt = Mathf.CeilToInt(xSize);
		yInt = Mathf.CeilToInt(ySize);
		zInt = Mathf.CeilToInt(zSize);

		offset = new Vector3(-xSize * 0.5f, 0, 0);

		if (xInt < 2)
		{
			xInt = 2;
			xLessThanTwo = true;
		}
		else
			xLessThanTwo = false;
		if (yInt < 2)
		{
			yInt = 2;
			yLessThanTwo = true;
		}
		else
		{
			yLessThanTwo = false;
		}
		if (zInt < 2)
		{
			zInt = 2;
			zLessThanTwo = true;
		}
		else
			zLessThanTwo = false;

		meshFilter.mesh = mesh = new Mesh();
		mesh.name = "Generated wall mesh test";
		CreateVertices();
		CreateTriangles();
		mesh.RecalculateNormals();
	}

	private void CreateVertices()
	{
		int cornerVertices = 8;
		int edgeVertices = (xInt + yInt + zInt - 3) * 4;
		int faceVertices = (
			(xInt - 1) * (yInt - 1) +
			(xInt - 1) * (zInt - 1) +
			(yInt - 1) * (zInt - 1)) * 2;
		vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];

		int v = 0;
		for (int y = 0; y <= yInt; y++)
		{
			float yTemp = y;
			if (y == yInt)
				yTemp = ySize;
			if (yLessThanTwo)
			{
				//y is less than 2, add 3 vertices, at origin, middle and ySize
				switch (y)
				{
					case 0:
						yTemp = 0;
						break;
					case 1:
						yTemp = ySize * 0.5f;
						break;
					case 2:
						yTemp = ySize;
						break;
				}
			}


			if (!xLessThanTwo)
			{
				for (int x = 0; x <= xInt; x++)
				{
					float xTemp = x;
					if (x == xInt)
						xTemp = xSize;
					vertices[v++] = new Vector3(xTemp + offset.x, yTemp + offset.y, 0 + offset.z);
				}
			}
			else
			{
				//x is less than 2, add 3 vertices, at origin, middle and xSize
				vertices[v++] = new Vector3(0 + offset.x, yTemp + offset.y, 0 + offset.z);
				vertices[v++] = new Vector3(xSize * 0.5f + offset.x, yTemp + offset.y, 0 + offset.z);
				vertices[v++] = new Vector3(xSize + offset.x, yTemp + offset.y, 0 + offset.z);
			}

			if (!zLessThanTwo)
			{
				for (int z = 1; z <= zInt; z++)
				{
					float zTemp = z;
					if (z == zInt)
						zTemp = zSize;
					vertices[v++] = new Vector3(xSize + offset.x, yTemp + offset.y, zTemp + offset.z);
				}
			}
			else
			{
				//z is less than 2, add 3 vertices, at origin, middle and zSize
				//vertices[v++] = new Vector3(xSize, yTemp, 0); //the first one was already added
				vertices[v++] = new Vector3(xSize + offset.x, yTemp + offset.y, zSize * 0.5f + offset.z);
				vertices[v++] = new Vector3(xSize + offset.x, yTemp + offset.y, zSize + offset.z);
			}

			if (!xLessThanTwo)
			{
				for (int x = xInt - 1; x >= 0; x--)
				{
					float xTemp = x;
					if (x == xInt)
						xTemp = xSize;
					vertices[v++] = new Vector3(xTemp + offset.x, yTemp + offset.y, zSize + offset.z);
				}
			}
			else
			{
				//x is less than 2, add 3 vertices, at origin, middle and xSize
				//vertices[v++] = new Vector3(xSize, yTemp, zSize);
				vertices[v++] = new Vector3(xSize * 0.5f + offset.x, yTemp + offset.y, zSize + offset.z);
				vertices[v++] = new Vector3(0 + offset.x, yTemp + offset.y, zSize + offset.z);
			}

			if (!zLessThanTwo)
			{
				for (int z = zInt - 1; z > 0; z--)
				{
					float zTemp = z;
					if (z == zInt)
						zTemp = zSize;
					vertices[v++] = new Vector3(0 + offset.x, yTemp + offset.y, zTemp + offset.z);
				}
			}
			else
			{
				//z is less than 2, add 3 vertices, at origin, middle and zSize
				//vertices[v++] = new Vector3(0, yTemp, zSize);
				vertices[v++] = new Vector3(0 + offset.x, yTemp + offset.y, zSize * 0.5f + offset.z);
				//vertices[v++] = new Vector3(0, yTemp, 0);
			}
		}

		//top vertices
		for (int z = 1; z < zInt; z++)
		{
			float zTemp = z;
			if (zLessThanTwo)
			{
				zTemp = zSize * 0.5f;
				Debug.Log("There should be only one middle point");
			}
			for (int x = 1; x < xInt; x++)
			{
				float xTemp = x;
				if (xLessThanTwo)
					xTemp = xSize * 0.5f;
				vertices[v++] = new Vector3(xTemp + offset.x, ySize + offset.y, zTemp + offset.z);
			}
		}

		//bottom vertices
		for (int z = 1; z < zInt; z++)
		{
			float zTemp = z;
			if (zLessThanTwo)
			{
				zTemp = zSize * 0.5f;
				Debug.Log("There should be only one middle point");
			}
			for (int x = 1; x < xInt; x++)
			{
				float xTemp = x;
				if (xLessThanTwo)
					xTemp = xSize * 0.5f;
				vertices[v++] = new Vector3(xTemp + offset.x, 0 + offset.y, zTemp + offset.z);
			}
		}

		mesh.vertices = vertices;
	}

	private void CreateTriangles()
	{
		int quads = (xInt * yInt + xInt * zInt + yInt * zInt) * 2;
		int[] triangles = new int[quads * 6];
		int ring = (xInt + zInt) * 2;
		int t = 0, v = 0;

		for (int y = 0; y < yInt; y++, v++)
		{
			for (int q = 0; q < ring - 1; q++, v++)
			{
				t = SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
			}
			t = SetQuad(triangles, t, v, v - ring + 1, v + ring, v + 1);
		}

		t = CreateTopFace(triangles, t, ring);
		t = CreateBottomFace(triangles, t, ring);
		mesh.triangles = triangles;
	}

	private int CreateTopFace(int[] triangles, int t, int ring)
	{
		//TO DO: error for x size 1
		int v = ring * yInt;
		for (int x = 0; x < xInt - 1; x++, v++)
		{
			t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + ring);
		}
		t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + 2);

		int vMin = ring * (yInt + 1) - 1;
		int vMid = vMin + 1;
		int vMax = v + 2;

		for (int z = 1; z < zInt - 1; z++, vMin--, vMid++, vMax++)
		{
			t = SetQuad(triangles, t, vMin, vMid, vMin - 1, vMid + xInt - 1);
			for (int x = 1; x < xInt - 1; x++, vMid++)
			{
				t = SetQuad(
					triangles, t,
					vMid, vMid + 1, vMid + xInt - 1, vMid + xInt);
			}
			t = SetQuad(triangles, t, vMid, vMax, vMid + xInt- 1, vMax + 1);
		}

		int vTop = vMin - 2;
		t = SetQuad(triangles, t, vMin, vMid, vTop + 1, vTop);
		for (int x = 1; x < xInt - 1; x++, vTop--, vMid++)
		{
			t = SetQuad(triangles, t, vMid, vMid + 1, vTop, vTop - 1);
		}
		t = SetQuad(triangles, t, vMid, vTop - 2, vTop, vTop - 1);

		return t;
	}

	private int CreateBottomFace(int[] triangles, int t, int ring)
	{
		int v = 1;
		int vMid = vertices.Length - (xInt - 1) * (zInt- 1);
		t = SetQuad(triangles, t, ring - 1, vMid, 0, 1);
		for (int x = 1; x < xInt - 1; x++, v++, vMid++)
		{
			t = SetQuad(triangles, t, vMid, vMid + 1, v, v + 1);
		}
		t = SetQuad(triangles, t, vMid, v + 2, v, v + 1);

		int vMin = ring - 2;
		vMid -= xInt - 2;
		int vMax = v + 2;

		for (int z = 1; z < zInt - 1; z++, vMin--, vMid++, vMax++)
		{
			t = SetQuad(triangles, t, vMin, vMid + xInt- 1, vMin + 1, vMid);
			for (int x = 1; x < xInt - 1; x++, vMid++)
			{
				t = SetQuad(
					triangles, t,
					vMid + xInt - 1, vMid + xInt, vMid, vMid + 1);
			}
			t = SetQuad(triangles, t, vMid + xInt - 1, vMax + 1, vMid, vMax);
		}

		int vTop = vMin - 1;
		t = SetQuad(triangles, t, vTop + 1, vTop, vTop + 2, vMid);
		for (int x = 1; x < xInt - 1; x++, vTop--, vMid++)
		{
			t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vMid + 1);
		}
		t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vTop - 2);

		return t;
	}

	private static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
	{
		triangles[i] = v00;
		triangles[i + 1] = triangles[i + 4] = v01;
		triangles[i + 2] = triangles[i + 3] = v10;
		triangles[i + 5] = v11;
		return i + 6;
	}

	//private void OnDrawGizmos()
	//{
	//	if (vertices == null)
	//	{
	//		return;
	//	}
	//	Gizmos.color = Color.black;
	//	for (int i = 0; i < vertices.Length; i++)
	//	{
	//		Gizmos.DrawSphere(vertices[i], 0.1f);
	//	}
	//}
}