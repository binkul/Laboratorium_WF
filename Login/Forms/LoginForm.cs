using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.Login.Repository;
using Laboratorium.Security;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Laboratorium.Login.Forms
{
    public partial class LoginForm : Form
    {
        //private readonly string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private readonly string _loginPath = @"\Data\login.txt";
        private List<string> _logins;
        private readonly SqlConnection _connection;
        private readonly LoginRepository _loginRepository;
        public UserDto User { get; private set; }
        public bool LoginOk { get; private set; } = false;

        public LoginForm(SqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
            //_connection = new SqlConnection(connectionString);
            _loginRepository = new LoginRepository(_connection);
        }

        private void PanelBlack_Paint(object sender, PaintEventArgs e)
        {
            int radius = 60;

            Rectangle corner = new Rectangle(0, 0, radius, radius);
            GraphicsPath path = new GraphicsPath();
            path.AddLine(0, 0, 0, 0); // path.AddArc(corner, 180, 90);
            corner.X = PanelBlack.Width - 2 - radius;
            path.AddArc(corner, 270, 90);
            corner.Y = PanelBlack.Height - 5 - radius;
            path.AddLine(PanelBlack.Width - 2, PanelBlack.Height - 5, radius, PanelBlack.Height - 5); // path.AddArc(corner, 0, 90);
            corner.X = 0;
            path.AddArc(corner, 90, 90);
            path.CloseFigure();

            Color color = Color.FromArgb(255, 46, 49, 55);

            e.Graphics.FillPath(new SolidBrush(color), path);
            e.Graphics.DrawPath(new Pen(color), path);

            Pen linePen = new Pen(Color.White)
            {
                Width = 1
            };
            int y_pos = LblLoging.Top + LblLoging.Height + 5;
            int x_pos = TxtPassword.Left;
            e.Graphics.DrawLine(linePen, new Point(x_pos, y_pos), new Point(x_pos + TxtPassword.Width, y_pos));

            y_pos = BtnSubmit.Top + BtnSubmit.Height + 20;
            e.Graphics.DrawLine(linePen, new Point(x_pos, y_pos), new Point(x_pos + TxtPassword.Width, y_pos));
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

            if (CheckProgram())
            {
                _logins = GetLogins();
                CmbLogin.DataSource = _logins;
                BtnSubmit.FlatStyle = FlatStyle.Flat;
                BtnSubmit.FlatAppearance.BorderSize = 0;
                BtnSubmit.FlatAppearance.BorderColor = Color.FromArgb(255, 84, 93, 106);

                BtnRegister.FlatStyle = FlatStyle.Flat;
                BtnRegister.FlatAppearance.BorderSize = 0;
                BtnRegister.FlatAppearance.BorderColor = Color.FromArgb(255, 68, 106, 211);
            }
            else
            {
                Application.Exit();
            }
        }

        private bool CheckProgram()
        {
            string password = Encrypt.MD5Encrypt("Jacek Binkul"); //3ef179d05525f4d84835b2703639e6af
            ProgramData programData = _loginRepository.GetProgramData(password);

            if (programData == null)
            {
                MessageBox.Show("Działanie aplikacji wstrzymane. Pierwsze uruchomienie od instalacji. Należy uzupełnić dane rozruchowe!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (programData.ColumnFour != null && programData.ColumnFour.Equals("Expire"))
            {
                MessageBox.Show("Działanie aplikacji wstrzymane. Wykryto wcześniejsze uruchamianie po utracie daty ważności. Należy uzupełnić dane rozruchowe.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            DateTime dateNow = DateTime.Today;
            int dateCompare = DateTime.Compare(dateNow, programData.Date);
            if (dateCompare < 0)
            {
                return true;
            }
            else if (dateCompare == 0)
            {
                MessageBox.Show("Dziś jest ostatni dzień działania aplikacji. Przedłuż ważność programu.", "Błąd wczytania", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Upłynęła data ważności oprogramowania. Przedłuż ważność programu.", "Błąd wczytania", MessageBoxButtons.OK, MessageBoxIcon.Error);
                programData.ColumnFour = "Expired";
                _loginRepository.UpdateToExpire();
                return false;
            }
        }

        private List<string> GetLogins()
        {
            List<string> logins = new List<string>();
            if (File.Exists(Environment.CurrentDirectory + _loginPath))
            {
                StreamReader file = new StreamReader(Environment.CurrentDirectory + _loginPath);
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    logins.Add(line);
                }
                file.Close();
            }
            return logins;
        }

        private void SaveLogins()
        {
            string login = CmbLogin.Text;
            string file = Environment.CurrentDirectory + _loginPath;

            _logins.Sort();
            _logins.Remove(login);
            if (!_logins.Contains(login))
            {
                _logins.Insert(0, login);
            }

            if (!Directory.Exists(Path.GetDirectoryName(file)))
                Directory.CreateDirectory(Path.GetDirectoryName(file));

            File.WriteAllLines(Environment.CurrentDirectory + _loginPath, _logins);
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CmbLogin.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                MessageBox.Show("Pole 'Login' i 'Hasło' nie mogą byc puste.", "Błąd logowania", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string password = Encrypt.MD5Encrypt(TxtPassword.Text);
            string login = CmbLogin.Text;
            UserDto user = _loginRepository.GetUserByLoginAndPassword(login, password);

            if (user == null)
            {
                _ = MessageBox.Show("Nieprawidłowy login lub hasło. Spróbuj ponownie",
                    "Błąd logowania", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (user.Active)
            {
                User = new UserDto(user.Id, user.Name, user.Surname, user.Email, user.Login, "", user.Permission, user.Identifier, user.Active, user.Date);
                LoginOk = true;
            }
            else
            {
                _ = MessageBox.Show("Użytkownik: '" + CmbLogin.Text + "' jest jeszcze nieaktywny. Skontaktuj się z administratorem.",
                    "Brak uprawnień", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            SaveLogins();
            Close();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            //using (RegisterForm register = new RegisterForm(this, _loginRepository))
            //{
            //    this.Hide();
            //    register.ShowDialog();
            //}
        }

    }
}
