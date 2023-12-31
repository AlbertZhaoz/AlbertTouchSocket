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
            // 关闭跨线程检测
            Control.CheckForIllegalCrossThreadCalls = false;

            // 初始化操作
            this.uiButtonEndServe.SetDisabled();
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButtonStartServe_Click(object sender, EventArgs e)
        {
            // 启动一个简易的 TcpClient
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
        /// 启动一个简易的 TcpClient
        /// </summary>
        private void CreateSimpleTcpServe()
        {
            try
            {
                // 有客户端正在连接
                _service.Connecting = (client, e) =>
                {
                    PrintServeLog($"客户端{client.IP}:{client.Port}正在连接--{e.Message}");
                };
                // 有客户端成功连接
                _service.Connected = (client, e) =>
                {
                    PrintServeLog($"客户端{client.IP}:{client.Port}连接成功");
                };
                // 有客户端正在断开连接，只有当主动断开时才有效。
                _service.Disconnecting = (client, e) =>
                {
                    PrintServeLog($"客户端{client.IP}:{client.Port}正在断开连接--{e.Message}");
                };
                // 有客户端断开连接
                _service.Disconnected = (client, e) =>
                {
                    PrintServeLog($"客户端{client.IP}:{client.Port}断开连接");
                };
                _service.Received = (client, byteBlock, requestInfo) =>
                {
                    //从客户端收到信息
                    string mes = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);//注意：数据长度是byteBlock.Len
                    PrintServeLog($"已从{client.Id}接收到信息：{mes}");


                    client.Send(mes);//将收到的信息直接返回给发送方
                    // client.Send("id",mes);//将收到的信息返回给特定ID的客户端
                    // 这边做了一个小广播功能
                    var ids = _service.GetIds();
                    foreach (var clientId in ids)//将收到的信息返回给在线的所有客户端。
                    {
                        if (clientId != client.Id)//不给自己发
                        {
                            _service.Send(clientId, mes);
                        }
                    }
                };

                _service.Setup(new TouchSocketConfig()//载入配置
                        .SetListenIPHosts("tcp://127.0.0.1:7222", 7223)//同时监听两个地址
                        .ConfigureContainer(a =>//容器的配置顺序应该在最前面
                        {
                            a.AddConsoleLogger();//添加一个控制台日志注入（注意：在maui中控制台日志不可用）
                        })
                        .ConfigurePlugins(a =>
                        {
                            //a.Add();//此处可以添加插件
                        }))
                    .Start();//启动
                PrintServeLog("Tcp 服务已启动，端口为 7000、7001");
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
            _tcpClient.Connecting = (client, e) => { };//即将连接到服务器，此时已经创建socket，但是还未建立tcp
            _tcpClient.Connected = (client, e) => { };//成功连接到服务器
            _tcpClient.Disconnecting = (client, e) => { };//即将从服务器断开连接。此处仅主动断开才有效。
            _tcpClient.Disconnected = (client, e) => { };//从服务器断开连接，当连接不成功时不会触发。
            _tcpClient.Received = (client, byteBlock, requestInfo) =>
            {
                //从服务器收到信息。但是一般byteBlock和requestInfo会根据适配器呈现不同的值。
                string mes = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);
                _tcpClient.Logger.Info($"客户端接收到信息：{mes}");
            };

            //载入配置
            _tcpClient.Setup(new TouchSocketConfig()
                .SetRemoteIPHost(this.uiipTextBoxIp.Text + ":" + this.uiTextBoxPort.Text)
                .ConfigureContainer(a =>
                {
                    // 两种日志方式
                    a.SetSingletonLogger(new LoggerGroup(new FileLogger(), new EasyLogger(PrintClinetLog)));
                }));

            Result result = _tcpClient.TryConnect();//或者可以调用TryConnect
            if (result.IsSuccess())
            {
                _tcpClient.Logger.Info("客户端成功连接");
                "客户端成功连接".LogInformation();
            }
            else
            {
                _tcpClient.Logger.Info("客户端成功失败");
                "客户端成功失败".LogError();
            }
        }

        #region 主题切换
        private void 经典蓝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.Blue;
        }

        private void 时尚黑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.Black;
        }

        private void 蓝灰色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.DarkBlue;
        }
        #endregion

        /// <summary>
        /// 这边断开服务，本质上是移除监听即可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButtonEndServe_Click(object sender, EventArgs e)
        {
            foreach (var item in _service.Monitors)
            {
                _service.RemoveListen(item);
            }
            PrintServeLog("Tcp 服务已关闭");
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
            _tcpClient.Connecting = (client, e) => { };//即将连接到服务器，此时已经创建socket，但是还未建立tcp
            _tcpClient.Connected = (client, e) => { };//成功连接到服务器
            _tcpClient.Disconnecting = (client, e) => { };//即将从服务器断开连接。此处仅主动断开才有效。
            _tcpClient.Disconnected = (client, e) => { };//从服务器断开连接，当连接不成功时不会触发。
            _tcpClient.Received = (client, byteBlock, requestInfo) =>
            {
                //从服务器收到信息。但是一般byteBlock和requestInfo会根据适配器呈现不同的值。
                string mes = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);
                _tcpClient.Logger.Info($"客户端接收到信息：{mes}");
            };

            //载入配置
            _tcpClient.Setup(new TouchSocketConfig()
                .SetRemoteIPHost("192.168.1.119:9100")
                .ConfigureContainer(a =>
                {
                    // 两种日志方式
                    a.SetSingletonLogger(new LoggerGroup(new FileLogger(), new EasyLogger(PrintClinetLog)));
                }));

            Result result = _tcpClient.TryConnect();//或者可以调用TryConnect
            if (result.IsSuccess())
            {
                _tcpClient.Logger.Info("客户端成功连接");
            }
            else
            {
                _tcpClient.Logger.Info("客户端成功失败");
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