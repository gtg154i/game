//https://github.com/gtg154i
//Untitled Roguelike RPG Project

using System;
using System.Collections;

namespace game
{
	class Program
	{
		public static void Main(string[] args)
		{
			//rnd used for generating random numbers
			Random rnd = new Random();
			
			//Matrix of chars that represent the map
			char[,] map = new char[14,14];
			
			//width of the map
			int width = map.GetLength(0);
			
			//height of the map
			int height = map.GetLength(1);
			
			//Sample Map:
			//OOOOOO
			//OXOOXO
			//OOOOOO
			//X in what I call center tiles.
			//I use the coordinates of center tiles later for populating tile contents
			
			//X coordinates of center tiles
			ArrayList xs = new ArrayList();
			
			//Y coordinates of center tiles
			ArrayList ys = new ArrayList();
			
			//for (int j = 0;j < height - 2;j++)
			for (int j = 0;j < height;j++)
			{
				for (int i = 0; i < width;i++)
				{
					if ((i == 0) || (i == width - 1))
						map[i,j] = 'W'; //W is a wall tile
					else
					{
						//we use modulus to mark the center tiles e.g. the 2nd of each 3 tiles
						if ((i % 3 == 2) && (j % 3 == 2))
						{
							xs.Add(i);
							ys.Add(j);
						}
						else
							map[i,j] = 'O';//initially the map was W's X's and O's, O's will be overwritten
					}
				}
			}
			
			//top wall
			for (int i = 0; i < width;i++)
				map[i,0] = 'W';
			
			//bottom wall
			for (int i = 0; i < width;i++)
				map[i,width - 1] = 'W';
			
			//populate spaces
			for (int i = 0;i < xs.Count;i++)
			{
				int currentx = (int)xs[i];
				int currenty = (int)ys[i];
				
				//This code was for viewing the center tiles and tiles around them
				//Each center tile is marked by '5' in this code.
				/*
				map[currentx - 1, currenty - 1] = '1';
				map[currentx, currenty - 1] = '2';
				map[currentx + 1, currenty - 1] = '3';
				map[currentx - 1, currenty ] = '4';
				map[currentx, currenty ] = '5';
				map[currentx + 1, currenty ] = '6';
				map[currentx - 1, currenty + 1 ] = '7';
				map[currentx, currenty + 1 ] = '8';
				map[currentx + 1, currenty + 1 ] = '9';
				*/
				
				//9 spaces total.
				//3 out of 9 spaces are walls.
				//4 walls or more per 9 spaces would cause some inaccessible tiles.
				//I decided the 6 remaining tiles would be 4 monsters 2 blank space or 3 monster 1 chest 2 blank
				
				ArrayList tileChoices = new ArrayList();
				tileChoices.Add('W');
				tileChoices.Add('W');
				tileChoices.Add('W');
				tileChoices.Add('M');
				tileChoices.Add('M');
				tileChoices.Add('M');
				//50% chance of chest, if not chest then monster so each time 3 monsters+1 chest or 4 monsters
				int chest = rnd.Next(100);
				if (chest > 50)
				tileChoices.Add('T');
				else
					tileChoices.Add('M');
				tileChoices.Add(' ');
				tileChoices.Add(' ');
				
				//index we pick
				int picked = 0;
				
				//We pick an index randomly out of the tileChoices remaining items
				picked = rnd.Next(tileChoices.Count);
				map[currentx - 1, currenty - 1] = (char)tileChoices[picked];
				//After the index's value is used, we remove that entry from tileChoices
				tileChoices.RemoveAt(picked);
				
				//This process repeats for the rest of the tiles:
				picked = rnd.Next(tileChoices.Count);
				map[currentx, currenty - 1] = (char)tileChoices[picked];
				tileChoices.RemoveAt(picked);
				
				picked = rnd.Next(tileChoices.Count);
				map[currentx + 1, currenty - 1] = (char)tileChoices[picked];
				tileChoices.RemoveAt(picked);
				
				picked = rnd.Next(tileChoices.Count);
				map[currentx - 1, currenty ] = (char)tileChoices[picked];
				tileChoices.RemoveAt(picked);
				
				picked = rnd.Next(tileChoices.Count);
				map[currentx, currenty ] = (char)tileChoices[picked];
				tileChoices.RemoveAt(picked);
				
				picked = rnd.Next(tileChoices.Count);
				map[currentx + 1, currenty ] = (char)tileChoices[picked];
				tileChoices.RemoveAt(picked);
				
				picked = rnd.Next(tileChoices.Count);
				map[currentx - 1, currenty + 1 ] = (char)tileChoices[picked];
				tileChoices.RemoveAt(picked);
				
				picked = rnd.Next(tileChoices.Count);
				map[currentx, currenty + 1 ] = (char)tileChoices[picked];
				tileChoices.RemoveAt(picked);
				
				picked = rnd.Next(tileChoices.Count);
				map[currentx + 1, currenty + 1 ] = (char)tileChoices[picked];
				tileChoices.RemoveAt(picked);
			}
			
			//startpoint and endpoint we just overwrite an existing tile for each
			
			//startpoint start
			int startx = rnd.Next(1, width - 1); // 1 to max -1 to prevent spawning on a wall
			int starty = rnd.Next(1, height - 1);
			map[startx,starty] = 'S';
			//startpoint end
			
			//endpoint start
			int endx = rnd.Next(1, width - 1);
			int endy = rnd.Next(1, height - 1);
			//in case endpoint is same as startpoint
			while ((startx == endx) && (starty == endy))
			{
			endx = rnd.Next(1, width - 1);
			endy = rnd.Next(1, height - 1);
			}
			map[endx,endy] = 'E';
			//endpoint end
			
			for (int j = 0;j < height;j++)
			{
				for (int i = 0; i < width;i++)
				{
					Console.Write(map[i,j]);
				}
				Console.WriteLine();
			}
			
			//this code prints list of center coords
			/*
			Console.WriteLine();
			for (int i = 0;i < xs.Count;i++)
			{
				Console.Write(xs[i]);
				Console.Write(",");
				Console.Write(ys[i]);
				Console.Write("\t");
			}
			*/
			
			Console.Write("'W' = Wall\t");
			Console.Write("'M' = Monster\t");
			Console.Write("'S' = Hero spawns here\t");
			Console.Write("'E' = Hero leaves stage here\t");
			Console.Write("'T' = Treasure Chest\t");
			Console.Write("' ' = Empty tile\t");
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Untitled Roguelike RPG Project");
			Console.WriteLine("Currently the mapping of the floor start, floor end, monsters, walls, empty spaces tiles are completed.");
			Console.WriteLine("To Do List:");
			Console.WriteLine("Hero stats, Monster stats, Battle mechanics, Pretty graphics");
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}