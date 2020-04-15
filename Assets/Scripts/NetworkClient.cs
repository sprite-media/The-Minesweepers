using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Networking.Transport;
using NetworkMessage;

public class NetworkClient : MonoBehaviour
{
	public static NetworkClient instance { get; private set; }

	public string IP;
	public ushort Port;

	public NetworkDriver m_Driver;
	public NetworkConnection m_Connection;

	public int ID { get; private set; }

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}

		m_Driver = NetworkDriver.Create();
		m_Connection = default(NetworkConnection);

		var endpoint = NetworkEndPoint.Parse(IP, Port);
		m_Connection = m_Driver.Connect(endpoint);
	}
	private void Update()
	{
		m_Driver.ScheduleUpdate().Complete();

		if (!m_Connection.IsCreated)
		{
			Debug.LogError("ConnectionError");
			return;
		}

		DataStreamReader stream;
		NetworkEvent.Type cmd;

		while ((cmd = m_Connection.PopEvent(m_Driver, out stream)) != NetworkEvent.Type.Empty)
		{
			if (cmd == NetworkEvent.Type.Data)
			{
				OnData(stream);
			}
		}
	}

	private void OnData(DataStreamReader stream)
	{
		NativeArray<byte> message = new NativeArray<byte>(stream.Length, Allocator.Temp);
		stream.ReadBytes(message);
		string returnData = Encoding.ASCII.GetString(message.ToArray());

		NetworkHeader header = new NetworkHeader();
		try
		{
			header = JsonUtility.FromJson<NetworkHeader>(returnData);
		}
		catch (System.ArgumentException e)
		{
			Debug.LogError(e.ToString() + "\nHeader loading failed. Disconnect");
			Disconnect();
			return;
		}

		try
		{
			switch (header.cmd)
			{
				case Command.Connect:
					//TODO Receive id and ready
					break;
				case Command.Drop:
					//TODO Game end
					break;
				case Command.Turn:
					//TODO change turn
					break;
				case Command.Result:
					//TODO Send result to local(client side) grid manager and reflect the result(visually)
					break;
				case Command.Timer:
					//TODO send server timer to timer script
					break;
				case Command.Chat:
					//TODO send chat message to chat script
					break;
				default:
					Debug.Log("Error");
					break;
			}
		}
		catch (System.Exception e)
		{
			Debug.LogError(e.ToString() + "\nMessage contents loading failed. Disconnecting.");
			Disconnect();
			return;
		}
	}

	private void Disconnect()
	{
		Debug.Log("Disconnecting");
		m_Connection.Disconnect(m_Driver);
	}
}
