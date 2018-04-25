using System;

namespace GMSERVER.ServerSocket
{
	/// <summary>
	/// ConnectionPool ��ժҪ˵����
	/// </summary>
	public class ConnectionPool
	{
		// ����һ��ͬ����Queue����
		private  System.Collections.Queue SyncdQ = System.Collections.Queue.Synchronized( new Queue() );
        /// <summary>
        /// ���ͻ������Ӿ����ӵ�Queue�Ľ�β��
        /// </summary>
        /// <param name="client">�ͻ������Ӿ��</param>
		public  void Enqueue(Handler client) 
		{
			SyncdQ.Enqueue(client) ;
		}
        /// <summary>
        /// ���ͻ������Ӿ���Ƴ�������λ�� Queue ��ʼ���ġ�
        /// </summary>
        /// <returns>���Ӿ��</returns>
		public Handler Dequeue() 
		{
			return (Handler) ( SyncdQ.Dequeue() ) ;
		}
        
		public  int Count 
		{
			get { return SyncdQ.Count ; }
		}

		public object SyncRoot 
		{
			get { return SyncdQ.SyncRoot ; }
		}
	}
        

}
