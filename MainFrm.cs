using System.Text;
using Sunny.UI;
using TouchSocket.Core;
using TouchSocket.Sockets;
using TcpClient = TouchSocket.Sockets.TcpClient;

namespace AlbertTouchSocket
{
    public partial class MainFrm : UIForm
    {
        private TcpService _service = new TcpService();
        private TcpClient _tcpClient = new TcpClient();

        public MainFrm()
        {
            InitializeComponent();
            // �رտ��̼߳��
            Control.CheckForIllegalCrossThreadCalls = false;

            // ��ʼ������
            this.uiButtonEndServe.SetDisabled();
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButtonStartServe_Click(object sender, EventArgs e)
        {
            // ����һ�����׵� TcpClient
            CreateSimpleTcpServe();
        }

        private void PrintServeLog(string msg)
        {
            try
            {
                this.uiListBoxServe.Items.Insert(0, msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void PrintClinetLog(string msg)
        {
            try
            {
                this.uiListBoxClientLog.Items.Insert(0, msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// ����һ�����׵� TcpClient
        /// </summary>
        private void CreateSimpleTcpServe()
        {
            try
            {
                // �пͻ�����������
                _service.Connecting = (client, e) =>
                {
                    PrintServeLog($"�ͻ���{client.IP}:{client.Port}��������--{e.Message}");
                };
                // �пͻ��˳ɹ�����
                _service.Connected = (client, e) =>
                {
                    PrintServeLog($"�ͻ���{client.IP}:{client.Port}���ӳɹ�");
                };
                // �пͻ������ڶϿ����ӣ�ֻ�е������Ͽ�ʱ����Ч��
                _service.Disconnecting = (client, e) =>
                {
                    PrintServeLog($"�ͻ���{client.IP}:{client.Port}���ڶϿ�����--{e.Message}");
                };
                // �пͻ��˶Ͽ�����
                _service.Disconnected = (client, e) =>
                {
                    PrintServeLog($"�ͻ���{client.IP}:{client.Port}�Ͽ�����");
                };
                _service.Received = (client, byteBlock, requestInfo) =>
                {
                    //�ӿͻ����յ���Ϣ
                    string mes = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);//ע�⣺���ݳ�����byteBlock.Len
                    client.Logger.Info($"�Ѵ�{client.Id}���յ���Ϣ��{mes}");

                    client.Send(mes);//���յ�����Ϣֱ�ӷ��ظ����ͷ�
                    // client.Send("id",mes);//���յ�����Ϣ���ظ��ض�ID�Ŀͻ���
                    // �������һ��С�㲥����
                    var ids = _service.GetIds();
                    foreach (var clientId in ids)//���յ�����Ϣ���ظ����ߵ����пͻ��ˡ�
                    {
                        if (clientId != client.Id)//�����Լ���
                        {
                            _service.Send(clientId, mes);
                        }
                    }
                };

                _service.Setup(new TouchSocketConfig()//��������
                        .SetListenIPHosts("tcp://127.0.0.1:7000", 7001)//ͬʱ����������ַ
                        .ConfigureContainer(a =>//����������˳��Ӧ������ǰ��
                        {
                            a.AddConsoleLogger();//���һ������̨��־ע�루ע�⣺��maui�п���̨��־�����ã�
                        })
                        .ConfigurePlugins(a =>
                        {
                            //a.Add();//�˴�������Ӳ��
                        }))
                    .Start();//����
                PrintServeLog("Tcp �������������˿�Ϊ 7000��7001");
                this.uiButtonStartServe.SetDisabled();
                this.uiButtonEndServe.SetEnabled();
            }
            catch (Exception ex)
            {
                PrintServeLog(ex.Message);
            }
        }

        private void CreateClient()
        {
            _tcpClient.Connecting = (client, e) => { };//�������ӵ�����������ʱ�Ѿ�����socket�����ǻ�δ����tcp
            _tcpClient.Connected = (client, e) => { };//�ɹ����ӵ�������
            _tcpClient.Disconnecting = (client, e) => { };//�����ӷ������Ͽ����ӡ��˴��������Ͽ�����Ч��
            _tcpClient.Disconnected = (client, e) => { };//�ӷ������Ͽ����ӣ������Ӳ��ɹ�ʱ���ᴥ����
            _tcpClient.Received = (client, byteBlock, requestInfo) =>
            {
                //�ӷ������յ���Ϣ������һ��byteBlock��requestInfo��������������ֲ�ͬ��ֵ��
                string mes = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);
                _tcpClient.Logger.Info($"�ͻ��˽��յ���Ϣ��{mes}");
            };

            //��������
            _tcpClient.Setup(new TouchSocketConfig()
                .SetRemoteIPHost(this.uiipTextBoxIp.Text + ":" + this.uiTextBoxPort.Text)
                .ConfigureContainer(a =>
                {
                    // ������־��ʽ
                    a.SetSingletonLogger(new LoggerGroup(new FileLogger(), new EasyLogger(PrintClinetLog)));
                }));

            Result result = _tcpClient.TryConnect();//���߿��Ե���TryConnect
            if (result.IsSuccess())
            {
                _tcpClient.Logger.Info("�ͻ��˳ɹ�����");
            }
            else
            {
                _tcpClient.Logger.Info("�ͻ��˳ɹ�ʧ��");
            }
        }

        #region �����л�
        private void ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.Blue;
        }

        private void ʱ�к�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.Black;
        }

        private void ����ɫToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.DarkBlue;
        }
        #endregion

        /// <summary>
        /// ��߶Ͽ����񣬱��������Ƴ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButtonEndServe_Click(object sender, EventArgs e)
        {
            foreach (var item in _service.Monitors)
            {
                _service.RemoveListen(item);
            }
            PrintServeLog("Tcp �����ѹر�");
            this.uiButtonEndServe.SetDisabled();
            this.uiButtonStartServe.SetEnabled();
        }

        private void uiButtonSend_Click(object sender, EventArgs e)
        {
            _tcpClient.Send(this.uiRichTextBoxClient.Text);
        }

        private void uiButtonClientConnect_Click(object sender, EventArgs e)
        {
            CreateClient();
        }
    }
}