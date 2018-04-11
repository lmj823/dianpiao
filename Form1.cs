using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace ebankGateway
{
   
    public class Form1 : System.Windows.Forms.Form
    {
        private IContainer components;
        private Label lbVer;
        private Label label2;
        private Label lbStartTime;
        private ListView listView1;
        private ColumnHeader lsh;
        private ColumnHeader wd;
        private ColumnHeader jym;
        private ColumnHeader jysj;
        private ColumnHeader errcode;
        private ColumnHeader eremsg;
        private System.Windows.Forms.Timer timer1;
        public static ActiveQueue<ShowData> FinalQueue = new ActiveQueue<ShowData>();
        private static ulong sxh = 1;
        private ColumnHeader jysj1;
        private static object _syncRoot;
        private static object SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    Interlocked.CompareExchange(ref  _syncRoot, new object(), null);
                }
                return _syncRoot;
            }
        }
        private static DateTime sdt;
        private ColumnHeader wdm;
        private static bool run1;
        
       
        public Form1()
        {

            InitializeComponent();
        }

     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbVer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbStartTime = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.lsh = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.wdm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.wd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.jym = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.jysj = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.jysj1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errcode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eremsg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbVer
            // 
            this.lbVer.AutoSize = true;
            this.lbVer.Location = new System.Drawing.Point(820, 579);
            this.lbVer.Name = "lbVer";
            this.lbVer.Size = new System.Drawing.Size(0, 18);
            this.lbVer.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(113, 580);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "开始时间：";
            // 
            // lbStartTime
            // 
            this.lbStartTime.AutoSize = true;
            this.lbStartTime.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStartTime.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbStartTime.Location = new System.Drawing.Point(212, 580);
            this.lbStartTime.Name = "lbStartTime";
            this.lbStartTime.Size = new System.Drawing.Size(0, 16);
            this.lbStartTime.TabIndex = 3;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsh,
            this.wdm,
            this.wd,
            this.jym,
            this.jysj,
            this.jysj1,
            this.errcode,
            this.eremsg});
            this.listView1.Location = new System.Drawing.Point(2, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1338, 574);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // lsh
            // 
            this.lsh.Text = "序号";
            this.lsh.Width = 52;
            // 
            // wdm
            // 
            this.wdm.Text = "银行";
            this.wdm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.wdm.Width = 80;
            // 
            // wd
            // 
            this.wd.Text = "网点号";
            this.wd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // jym
            // 
            this.jym.Text = "交易码";
            this.jym.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.jym.Width = 70;
            // 
            // jysj
            // 
            this.jysj.Text = "申请时间";
            this.jysj.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.jysj.Width = 70;
            // 
            // jysj1
            // 
            this.jysj1.Text = "返回时间";
            this.jysj1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.jysj1.Width = 70;
            // 
            // errcode
            // 
            this.errcode.Text = "返回码";
            // 
            // eremsg
            // 
            this.eremsg.Text = "返回信息";
            this.eremsg.Width = 328;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(10, 21);
            this.ClientSize = new System.Drawing.Size(1345, 612);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lbStartTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbVer);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1367, 668);
            this.MinimumSize = new System.Drawing.Size(1367, 668);
            this.Name = "Form1";
            this.Text = "电票前置网关";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }
        
        private void Acceptclient()
        {
            try
            {
                IPAddress ip = IPAddress.Any;
                int myport = int.Parse(ConfigApp.MyPort);
                TcpListener tl = new TcpListener(ip,myport);
                TcpClient tc;

                while (true)
                {
                    tl.Start();
                    tc = tl.AcceptTcpClient();
                    string ip1= tc.Client.RemoteEndPoint.ToString();
                    if (Common.isAllowIP(ip1))
                    {
                        Listen listen = new Listen(tc);
                        Thread thread = new Thread(new ThreadStart(listen.startAccept));
                        thread.Start();
                    }
                    else
                    {
                        LogWrite.WriteLog(ip1, "非法客户端：");
                        tc.Close();                        
                    }
                }

            }
            catch (Exception ex)
            {
                LogWrite.WriteLog(ex.ToString(), "Socket错误");
                DialogResult dr = MessageBox.Show("创建socket连接失败。", "退出程序", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dr == DialogResult.OK)
                {
                    Application.ExitThread();
                    Application.Exit();
                }
            }
        }

        
              

        private void startThreading()
        {

            Thread thread = new Thread(new ThreadStart(Acceptclient));
            thread.Start();

        }
        

        private void Form1_Load(object sender, System.EventArgs e)
        {
            try
            {
                mydataset myds = new mydataset();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"IpFormat失败");
                this.Close();
            }            
            ////////////////////////debug//////////////////////
            //string hex = "504f5354202f626273702f73657276696365732f62696c6c45646974536572766963652f62696c6c456469745365727669636520485454502f312e310d0a436f6e74656e742d547970653a20746578742f786d6c0d0a486f73743a203136332e312e392e39333a383238320d0a436f6e74656e742d4c656e6774683a20313639320d0a4578706563743a203130302d636f6e74696e75650d0a436f6e6e656374696f6e3a204b6565702d416c6976650d0a0d0a";
            //byte[] bytes = new byte[hex.Length / 2];
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    try
            //    {
            //        // 每两个字符是一个 byte。 
            //        bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
            //        System.Globalization.NumberStyles.HexNumber);
            //    }
            //    catch
            //    {

            //    }
            //}
            //System.Text.Encoding chs = System.Text.Encoding.GetEncoding("utf-8");
            //string bb = chs.GetString(bytes);

         


            ///////////////////////////////////////////////////
          

            
            startThreading();
           

            sdt = DateTime.Now;
            run1 = true;
            

            timer1.Interval = int.Parse(ConfigApp.TimerTick) * 60 * 1000;
            timer1.Enabled = true;


            lbVer.Text ="版本: "+ Common.AssemblyFileVersion();
            lbStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Thread thShow = new Thread(new ThreadStart(doDispos));
            thShow.Start();

            LogWrite.WriteLog("", "程序开始运行...");
        }



        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.ExitThread();
            Environment.Exit(Environment.ExitCode);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //string dt1 = DateTime.Now.ToString("yyyyMMdd");
            //if (sdt.AddDays(1).ToString("yyyyMMdd") == dt1)
            //{
            //    run1 = true;
            //    sdt = sdt.AddDays(1);
            //}
            //try
            //{
            //    if (run1 && DateTime.Now.ToString("HH") == ConfigApp.LhhUpdateTtime)
            //    {
            //        ODSD.Done();
            //        Common.DeleteFile(ConfigApp.HxFilePath, 168);
                    
            //        Common.MoveFile(ConfigApp.FtpPath, 168);

            //        run1 = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogWrite.WriteLog(ex.Message, "定时任务执行出错，原因：");
            //}
        }

        private void doDispos()
        {
            if (ConfigApp.Show)
            {
                while (true)
                {
                    if (FinalQueue.Count > 0)
                    {
                        add();
                    }
                    Thread.Sleep(100);
                }
            }
        }
        private void show()
        {
            ShowData sdata = FinalQueue.Dequeue();

            try   
            {
                if (listView1.Items.Count > 26)
                {
                    listView1.Items.Clear();
                }
                string xxh;
                lock (SyncRoot)
                {
                    if (sxh > 99999999)
                    {
                        sxh = 1;
                    }
                    xxh = Convert.ToString(sxh);
                    sxh++;
                }
                ListViewItem it = new ListViewItem(new string[] { xxh, sdata.Wdm, sdata.Wd, sdata.Jym, sdata.Jysj, sdata.Jysj1, sdata.Errcode, sdata.Errmsg });
                listView1.Items.Add(it);
            }
            catch { }
        }
        private delegate void DelegateShowResult();
        private void add()
        {
            try
            {
                if (this.listView1.InvokeRequired)
                {
                    DelegateShowResult dsr = new DelegateShowResult(show);
                    this.Invoke(dsr, new object[] { });
                }
                else
                {
                    show();
                }
            }
            catch (Exception ee)
            {
                LogWrite.WriteLog(ee.Message, "界面显示出错");
            }
        }
        
    }
}
