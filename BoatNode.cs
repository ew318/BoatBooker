using System;

namespace Coursework
{
	class BoatNode
	{
		protected string ownerName, boatName, boatType = " ";
		protected int length, id;
		private BoatNode next, previous;

		public BoatNode(string ownerName, string boatName, int length, int id)
		{
			this.id = id;
			this.ownerName = ownerName;
			this.boatName = boatName;
			this.length = length;
		}

		public BoatNode getNext()
		{
			return next;
		}

		public BoatNode getPrevious()
		{
			return previous;
		}

		public string getName()
		{
			return ownerName;
		}
		public int getId()
		{
			return id;
		}
		public string getBoatName()
		{
			return boatName;
		}
		public string getBoatType()
		{
			return boatType;
		}
		public int getLength()
		{
			return length;
		}
		public void setNext(BoatNode current)
		{
			next = current;
		}
		public void setPrevious(BoatNode current)
		{
			previous = current;
		}
	}
	class Narrow : BoatNode
	{
		public Narrow(string ownerName, string boatName, int length, int id) : base(ownerName, boatName, length, id)
		{
			boatType = "Narrow";
		}
	}
	class Sailing : BoatNode
	{
		public Sailing(string ownerName, string boatName, int length, int id) : base(ownerName, boatName, length, id)
		{
			boatType = "Sailing";
		}
	}
	class Motor : BoatNode
	{
		public Motor(string ownerName, string boatName, int length, int id) : base(ownerName, boatName, length, id)
		{
			boatType = "Motor";
		}
	}

}
