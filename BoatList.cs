using System;

namespace Coursework
{
	class BoatList
	{
		private BoatNode start;
		private BoatNode end;
		private int marinaSpace = 150;

		public int getMarinaSpace()
		{
			return marinaSpace;
		}
		public BoatNode getStart()
		{
			return start;
		}

		public void setMarinaSpace(int marinaSpace)
		{
			this.marinaSpace = marinaSpace;
		}

		public BoatList()
		{
			start = null;
			end = null;
		}
		public void addBoatAtEnd(BoatNode boat)
		{
			BoatNode current = boat;
			
			if (end == null)
			{
				start = current;
				end = current;
			}
			else
			{
				boat.setPrevious(end);
				end.setNext(current);
				end = current;
			}
			setMarinaSpace(getMarinaSpace() - boat.getLength());
		}
		public void listAllBoats()
		{
			BoatNode current = start;
			while (current != null)
			{
				Console.WriteLine(current.getId() + ": " + current.getName() + " - " + current.getBoatName() + " - " + current.getBoatType() + " - " + current.getLength());
				current = current.getNext();
			}
		}

		public void removeRecord(int index)
		{
			BoatNode current = end;
			if (current == null)
			{
				Console.WriteLine("There are no boat booking records to be removed");
				return;
			}

			if (current.getId() == index) // if boat to be removed is at the end of the list
			{
				setMarinaSpace(marinaSpace + current.getLength());
				end = current.getPrevious();
				end.setNext(null);
				Console.WriteLine("Boat " + current.getBoatName() + " leaves without any others needing to move out of the way.");
			}

			else
			{
				while (current != null)
				{
					Console.WriteLine("Boat " + current.getBoatName() + " moves to holding bay.");
					if (current.getPrevious().getId() == index)
					{
						if (current.getPrevious() == start)
						{
							start = current;
						}
						else
						{
							current.getPrevious().getPrevious().setNext(current);
						}
						Console.WriteLine("Boat " + current.getPrevious().getBoatName() + " leaves the Marina.");
						setMarinaSpace(marinaSpace + current.getPrevious().getLength());
						current.setPrevious(current.getPrevious().getPrevious());
						while (current != null)
						{
							Console.WriteLine("Boat " + current.getBoatName() + " moves back into the Marina.");
							current = current.getNext();
						}
						return;
					}
					else
					{
						current = current.getPrevious();
					}
				}
			}
		}
	}

}
