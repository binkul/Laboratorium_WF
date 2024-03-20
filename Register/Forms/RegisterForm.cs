using Laboratorium.ADO.DTO;
using Laboratorium.Login.Forms;
using Laboratorium.Login.Repository;
using Laboratorium.Security;
using Laboratorium.User.Repository;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Laboratorium.Register.Forms
{
    public partial class RegisterForm : Form
    {
        private readonly LoginForm _loginForm;
        private readonly UserRepository _userRepository;

        public RegisterForm(LoginForm loginForm, UserRepository userRepository)
        {
            _loginForm = loginForm;
            _userRepository = userRepository;
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            BtnRegister.FlatStyle = FlatStyle.Flat;
            BtnRegister.FlatAppearance.BorderSize = 0;
            BtnRegister.FlatAppearance.BorderColor = Color.FromArgb(255, 84, 93, 106);
        }

        private void PanelBlack_Paint(object sender, PaintEventArgs e)
        {
            int radius = 60;

            Rectangle corner = new Rectangle(0, 0, radius, radius);
            GraphicsPath path = new GraphicsPath();
            path.AddLine(0, 0, 0, 0);
            corner.X = PanelBlack.Width - 2 - radius;
            path.AddArc(corner, 270, 90);
            corner.Y = PanelBlack.Height - 5 - radius;
            path.AddLine(PanelBlack.Width - 2, PanelBlack.Height - 5, radius, PanelBlack.Height - 5);
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
            int x_pos = TxtName.Left;
            e.Graphics.DrawLine(linePen, new Point(x_pos, y_pos), new Point(x_pos + TxtName.Width, y_pos));
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (CheckEmtpyFields() && CheckPasswordAndRepeat() && ExistUser())
            {
                string password = Encrypt.MD5Encrypt(TxtPassword.Text);

                UserDto user = new UserDto
                {
                    Name = TxtName.Text,
                    Surname = TxtSurname.Text,
                    Email = TxtEmail.Text,
                    Login = TxtLogin.Text,
                    Password = password,
                    Identifier = TxtName.Text.ToUpper().Substring(0, 1) + TxtSurname.Text.ToUpper().Substring(0, 1),
                    Permission = "user",
                    Active = false,
                    DateCreated = DateTime.Today
                };


                _userRepository.Save(user);
                Close();
            }
        }

        private bool CheckEmtpyFields()
        {
            bool result = true;

            if (string.IsNullOrEmpty(TxtName.Text))
            {
                MessageBox.Show("Pole 'Imie' nie może byc puste. Podaj imie.", "Puste pole", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            else if (string.IsNullOrEmpty(TxtSurname.Text))
            {
                MessageBox.Show("Pole 'Nazwisko' nie może byc puste. Podaj nazwisko.", "Puste pole", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            else if (string.IsNullOrEmpty(TxtEmail.Text))
            {
                MessageBox.Show("Pole 'E-mail' nie może byc puste. Podaj email.", "Puste pole", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            else if (string.IsNullOrEmpty(TxtLogin.Text))
            {
                MessageBox.Show("Pole 'Login' nie może byc puste. Podaj login.", "Puste pole", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            else if (string.IsNullOrEmpty(TxtPassword.Text))
            {
                MessageBox.Show("Pole 'Hasło' nie może byc puste. Podaj hasło.", "Puste pole", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            else if (string.IsNullOrEmpty(TxtRepeatPassword.Text))
            {
                MessageBox.Show("Pole 'Powtórz hasło' nie może byc puste. Powtórz hasło.", "Puste pole", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }

            return result;
        }

        private bool CheckPasswordAndRepeat()
        {
            if (TxtPassword.Text != TxtRepeatPassword.Text)
            {
                MessageBox.Show("Pole 'Hasło' i pole 'Powtórz hasło' muszą być identyczne. Podaj jeszcze raz hasło i powtórz je.", "Błąd hasła", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        private bool ExistUser()
        {
            string login = TxtLogin.Text;
            bool log = _userRepository.ExistByName(login);

            if (log)
            {
                MessageBox.Show("Użytkownik o loginie '" + login + "' istnieje już bazie. Zmień login.", "Zły login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _loginForm.Show();
        }

    }
}
