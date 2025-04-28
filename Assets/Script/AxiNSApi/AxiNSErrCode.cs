public static class AxiNSErrCode
{
#if UNITY_SWITCH
	public static string GetErrorInfo(this nn.Result result)
	{
		if (result.IsSuccess())
			return "NoErr";
		return GetErrorDetails(result.GetModule(), result.GetDescription());
	}
#endif
	/// <summary>
	/// ����ģ�� ID ������ ID ���������� Switch ������ĺ��塢����ԭ�򼰽���취��
	/// </summary>
	/// <param name="moduleId">ģ�� ID</param>
	/// <param name="descriptionId">���� ID</param>
	/// <returns>���������롢���塢����ԭ�򼰽���취���ַ���</returns>
	public static string GetErrorDetails(int moduleId, int descriptionId)
	{
		string errorCode = $"2{moduleId:D3}-{descriptionId:D4}"; // ��ʽ��Ϊ 2XXX-YYYY
		string meaning = "δ֪����";
		string causeAndSolution = "δ֪����������־����ϵ������֧�֡�";

		switch (moduleId)
		{
			case 2: // nn::fs (�ļ�ϵͳ)
				switch (descriptionId)
				{
					case 1:
						meaning = "ResultPathNotFound";
						causeAndSolution = "·��δ�ҵ������·���Ƿ���ȷ��ȷ����Ŀ¼���ڡ�ʹ�� nn.fs.Directory.Create ������Ŀ¼��";
						break;
					case 2:
						meaning = "ResultPermissionDenied";
						causeAndSolution = "Ȩ�ޱ��ܾ��������� Atmosphere ������ save:/ ��дȨ�ޡ����Ե��� Atmosphere ���û�ʹ�� sd:/ ���ص㡣";
						break;
					case 3:
						meaning = "ResultPathAlreadyExists";
						causeAndSolution = "·���Ѵ��ڡ����·���Ƿ�ռ�ã�ɾ���������������ļ�/Ŀ¼��";
						break;
					case 5:
						meaning = "ResultTargetLocked";
						causeAndSolution = "Ŀ�걻�������������ļ�����ʹ���У��ر���س�������ԡ�";
						break;
					case 7:
						meaning = "ResultTargetNotFound";
						causeAndSolution = "Ŀ��δ�ҵ���ȷ��Ŀ���ļ�/Ŀ¼�Ƿ���ڣ����·��ƴд��";
						break;
				}
				break;

			case 5: // nn::err (������)
				switch (descriptionId)
				{
					case 3:
						meaning = "microSD ����ش���";
						causeAndSolution = "�޷���������������� microSD ���𻵡��Ƴ� microSD �������²��룬���������";
						break;
				}
				break;

			case 16: // nn::oe (����ϵͳ����)
				switch (descriptionId)
				{
					case 247:
						meaning = "microSD ���洢����";
						causeAndSolution = "microSD ���𻵻򲻼��ݡ����� microSD ������Ĭ�ϴ洢λ������Ϊϵͳ�ڴ档";
						break;
					case 390:
						meaning = "��֧�ֵ� microSD ��";
						causeAndSolution = "microSD ����ʽ����֧�֡���ʽ��Ϊ exFAT �� FAT32 �����ԡ�";
						break;
					case 601:
						meaning = "microSD ��������";
						causeAndSolution = "microSD �������𻵡��������ݺ��ʽ������������¿���";
						break;
				}
				break;

			case 21: // nn::settings (����)
				switch (descriptionId)
				{
					case 3:
						meaning = "ϵͳ���δ���»���";
						causeAndSolution = "ϵͳ�̼��汾���ɻ��𻵡�����ϵͳ�̼��������°�װϵͳ��";
						break;
				}
				break;

			case 101: // nn::fssrv (�ļ�ϵͳ����)
				switch (descriptionId)
				{
					case 1:
						meaning = "ϵͳ����";
						causeAndSolution = "ͨ��ϵͳ�������� Switch���������������ϵ������֧�֡�";
						break;
					case 2:
						meaning = "�̼���";
						causeAndSolution = "ϵͳ�̼��𻵡����Ը���ϵͳ����ָ��������ã�ע�ⱸ�����ݣ���";
						break;
				}
				break;

			case 107: // nn::nim (���簲װ����)
				switch (descriptionId)
				{
					case 427:
						meaning = "�̼���";
						causeAndSolution = "ϵͳ�̼��𻵡�����ϵͳ�̼��������°�װϵͳ��";
						break;
					case 445:
						meaning = "Ӳ���𻵻��������";
						causeAndSolution = "������Ӳ���𻵻���ڵ������ݡ�ɾ���������ݣ����� Switch������Ч��ϵ������֧�֡�";
						break;
				}
				break;

			case 110: // nn::socket (�����׽���)
				switch (descriptionId)
				{
					case 1000:
						meaning = "��������ʧ��";
						causeAndSolution = "�޷����ӵ����硣����������ӣ�����·�����������л�����������Ƶ�Σ�2.4GHz/5GHz����";
						break;
					case 2003:
						meaning = "������������ʧ��";
						causeAndSolution = "�����ź������ȶ�������·�������Ƴ������������·������";
						break;
					case 2004:
						meaning = "�������ò�֧��";
						causeAndSolution = "���簲ȫ���Ͳ���֧�֡�Switch ֧�� WEP/WPA/WPA2������·�������ú����ԡ�";
						break;
					case 2091:
						meaning = "������������ʧ��";
						causeAndSolution = "�����������⡣��������Ƿ��ã����� Switch ��·������";
						break;
					case 3127:
						meaning = "��������ʧ��";
						causeAndSolution = "���粻�ȶ��������������ӣ�����·����������ϵ�����ṩ�̡�";
						break;
				}
				break;

			case 115: // nn::mii (Mii ���)
				switch (descriptionId)
				{
					case 96:
						meaning = "Mii ���ݴ���";
						causeAndSolution = "Mii �����𻵡�ɾ�������´��� Mii�������ϵͳ��";
						break;
				}
				break;

			case 139: // nn::nfp (NFC/Amiibo)
				switch (descriptionId)
				{
					case 6:
						meaning = "Amiibo ��ȡ����";
						causeAndSolution = "Amiibo ��ȡʧ�ܡ���� Amiibo �Ƿ��𻵣�����ϵͳ���������� Amiibo��";
						break;
				}
				break;

			case 153: // nn::ir (��������ͷ)
				switch (descriptionId)
				{
					case 321:
						meaning = "��������ͷ��ȡ����";
						causeAndSolution = "��������ͷ�޷���ȡ���������ͷ�Ƿ��ڵ�����ྵͷ������ϵ������֧�֡�";
						break;
					case 1540:
						meaning = "��������ͷӲ������";
						causeAndSolution = "��������ͷӲ�����ϡ���ϵ������֧�ֽ���ά�ޡ�";
						break;
				}
				break;

			case 155: // nn::account (�˻�����)
				switch (descriptionId)
				{
					case 8006:
						meaning = "�޷������������˻�";
						causeAndSolution = "�������������жϡ�����������ӣ��Ժ����ԣ���鿴����������״̬ҳ�档";
						break;
				}
				break;

			case 160: // nn::online (���߷���)
				switch (descriptionId)
				{
					case 103:
						meaning = "�޷���������ƥ��";
						causeAndSolution = "���粻�ȶ������� Switch������������ӣ����Ժ����ԡ�";
						break;
					case 202:
						meaning = "�޷����ӵ����߷���";
						causeAndSolution = "�����������ά�����鿴����������״̬ҳ�棬�Ժ����ԡ�";
						break;
					case 8006:
						meaning = "���Ӳ���ʧ��";
						causeAndSolution = "�������⡣����·����������������ã������������硣";
						break;
				}
				break;

			case 162: // nn::application (Ӧ�ó���)
				switch (descriptionId)
				{
					case 2:
						meaning = "�������";
						causeAndSolution = "��������������ǵ������ݻ�̼����⡣ɾ���������ݣ�����ϵͳ�������°�װ�����";
						break;
					case 101:
						meaning = "�����Ҫ����";
						causeAndSolution = "��Ϸ�������Ҫ���¡����������²���װ��";
						break;
				}
				break;

			case 168: // nn::sys (ϵͳ)
				switch (descriptionId)
				{
					case 0:
						meaning = "��Ҫ�������";
						causeAndSolution = "�����Ҫ���¡�������Ϸ��ϵͳ�̼���";
						break;
					case 2:
						meaning = "ϵͳ����";
						causeAndSolution = "ϵͳ������������Ӳ�����⡣���� Switch������Ч��ϵ������֧�֡�";
						break;
				}
				break;

			case 205: // nn::camera (����ͷ)
				switch (descriptionId)
				{
					case 123:
						meaning = "����ͷ��ȡ����";
						causeAndSolution = "����ͷ�޷���ȡ���������ͷ�Ƿ��ڵ�����ྵͷ������ϵ������֧�֡�";
						break;
				}
				break;

			case 306: // nn::ngc (������Ϸ����)
				switch (descriptionId)
				{
					case 501:
						meaning = "�޷���������ƥ��";
						causeAndSolution = "���������жϡ����� Switch������������ӣ����Ժ����ԡ�";
						break;
					case 502:
						meaning = "ƥ�����ʧ��";
						causeAndSolution = "���粻�ȶ��������������ӣ�����·����������ϵ�����ṩ�̡�";
						break;
					case 820:
						meaning = "���߷��񲻿���";
						causeAndSolution = "�����������ά�����鿴����������״̬ҳ�棬�Ժ����ԡ�";
						break;
				}
				break;

			case 613: // nn::eShop (eShop)
				switch (descriptionId)
				{
					case 1400:
						meaning = "�޷�ʹ�����ÿ�����";
						causeAndSolution = "���ÿ���Ϣ����� eShop �������⡣������ÿ���Ϣ���Ժ����ԣ�����ϵ������֧�֡�";
						break;
					case 6838:
						meaning = "eShop ����ʧ��";
						causeAndSolution = "��������� eShop ά��������������ӣ��鿴����������״̬ҳ�棬�Ժ����ԡ�";
						break;
				}
				break;

			case 618: // nn::ngc (������Ϸ����)
				switch (descriptionId)
				{
					case 6:
						meaning = "�޷���������ƥ��";
						causeAndSolution = "���粻�ȶ������� Switch������������ӣ����Ժ����ԡ�";
						break;
					case 201:
						meaning = "ƥ������";
						causeAndSolution = "���Լ����ƥ���������Ժ����ԣ����������ƥ�䡣";
						break;
					case 501:
						meaning = "ƥ�����ʧ��";
						causeAndSolution = "���������жϡ����� Switch������������ӣ����Ժ����ԡ�";
						break;
				}
				break;

			case 801: // nn::sns (�罻�������)
				switch (descriptionId)
				{
					case 7002:
						meaning = "�޷��ϴ���ͼ�� Twitter";
						causeAndSolution = "�����������ά�����鿴����������״̬ҳ�棬�Ժ����ԡ�";
						break;
					case 7199:
						meaning = "�޷��ϴ���Ƭ�� Facebook";
						causeAndSolution = "�����������ά�����鿴����������״̬ҳ�棬�Ժ����ԡ�";
						break;
				}
				break;

			case 810: // nn::account (�˻�����)
				switch (descriptionId)
				{
					case 1224:
						meaning = "�޷���¼�������˻�";
						causeAndSolution = "�������������жϡ�����������ӣ��Ժ����ԣ���鿴����������״̬ҳ�档";
						break;
					case 1500:
						meaning = "�޷���¼ Facebook �˻�";
						causeAndSolution = "�����������ά�������� Switch���Ժ����ԡ�";
						break;
				}
				break;

			case 811: // nn::account (�˻�����)
				switch (descriptionId)
				{
					case 1006:
						meaning = "�޷������������˻�";
						causeAndSolution = "��������� DNS ���󡣼���������ӣ����Ը��� DNS���� 8.8.8.8�������Ժ����ԡ�";
						break;
					case 5001:
						meaning = "�޷����� eShop";
						causeAndSolution = "eShop �����жϡ��鿴����������״̬ҳ�棬�Ժ����ԡ�";
						break;
				}
				break;

			case 813: // nn::eShop (eShop)
				switch (descriptionId)
				{
					case 0:
						meaning = "eShop ����ʧ��";
						causeAndSolution = "eShop �����жϡ��鿴����������״̬ҳ�棬�Ժ����ԡ�";
						break;
					case 2470:
						meaning = "���״���ʧ��";
						causeAndSolution = "���ÿ���Ϣ����� eShop �������⡣������ÿ���Ϣ���Ժ����ԣ�����ϵ������֧�֡�";
						break;
				}
				break;

			case 819: // nn::online (���߷���)
				switch (descriptionId)
				{
					case 3:
						meaning = "�������ͣ";
						causeAndSolution = "ͬһ�˻�����һ̨�豸��ʹ�á��ر������豸�ϵ��������ʹ�ò�ͬ�˻���";
						break;
				}
				break;

			default:
				meaning = "δ֪ģ��";
				causeAndSolution = "δʶ���ģ�� ID��������־����ϵ������֧�֡�";
				break;
		}

		return $"������: {errorCode}\n����: {meaning}\n����ԭ�򼰽���취: {causeAndSolution}";
	}
}
