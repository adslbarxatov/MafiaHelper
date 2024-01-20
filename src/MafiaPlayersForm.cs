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
		private List<string> rolesNames = new List<string> ();

		private const uint minPlayers = 5;
		private const uint maxPlayers = 30;

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

			BExit.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Cancel);
			BBegin.Text = RDLocale.GetText ("BBegin");
			BRules.Text = RDLocale.GetText ("BRules");
			RolesTitle.Text = RDLocale.GetText ("RolesTitle");
			PlayersTitle.Text = RDLocale.GetText ("PlayersTitle");

			/*PlayersText.Text = RDGenerics.GetAppSettingsValue ("PlayersList");
			*/
			PlayersLabel.Text = string.Format (RDLocale.GetText ("PlayersText01"), minPlayers);

			// Загрузка ролей
			rolesAliases = RDLocale.GetText ("RolesAliases");
			rolesNames.AddRange (RDLocale.GetText ("RolesNames").Split (linesSplitters,
				StringSplitOptions.RemoveEmptyEntries));

			/*
			PlayersText.ContextMenu = new ContextMenu ();
			for (int i = 0; i < rolesNames.Length; i++)
				{
				PlayersText.ContextMenu.MenuItems.Add (rolesNames[i], AddRole_Click);
				PlayersText.ContextMenu.MenuItems[i].Tag = i;
				}
			*/

			// Загрузка сохранённых игроков
			string[] savedPlayers = RDGenerics.GetAppSettingsValue ("PlayersList").Split (linesSplitters,
				StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < savedPlayers.Length; i++)
				players.Add (new MafiaPlayer (savedPlayers[i].Trim (), MafiaPlayerRoles.Townspeople));
			RefreshPlayersList ();

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
			for (int i = 0; i < rolesNames.Count; i++)
				PlayersLabel.Text += (RDLocale.RN + "• " + rolesAliases[i] +
					(i == 0 ? RDLocale.GetText ("RolesOPrefix") : "") + " – " + rolesNames[i].ToLower ());

			PlayersLabel.Text += RDLocale.RNRN + RDLocale.GetText ("PlayersText02");

			// Запуск
			this.ShowDialog ();
			}

		// Закрытие окна
		private void BExit_Click (object sender, EventArgs e)
			{
			this.Close ();
			}

		private void MafiaPlayersForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			RDGenerics.SaveWindowDimensions (this);
			}

		// Запуск
		private void BBegin_Click (object sender, EventArgs e)
			{
			/* Определение числа игроков
			string[] pl = PlayersText.Text.Split (linesSplitters, StringSplitOptions.RemoveEmptyEntries);
			*/

			// Счётчики
			uint[] counters = new uint[rolesAliases.Length];
			for (int i = 0; i < rolesAliases.Length; i++)
				counters[i] = 0;

			// Разбор
			for (int i = 0; i < players.Count; i++)
				{
				/*
				string[] v = pl[i].Split (rolesSplitters, StringSplitOptions.RemoveEmptyEntries);

				// Такое может случиться при забое строки пробелами
				if (v.Length < 1)
					continue;
				*/

				/* Обычный игрок
				if (v.Length == 1)
					{
					players.Add (new MafiaPlayer (v[0], MafiaPlayerRoles.Townspeople));
					counters[0]++;
					continue;
					}
				*/
				counters[(int)players[i].Role]++;

				/* Роли
				string c = v[1].ToUpper ().Substring (0, 1);
				if (!rolesAliases.Contains (c))
					{
					RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
						string.Format (RDLocale.GetText ("UnknownRoleMessage"), i + 1));
					players.Clear ();
					return;
					}

				int r = rolesAliases.IndexOf (c);
				players.Add (new MafiaPlayer (v[0], (MafiaPlayerRoles)r));
				counters[r]++;*/
				}

			// Контроль числа игроков
			if (players.Count < minPlayers)
				{
				RDGenerics.LocalizedMessageBox (RDMessageTypes.Warning_Center, "NotEnoughPlayersMessage");
				/*players.Clear ();
				*/
				return;
				}

			// (вообще, такого быть не должно, конечно)
			if (players.Count > maxPlayers)
				{
				RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
					string.Format (RDLocale.GetText ("TooMuchPlayersMessage"), maxPlayers));
				/*players.Clear ();
				*/
				return;
				}

			// Контроль баланса мафии и жителей
			uint mafia = counters[(int)MafiaPlayerRoles.Mafia] + counters[(int)MafiaPlayerRoles.MafiaBoss];
			uint yakuza = counters[(int)MafiaPlayerRoles.Yakuza] + counters[(int)MafiaPlayerRoles.YakuzaBoss];
			if ((mafia + yakuza > players.Count / 2) || (mafia < 1))
				{
				RDGenerics.LocalizedMessageBox (RDMessageTypes.Warning_Center, "NotEnoughRolesMessage");
				/*players.Clear ();
				*/
				return;
				}

			// Контроль уникальности остальных ролей
			for (int i = 0; i < rolesAliases.Length; i++)
				{
				/*
				if (i == (int)MafiaPlayerRoles.Yakuza)
					continue;
				*/

				if (!MafiaPlayer.RoleCanBeTeam ((MafiaPlayerRoles)i) && (counters[i] > 1))
					{
					RDGenerics.LocalizedMessageBox (RDMessageTypes.Warning_Center, "TooMuchSecondaryRolesMessage");
					/*players.Clear ();
					*/
					return;
					}
				}

			/* Успешно. Сохранение игроков без ролей
			string s = "";
			for (int i = 0; i < players.Count; i++)
				s += (players[i].Name + rolesSplitters[0].ToString () + RDLocale.RN);
			RDGenerics.SetAppSettingsValue ("PlayersList", s);
			*/

			// Сохраненеи имён игроков
			string savedPlayers = "";
			for (int i = 0; i < players.Count; i++)
				savedPlayers += (players[i].Name + RDLocale.RN);

			RDGenerics.SetAppSettingsValue ("PlayersList", savedPlayers);

			// Сохранение порядка применения ролей
			string s = "";
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

			cancelled = false;
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
			RDGenerics.ShowAbout (false);
			}

		/* Добавление роли
		private void AddRole_Click (object sender, EventArgs e)
			{
			PlayersText.Text = PlayersText.Text.Insert (PlayersText.SelectionStart, rolesSplitters[1].ToString () +
				rolesAliases[(int)((MenuItem)sender).Tag].ToString ());
			}
		*/

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

		/* Расчёт числа введённых игроков
		private void PlayersText_TextChanged (object sender, EventArgs e)
			{
			int count = PlayersText.Text.Length - PlayersText.Text.Replace (RDLocale.RN, "").Length;
			count /= 2;

			if (!string.IsNullOrWhiteSpace (PlayersText.Text) && !PlayersText.Text.EndsWith (RDLocale.RN))
				count++;

			PlayersTitle.Text = RDLocale.GetText ("PlayersTitle") + " " + count.ToString ();
			}
		*/

		// Добавление нового игрока
		private void AddPlayer_Click (object sender, EventArgs e)
			{
			// Запрос параметров
			MafiaRoleForm mrf = new MafiaRoleForm (GetAvailableRoles ());
			if (mrf.Cancelled)
				{
				mrf.Dispose ();
				return;
				}

			// Добавление игрока
			players.Add (new MafiaPlayer (mrf.SelectedName, (MafiaPlayerRoles)rolesNames.IndexOf (mrf.SelectedRole)));
			mrf.Dispose ();

			RefreshPlayersList ();
			PlayersList.SelectedIndex = PlayersList.Items.Count - 1;
			}

		private string[] GetAvailableRoles ()
			{
			// Определение занятых ролей
			List<int> lcRoles = new List<int> ();
			for (int i = 0; i < players.Count; i++)
				{
				if (MafiaPlayer.RoleCanBeTeam (players[i].Role))
					continue;

				int r = (int)players[i].Role;
				if (!lcRoles.Contains (r))
					lcRoles.Add (r);
				}

			// Формирование списка незанятых ролей
			List<string> avRoles = new List<string> ();
			for (int i = 0; i < rolesNames.Count; i++)
				if (!lcRoles.Contains (i))
					avRoles.Add (rolesNames[i]);

			return avRoles.ToArray ();
			}

		// Удаление игрока
		private void RemovePlayer_Click (object sender, EventArgs e)
			{
			// Контроль
			if (PlayersList.SelectedIndex < 0)
				return;

			// Удаление
			int idx = PlayersList.SelectedIndex;
			players.RemoveAt (idx);

			RefreshPlayersList ();
			if (players.Count > 0)
				{
				PlayersList.SelectedIndex = (idx > PlayersList.Items.Count - 1) ?
					PlayersList.Items.Count - 1 : idx;
				}
			}

		// Обновление списка
		private void RefreshPlayersList ()
			{
			PlayersList.Items.Clear ();

			for (int i = 0; i < players.Count; i++)
				PlayersList.Items.Add ((i + 1).ToString ().PadLeft (2) + "  " +
					(players[i].Role == MafiaPlayerRoles.Townspeople ? " " :
					rolesAliases[(int)players[i].Role].ToString ()) + "  " +
					players[i].Name);

			AddPlayer.Enabled = (players.Count < maxPlayers);
			RemovePlayer.Enabled = PlayerUp.Enabled = PlayerDown.Enabled =
				ResetRoles.Enabled = (players.Count > 0);
			}

		// Изменение игрока
		private void PlayersList_DoubleClick (object sender, EventArgs e)
			{
			// Контроль
			if (PlayersList.SelectedIndex < 0)
				return;
			int idx = PlayersList.SelectedIndex;

			// Запрос параметров
			MafiaRoleForm mrf = new MafiaRoleForm (players[idx].Name, rolesNames[(int)players[idx].Role],
				GetAvailableRoles ());
			if (mrf.Cancelled)
				{
				mrf.Dispose ();
				return;
				}

			// Добавление игрока
			players.RemoveAt (idx);
			players.Insert (idx, new MafiaPlayer (mrf.SelectedName,
				(MafiaPlayerRoles)rolesNames.IndexOf (mrf.SelectedRole)));
			mrf.Dispose ();

			RefreshPlayersList ();
			PlayersList.SelectedIndex = idx;
			}

		/// <summary>
		/// Возвращает true, если действие было прервано
		/// </summary>
		public bool Cancelled
			{
			get
				{
				return cancelled;
				}
			}
		private bool cancelled = true;

		// Поднять игрока выше
		private void PlayerUp_Click (object sender, EventArgs e)
			{
			// Контроль
			bool up = ((Button)sender).Name.Contains ("Up");

			if ((PlayersList.SelectedIndex < 0) ||
				up && (PlayersList.SelectedIndex < 1) ||
				!up && (PlayersList.SelectedIndex > players.Count - 2))
				return;

			// Перемещение
			int idx = PlayersList.SelectedIndex;
			MafiaPlayer pl = new MafiaPlayer (players[idx].Name, players[idx].Role);
			players.RemoveAt (idx);

			if (up)
				idx--;
			else
				idx++;
			players.Insert (idx, pl);

			RefreshPlayersList ();
			PlayersList.SelectedIndex = idx;
			}

		// Сброс списка
		private void ResetRoles_Click (object sender, EventArgs e)
			{
			// Выбор варианта
			RDMessageButtons res = RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
				RDLocale.GetText ("PlayersClearMessage"), RDLocale.GetText ("RolesButton"),
				RDLocale.GetText ("AllButton"), RDLocale.GetDefaultText (RDLDefaultTexts.Button_Cancel));
			if (res == RDMessageButtons.ButtonThree)
				return;

			// Полный сброс
			if (res == RDMessageButtons.ButtonTwo)
				{
				players.Clear ();
				RefreshPlayersList ();
				return;
				}

			// Сброс ролей
			int count = players.Count;
			for (int i = 0; i < count; i++)
				{
				players.Add (new MafiaPlayer (players[0].Name, MafiaPlayerRoles.Townspeople));
				players.RemoveAt (0);
				}

			RefreshPlayersList ();
			PlayersList.SelectedIndex = 0;
			}
		}
	}
