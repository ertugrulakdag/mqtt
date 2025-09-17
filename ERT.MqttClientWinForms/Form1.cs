using ERT.Shared.Configuration;
using ERT.Shared.Services;

namespace ERT.MqttClientWinForms
{
    public partial class Form1 : Form
    {
        private readonly MqttService _mqtt = new();
        private MqttClientSettings? _cfg;

        public Form1()
        {
            InitializeComponent();
            txtTopic.Text = "topic-name";
            txtMessage.Text = "merhaba mqtt";
        }

        private void AppendLog(string text)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(AppendLog), text);
                return;
            }

            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {text}{Environment.NewLine}");
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _cfg = MqttConfig.Read();
                txtClientId.Text = _cfg.ClientId;
                txtHost.Text = _cfg.Host;
                txtUser.Text = _cfg.Username;
                txtPass.Text = _cfg.Password;
                txtPort.Text = _cfg.Port.ToString() ?? string.Empty;

                AppendLog($"ClientId:{_cfg.ClientId} Host:{_cfg.Host} Port:{_cfg.Port} hazir.");

            }
            catch (Exception ex)
            {
                AppendLog("Konfigürasyon okunamadı: " + ex.Message);
            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (_cfg == null)
            {
                AppendLog("Konfigürasyon yok.");
                return;
            }

            try
            {
                var host = txtHost.Text.Trim();
                var clientId = txtClientId.Text.Trim();
                var topic = txtTopic.Text.Trim();
                var user = txtUser.Text.Trim();
                var pass = txtPass.Text;

                var result = await _mqtt.Start(
                    brokerIp: host,
                    clientId: clientId,
                    topic: topic,
                    username: user,
                    password: pass,
                    callback: payload =>
                    {
                        AppendLog($"GELEN: topic={topic}, payload={payload}");
                    });
                if (result.Success)
                {
                    AppendLog(result.Message);
                    btnConnect.BackColor = Color.Lime;
                    btnConnect.Text = "Bağlandı";
                    btnConnect.Enabled = false;
                }
                else
                {
                    AppendLog(result.Message);
                    btnConnect.BackColor = Color.Red;
                    btnConnect.Text = "Bağlan";
                }
            }
            catch (Exception ex)
            {
                btnConnect.Enabled = true;
                btnConnect.BackColor = Color.Red;
                AppendLog($"MQTT bağlanmadı ve abone olunmadı! Ex:{ex}");
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var topic = txtTopic.Text.Trim();
                var msg = txtMessage.Text;

                await _mqtt.SendCode(topic, msg);
                AppendLog($"GÖNDERİLEN: topic={topic}, payload={msg}");
            }
            catch (Exception ex)
            {
                AppendLog("Gönderim hatası: " + ex.Message);
            }
        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                await _mqtt.StopAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
