using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает форму добавления / изменения игроков
	/// </summary>
	public partial class MafiaRoleForm: Form
		{
		/// <summary>
		/// Конструктор. Запускает форму изменения параметров игрока
		/// </summary>
		/// <param name="AvailableRoles">Названия доступных ролей</param>
		/// <param name="ExPlayerRole">Название роли существующего игрока</param>
		/// <param name="ExPlayerName">Имя существующего игрока</param>
		public MafiaRoleForm (string ExPlayerName, string ExPlayerRole, string[] AvailableRoles)
			{
			MafiaRoleForm_Init (ExPlayerName, ExPlayerRole, AvailableRoles);
			}

		/// <summary>
		/// Конструктор. Запускает форму ввода параметров игрока
		/// </summary>
		/// <param name="AvailableRoles">Названия доступных ролей</param>
		public MafiaRoleForm (string[] AvailableRoles)
			{
			MafiaRoleForm_Init (null, null, AvailableRoles);
			}

		private void MafiaRoleForm_Init (string ExPlayerName, string ExPlayerRole, string[] AvailableRoles)
			{
			// Инициализация
			InitializeComponent ();
			this.Text = ProgramDescription.AssemblyTitle;
			PlayerLabel.Text = RDLocale.GetText ("Header1");
			RoleLabel.Text = RDLocale.GetText ("Header2");

			bool editing = !string.IsNullOrWhiteSpace (ExPlayerRole);
			this.CancelButton = BExit;

			// Настройка контролов
			BExit.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Cancel);

			List<string> roles = new List<string> (AvailableRoles);
			int highlightIndex = -1;
			if (editing)
				{
				if (!roles.Contains (ExPlayerRole))
					roles.Add (ExPlayerRole);

				highlightIndex = roles.IndexOf (ExPlayerRole);
				PlayerName.Text = ExPlayerName;
				}

			// Сборка кнопочной части
			Button00.Text = roles[0];
			if (highlightIndex == 0)
				Button00.BackColor = Color.Yellow;

			for (int i = 1; i < roles.Count; i++)
				{
				Button b = new Button ();
				b.Font = Button00.Font;

				b.Width = Button00.Width;
				b.Height = Button00.Height;
				b.Left = Button00.Left;
				b.Top = Button00.Top + 24 * i;
				b.FlatStyle = FlatStyle.Popup;

				b.Text = roles[i];
				b.Click += BApply_Click;

				if (highlightIndex == i)
					b.BackColor = Color.Yellow;

				this.Controls.Add (b);
				}

			// Обновление размера окна
			this.MinimumSize = new Size (this.MinimumSize.Width, this.MinimumSize.Height + 24 * (roles.Count - 1));
			BExit.Top += 24 * (roles.Count - 1);

			// Загрузка настроек
			RDGenerics.LoadWindowDimensions (this);

			// Запуск
			this.ShowDialog ();
			}

		// Закрытие окна
		private void BExit_Click (object sender, EventArgs e)
			{
			this.Close ();
			}

		private void MafiaRoleForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			// Сохранение настроек
			RDGenerics.SaveWindowDimensions (this);
			}

		/// <summary>
		/// Возвращает выбранную роль игрока
		/// </summary>
		public string SelectedRole
			{
			get
				{
				return selectedRole;
				}
			}
		private string selectedRole;

		/// <summary>
		/// Возвращает выбранное имя игрока
		/// </summary>
		public string SelectedName
			{
			get
				{
				return selectedName;
				}
			}
		private string selectedName;

		/// <summary>
		/// Возвращает true, если выбор был отменён
		/// </summary>
		public bool Cancelled
			{
			get
				{
				return cancelled;
				}
			}
		private bool cancelled = true;

		// Выбор параметров
		private void BApply_Click (object sender, EventArgs e)
			{
			if (string.IsNullOrWhiteSpace (PlayerName.Text))
				{
				RDGenerics.LocalizedMessageBox (RDMessageTypes.Warning_Center, "PlayerNameEmptyMessage", 1000);
				return;
				}

			selectedName = PlayerName.Text;
			selectedRole = ((Button)sender).Text;

			cancelled = false;
			this.Close ();
			}
		}
	}
