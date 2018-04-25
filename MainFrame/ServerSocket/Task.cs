using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using lg = Common.API.LanguageAPI;
namespace GMSERVER.ServerSocket
{
	
	/// <summary>
	/// task ��ժҪ˵����
	/// </summary>
	public class Task
	{
		ConnectionPool ConnectionPool = null;
		TaskManager ClientTask  = null;
		TcpListener listener = null;
		public Task(int i)
		{
			
		}
		public Task()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��ʼ���̵߳�����
		/// </summary>
		public int initialize()
		{
			IPAddress ipAddress = Dns.Resolve(Dns.GetHostName()).AddressList[0];
			IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, 10116);
        
			//�̳߳�
			ConnectionPool = new ConnectionPool()  ;    
            //���̳߳ش���һ���߳�
			ClientTask = new TaskManager(ConnectionPool) ;
			//��һ���߳�
			ClientTask.Start() ;
    
			listener = new TcpListener(ipLocalEndPoint);
			try 
			{
				//Ϊÿ�����ӹ����ͻ�����һ������ͨ��
				listener.Start();
				Console.WriteLine("Waiting for a sender client connection...");
			}
			catch (Exception e) 
			{
				Console.WriteLine(e.ToString());
			}
        
			Console.WriteLine("\n" + lg.ServerSocket_Task_Continue);
			Console.Read();
			return 1;
		}
		/// <summary>
		/// �̴߳����Ժ�,������̵߳Ĵ�������
		/// </summary>
		public void process()
		{
			int TestingCycle = 5 ; // Number of testing cycles
			int ClientNbr = 0 ;
			try
			{
				while ( TestingCycle > 0 ) 
				{
                    //���ܿͻ�����������
					TcpClient handler = listener.AcceptTcpClient();
                    //�õ��ͻ������Ӿ��
					if (  handler != null)  
					{
						Console.WriteLine(lg.ServerSocket_Task_Query+"#{0}!", ++ClientNbr) ;
                                
						// һ������������Ĵ���
						ConnectionPool.Enqueue( new Handler(handler) ) ;
                                
						--TestingCycle ;
					}
					else 
						break;                
				}
			}
			catch(SocketException ex)
			{
				Console.WriteLine(ex.Message);
			}
			listener.Stop();
              
			// Stop client requests handling
			ClientTask.Stop() ;
		}
	}
}
