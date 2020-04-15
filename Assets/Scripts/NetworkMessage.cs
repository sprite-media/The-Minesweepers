using System;
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
	public class Result : NetworkHeader //From server to clients
	{
		//TODO list of results

		public Result()
		{
			cmd = Command.Result;
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
		public int playerID;
		public string chatMessage;
		public Chat()
		{
			cmd = Command.Turn;
		}
	}
}