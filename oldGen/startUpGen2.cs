using UnityEngine;
using System.Collections;
namespace AssemblyCSharp
{
	public class startUpGen2 : MonoBehaviour 
	{
		
		private static int column = DataBase.column;//x
		private static int row = DataBase.row;//z
		private static int maxHeight = DataBase.maxHeight;//y
		private static float blockHeight = DataBase.blockHeight;
		
		// Use this for initialization
		void Start () 
		{
			createBoard();
			addPlayer(0f,0f);
			addPlayer(5f,5f);
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		}
		
		public static void createBoard()
		{

			Square[,] board = new Square[column,row];
			int read = Random.Range (0, 3);

			for(int z = 0; z < row; z ++)
			{
				for(int x = 0; x < column; x ++)
				{
					// gota initialize dis first, elevation is all -1
					board[x,z] = new Square();
				}
			}
			
			elevate(board, column, row, maxHeight);//start elevate.
			terrainSet(board, column, row, maxHeight);

			for (int x = 0; x < column; x++)
			{
				for(int z = 0; z < row; z ++)
				{
					Debug.Log (board[x,z].getMaterial());
				}
			
			}
			//board = rotate(board);

			for(int z = 0; z < row; z ++)
			{
				for(int x = 0; x < column; x ++)
				{
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.transform.position = new Vector3(x, board[x,z].getElevation()*blockHeight/2, z);
					cube.transform.localScale += new Vector3(0, board[x,z].getElevation()*blockHeight, 0);

					cube.renderer.material = (Material)Resources.Load("Materials/M" + board[x,z].getMaterial());

					board[x,z].setObject(cube);
				}
			}

			DataBase.setBoard(board);

		}
		
		public void addPlayer(float x, float z)
		{
			GameObject player = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			player.transform.position = new Vector3(x, DataBase.board[(int)x,(int)z].getElevation()*blockHeight + DataBase.blockHeight/2f + 0.5f, z);
			player.AddComponent("playerControl");
		}

		public static void elevate(Square[,] board, int column, int row, int max)
		{

			board[0,0].setElevation(Random.Range(0,5));

			for (int i = 1; i < column; i++)
			{
				int check = Random.Range(0, 100);
				int check2 = Random.Range(0, 100);

				if(check <= 60)
				{
					board[i,0].setElevation(board[i-1,0].getElevation());
				}

				else if(check >= 61 && check < 80)
				{
					if(board[i-1,0].getElevation() != max)
					{
					board[i,0].setElevation(board[i-1,0].getElevation() + 1);
					}
					else
						if(check2 < 70)
							{
							board[i,0].setElevation(board[i-1,0].getElevation());
							}
						else if(check2 >= 70)
							{
							board[i,0].setElevation(board[i-1,0].getElevation() - 1);
							}

				}

				else if(check >= 80)
				{
					if(board[i-1,0].getElevation() > 0)
						board[i,0].setElevation(board[i-1,0].getElevation() - 1);
					else
						if(check2 < 70)
					{
						board[i,0].setElevation(board[i-1,0].getElevation());
					}
					else if(check2 >= 70)
					{
						board[i,0].setElevation(board[i-1,0].getElevation() + 1);
					}
				}
			}

			for (int z = 1; z < row; z++)
			{
				int check = Random.Range(0, 100);
				int check2 = Random.Range(0, 100);

				if(check <= 60)
				{
					board[0,z].setElevation(board[0,z-1].getElevation());
				}

				else if(check >= 61 && check < 80)
				{
					if(board[0,z-1].getElevation() != max)
					board[0,z].setElevation(board[0,z-1].getElevation() + 1);
					else
						if(check2 < 70)
							{
							board[0,z].setElevation(board[0,z-1].getElevation());
							}
						else if(check2 >= 70)
							{
							board[0,z].setElevation(board[0,z-1].getElevation() - 1);
							}
				}

				else if(check >= 80)
				{
					if(board[0,z-1].getElevation() > 0)
						board[0,z].setElevation(board[0,z-1].getElevation() - 1);
					else
						if(check2 < 70)
					{
						board[0,z].setElevation(board[0,z-1].getElevation());
					}
					else if(check2 >= 70)
					{
						board[0,z].setElevation(board[0,z-1].getElevation() + 1);
					}
				}
			}

			for (int i = 1; i < column; i++)
			{
				for(int z = 1; z < row; z++)
				{
					int check = Random.Range(0, 100);
					int check2 = Random.Range(0, 100);

					int mixval1 = board[i-1,z].getElevation();
					int mixval2 = board[i,z-1].getElevation();

					int mixed = (mixval1 + mixval2)/ 2;

					if(check <= 60)
					{
						board[i,z].setElevation(mixed);
					}
					
					else if(check >= 61 && check < 80)
					{
						if(mixed != max)
						{
							board[i,z].setElevation(mixed + 1);
						}
						else
							if(check2 < 70)
						{
							board[i,z].setElevation(mixed);
						}
						else if(check2 >= 70)
						{
							board[i,z].setElevation(mixed - 1);
						}
						
					}
					
					else if(check >= 80)
					{
						if(mixed > 0)
							board[i,z].setElevation(mixed - 1);
						else
							if(check2 < 70)
						{
							board[i,z].setElevation(mixed);
						}
						else if(check2 >= 70)
						{
							board[i,z].setElevation(mixed + 1);
						}
					}
				}
			}
		}

