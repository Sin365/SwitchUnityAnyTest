#if UNITY_SWITCH
using nn.account;
#endif

public class AxiNSUser
{
	bool m_bInit = false;
	bool m_bGotOpenPreselectedUser = false;

#if UNITY_SWITCH
	Uid m_UserId;
	nn.account.UserHandle mUserHandle;
	nn.account.Nickname m_NickName;
#endif

	#region ���⹫���ӿڣ�ȷ���ڲ���ȫ�����������

#if UNITY_SWITCH
	public bool GetUserID(out Uid uid)
	{
		InitPreselectedUserInfo();
		if (!m_bGotOpenPreselectedUser)
		{
			uid = Uid.Invalid;
			return false;
		}
		uid = m_UserId;
		return true;
	}
#endif
	public bool GetNickName(out string NickName)
	{
#if !UNITY_SWITCH
		NickName = "";
		return true;
#else
		InitPreselectedUserInfo();
		if (!m_bGotOpenPreselectedUser)
		{
			NickName = string.Empty;
			return false;
		}
		NickName = m_NickName.ToString();
		return true;
#endif
	}
#endregion
	/// <summary>
	/// ��ʼ��Accountģ���
	/// </summary>
	void InitNSAccount()
    {
#if UNITY_SWITCH
		if (m_bInit)
			return;
		//�����ȳ�ʼ��NS��Account ��Ȼ���ü���
		nn.account.Account.Initialize();
		m_bInit = true;
#endif
	}

	/// <summary>
	/// ��ȡԤѡ�û�
	/// </summary>
	void InitPreselectedUserInfo()
    {
#if UNITY_SWITCH
		if (m_bGotOpenPreselectedUser)
			return;

		InitNSAccount();
		nn.Result result;
		mUserHandle = new nn.account.UserHandle();
		if (!nn.account.Account.TryOpenPreselectedUser(ref mUserHandle))
		{
			UnityEngine.Debug.LogError("��Ԥѡ���û�ʧ��.");
			return;
		}
		UnityEngine.Debug.Log("��Ԥѡ�û��ɹ�.");
		result = nn.account.Account.GetUserId(ref m_UserId, mUserHandle);
		//result.abortUnlessSuccess();
		if (!result.IsSuccess())
		{
			UnityEngine.Debug.LogError($"GetUserIdʧ��: {result.ToString()}");
			return;
		}

		if (m_UserId == Uid.Invalid)
		{
			UnityEngine.Debug.LogError("�޷���ȡ�û� ID");
			return;
		}
		UnityEngine.Debug.Log($"��ȡ�û� ID:{m_UserId.ToString()}");

		result = nn.account.Account.GetNickname(ref m_NickName, m_UserId);
		//result.abortUnlessSuccess();
		if (!result.IsSuccess())
		{
			UnityEngine.Debug.LogError($"GetNicknameʧ��: {result.ToString()}");
			return;
		}
		UnityEngine.Debug.Log($"��ȡ�û� NickName ID:{m_NickName.ToString()}");
		m_bGotOpenPreselectedUser = true;
#endif
	}
}
