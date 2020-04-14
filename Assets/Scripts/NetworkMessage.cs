using System;
using UnityEngine;

namespace NetworkMessage
{
	public enum Command
	{
		Connect,
		Drop,
		Input,
		Turn,
		Result,
		Time,
		Chat
	}

	[Serializable]
	public class NetworkHeader
	{
		public Command cmd;
	}
}