		public static void terrainSet(Square[,] board, int column, int row, int max)
		{
			if(board[0,0].getMaterial() == -1)
			{
			int check1 = Random.Range(0,100);
			int checkH = board[0,0].getElevation();
			int heightBound = (max/2) +(max/4);

			int terrainBound1 = 40;
			int terrainBound2 = 65;
			int terrainBound3 = 85;
			int terrainBound4 = 95;
			int terrainBound5 = 100;

			int grass = 0;
			int water = 1;
			int beach = 2;
			int rock = 3;
			int forest = 4;


			if(checkH > heightBound)
			{
				terrainBound3 +=5;

				if(check1 < terrainBound1) // 40$ rock
					board[0,0].setMaterial(rock);
				else if(check1 >= terrainBound1 && check1 < terrainBound2) //25% grass
					board[0,0].setMaterial(grass);
				else if(check1 >= terrainBound2 && check1 < terrainBound3) //25% forest
					board[0,0].setMaterial(forest);
				else if(check1 >= terrainBound3 && check1 < terrainBound5) //10% water
					board[0,0].setMaterial(water);
			}
			else if (checkH <= heightBound)
				board[0,0].setMaterial(Random.Range(0,4)); //25% all
			}

			for(int x = 1; x < column; x++)
			{
				int check1 = Random.Range(0, 100);
				int checkH = board[x, 0].getElevation();
				int checkM = board[x-1, 0].getMaterial();
				int heightBound = (max/2) +(max/4);

				int terrainBound1 = 40;
				int terrainBound2 = 65;
				int terrainBound3 = 85;
				int terrainBound4 = 95;
				int terrainBound5 = 100;

				int grass = 0;
				int water = 1;
				int beach = 2;
				int rock = 3;
				int forest = 4;

				if(checkH > heightBound && checkM == rock) 							//done
				{
					terrainBound1 +=20; //60% rock
					terrainBound2 +=15; //20% grass
					terrainBound3 +=10; //15% forest, 5% water
				

					if(check1 < terrainBound1)
						board[x,0].setMaterial(board[x-1,0].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[x,0].setMaterial(grass);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[x,0].setMaterial(forest);
					else if(check1 >= terrainBound3 && check1 < terrainBound5)
						board[x,0].setMaterial(water);
				}
			

				else if(checkH > heightBound && checkM == grass)					 //done
				{
					terrainBound1 +=10; //50% grass,
					terrainBound2 +=10; //25% rock
					terrainBound3 +=5;  //15% forest , 10% water
					
					if(check1 < terrainBound1)
						board[x,0].setMaterial(board[x-1,0].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[x,0].setMaterial(rock);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[x,0].setMaterial(forest);
					else if(check1 >= terrainBound3 && check1 < terrainBound5)
						board[x,0].setMaterial(water);
				}
				else if(checkH > heightBound && checkM == water) 					//done
				{
					terrainBound2 +=10; // 40$ water, 35% rock,
					terrainBound3 +=5;  // 20% grass, 5% forest
					
					if(check1 < terrainBound1)
						board[x,0].setMaterial(board[x-1,0].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[x,0].setMaterial(rock);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[x,0].setMaterial(grass);
					else if(check1 >= terrainBound3 && check1 < terrainBound5)
						board[x,0].setMaterial(forest);
				}
				else if(checkH > heightBound && checkM == forest) 					//done
				{
					terrainBound1 +=10; //50% forest,
					terrainBound2 +=10; //25% rock
					terrainBound3 +=5;  //15% grass , 10% water
					
					if(check1 < terrainBound1)
						board[x,0].setMaterial(board[x-1,0].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[x,0].setMaterial(rock);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[x,0].setMaterial(grass);
					else if(check1 >= terrainBound3 && check1 < terrainBound5)
						board[x,0].setMaterial(water);
				}
				else if(checkH > heightBound && checkM == beach)					//done
				{
					terrainBound1 +=20; //60% rock
					terrainBound2 +=15; //20% water
					terrainBound3 +=10; //15% grass, 5% forest
					
					if(check1 < terrainBound1)
						board[x,0].setMaterial(rock);
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[x,0].setMaterial(water);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[x,0].setMaterial(grass);
					else if(check1 >= terrainBound3 && check1 < terrainBound5)
						board[x,0].setMaterial(forest);
				}

				else if(checkH <= heightBound && checkM == grass)					//done
				{	
					//40% grass, 25% forest, 20% beach, 10% rock, 5% water.

					if(check1 < terrainBound1)
						board[x,0].setMaterial(board[x-1,0].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[x,0].setMaterial(forest);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[x,0].setMaterial(beach);
					else if(check1 >= terrainBound3 && check1 < terrainBound4)
						board[x,0].setMaterial(rock);
					else if(check1 >= terrainBound4 && check1 < terrainBound5)
						board[x,0].setMaterial(water);
				}

				else if(checkH <= heightBound && checkM == rock)					//done
				{	

					//40% rock, 25% grass, 20% forest, 10% beach, 5% water.

					if(check1 < terrainBound1)
						board[x,0].setMaterial(board[x-1,0].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[x,0].setMaterial(grass);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[x,0].setMaterial(forest);
					else if(check1 >= terrainBound3 && check1 < terrainBound4)
						board[x,0].setMaterial(beach);
					else if(check1 >= terrainBound4 && check1 < terrainBound5)
						board[x,0].setMaterial(water);
				}
				else if(checkH <= heightBound && checkM == beach)					//done
				{	
					//40% beach, 25% water, 20% grass, 10% rock, 5% forest.

					if(check1 < terrainBound1)
						board[x,0].setMaterial(board[x-1,0].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[x,0].setMaterial(water);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[x,0].setMaterial(grass);
					else if(check1 >= terrainBound3 && check1 < terrainBound4)
						board[x,0].setMaterial(rock);
					else if(check1 >= terrainBound4 && check1 < terrainBound5)
						board[x,0].setMaterial(forest);
				}
				else if(checkH <= heightBound && checkM == forest)					//done
				{	
					//40% forest, 25% grass, 20% rock, 10% beach, 5% water.

					if(check1 < terrainBound1)
						board[x,0].setMaterial(board[x-1,0].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[x,0].setMaterial(grass);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[x,0].setMaterial(rock);
					else if(check1 >= terrainBound3 && check1 < terrainBound4)
						board[x,0].setMaterial(beach);
					else if(check1 >= terrainBound4 && check1 < terrainBound5)
						board[x,0].setMaterial(water);
				}
				else if(checkH <= heightBound && checkM == water)					//done
				{	
					terrainBound1 += 40;

					if(check1 < terrainBound1)
						board[x,0].setMaterial(board[x-1,0].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound5)
						board[x,0].setMaterial(beach);
				}

			}

			for(int z = 1; z < row; z++)
			{
				int check1 = Random.Range(0, 100);
				int checkH = board[0, z].getElevation();
				int checkM = board[0, z-1].getMaterial();
				int heightBound = (max/2) +(max/4);
				
				int terrainBound1 = 40;
				int terrainBound2 = 65;
				int terrainBound3 = 85;
				int terrainBound4 = 95;
				int terrainBound5 = 100;
				
				int grass = 0;
				int water = 1;
				int beach = 2;
				int rock = 3;
				int forest = 4;
				
				if(checkH > heightBound && checkM == rock) 							// done
				{
					terrainBound1 +=20; //60% rock
					terrainBound2 +=15; //20% grass
					terrainBound3 +=10; //15% forest, 5% water
					
					if(check1 < terrainBound1)
						board[0,z].setMaterial(board[0,z-1].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[0,z].setMaterial(grass);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[0,z].setMaterial(forest);
					else if(check1 >= terrainBound3 && check1 < terrainBound5)
						board[0,z].setMaterial(water);
				}
				
				else if(checkH > heightBound && checkM == grass) 					//done
				{
					terrainBound1 +=10; //50% grass,
					terrainBound2 +=10; //25% rock
					terrainBound3 +=5;  //15% forest , 10% water
					
					if(check1 < terrainBound1)
						board[0,z].setMaterial(board[0,z-1].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[0,z].setMaterial(rock);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[0,z].setMaterial(forest);
					else if(check1 >= terrainBound3 && check1 < terrainBound5)
						board[0,z].setMaterial(water);
				}
				else if(checkH > heightBound && checkM == water)					//done
				{
					terrainBound2 +=10; // 40$ water, 35% rock,
					terrainBound3 +=5;  // 20% grass, 5% forest
					
					if(check1 < terrainBound1)
						board[0,z].setMaterial(board[0,z-1].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[0,z].setMaterial(rock);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[0,z].setMaterial(grass);
					else if(check1 >= terrainBound3 && check1 < terrainBound5)
						board[0,z].setMaterial(forest);
				}
				else if(checkH > heightBound && checkM == forest)					//done
				{
					terrainBound1 +=10; //50% forest,
					terrainBound2 +=10; //25% rock
					terrainBound3 +=5;  //15% grass , 10% water
					
					if(check1 < terrainBound1)
						board[0,z].setMaterial(board[0,z-1].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[0,z].setMaterial(rock);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[0,z].setMaterial(grass);
					else if(check1 >= terrainBound3 && check1 < terrainBound5)
						board[0,z].setMaterial(water);
				}
				else if(checkH > heightBound && checkM == beach) 					//done
				{
					terrainBound1 +=20; //60% rock
					terrainBound2 +=15; //20% water
					terrainBound3 +=10; //15% grass, 5% forest
					
					if(check1 < terrainBound1)
						board[0,z].setMaterial(rock);
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[0,z].setMaterial(water);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[0,z].setMaterial(grass);
					else if(check1 >= terrainBound3 && check1 < terrainBound5)
						board[0,z].setMaterial(forest);
				}
				
				else if(checkH <= heightBound && checkM == grass)					//done
				{	
					//40% grass, 25% forest, 20% beach, 10% rock, 5% water.

					if(check1 < terrainBound1)
						board[0,z].setMaterial(board[0,z-1].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[0,z].setMaterial(forest);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[0,z].setMaterial(beach);
					else if(check1 >= terrainBound3 && check1 < terrainBound4)
						board[0,z].setMaterial(rock);
					else if(check1 >= terrainBound4 && check1 < terrainBound5)
						board[0,z].setMaterial(water);
				}
				
				else if(checkH <= heightBound && checkM == rock)					//done
				{	
					//40% rock, 25% grass, 20% forest, 10% beach, 5% water.

					if(check1 < terrainBound1)
						board[0,z].setMaterial(board[0,z-1].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[0,z].setMaterial(grass);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[0,z].setMaterial(forest);
					else if(check1 >= terrainBound3 && check1 < terrainBound4)
						board[0,z].setMaterial(beach);
					else if(check1 >= terrainBound4 && check1 < terrainBound5)
						board[0,z].setMaterial(water);
				}
				else if(checkH <= heightBound && checkM == beach)					//done
				{	
					//40% beach, 25% water, 20% grass, 10% rock, 5% forest.

					if(check1 < terrainBound1)
						board[0,z].setMaterial(board[0,z-1].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[0,z].setMaterial(water);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[0,z].setMaterial(grass);
					else if(check1 >= terrainBound3 && check1 < terrainBound4)
						board[0,z].setMaterial(rock);
					else if(check1 >= terrainBound4 && check1 < terrainBound5)
						board[0,z].setMaterial(forest);
				}
				else if(checkH <= heightBound && checkM == forest)					//done
				{	
					//40% forest, 25% grass, 20% rock, 10% beach, 5% water.

					if(check1 < terrainBound1)
						board[0,z].setMaterial(board[0,z-1].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound2)
						board[0,z].setMaterial(grass);
					else if(check1 >= terrainBound2 && check1 < terrainBound3)
						board[0,z].setMaterial(rock);
					else if(check1 >= terrainBound3 && check1 < terrainBound4)
						board[0,z].setMaterial(beach);
					else if(check1 >= terrainBound4 && check1 < terrainBound5)
						board[0,z].setMaterial(water);
				}
				else if(checkH <= heightBound && checkM == water)
				{	
					terrainBound1 += 40; //80% water, 20%beach

					if(check1 < terrainBound1)
						board[0,z].setMaterial(board[0,z-1].getMaterial());
					else if(check1 >= terrainBound1 && check1 < terrainBound5)
						board[0,z].setMaterial(beach);
				}
				
			}

			for (int x = 1; x < column; x++)
			{
				for (int z = 1; z < row; z++)
				{
					int check1 = Random.Range(0, 100);
					int checkH = board[0, z].getElevation();
					int[] sortM = new int[2];
					int heightBound = (max/2) +(max/4);

					int terrainBound1 = 40;
					int terrainBound2 = 65;
					int terrainBound3 = 85;
					int terrainBound4 = 95;
					int terrainBound5 = 100;

					int grass = 0;
					int water = 1;
					int beach = 2;
					int rock = 3;
					int forest = 4;

					int temp = 0;

					sortM[0] = board[x-1,z].getMaterial();
					sortM[1] = board[x,z-1].getMaterial();

					if(sortM[0] > sortM[1])
					{
						temp = sortM[0];
						sortM[0] = sortM[1];
						sortM[1] = temp;
					}


					if(checkH > heightBound && sortM[0] == grass)
					{
						if(sortM[1] == rock)
						{
						terrainBound1 +=20;
						terrainBound2 +=15;
						terrainBound3 +=10;
						}
						else if(sortM[1] == grass)
						{
							terrainBound2 +=10;
						}
						
						if(check1 < terrainBound1)
							board[x,z].setMaterial(sortM[0]);
						else if(check1 >= terrainBound1 && check1 < terrainBound2)
							board[x,z].setMaterial(rock);
						else if(check1 >= terrainBound2 && check1 < terrainBound3)
							board[x,z].setMaterial(water);
						else if(check1 >= terrainBound3 && check1 < terrainBound5)
							board[x,z].setMaterial(forest);
					}

					else if(checkH > heightBound && sortM[0] == water)
					{
						if(sortM[1] == rock)
						{
							terrainBound1 +=20;
							terrainBound2 +=15;
							terrainBound3 +=10;
						}
						else if(sortM[1] == water)
						{
							terrainBound1 +=10;
						}
						
						if(check1 < terrainBound1)
							board[x,z].setMaterial(sortM[0]);
						else if(check1 >= terrainBound1 && check1 < terrainBound2)
							board[x,z].setMaterial(rock);
						else if(check1 >= terrainBound2 && check1 < terrainBound3)
							board[x,z].setMaterial(grass);
						else if(check1 >= terrainBound3 && check1 < terrainBound5)
							board[x,z].setMaterial(forest);
					}

					else if(checkH > heightBound && sortM[0] == rock)
					{
						if(sortM[1] == rock)
						{
							terrainBound1 +=20;
							terrainBound2 +=15;
							terrainBound3 +=10;
						}
						else
						{
							terrainBound2 +=10;
						}
						
						if(check1 < terrainBound1)
							board[x,z].setMaterial(sortM[0]);
						else if(check1 >= terrainBound1 && check1 < terrainBound2)
							board[x,z].setMaterial(grass);
						else if(check1 >= terrainBound2 && check1 < terrainBound3)
							board[x,z].setMaterial(water);
						else if(check1 >= terrainBound3 && check1 < terrainBound5)
							board[x,z].setMaterial(forest);
					}

					else if(checkH > heightBound && sortM[0] == beach)
					{
					if(sortM[1] == rock)
						{
							terrainBound1 +=10;
						}
						
						if(check1 < terrainBound1)
							board[x,z].setMaterial(rock);
						else if(check1 >= terrainBound1 && check1 < terrainBound2)
							board[x,z].setMaterial(grass);
						else if(check1 >= terrainBound2 && check1 < terrainBound3)
							board[x,z].setMaterial(forest);
						else if(check1 >= terrainBound3 && check1 < terrainBound5)
							board[x,z].setMaterial(water);
					}

					else if(checkH > heightBound && sortM[0] == forest)
					{
						 if(sortM[1] == forest)
						{
							terrainBound1 +=10;
						}
						
						if(check1 < terrainBound1)
							board[x,z].setMaterial(sortM[0]);
						else if(check1 >= terrainBound1 && check1 < terrainBound2)
							board[x,z].setMaterial(grass);
						else if(check1 >= terrainBound2 && check1 < terrainBound3)
							board[x,z].setMaterial(water);
						else if(check1 >= terrainBound3 && check1 < terrainBound5)
							board[x,z].setMaterial(rock);
					}

					else if(checkH <= heightBound && sortM[0] == grass)
					{	
						if(sortM[1] == grass)
						{
							terrainBound1 +=10;
						}
						if(check1 < terrainBound1)
							board[x,z].setMaterial(sortM[0]);
						else if(check1 >= terrainBound1 && check1 < terrainBound2)
							board[x,z].setMaterial(rock);
						else if(check1 >= terrainBound2 && check1 < terrainBound3)
							board[x,z].setMaterial(beach);
						else if(check1 >= terrainBound3 && check1 < terrainBound4)
							board[x,z].setMaterial(forest);
						else if(check1 >= terrainBound4 && check1 < terrainBound5)
							board[x,z].setMaterial(water);
					}

					else if(checkH <= heightBound && sortM[0] == water)
					{	
						if(sortM[1] == water)
						{
							terrainBound1 +=10;
						}
						if(check1 < terrainBound1)
							board[x,z].setMaterial(sortM[0]);
						else if(check1 >= terrainBound1 && check1 < terrainBound2)
							board[x,z].setMaterial(rock);
						else if(check1 >= terrainBound2 && check1 < terrainBound3)
							board[x,z].setMaterial(beach);
						else if(check1 >= terrainBound3 && check1 < terrainBound4)
							board[x,z].setMaterial(forest);
						else if(check1 >= terrainBound4 && check1 < terrainBound5)
							board[x,z].setMaterial(grass);
					}

					else if(checkH <= heightBound && sortM[0] == beach)
					{	
						if(sortM[1] == beach)
						{
							terrainBound1 +=10;
						}
						if(check1 < terrainBound1)
							board[x,z].setMaterial(sortM[0]);
						else if(check1 >= terrainBound1 && check1 < terrainBound2)
							board[x,z].setMaterial(rock);
						else if(check1 >= terrainBound2 && check1 < terrainBound3)
							board[x,z].setMaterial(water);
						else if(check1 >= terrainBound3 && check1 < terrainBound4)
							board[x,z].setMaterial(grass);
						else if(check1 >= terrainBound4 && check1 < terrainBound5)
							board[x,z].setMaterial(forest);
					}

					else if(checkH <= heightBound && sortM[0] == rock)
					{	
						if(sortM[1] == rock)
						{
							terrainBound1 +=10;
						}
						if(check1 < terrainBound1)
							board[x,z].setMaterial(sortM[0]);
						else if(check1 >= terrainBound1 && check1 < terrainBound2)
							board[x,z].setMaterial(grass);
						else if(check1 >= terrainBound2 && check1 < terrainBound3)
							board[x,z].setMaterial(beach);
						else if(check1 >= terrainBound3 && check1 < terrainBound4)
							board[x,z].setMaterial(forest);
						else if(check1 >= terrainBound4 && check1 < terrainBound5)
							board[x,z].setMaterial(water);
					}

					else if(checkH <= heightBound && sortM[0] == forest)
					{	
						if(sortM[1] == forest)
						{
							terrainBound1 +=10;
						}
						if(check1 < terrainBound1)
							board[x,z].setMaterial(sortM[0]);
						else if(check1 >= terrainBound1 && check1 < terrainBound2)
							board[x,z].setMaterial(grass);
						else if(check1 >= terrainBound2 && check1 < terrainBound3)
							board[x,z].setMaterial(rock);
						else if(check1 >= terrainBound3 && check1 < terrainBound4)
							board[x,z].setMaterial(water);
						else if(check1 >= terrainBound4 && check1 < terrainBound5)
							board[x,z].setMaterial(beach);
					}

				}
			}
		}

		public static Square[,] rotate(Square[,] board)
		{
			Square[,] finalBoard = new Square[column,row];

			int check = Random.Range (0, 3);

			int assign1 = 0;
			int assign2 = 0;

			if(check == 0)
			{
				finalBoard = board;
			}
			else if(check == 1)
			{
				assign1 = row-1;
				assign2 = 0;

				for (int i = 0; i < row; i++)
				{
					for(int j = 0; j < column; j++)
					{
						finalBoard[assign1, assign2] = board[i, j];
						assign2++;
					}

					assign2 = 0;
					assign1--;
				}
			}

			else if(check == 2)
			{
				assign1 = row-1;
				assign2 = column-1;

				for (int i = 0; i < row; i++)
				{
					for(int j = 0; j < column; j++)
					{
						finalBoard[assign1, assign2] = board[i, j];
						assign2--;
					}

					assign2 = column-1;
					assign1--;
				}
			}

			else if(check == 3)
			{
				assign1 = 0;
				assign2 = column-1;
				
				for (int i = 0; i < row; i++)
				{
					for(int j = 0; j < column; j++)
					{
						finalBoard[assign1, assign2] = board[i, j];
						assign2--;
					}
					
					assign2 = column-1;
					assign1++;
				}
			}
				return finalBoard;
		}



		public static bool isValid(Square[,] board, int column, int row, int x, int y)
		{
			return (x > -1 && x < column) && (y > -1 && y < row) && board[x,y].getElevation() == -1;
		}
	}
}