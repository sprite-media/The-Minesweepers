using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Unity.Collections;
using Unity.Networking.Transport;
using NetworkMessage;

public class NetworkServer : MonoBehaviour
{
	public static NetworkServer instance { get; private set; }
	public NetworkDriver m_Driver;
	private NativeList<NetworkConnection> m_Connections;
	private const float INTERVAL = 1.0f / 60.0f;
	private float timer = 0.0f;

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
		var endpoint = NetworkEndPoint.AnyIpv4;
		endpoint.Port = 12345;

		if (m_Driver.Bind(endpoint) != 0)
			Debug.Log("Failed to bind to port 12345");
		else
			m_Driver.Listen();

		m_Connections = new NativeList<NetworkConnection>(2, Allocator.Persistent);
	}
	private void Start()
	{
		InvokeRepeating("TimeCorrection", 1, INTERVAL);
	}
	private void Update()
	{
		m_Driver.ScheduleUpdate().Complete();
		timer += Time.deltaTime;

		NetworkConnection c;
		while ((c = m_Driver.Accept()) != default(NetworkConnection))
		{
			OnConnect();
		}

		DataStreamReader stream;
		for (int i = 0; i < m_Connections.Length; i++)
		{
			if (!m_Connections[i].IsCreated)
				Assert.IsTrue(true);

			NetworkEvent.Type cmd = m_Driver.PopEventForConnection(m_Connections[i], out stream);

			while (cmd != NetworkEvent.Type.Empty)
			{
				//When received data from client
				if (cmd == NetworkEvent.Type.Data)
				{
					OnData(stream, i);
				}
				else if (cmd == NetworkEvent.Type.Disconnect)
				{
					SendData(new DropInfo());
					m_Connections[i] = default(NetworkConnection);
					continue;
				}

				cmd = m_Driver.PopEventForConnection(m_Connections[i], out stream);
			}
		}
	}

	private void OnConnect()
	{
		if (m_Connections.Length >= 2)
			return;

		//TODO Check if number of player is 2 and start turn
	}

	private void OnData(DataStreamReader stream, int connectionIndex)
	{
		NativeArray<byte> bytes = new NativeArray<byte>(stream.Length, Allocator.Temp);
		stream.ReadBytes(bytes);
		string returnData = Encoding.ASCII.GetString(bytes.ToArray());

		NetworkHeader header = JsonUtility.FromJson<NetworkHeader>(returnData);
		 
		switch (header.cmd)
		{
			case Command.Click:
			{
				//TODO receive indices and GridManager.instance.ClickAt( received ) then send result
			}
			break;
			default:
				break;
		}
	}

	private void SendData(object data, NetworkConnection c)
	{
		if (c == default(NetworkConnection))
		{
			/*/
            Assert.IsTrue(true);
            /*/
			Debug.LogError("Invalid NetworkConnection. Exiting function.");
			return;
			//*/
		}
		var writer = m_Driver.BeginSend(NetworkPipeline.Null, c);
		string jsonString = JsonUtility.ToJson(data);
		NativeArray<byte> sendBytes = new NativeArray<byte>(Encoding.ASCII.GetBytes(jsonString), Allocator.Temp);
		writer.WriteBytes(sendBytes);
		m_Driver.EndSend(writer);
	}
	private void SendData(object data) // Overloaded version of SendData. Sending data to all connections
	{
		foreach (NetworkConnection c in m_Connections)
		{
			SendData(data, c);
		}
	}

	public void NotifyTurn(int playerIndex)
	{
		Turn turn = new Turn();
		turn.playerID = playerIndex;

		SendData(turn);
	}
	public void TimeCorrection()
	{
		Timer timer = new Timer();
		timer.timer = instance.timer;
		SendData(timer);
	}
	public void SendResult()//TODO receive result data(must be collection type)
	{

	}
}
