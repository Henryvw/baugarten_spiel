using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
	Mesh mesh;

	Vector3[] points;

	int[] triangles;

	public int xBlocks = 30;
	public int zBlocks = 30;

	public float y;

	[Range(0.1f, 10.0f)]
	public float xOffset = 0.5f;
	[Range(0.1f, 10.0f)]
	public float zOffset = 0.5f;
	[Range(0.1f, 10.0f)]
	public float yOffset = 1.5f;

	public int numberOfTrees;
	public GameObject tree;

	public int numberOfSmallRocks;
	public GameObject smallrock;
	
	public int numberOfLargeRocks;
	public GameObject largerock;

	GameObject terrain;
	Component terrain_mesh;

	// Start is called before the first frame update
	void Start()
	{
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
		CreateTerrainGeometry();
		UpdateMesh();
		GenerateTrees();
		GenerateSmallRocks();
		GenerateLargeRocks();
		//Debug.Log("Start is getting called");

	}

	void FixedUpdate()
	{
		//Debug.Log("FixedUpate is getting called");
		CreateTerrainGeometry();
		UpdateMesh();
		GrabTerrain();

	}

	void CreateTerrainGeometry()
	{
		    // Debug.Log("CreateTerrainGeometry is getting called");
		points = new Vector3[(xBlocks + 1) * (zBlocks + 1)];
		int i = 0;
		for(int z=0; z<= zBlocks; z++)
		{
			for(int x=0; x<= xBlocks; x++)
			{
				y = Mathf.PerlinNoise(x * xOffset, z * zOffset) * yOffset;
				points[i] = new Vector3(x*2, y, z*2);
				i++;
		//		Debug.Log("innermost loop of CreateTerrainGeometry z and x blocks points thing is getting called");

			}

		}

		triangles = new int[xBlocks * zBlocks * 6];

		int vertex = 0;
		int trianglecount = 0;

		for(int z=0; z<zBlocks; z++)
		{
			for(int x=0; x<xBlocks; x++)
			{
				triangles[0+trianglecount] = vertex;
				triangles[1+trianglecount] = vertex + xBlocks + 1;
				triangles[2+trianglecount] = vertex + 1;
				triangles[3+trianglecount] = vertex + 1;
				triangles[4+trianglecount] = vertex + xBlocks + 1;
				triangles[5+trianglecount] = vertex + xBlocks + 2;
		//		Debug.Log("innermost loop of trianglecounts z and x blocks points thing is getting called");

				vertex++;
				trianglecount += 6;
			}
			vertex++;
		}
	}

	void GrabTerrain()
	{
		//GL.wireframe = true;
		Mesh terrain_mesh = terrain.GetComponent<MeshFilter>().mesh;
		// ... after preparing vertices, uvs and triangle indices;
		// assign mesh data
		terrain_mesh.Clear(false);
		//terrain_mesh.vertices = vertices;
		terrain_mesh.subMeshCount = 2;
		// submesh 0: main-mesh
		//terrain_mesh.uv = uvs;
		terrain_mesh.SetTriangles(triangles, 0);
		// submesh 1: wire-frame
		// 3 lines require 6 indices per triangle which has 3 indices at main-mesh, so double indices count
		//int[] wires = new int[triangles.Length * 2];
		//for (int iTria = 0; iTria < trianglesCount; iTria++)
		//{
		//	for (int iVertex = 0; iVertex < 3; iVertex++)
	//		{
	//			wires[6 * iTria + 2 * iVertex] = triangles[3 * iTria + iVertex];
	//			wires[6 * iTria + 2 * iVertex + 1] = triangles[3 * iTria + (iVertex + 1) % 3];
	//		}
	//	}
	//	mesh.SetIndices(wires, MeshTopology.Lines, 1);

		//Destroy(GameObject.Find("Terrain"));
		//Mesh.triangles;

	}

	// Update is called once per frame
	void UpdateMesh()
	{
		mesh.Clear();
		mesh.vertices = points;
		mesh.triangles = triangles;
		GetComponent<MeshCollider>().sharedMesh = mesh;
		mesh.RecalculateNormals();
	}

	void OnDrawGizmos()
	{

		if (points == null) return;

		for(int i = 0; i < points.Length; i++)
		{
			Gizmos.DrawSphere(points[i], 0.5f);
		}
	}

	// Methods Getrandompoint, nearestgridpoint and GenerateTrees are all about generating trees
	public Vector3 GetRandomPoint()
	{
		return points[Random.Range(0,xBlocks * zBlocks)];
	}

	public Vector3 nearestGridPoint(Vector3 point)
	{
		int xPoint = (int)Mathf.Floor(point.x);
		int zPoint = (int)Mathf.Floor(point.z);
		return new Vector3(xPoint, 1, zPoint);
	}

	public void GenerateTrees()
	{
		if(tree == null)
		{
			return;
		}

		GameObject tmpGameObject;
		Vector3 spawnPoint;

		for(int i = 0; i< numberOfTrees; i++) 
		{
			tmpGameObject = Instantiate(tree);
			spawnPoint = nearestGridPoint(GetRandomPoint());
			spawnPoint.y = 4.8f;

			tmpGameObject.transform.position = spawnPoint;
		}

	}

	public void GenerateLargeRocks()
	{
		if(tree == null)
		{
			return;
		}

		GameObject tmpGameObject;
		Vector3 spawnPoint;

		for(int i = 0; i< numberOfLargeRocks; i++) 
		{
			tmpGameObject = Instantiate(largerock);
			spawnPoint = nearestGridPoint(GetRandomPoint());
			spawnPoint.y = 1.39f;

			tmpGameObject.transform.position = spawnPoint;
		}

	}

	public void GenerateSmallRocks()
	{
		if(tree == null)
		{
			return;
		}

		GameObject tmpGameObject;
		Vector3 spawnPoint;

		for(int i = 0; i< numberOfSmallRocks; i++) 
		{
			tmpGameObject = Instantiate(smallrock);
			spawnPoint = nearestGridPoint(GetRandomPoint());
			spawnPoint.y = 1.23f;

			tmpGameObject.transform.position = spawnPoint;
		}

	}
}
