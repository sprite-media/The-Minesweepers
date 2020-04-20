using System.Text;
using System.Collections.Generic;
using System.Linq;
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
		CleanupConnection();

		timer += Time.deltaTime;

		NetworkConnection c;
		while ((c = m_Driver.Accept()) != default(NetworkConnection))
		{
			OnConnect(c);
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

				cmd = m_Driver.PopEventForConnection(m_Connections[i], out stream);
			}
		}
	}
	public void OnDestroy()
	{
		m_Driver.Dispose();
		m_Connections.Dispose();
	}

	private void CleanupConnection()
	{
		for (int i = 0; i < m_Connections.Length; i++)
		{
			if (!m_Connections[i].IsCreated)
			{
				SendData(new DropInfo());
				m_Connections.RemoveAtSwapBack(i);
				--i;
			}
		}
	}

	private void OnConnect(NetworkConnection c)
	{
		if (m_Connections.Length >= 2)
			return;
		Debug.Log("Connect");
		ConnectInfo connectInfo = new ConnectInfo();
		connectInfo.playerID = c.InternalId;
		SendData(connectInfo, c);
		m_Connections.Add(c);

		if (m_Connections.Length == 2)
		{
			StartGame();
			NotifyTurn(0);
		}
	}

	private void OnData(DataStreamReader stream, int connectionIndex)
	{
		NativeArray<byte> bytes = new NativeArray<byte>(stream.Length, Allocator.Temp);
		stream.ReadBytes(bytes);
		string returnData = Encoding.ASCII.GetString(bytes.ToArray());
		if (returnData == "heartbeat")
			return;

		NetworkHeader header = JsonUtility.FromJson<NetworkHeader>(returnData);
		 
		switch (header.cmd)
		{
			case Command.Click:
			{
				Debug.Log("Click");
				Click click = JsonUtility.FromJson<Click>(returnData);
				bool validClick = false;
				Result result = new Result();
				switch (click.mouse)
				{
					case 0:
						//TODO collect result
						validClick = GridManager.instance.ClickAt(click.index, ref result);
						break;
					case 1:
						//TODO collect result
						validClick = GridManager.instance.FlagAt(click.index, ref result);
						break;
					default:
						Debug.Log("Nothing happens");
						break;
				}
				SendResult(result);
				if (result.result[0].status == Cell.Status.MINE)
					Time.timeScale = 0.0f;

				if (validClick)
					NotifyTurn(((connectionIndex + 1) % 2));
				else
					NotifyTurn(connectionIndex);
			}
			break;
			case Command.Chat:
				Chat chat = JsonUtility.FromJson<Chat>(returnData);
				chat.chatMessage = chat.RemoveQuestionMark(chat.chatMessage);
				Debug.Log("Chat message : " + chat.chatMessage);
				SendData(chat);
				break;
			default:
				break;
		}
	}
	private void RemoveDuplicates(ref List<CellResult> result)
	{
		for (int i = 0; i < result.Count-1; i++)
		{
			for (int j = i + 1; j < result.Count; j++)
			{
				if (result[i].index == result[j].index)
				{
					result.RemoveAt(j);
					j--;
				}
			}
		}
	}

	private void SendData(object data, NetworkConnection c)
	{
		if ((c == default(NetworkConnection)) || !c.IsCreated)
		{
			/*/
            Assert.IsTrue(true);
            /*/
			Debug.LogError("Invalid NetworkConnection(" + c +"). Exiting function.");
			return;
			//*/
		}
		try
		{
			var writer = m_Driver.BeginSend(NetworkPipeline.Null, c);
			string jsonString = JsonUtility.ToJson(data);
			NativeArray<byte> sendBytes = new NativeArray<byte>(Encoding.ASCII.GetBytes(jsonString), Allocator.Temp);
			writer.WriteBytes(sendBytes);
			m_Driver.EndSend(writer);
		}
		catch
		{
			Debug.Log(data);
		}
	}
	private void SendData(object data) // Overloaded version of SendData. Sending data to all connections
	{
		foreach (NetworkConnection c in m_Connections)
		{
			SendData(data, c);
		}
	}

	public void StartGame()
	{
		SendData(new StartInfo());
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
	public void SendResult(Result result)
	{
		RemoveDuplicates(ref result.result);
		Debug.Log(result.ToString());
		List<Result> splitResult = new List<Result>();
		if (result.result.Count >= 20)
		{
			for (int i = 0; i < result.result.Count; i++)
			{
				if (i % 20 == 0)
				{
					splitResult.Add(new Result());
				}
				splitResult[i / 20].result.Add(result.result[i]);
			}
			foreach (Result r in splitResult)
			{
				SendData(r);
			}
		}
		else
			SendData(result);
	}
}
