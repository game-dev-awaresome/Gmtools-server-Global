using System;

namespace Common.Logic
{
	/// <summary>
	/// Packet ��ժҪ˵����
	/// </summary>
	public class Packet
	{
		/// <summary>
		/// ��Ϣ����󳤶�
		/// </summary>
		public int MAX_MESSAGE_SIZE = 128;
		/// <summary>
		/// �Ƿ��ǺϷ���Ϣ��
		/// </summary>
		public bool IsValidPacket = false;
		/// <summary>
		/// ��Ϣ��Byte����
		/// </summary>
		public byte[] m_PacketBuffer;
		/// <summary>
		/// ��Ϣ������
		/// </summary>
		public uint m_uiPacketLen;
		/// <summary>
		/// ��Ϣͷ
		/// </summary>
		public Packet_Head m_Head;
		/// <summary>
		/// ��Ϣ��
		/// </summary>
		public Packet_Body m_Body;

		public Packet()
		{
		}
		/// <summary>
		/// ������Ϣ��
		/// </summary>
		/// <param name="packet">��Ϣ��</param>
		/// <param name="packet_size">��Ϣ������</param>
		public Packet( byte[] packet , uint packet_size)
		{
			if (packet == null|| packet.Length < 16||packet.Length != packet_size)
				return;
			this.m_PacketBuffer = packet;
			this.m_uiPacketLen = packet_size;
			uint headlen = Packet_Head.HEAD_LENGTH,bodylen = this.m_uiPacketLen - Packet_Head.HEAD_LENGTH;
			
			byte[] head = new byte[headlen];
			System.Array.Copy(this.m_PacketBuffer,0,head,0,headlen);
			this.m_Head = new Packet_Head(head,headlen);
			
			byte[] body = new byte[bodylen];
			if (this.m_uiPacketLen > headlen)
				System.Array.Copy(this.m_PacketBuffer,headlen,body,0,bodylen);
			this.m_Body = new Packet_Body(body,bodylen);
			this.IsValidPacket = true;

		}
        /// <summary>
        /// ������Ϣ��
        /// </summary>
        /// <param name="head">��Ϣͷ</param>
        /// <param name="body">��Ϣ��</param>
		public Packet( Packet_Head head , Packet_Body body ) 
		{
			this.m_Head =  head;
			this.m_Body = body;
			byte[] headbuffer = this.m_Head.ToByteArray();
			byte[] bodybuffer = this.m_Body.ToByteArray();
			this.m_PacketBuffer = new byte[headbuffer.Length+bodybuffer.Length];
			headbuffer.CopyTo(this.m_PacketBuffer,0);
			bodybuffer.CopyTo(this.m_PacketBuffer,headbuffer.Length);
			this.IsValidPacket = true;
		}
		public byte[] ToByteArray() 
		{
			if (this.IsValidPacket)
				return this.m_PacketBuffer;
			else
				return new byte[0];
		}
		public override string ToString()
		{
			if (!this.IsValidPacket)
				return "Invalid Packet\r\n";
			else
				return "-------------Packet_Head----------\r\n"
					+ this.m_Head.ToString()
					+"-------------Packet_Head----------\r\n"
					+"-------------Packet_Body----------\r\n"
					+this.m_Body.ToString()
					+"-------------Packet_Body----------\r\n";
		}

	}
}
