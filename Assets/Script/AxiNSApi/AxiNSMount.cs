#if UNITY_SWITCH
using nn.account;
#endif
public class AxiNSMount
{
    static bool bInMount = false;
    internal static string m_SaveMountName;
    static bool bInSdCardMount = false;
    internal static string m_SdCardMountName;
	static bool bInSdCardDebugMount = false;
	internal static string m_SdCardDebugMountName;

	public bool SaveIsMount => bInMount;
    public string SaveMountName
    {
        get
        {
            if (!bInMount)
                return string.Empty;
            else
                return m_SaveMountName;
        }
    }

#if UNITY_SWITCH
	public bool MountSave(Uid userId, string mountName = "save")
	{
		if (bInMount)
			return true;

		if (!nn.fs.SaveData.IsExisting(userId))
		{
			UnityEngine.Debug.LogError($"{userId.ToString()}�浵�����ڣ�");
			return false;
		}
		UnityEngine.Debug.Log($"{userId.ToString()}�浵ȷ�����ڣ�");

		nn.Result result;
		result = nn.fs.SaveData.Mount(mountName, userId);
		//result.abortUnlessSuccess();

		if (!result.IsSuccess())
		{
			UnityEngine.Debug.LogError($"MountSave->����{mountName}:/ ʧ��: " + result.ToString());
			return false;
		}
		UnityEngine.Debug.Log($"MountSave->����{mountName}:/ �ɹ� ");
		m_SaveMountName = mountName;
		bInMount = true;
		return true;
	}
#endif

	public bool MountSDForDebug(string mountName = "dbgsd")
    {
#if !UNITY_SWITCH
        return false;
#else
        if (bInSdCardDebugMount)
            return true;
        nn.Result result;
        result = nn.fs.SdCard.MountForDebug(mountName);
        //result.abortUnlessSuccess();
        if (!result.IsSuccess())
        {
            UnityEngine.Debug.LogError($"nn_fs_MountSdCardForDebug->����{mountName}:/ ʧ��: " + result.ToString());
            return false;
        }
        UnityEngine.Debug.Log($"nn_fs_MountSdCardForDebug->����{mountName}:/ �ɹ� ");
		m_SdCardDebugMountName = mountName;
		bInSdCardDebugMount = true;
        return true;
#endif
    }
    public bool MountSD(string mountName = "sd")
    {
#if !UNITY_SWITCH
        return false;
#else
        if (bInSdCardMount)
            return true;
        nn.Result result;
        result = AxiNSSDCard.Mount(mountName);
        if (!result.IsSuccess())
        {
            UnityEngine.Debug.LogError($"nn_fs_MountSdCard->����{mountName}:/ ʧ��: " + result.ToString());
            return false;
        }
        UnityEngine.Debug.Log($"nn_fs_MountSdCard->����{mountName}:/ �ɹ� ");
        m_SdCardMountName = mountName;
        bInSdCardMount = true;
        return true;
#endif
    }
    public void UnmountSave()
    {
#if UNITY_SWITCH
        if (!bInMount)
        {
            UnityEngine.Debug.LogError($"{m_SaveMountName}:/ û�б����أ�����ж��");
            return;
        }
        nn.fs.FileSystem.Unmount(m_SaveMountName);
        UnityEngine.Debug.LogError($"UnmountSaveForDebufa->��ж��{m_SaveMountName}:/ ");
        bInMount = false;
#endif
    }
    public void UnmountSDCardForDebug()
    {
#if UNITY_SWITCH
        if (!bInSdCardDebugMount)
        {
            UnityEngine.Debug.LogError($"{m_SdCardDebugMountName}:/ û�б����أ�����ж��");
            return;
        }
        nn.fs.FileSystem.Unmount(m_SdCardDebugMountName);
        UnityEngine.Debug.LogError($"UnmountSDCardForDebug->��ж��{m_SdCardDebugMountName}:/ ");
		bInSdCardDebugMount = false;
#endif
    }
	public void UnmountSDCard()
	{
#if UNITY_SWITCH
		if (!bInSdCardMount)
		{
			UnityEngine.Debug.LogError($"{m_SdCardMountName}:/ û�б����أ�����ж��");
			return;
		}
		nn.fs.FileSystem.Unmount(m_SdCardMountName);
		UnityEngine.Debug.LogError($"UnmountSDCard->��ж��{m_SdCardMountName}:/ ");
		bInSdCardMount = false;
#endif
	}
}