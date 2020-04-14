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
		//Invoke heart beat
	}
	private void Update()
	{
		m_Driver.ScheduleUpdate().Complete();


	}
}
