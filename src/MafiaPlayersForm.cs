using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает форму ввода списка игроков
	/// </summary>
	public partial class MafiaPlayersForm: Form
		{
		// Переменные и константы
		private char[] linesSplitters = new char[] { '\r', '\n' };
		private char[] rolesSplitters = new char[] { '\t', ' ' };
		private string rolesAliases;
		private string[] rolesNames;

		private const uint minPlayers = 5;
		private const uint maxPlayers = 20;

		/// <summary>
		/// Конструктор. Запускает главную форму
		/// </summary>
		public MafiaPlayersForm ()
			{
			// Инициализация
			InitializeComponent ();
			this.Text = ProgramDescription.AssemblyTitle;
			RDGenerics.LoadWindowDimensions (this);

			this.CancelButton = BExit;
			this.AcceptButton = BBegin;

			BExit.Text = Localization.GetDefaultText (LzDefaultTextValues.Button_Cancel);
			BBegin.Text = Localization.GetText ("BBegin");
			BRules.Text = Localization.GetText ("BRules");
			RolesTitle.Text = Localization.GetText ("RolesTitle");
			PlayersTitle.Text = Localization.GetText ("PlayersTitle");

			PlayersText.Text = RDGenerics.GetAppSettingsValue ("PlayersList");
			PlayersLabel.Text = string.Format (Localization.GetText ("PlayersText01"), minPlayers);

			// Загрузка ролей
			rolesAliases = Localization.GetText ("RolesAliases");
			rolesNames = Localization.GetText ("RolesNames").Split (linesSplitters,
				StringSplitOptions.RemoveEmptyEntries);

			PlayersText.ContextMenu = new ContextMenu ();
			for (int i = 0; i < rolesNames.Length; i++)
				{
				PlayersText.ContextMenu.MenuItems.Add (rolesNames[i], AddRole_Click);
				PlayersText.ContextMenu.MenuItems[i].Tag = i;
				}

			// Стандартный или сохранённый ранее порядок
			string rolesOrder = RDGenerics.GetAppSettingsValue ("RolesOrder");
			List<int> order = new List<int> ();

			string[] v = rolesOrder.Split (rolesSplitters, StringSplitOptions.RemoveEmptyEntries);
			int count = MafiaRolesOrder.DefaultNightRolesOrder.Length;

			for (int i = 0; i < count; i++)
				{
				try
					{
					order.Add (int.Parse (v[i]));
					}
				catch
					{
					order = new List<int> (MafiaRolesOrder.DefaultNightRolesOrder);
					break;
					}
				}

			for (int i = 0; i < order.Count; i++)
				{
				MafiaPlayerRoles role = (MafiaPlayerRoles)order[i];
				string roleLine = rolesAliases[order[i]] + " – " + rolesNames[order[i]].ToLower ();

				if (MafiaPlayer.RoleShouldBeAppliedFirst (role))
					RolesFirstOrder.Items.Add (roleLine);
				else
					RolesSecondOrder.Items.Add (roleLine);
				}

			// Формирование комментария
			for (int i = 0; i < rolesNames.Length; i++)
				PlayersLabel.Text += (Localization.RN + "• " + rolesAliases[i] +
					(i == 0 ? Localization.GetText ("RolesOPrefix") : "") + " – " + rolesNames[i].ToLower ());

			PlayersLabel.Text += Localization.RNRN + Localization.GetText ("PlayersText02");

			// Запуск
			this.ShowDialog ();
			}

		// Закрытие окна
		private void BExit_Click (object sender, EventArgs e)
			{
			RDGenerics.SetAppSettingsValue ("PlayersList", PlayersText.Text);
			this.Close ();
			}

		private void MafiaPlayersForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			RDGenerics.SaveWindowDimensions (this);
			}

		// Запуск
		private void BBegin_Click (object sender, EventArgs e)
			{
			// Определение числа игроков
			string[] pl = PlayersText.Text.Split (linesSplitters, StringSplitOptions.RemoveEmptyEntries);

			// Счётчики
			uint[] counters = new uint[rolesAliases.Length];
			for (int i = 0; i < rolesAliases.Length; i++)
				counters[i] = 0;

			// Разбор
			for (int i = 0; i < pl.Length; i++)
				{
				string[] v = pl[i].Split (rolesSplitters, StringSplitOptions.RemoveEmptyEntries);

				// Такое может случиться при забое строки пробелами
				if (v.Length < 1)
					continue;

				// Обычный игрок
				if (v.Length == 1)
					{
					players.Add (new MafiaPlayer (v[0], MafiaPlayerRoles.Townspeople));
					counters[0]++;
					continue;
					}

				// Роли
				string c = v[1].ToUpper ().Substring (0, 1);
				if (!rolesAliases.Contains (c))
					{
					RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
						string.Format (Localization.GetText ("UnknownRoleMessage"), i + 1));
					players.Clear ();
					return;
					}

				int r = rolesAliases.IndexOf (c);
				players.Add (new MafiaPlayer (v[0], (MafiaPlayerRoles)r));
				counters[r]++;
				}

			// Контроль числа игроков
			if (players.Count < minPlayers)
				{
				RDGenerics.LocalizedMessageBox (RDMessageTypes.Warning_Center, "NotEnoughPlayersMessage");
				players.Clear ();
				return;
				}

			if (players.Count > maxPlayers)
				{
				RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
					string.Format (Localization.GetText ("TooMuchPlayersMessage"), maxPlayers));
				players.Clear ();
				return;
				}

			// Контроль баланса мафии и жителей
			uint mafia = counters[(int)MafiaPlayerRoles.Mafia] + counters[(int)MafiaPlayerRoles.MafiaBoss];
			if ((mafia > players.Count / 2) || (mafia < 1))
				{
				RDGenerics.LocalizedMessageBox (RDMessageTypes.Warning_Center, "NotEnoughRolesMessage");
				players.Clear ();
				return;
				}

			// Контроль уникальности остальных ролей
			for (int i = (int)MafiaPlayerRoles.Doctor; i < rolesAliases.Length; i++)
				if (counters[i] > 1)
					{
					RDGenerics.LocalizedMessageBox (RDMessageTypes.Warning_Center, "TooMuchSecondaryRolesMessage");
					players.Clear ();
					return;
					}

			// Успешно. Сохранение игроков без ролей
			string s = "";
			for (int i = 0; i < players.Count; i++)
				s += (players[i].Name + rolesSplitters[0].ToString () + Localization.RN);
			RDGenerics.SetAppSettingsValue ("PlayersList", s);

			// Сохранение порядка применения ролей
			s = "";
			List<int> order = new List<int> ();
			for (int i = 0; i < RolesFirstOrder.Items.Count; i++)
				{
				int n = rolesAliases.IndexOf (RolesFirstOrder.Items[i].ToString ().Substring (0, 1));
				order.Add (n);
				s += (n.ToString () + rolesSplitters[0].ToString ());
				}
			for (int i = 0; i < RolesSecondOrder.Items.Count; i++)
				{
				int n = rolesAliases.IndexOf (RolesSecondOrder.Items[i].ToString ().Substring (0, 1));
				order.Add (n);
				s += (n.ToString () + rolesSplitters[0].ToString ());
				}

			RDGenerics.SetAppSettingsValue ("RolesOrder", s);
			playersRolesOrder = new MafiaRolesOrder (order.ToArray ());

			this.Close ();
			}

		/// <summary>
		/// Возвращает список введённых игроков
		/// </summary>
		public List<MafiaPlayer> Players
			{
			get
				{
				return players;
				}
			}
		private List<MafiaPlayer> players = new List<MafiaPlayer> ();

		/// <summary>
		/// Возвращает обработчик порядка применения ролей
		/// </summary>
		public MafiaRolesOrder PlayersRolesOrder
			{
			get
				{
				return playersRolesOrder;
				}
			}
		private MafiaRolesOrder playersRolesOrder;

		// Открытие ссылки на правила игры
		private void BRules_Click (object sender, EventArgs e)
			{
			/*RDGenerics.RunURL (RDGenerics.AssemblyGitPageLink);*/
			RDGenerics.ShowAbout (false);
			}

		// Добавление роли
		private void AddRole_Click (object sender, EventArgs e)
			{
			PlayersText.Text = PlayersText.Text.Insert (PlayersText.SelectionStart, rolesSplitters[1].ToString () +
				rolesAliases[(int)((MenuItem)sender).Tag].ToString ());
			}

		// Сортировка ролей
		private void RolesOrderUp_Click (object sender, EventArgs e)
			{
			int i = RolesSecondOrder.SelectedIndex;
			bool up = ((Button)sender).Name.Contains ("Up");

			if (up && (i <= 0) ||
				!up && ((i < 0) || (i + 1 >= RolesSecondOrder.Items.Count)))
				return;

			string s = RolesSecondOrder.SelectedItem.ToString ();
			RolesSecondOrder.Items.RemoveAt (i);
			RolesSecondOrder.Items.Insert (up ? (i - 1) : (i + 1), s);
			RolesSecondOrder.SelectedIndex = up ? (i - 1) : (i + 1);
			}

		// Расчёт числа введённых игроков
		private void PlayersText_TextChanged (object sender, EventArgs e)
			{
			int count = PlayersText.Text.Length - PlayersText.Text.Replace (Localization.RN, "").Length;
			count /= 2;

			if (!string.IsNullOrWhiteSpace (PlayersText.Text) && !PlayersText.Text.EndsWith (Localization.RN))
				count++;

			PlayersTitle.Text = Localization.GetText ("PlayersTitle") + " " + count.ToString ();
			}
		}
	}
