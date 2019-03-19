using System;
using System.IO;


namespace Coursework
{
	class UserInterface
	{
		private static BoatList boats = new BoatList();
		private static int id = 1;

		public static void Main()
		{
			// if data.txt file exists
			if (File.Exists("data.txt"))
			{
				UserInterface.ReadFile();
			}
			UserInterface.Menu();
			return;
		}

		public static void Menu()
		{
			char input = ' ';

			while (input != 'Q')
			{
				Console.WriteLine("");
				Console.WriteLine("---------------------------------------------");
				Console.WriteLine("What would you like to do? Select");
				Console.WriteLine("Q: Quit");
				Console.WriteLine("A: Add a booking");
				Console.WriteLine("V: View all records and available berth space");
				Console.WriteLine("D: Delete specific record");
				Console.WriteLine("---------------------------------------------");

				input = Console.ReadLine().ToUpper()[0];

				switch (input)
				{
					case 'A':
						UserInterface.Validate();
						break;
					case 'V':
						Console.WriteLine("View records and available berth space");
						Console.WriteLine("ID: Owner - Boat - Type - Length (m)");
						Console.WriteLine("--------------------------------------");
						boats.listAllBoats();
						Console.WriteLine("The marina space available is " + boats.getMarinaSpace() + " metres.");
						break;
					case 'D':
						Console.WriteLine("Enter the id number of the record to be deleted: ");
						try
						{
							boats.removeRecord(Convert.ToInt16(Console.ReadLine()));
						}
						catch (FormatException)
						{
							Console.WriteLine("Not a valid id number, returning to Menu");
						}
						catch (NullReferenceException)
						{
							Console.WriteLine("ID number does not exist, returning to Menu");
						}
						break;
					case 'Q':
						break;
					default:
						Console.WriteLine("Invalid input - returning to Menu.");
						break;
				}
			}
			UserInterface.WriteFile(boats, id);
			Console.WriteLine("Choosing to quit");
			return;
		}
		
		public static void Validate()
		{
			int depth, length=0;
			bool lengthValid = false;
			while (lengthValid is false)
			{
				try
				{
					Console.WriteLine("What is the boat length (to the nearest meter)?");
					length = Convert.ToInt32(Console.ReadLine());
					lengthValid = true;
					if (length > 15)
					{
						Console.WriteLine("The boat is too long, the Marina only accepts boats shorter than 15m.");
						return;
					}

					if (boats.getMarinaSpace() < length)
					{
						Console.WriteLine("There is not enough space in the Marina.");
						return;
					}
				}
				catch (FormatException)
				{
					Console.WriteLine("Input an integer for boat length: ");
				}
			}
			bool depthValid = false;
			while (depthValid is false)
			{
				try
				{
					Console.WriteLine("What is the boat depth (to the nearest meter)?");
					depth = Convert.ToInt32(Console.ReadLine());
					depthValid = true;
					if (depth > 5)
					{
						Console.WriteLine("The boat is too deep, the Marina only accepts boats with a maximum depth of 5m");
						return;
					}
				}
				catch (FormatException)
				{
					Console.WriteLine("Input an integer for depth: ");
				}
			}
			UserInterface.Add(length);
		}

		public static void Add(int length)
		{
			char answer;
			int months=0, cost;
			string owner, boatName;
			bool boatType = false, monthsValid = false;

			while (monthsValid is false)
			{
				try
				{
					Console.WriteLine("How many months is the booking for?");
					months = Convert.ToInt32(Console.ReadLine());
					monthsValid = true;
				}
				catch (FormatException)
				{
					Console.WriteLine("Input an integer for number of months.");
				}
			}
			cost = 15 * length * months;
			Console.WriteLine("The cost of the booking is £" + cost + ". Would you like to book?");
			Console.WriteLine("Enter Yes or No");
			answer = Console.ReadLine().ToUpper()[0];
			if (answer != 'Y')
			{
				return;
			}
			Console.WriteLine("What's the name of the owner?");
			owner = Console.ReadLine();
			Console.WriteLine("What's the name of the boat?");
			boatName = Console.ReadLine();
			while (boatType == false)
			{
				Console.WriteLine("What type of boat is it?");
				Console.WriteLine("Enter N for narrow, S for sailing, M for Motor: ");
				switch (Console.ReadLine().ToUpper()[0])
				{
					case 'N':
						Narrow narrow = new Narrow(owner, boatName, length, id);
						boats.addBoatAtEnd(narrow);
						id += 1;
						boatType = true;
						break;
					case 'S':
						Sailing sailing = new Sailing(owner, boatName, length, id);
						boats.addBoatAtEnd(sailing);
						id += 1;
						boatType = true;
						break;
					case 'M':
						Motor motor = new Motor(owner, boatName, length, id);
						boats.addBoatAtEnd(motor);
						id += 1;
						boatType = true;
						break;
					default:
						Console.WriteLine("Incorrect boat type input, please try again.");
						break;
				}
			}

		}

		public static void WriteFile(BoatList boats, int id)
		{
			StreamWriter writer;
			try
			{
				writer = new StreamWriter("data.txt");
				writer.WriteLine(id);

				BoatNode current = boats.getStart();
				while (current != null)
				{
					writer.WriteLine(current.getId().ToString() + "," + current.getName() + "," + current.getBoatName() + "," + current.getBoatType() + "," + current.getLength().ToString());
					current = current.getNext();
				}
				writer.Close();
			}
			catch (IOException e)
			{
				Console.WriteLine("Error writing to file.");
			}
		}

		public static void ReadFile()
		{
			string line;
			string[] inputs;
			try
			{
				StreamReader reader = new StreamReader(@"data.txt");
				id = Convert.ToInt16(reader.ReadLine());

				while (reader.EndOfStream == false)
				{
					line = reader.ReadLine();
					inputs = line.Split(',');
					if (inputs[3] == "Sailing")
					{
						Sailing boat = new Sailing(inputs[1], inputs[2], Convert.ToInt16(inputs[4]), Convert.ToInt16(inputs[0]));
						boats.addBoatAtEnd(boat);
					}
					else if (inputs[3] == "Narrow")
					{
						Narrow boat = new Narrow(inputs[1], inputs[2], Convert.ToInt16(inputs[4]), Convert.ToInt16(inputs[0]));
						boats.addBoatAtEnd(boat);
					}
					else if (inputs[3] == "Motor")
					{
						Motor boat = new Motor(inputs[1], inputs[2], Convert.ToInt16(inputs[4]), Convert.ToInt16(inputs[0]));
						boats.addBoatAtEnd(boat);
					}
				}
				reader.Close();
			}
			catch(IOException e)
			{
				Console.WriteLine("File input error");
			}
		}
	}
}
