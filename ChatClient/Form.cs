//ChatClient
using System;
using System.Windows.Forms;

namespace ChatClient
{
    public class ChatClient : Form
    {
        private TextBox txtChat;
        private TextBox txtMessage;
        private Button btnSend;
        private Button btnExit;

        public ChatClient()
        {
            // Инициализация компонентов
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Создаем элементы управления
            this.txtChat = new TextBox();
            this.txtMessage = new TextBox();
            this.btnSend = new Button();
            this.btnExit = new Button();

            // Настройка txtChat
            this.txtChat.Multiline = true;
            this.txtChat.ReadOnly = true;
            this.txtChat.ScrollBars = ScrollBars.Vertical;
            this.txtChat.Dock = DockStyle.Top;
            this.txtChat.Height = 300;

            // Настройка txtMessage
            this.txtMessage.Dock = DockStyle.Bottom;
            this.txtMessage.Height = 30;

            // Настройка btnSend
            this.btnSend.Text = "Отправить";
            this.btnSend.Dock = DockStyle.Bottom;
            this.btnSend.Click += new EventHandler(this.BtnSend_Click);

            // Настройка btnExit
            this.btnExit.Text = "Выйти";
            this.btnExit.Dock = DockStyle.Bottom;
            this.btnExit.Click += new EventHandler(this.BtnExit_Click);

            // Добавление элементов управления на форму
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnExit);

            // Настройки формы
            this.Text = "Чат";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(400, 400);
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                // Добавляем сообщение в чат
                txtChat.AppendText("Вы: " + txtMessage.Text + Environment.NewLine);
                txtMessage.Clear(); // Очищаем поле ввода
                txtMessage.Focus(); // Устанавливаем фокус обратно на поле ввода
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Закрываем приложение
        }

        /*[STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChatClient());
        }*/
    }
}