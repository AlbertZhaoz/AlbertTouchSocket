using System.Text;
using System.Windows.Forms;
using Furion.Logging.Extensions;
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
                    PrintServeLog($"�Ѵ�{client.Id}���յ���Ϣ��{mes}");


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
                        .SetListenIPHosts("tcp://127.0.0.1:7222", 7223)//ͬʱ����������ַ
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
                "�ͻ��˳ɹ�����".LogInformation();
            }
            else
            {
                _tcpClient.Logger.Info("�ͻ��˳ɹ�ʧ��");
                "�ͻ��˳ɹ�ʧ��".LogError();
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

        private void uiListBoxClientLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            uiListBoxClientLog.SelectedIndex = 0;
        }

        private void uiButton_Plc_Click(object sender, EventArgs e)
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
                .SetRemoteIPHost("192.168.1.119:9100")
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

        private void uiButton1_Click(object sender, EventArgs e)
        {

            _tcpClient.Send("~JA");

            var test = @"^XA
~TA000
~JSN
^LT0
^MNW
^MTT
^PON
^PMN
^LH30,30
^JMA
^PR4,4
~SD28
^JUS
^LRN
^CI27
^PA0,1,1,0
^XZ
^XA
^MMT
^PW508
^LL236
^LS0
^FT373,212^BQN,2,5
^FH\^FDLA,20230926044438^FS
^FO180,81^GFA,537,840,20,:Z64:eJzFkbGK20AQhn9pDRt04HVzh4tgGZVp4tLV2YUfRI+g7q4IeDs3gbzB4dL4CQwpso8yxs1xxaFUcSE0md31msSkz6IRq0///Du/RABxv8MP3uLzmwWzRYPsyNxlzGes2SlmQouMmFn2vVRr/Luo86zzTEvhHHUb0YmmSzoC9MZioh2+TIDn4AcoYUMDPAqbJ522MCUwCUyAXLnXDRFqjn+tqqqmeVXlokWsKZ5+PZHURjwfYs0kH/v5vskZ4m/CGfLsc2xu2OmWyXwnkvnE68ok78lB/eXXBqYvveWV2ZDXDFO2Jvb6+XRif/gZiz71pvnkG3dXvwuTb39Ofl7n55N/1ya/lFcyUWBLYDlYrQbAAPcruf3XVWynH4ptscv3sNN8F1hJzbhsTac5s7U5B7am+WzRml71ytEisgYf65Gk0cgcjaLf2g5pQXD6XR2pfA1s4bQrW2F99pPK2Fu0yt4d4NRefa/zQ2xuFDAOfmUjG78+kbKF6PRevdTF4TKLQvDj7Gsd/H4DJ/jiJA==:1E6E
^FO24,97^A0N,17,18^FB111,1,4,L^FH\^CI28^FDLineID^FS^CI27
^FO24,131^A0N,17,18^FB111,1,4,L^FH\^CI28^FDSerialNumber^FS^CI27
^SLS,1
^FT24,177^A0N,17,18
^FC%,{,#
^FH\^CI28^FD%d/%m/%Y^FS^CI27
^FO24,198^A0N,17,18^FB111,1,4,L^FH\^CI28^FDCountryOfOrigin^FS^CI27
^SLS,1
^FT146,177^A0N,17,18
^FC%,{,#
^FH\^CI28^FD%H:%M^FS^CI27
^FO24,28^A0N,17,18^FB154,1,4,L^FH\^CI28^FDMbItemNumber^FS^CI27
^FO185,28^A0N,17,18^FB154,1,4,L^FH\^CI28^FDZgs^FS^CI27
^PQ1,,,Y
^XZ
";

            _tcpClient.Send(test);
        }
    }
}