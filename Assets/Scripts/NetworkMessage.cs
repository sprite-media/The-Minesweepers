using System;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkMessage
{
	public enum Command
	{
		Connect,
		Start,
		Drop,
		Click,
		Turn,
		Result,
		Timer,
		Chat
	}

	[Serializable]
	public class NetworkHeader
	{
		public Command cmd;
	}

	[Serializable]
	public class StartInfo : NetworkHeader
	{
		public StartInfo()
		{
			cmd = Command.Start;
		}
	}

	[Serializable]
	public class ConnectInfo : NetworkHeader
	{
		public int playerID;
		public ConnectInfo()
		{
			cmd = Command.Connect;
		}
	}

	[Serializable]
	public class DropInfo : NetworkHeader
	{
		public DropInfo()
		{
			cmd = Command.Drop;
		}
	}

	[Serializable]
	public class Click : NetworkHeader//From player
	{
		public int mouse;
		public Vector2Int index;//x y index of grid

		public Click(Vector2Int i, int m)
		{
			cmd = Command.Click;
			index = i;
			mouse = m;
		}
		public override string ToString()
		{
			return "NetworkInput : " + index.ToString() + ", mouse : " + mouse.ToString();
		}
	}

	[Serializable]
	public class Turn : NetworkHeader//From server to client
	{
		public int playerID;
		public Turn()
		{
			cmd = Command.Turn;
		}
	}

	[Serializable]
	public class CellResult
	{
		public Vector2Int index;
		public Cell.Status status;
		public int surrounding;

		public CellResult()
		{
			status = Cell.Status.HIDDEN;
		}
	}

	[Serializable]
	public class Result : NetworkHeader //From server to clients
	{
		public List<CellResult> result;

		public Result()
		{
			cmd = Command.Result;
			result = new List<CellResult>();
		}
		public override string ToString()
		{
			string toString = "Result : \n";
			toString += "Lenght : " + result.Count + "\n";
			return toString;
		}
	}

	[Serializable]
	public class Timer : NetworkHeader //From server to clients
	{
		public float timer; // elapsed time from server
		public Timer()
		{
			cmd = Command.Timer;
		}
	}

	[Serializable]
	public class Chat : NetworkHeader //Both way. Works differently on server side and client side
	{
		public string chatMessage;
		public Chat()
		{
			cmd = Command.Chat;
		}
		public string RemoveQuestionMark(string original)
		{
			string removed = "";
			for (int i = 0; i < original.Length-1; i++)
			{
				removed += original[i];
			}
			return removed;
		}
	}
}