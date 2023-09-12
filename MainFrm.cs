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
                    client.Logger.Info($"已从{client.Id}接收到信息：{mes}");

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
                        .SetListenIPHosts("tcp://127.0.0.1:7000", 7001)//同时监听两个地址
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
            }
            else
            {
                _tcpClient.Logger.Info("客户端成功失败");
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
    }
}