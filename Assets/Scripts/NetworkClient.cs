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
	public void OnDestroy()
	{
		m_Driver.Dispose();
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
				case Command.Start:
					StartInfo startInfo = JsonUtility.FromJson<StartInfo>(returnData);
					GameManager.instance.CreatePlayer();
					break;
				case Command.Connect:
					ConnectInfo connectInfo = JsonUtility.FromJson<ConnectInfo>(returnData);
					ID = connectInfo.playerID;
					Debug.Log(ID);
					break;
				case Command.Drop:
					DropInfo dropInfo = JsonUtility.FromJson<DropInfo>(returnData);
					//TODO Game end
					break;
				case Command.Turn:
					Turn turn = JsonUtility.FromJson<Turn>(returnData);
					GameManager.instance.Turn(turn.playerID == ID);
					break;
				case Command.Result:
					Result result = JsonUtility.FromJson<Result>(returnData);
					//TODO Send result to local(client side) grid manager and reflect the result(visually)
					break;
				case Command.Timer:
					Timer timer = JsonUtility.FromJson<Timer>(returnData);
					//TODO send server timer to timer script
					break;
				case Command.Chat:
					Chat chat = JsonUtility.FromJson<Chat>(returnData);
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

	private void SendData(object data)
	{
		var writer = m_Driver.BeginSend(m_Connection);
		NativeArray<byte> sendBytes = new NativeArray<byte>(Encoding.ASCII.GetBytes(JsonUtility.ToJson(data)), Allocator.Temp);
		writer.WriteBytes(sendBytes);
		m_Driver.EndSend(writer);
	}
	public void SendInput(Vector2Int index, int mouse)
	{
		Click click = new Click(index, mouse);
		SendData(click);
	}
}
