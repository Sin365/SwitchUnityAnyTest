#if UNITY_SWITCH
using nn.account;
#endif

public class AxiNS
{
	static AxiNS _instance;
	public static AxiNS instance
	{
		get
		{
			if (_instance == null)
				_instance = new AxiNS();
			return _instance;
		}
	}

	public AxiNSUser user;
	public AxiNSMount mount;
	public AxiNSIO io;
	public AxiNSWaitHandle wait;
	AxiNS()
	{
		user = new AxiNSUser();
		mount = new AxiNSMount();
		io = new AxiNSIO();
		wait = new AxiNSWaitHandle();
	}

	/// <summary>
	/// ��ʼ�����������Ŀ��һʱ���ʼ������֤�ȳ�ʼ����ʹ��ĳЩ�������Ų����ˣ�
	/// </summary>
	public void Init()
	{
#if UNITY_SWITCH
		if (!user.GetUserID(out Uid uid))
			return;
		mount.MountSave(uid);
#endif
	}
}
